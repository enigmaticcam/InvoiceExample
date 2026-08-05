using Invoice_WPF.Services.Core;
using Invoice_WPF.Services.Entities;
using Invoice_WPF.Services.States;
using System.Net.Http;

namespace Invoice_WPF.Services
{
    public interface IFactory
    {
        IClient Client { get; }
        IInvoiceHeaderState InvoiceHeaderState { get; }
        IInvoiceSearchState InvoiceSearchState { get; }
        IResultStatusTypeState ResultStatusTypeState { get; }
        IServiceWrapper ServiceWrapper { get; }
        IStatusTypeState StatusTypeState { get; }
    }


    public class Factory : IFactory
    {
        private HttpClient _httpClient;
        private Lazy<IClient> _client;
        private Lazy<IInvoiceHeaderState> _invoiceHeaderState;
        private Lazy<IInvoiceSearchState> _invoiceSearchState;
        private Lazy<IResultStatusTypeState> _resultStatusTypeState;
        private Lazy<IServiceWrapper> _serviceWrapper;
        private Lazy<IStatusTypeState> _statusTypeState;

        public Factory()
        {
            _httpClient = new HttpClient() { BaseAddress = new Uri("https://localhost:7206") };
            _client = new Lazy<IClient>(() => new Client(_httpClient));
            _invoiceHeaderState = new Lazy<IInvoiceHeaderState>(() => new InvoiceHeaderState());
            _invoiceSearchState = new Lazy<IInvoiceSearchState>(() => new InvoiceSearchState());
            _resultStatusTypeState = new Lazy<IResultStatusTypeState>(() => new ResultStatusTypeState());
            _serviceWrapper = new Lazy<IServiceWrapper>(() => new ServiceWrapper(Client));
            _statusTypeState = new Lazy<IStatusTypeState>(() => new StatusTypeState());
        }

        public IClient Client => _client.Value;
        public IInvoiceHeaderState InvoiceHeaderState => _invoiceHeaderState.Value;
        public IInvoiceSearchState InvoiceSearchState => _invoiceSearchState.Value;
        public IResultStatusTypeState ResultStatusTypeState => _resultStatusTypeState.Value;
        public IStatusTypeState StatusTypeState => _statusTypeState.Value;
        public IServiceWrapper ServiceWrapper => _serviceWrapper.Value;
    }
}
