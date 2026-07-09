using ClosedXML.Excel;
using Invoice_Logic.Servers;

namespace Invoice_Logic.Excel;

public class ClosedXML : IExcel
{
    private IWebServer _webServer;
    private XLWorkbook _workbook;
    private IXLWorksheet _worksheet;

    public ClosedXML(IWebServer webServer)
    {
        _webServer = webServer;
        _workbook = new XLWorkbook();
        _worksheet = _workbook.Worksheets.Add("Sheet1");
    }

    public DateTime GetCellValueAsDateTime(Cell cell)
    {
        var cellObj = _worksheet.Cell(cell.Row, cell.Col);
        return Convert.ToDateTime(cellObj.Value.GetDateTime());
    }

    public decimal GetCellValueAsDecimal(Cell cell)
    {
        var cellObj = _worksheet.Cell(cell.Row, cell.Col);
        if (cellObj.IsEmpty())
        {
            return 0;
        }
        return Convert.ToDecimal(cellObj.Value.GetNumber());
    }

    public int GetCellValueAsInt32(Cell cell)
    {
        var cellObj = _worksheet.Cell(cell.Row, cell.Col);
        if (cellObj.IsEmpty())
        {
            return 0;
        }
        return Convert.ToInt32(cellObj.Value.GetNumber());
    }

    public string GetCellValueAsString(Cell cell)
    {
        var cellObj = _worksheet.Cell(cell.Row, cell.Col);
        if (cellObj.IsEmpty())
        {
            return "";
        }
        return cellObj.GetValue<string>();
    }

    public List<string> GetWorksheetNames()
    {
        return _workbook.Worksheets.Select(x => x.Name).ToList();
    }

    public void OpenFile(string fileName)
    {
        var local = _webServer.Get();
        Environment.SetEnvironmentVariable(
            variable: "TEMP",
            value: local.LocalDirectory);
        Environment.SetEnvironmentVariable(
            variable: "TMP",
            value: local.LocalDirectory);
        _workbook = new XLWorkbook(fileName);
        _worksheet = _workbook.Worksheets.First();
    }

    public void SelectWorksheet(string name)
    {
        _worksheet = _workbook.Worksheet(name);
    }
}
