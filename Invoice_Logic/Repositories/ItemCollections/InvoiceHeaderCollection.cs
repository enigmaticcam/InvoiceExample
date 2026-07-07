using Invoice_Logic.Data.EF;

namespace Invoice_Logic.Repositories.ItemCollections;

public interface IInvoiceHeaderCollection : IItemCollection<InvoiceHeader> { }

public class InvoiceHeaderCollection : ItemCollection<InvoiceHeader>, IInvoiceHeaderCollection
{
    public InvoiceHeaderCollection(IAllItemCollections allItemCollections) : base(allItemCollections)
    {
    }
}
