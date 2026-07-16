using Invoice_Logic.Data.DTOs;
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

    public async Task<RandomInvoiceDTO> Perform()
    {
        var invoice = await GenerateRandomInvoice();
        return ConvertToText(invoice);
    }

    private async Task<List<InvoiceResult>> GenerateRandomInvoice()
    {
        var result = await _connection.LoadDataAsync<InvoiceResult>("dbo.GenerateRandomInvoice");
        return result.ToList();
    }

    private RandomInvoiceDTO ConvertToText(List<InvoiceResult> result)
    {
        var header = ConvertToTextHeader(result);
        var detail = ConvertToTextDetail(result);
        return new RandomInvoiceDTO(header, detail);
    }

    private string ConvertToTextHeader(List<InvoiceResult> result)
    {
        var text = new StringBuilder();
        text.AppendLine(result.First().Customer);
        text.AppendLine(result.First().InvoiceDate.ToShortDateString());
        text.AppendLine("Random invoice");
        return text.ToString();
    }

    private string ConvertToTextDetail(List<InvoiceResult> result)
    {
        var text = new StringBuilder();
        foreach (var line in result)
        {
            var join = string.Join("\t",
                line.CustomerItemCode,
                "",
                line.Rate,
                line.Cases);
            text.AppendLine(join);
        }
        return text.ToString();
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
