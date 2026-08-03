using Invoice_WPF.Services.Core;
using Invoice_WPF.Services.Entities;
using System.Net.Http;

namespace Invoice_WPF.Services
{
    public interface IFactory
    {
        IClient Client { get; }
        IInvoiceSearchState InvoiceSearchState { get; }
        IServiceWrapper ServiceWrapper { get; }
    }


    public class Factory : IFactory
    {
        private HttpClient _httpClient;
        private Lazy<IClient> _client;
        private Lazy<IInvoiceSearchState> _invoiceSearchState;
        private Lazy<IServiceWrapper> _serviceWrapper;

        public Factory()
        {
            _httpClient = new HttpClient() { BaseAddress = new Uri("https://localhost:7206") };
            _client = new Lazy<IClient>(() => new Client(_httpClient));
            _invoiceSearchState = new Lazy<IInvoiceSearchState>(() => new InvoiceSearchState());
            _serviceWrapper = new Lazy<IServiceWrapper>(() => new ServiceWrapper(Client));
        }

        public IClient Client => _client.Value;
        public IInvoiceSearchState InvoiceSearchState => _invoiceSearchState.Value;

        public IServiceWrapper ServiceWrapper => _serviceWrapper.Value;
    }
}
