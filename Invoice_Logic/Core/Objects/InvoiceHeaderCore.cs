using Invoice_Logic.Core.Interfaces;
using Invoice_Logic.Data.DTOs;
using Invoice_Logic.Data.DTOs.Create;
using Invoice_Logic.Data.DTOs.Entity;
using Invoice_Logic.Enums;
using Invoice_Logic.Factories;
using Invoice_Logic.Repositories;
using Invoice_Logic.Repositories.CacheEntities;

namespace Invoice_Logic.Core.Objects;

public class InvoiceHeaderCore : IInvoiceHeaderCore
{
    private IFactoryMain _factory;
    private IInvoiceHeaderCacheEntity _invoiceHeaderCacheEntity;
    private IInvoiceDetailCacheEntity _invoiceDetailCacheEntity;
    private IInvoiceResultCacheEntity _invoiceResultCacheEntity;

    public InvoiceHeaderCore(IFactoryMain factory, IInvoiceHeaderCacheEntity invoiceHeaderCacheEntity, IInvoiceDetailCacheEntity invoiceDetailCacheEntity, IInvoiceResultCacheEntity invoiceResultCacheEntity)
    {
        _factory = factory;
        _invoiceHeaderCacheEntity = invoiceHeaderCacheEntity;
        _invoiceDetailCacheEntity = invoiceDetailCacheEntity;
        _invoiceResultCacheEntity = invoiceResultCacheEntity;
    }

    public Task<InvoiceHeaderEntity> Get(int id)
    {
        return _invoiceHeaderCacheEntity.Get(id);
    }

    public Task<List<InvoiceHeaderEntity>> Get(InvoiceFilterDTO filter)
    {
        return _invoiceHeaderCacheEntity.Get(filter);
    }

    public Task<List<InvoiceDetailEntity>> GetDetail(int id)
    {
        return _invoiceDetailCacheEntity.Get(id);
    }

    public async Task<InvoicePermissionsDTO> GetPermissions(int id)
    {
        var headerObj = await Get(id);
        var headerType = FactoryInvoiceHeaderType.Create((enumStatusType)headerObj.StatusTypeId);
        return headerType.GetPermissions(headerObj);
    }

    public async Task<List<InvoiceFullResultDTO>> GetResults(int id)
    {
        var list = new List<InvoiceFullResultDTO>();
        var detail = await GetDetail(id);
        var results = await _invoiceResultCacheEntity.Get(id);
        var hash = results.ToDictionary(x => x.InvoiceDetailId, x => x);
        foreach (var line in detail)
        {
            InvoiceResultEntity? result = null;
            if (hash.ContainsKey(line.InvoiceDetailId))
            {
                result = hash[line.InvoiceDetailId];
            }
            list.Add(new InvoiceFullResultDTO(
                InvoiceHeaderId: line.InvoiceHeaderId,
                InvoiceDetailId: line.InvoiceDetailId,
                CustItemCode: line.CustItemCode,
                CustItemDesc: line.CustItemDesc,
                CustomerRate: line.CustomerRate,
                ApprovedRate: line.ApprovedRate,
                Cases: line.Cases,
                OurItemCode: result?.OurItemCode,
                CasesRemaining: result?.CasesRemaining,
                HasFailedCase: result?.HasFailedCase,
                HasFailedRate: result?.HasFailedRate,
                ResultStatusTypeId: result?.ResultStatusTypeId
            ));
        }
        return list;
    }

    public Task<LateLoader<int, InvoiceHeaderEntity>> QueueCreate(InvoiceHeaderCreateDTO create)
    {
        return _invoiceHeaderCacheEntity.Create(create);
    }

    public Task QueueCreate(int headerId, IEnumerable<InvoiceDetailCreateDTO> creates)
    {
        return _invoiceDetailCacheEntity.Create(headerId, creates);
    }

    public async Task<List<InvoiceFullResultDTO>> UpdateRefreshResults(int headerId)
    {
        await CanPerform(headerId, enumInvoiceActionType.RefreshResults);
        await _factory.InvoiceProcedures.ProcessInvoicesAsync(headerId);
        await _invoiceDetailCacheEntity.Clear(headerId);
        return await GetResults(headerId);
    }

    private async Task CanPerform(int headerId, enumInvoiceActionType actionType)
    {
        var headerObj = await Get(headerId);
        var headerType = FactoryInvoiceHeaderType.Create((enumStatusType)headerObj.StatusTypeId);
        headerType.CanPerformAction(actionType, _factory.UserLogging);
    }
}
