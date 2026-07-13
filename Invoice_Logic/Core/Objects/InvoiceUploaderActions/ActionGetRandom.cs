using Invoice_Logic.Database;
using System.Text;

namespace Invoice_Logic.Core.Objects.InvoiceUploaderActions;

public class ActionGetRandom
{
    private IConnectionControl _connection;

    public ActionGetRandom(IConnectionControl connection)
    {
        _connection = connection;
    }

    public async Task<string> Perform()
    {
        var invoice = await GenerateRandomInvoice();
        return ConvertToText(invoice);
    }

    private async Task<List<InvoiceResult>> GenerateRandomInvoice()
    {
        var result = await _connection.LoadDataAsync<InvoiceResult>("dbo.GenerateRandomInvoice");
        return result.ToList();
    }

    private string ConvertToText(List<InvoiceResult> result)
    {
        var text = new StringBuilder();
        ConvertToTextHeader(result, text);
        ConvertToTextDetail(result, text);
        return text.ToString();
    }

    private void ConvertToTextHeader(List<InvoiceResult> result, StringBuilder text)
    {
        text.AppendLine("-------------------");
        text.AppendLine("Header");
        text.AppendLine("-------------------");
        text.AppendLine(result.First().Customer);
        text.AppendLine(result.First().InvoiceDate.ToShortDateString());
        text.AppendLine("Random invoice");
    }

    private void ConvertToTextDetail(List<InvoiceResult> result, StringBuilder text)
    {
        text.AppendLine("-------------------");
        text.AppendLine("Detail");
        text.AppendLine("-------------------");
        foreach (var line in result)
        {
            var join = string.Join("\t",
                line.CustomerItemCode,
                "",
                line.Rate,
                line.Cases);
            text.AppendLine(join);
        }
    }

    private class InvoiceResult
    {

        public string? Customer { get; set; }
        public DateTime InvoiceDate { get; set; }
        public string? CustomerItemCode { get; set; }
        public decimal Rate { get; set; }
        public decimal Cases { get; set; }
    }
}
