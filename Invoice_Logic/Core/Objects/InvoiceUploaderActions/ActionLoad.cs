using Invoice_Logic.API;
using Invoice_Logic.Data.DTOs.Create;
using Invoice_Logic.Data.DTOs.Entity;
using Invoice_Logic.Enums;
using Invoice_Logic.Excel;
using Invoice_Logic.Repositories;
using Invoice_Logic.Repositories.CacheEntities;
using Invoice_Logic.Servers;

namespace Invoice_Logic.Core.Objects.InvoiceUploaderActions;

public class ActionLoad
{
    private IExcel _excel;
    private IWebServer _webServer;
    private IUserLogging _userLogging;
    private IInvoiceHeaderCacheEntity _invoiceHeaderCacheEntity;
    private IInvoiceDetailCacheEntity _invoiceDetailCacheEntity;
    private IRepository _repository;

    public ActionLoad(IExcel excel, IWebServer webServer, IUserLogging userLogging, IInvoiceHeaderCacheEntity invoiceHeaderCacheEntity, IInvoiceDetailCacheEntity invoiceDetailCacheEntity, IRepository repository)
    {
        _excel = excel;
        _webServer = webServer;
        _userLogging = userLogging;
        _invoiceHeaderCacheEntity = invoiceHeaderCacheEntity;
        _invoiceDetailCacheEntity = invoiceDetailCacheEntity;
        _repository = repository;
    }

    public async Task<List<InvoiceHeaderEntity>> Perform(Stream stream)
    {
        await LoadFile(stream);
        var result = await CreateInvoices();
        // Save created invoice to Search
        // Run stored proc
        return result;
    }

    private async Task LoadFile(Stream stream)
    {
        var path = await _webServer.AllocateFile();
        path += ".xlsx";
        using var localStream = File.Create(path);
        await stream.CopyToAsync(localStream);
        localStream.Close();
        _excel.OpenFile(path);
    }

    private async Task<List<InvoiceHeaderEntity>> CreateInvoices()
    {
        var creates = new List<LateLoader<int, InvoiceHeaderEntity>>();
        foreach (var worksheet in _excel.GetWorksheetNames())
        {
            _excel.SelectWorksheet(worksheet);
            var create = await CreateInvoice(worksheet);
            creates.Add(create);
        }
        await _repository.SaveChanges();
        return creates
            .Select(x => x.LoadObject!)
            .ToList();
    }

    private async Task<LateLoader<int, InvoiceHeaderEntity>> CreateInvoice(string worksheet)
    {
        var header = await CreateHeader(worksheet);
        var detail = CreateDetail(worksheet, header.TempId);
        return header;
    }

    private async Task<LateLoader<int, InvoiceHeaderEntity>> CreateHeader(string worksheet)
    {
        var customer = GetIntValue(ExcelInvoiceTemplate.CellCustomer, $"Invalid Customer in worksheet {worksheet}");
        var date = GetDateTimeValue(ExcelInvoiceTemplate.CellDate, $"Invalid Date in worksheet {worksheet}");
        var description = _excel.GetCellValueAsString(ExcelInvoiceTemplate.CellDesc);
        var create = new InvoiceHeaderCreateDTO(
            Customer: customer,
            InvoiceDate: DateOnly.FromDateTime(date),
            StatusTypeId: (int)enumStatusType.Draft,
            Description: description);
        var header = await _invoiceHeaderCacheEntity.Create(create);
        return header;
    }

    private async Task CreateDetail(string worksheet, int headerId)
    {
        var creates = new List<InvoiceDetailCreateDTO>();
        int row = ExcelInvoiceTemplate.RowStart;
        while (_excel.GetCellValueAsString(new Cell(row, ExcelInvoiceTemplate.ColumnCustItemCode)) != "")
        {
            var custItemCode = _excel.GetCellValueAsString(new Cell(row, ExcelInvoiceTemplate.ColumnCustItemCode));
            var custItemDesc = _excel.GetCellValueAsString(new Cell(row, ExcelInvoiceTemplate.ColumnCustItemDesc));
            var custRate = GetDecimalValue(new Cell(row, ExcelInvoiceTemplate.ColumnCustRate), $"Invalid rate in worksheet {worksheet}");
            var custCases = GetDecimalValue(new Cell(row, ExcelInvoiceTemplate.ColumnCases), $"Invalid cases in worksheet {worksheet}");
            creates.Add(new InvoiceDetailCreateDTO(
                CustItemCode: custItemCode,
                CustItemDesc: custItemDesc,
                CustomerRate: custRate,
                ApprovedRate: 0,
                Cases: custCases));
        }
        creates = Aggregate(creates);
        await _invoiceDetailCacheEntity.Create(headerId, creates);
    }

    private List<InvoiceDetailCreateDTO> Aggregate(List<InvoiceDetailCreateDTO> creates)
    {
        return (
            from a in creates
            group a by new
            {
                Credit = Math.Round(a.CustomerRate, 2),
                a.CustItemCode,
                a.CustItemDesc
            } into b
            select new InvoiceDetailCreateDTO(
                CustItemCode: b.First().CustItemCode,
                CustItemDesc: b.First().CustItemDesc,
                CustomerRate: Math.Round(b.First().CustomerRate, 2),
                ApprovedRate: 0,
                Cases: Math.Round(b.Sum(x => x.Cases), 2)
        )).ToList();
    }

    private DateTime GetDateTimeValue(Cell cell, string error)
    {
        int serialDate = GetIntValue(cell);
        if (serialDate != 0)
        {
            return FromExcelSerialDate(serialDate);
        }
        try
        {
            var value = _excel.GetCellValueAsDateTime(cell);
            return value;
        }
        catch (Exception)
        {
            _userLogging.AddLog(error);
            throw new Exception(error);
        }
    }

    private decimal GetDecimalValue(Cell cell, string error)
    {
        try
        {
            var value = _excel.GetCellValueAsDecimal(cell);
            return value;
        }
        catch (Exception)
        {
            _userLogging.AddLog(error);
            throw new Exception(error);
        }
    }

    private int GetIntValue(Cell cell)
    {
        try
        {
            var value = _excel.GetCellValueAsInt32(cell);
            return value;
        }
        catch (Exception)
        {
            return 0;
        }
    }

    private int GetIntValue(Cell cell, string error)
    {
        try
        {
            var value = _excel.GetCellValueAsInt32(cell);
            return value;
        }
        catch (Exception)
        {
            _userLogging.AddLog(error);
            throw new Exception(error);
        }
    }

    private DateTime FromExcelSerialDate(int serialDate)
    {
        if (serialDate > 59)
        {
            serialDate -= 1;
        }
        return new DateTime(1899, 12, 31).AddDays(serialDate);
    }
}
