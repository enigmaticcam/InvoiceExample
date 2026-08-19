using Invoice_WPF.Services.Commands.InvoiceDetail;
using Invoice_WPF.Services.Commands.InvoiceHeader;
using Invoice_WPF.Services.Commands.InvoiceSearch;
using Invoice_WPF.Services.Commands.InvoiceUploader;
using Invoice_WPF.Services.Commands.ResultStatusType;
using Invoice_WPF.Services.Commands.StatusType;
using Invoice_WPF.Services.Core;
using Invoice_WPF.Services.Entities;
using Invoice_WPF.Services.Invoking;
using Invoice_WPF.Services.States;
using System.Net.Http;

namespace Invoice_WPF.Services
{
    public interface IFactory
    {
        IClient Client { get; }
        HttpClient HClient { get; }
        IFileDownload FileDownload { get; }
        IInvoiceDetailInvoker InvoiceDetailInvoker { get; }
        IInvoiceDetailState InvoiceDetailState { get; }
        IInvoiceDetailUpdateState InvoiceDetailUpdateState { get; }
        IInvoiceHeaderInvoker InvoiceHeaderInvoker { get; }
        IInvoiceHeaderState InvoiceHeaderState { get; }
        IInvoiceSearchInvoker InvoiceSearchInvoker { get; }
        IInvoiceSearchState InvoiceSearchState { get; }
        IInvoiceUploaderInvoker InvoiceUploaderInvoker { get; }
        IInvoiceUploaderState InvoiceUploaderState { get; }
        IModalNavigation ModalNavigation { get; }
        IResultStatusInvoker ResultStatusInvoker { get; }
        IResultStatusTypeState ResultStatusTypeState { get; }
        IServiceWrapper ServiceWrapper { get; }
        IStatusTypeInvoker StatusTypeInvoker { get; }
        IStatusTypeState StatusTypeState { get; }
    }


    public class Factory : IFactory
    {
        private HttpClient _httpClient;
        private Lazy<IClient> _client;
        private Lazy<IFileDownload> _fileDownload;
        private Lazy<IInvoiceDetailInvoker> _invoiceDetailInvoker;
        private Lazy<IInvoiceDetailState> _invoiceDetailState;
        private Lazy<IInvoiceDetailUpdateState> _invoiceDetailUpdateState;
        private Lazy<IInvoiceHeaderInvoker> _invoiceHeaderInvoker;
        private Lazy<IInvoiceHeaderState> _invoiceHeaderState;
        private Lazy<IInvoiceUploaderInvoker> _invoiceUploaderInvoker;
        private Lazy<IInvoiceUploaderState> _invoiceUploaderState;
        private Lazy<IInvoiceSearchInvoker> _invoiceSearchInvoker;
        private Lazy<IInvoiceSearchState> _invoiceSearchState;
        private Lazy<IModalNavigation> _modalNavigation;
        private Lazy<IResultStatusInvoker> _resultStatusInvoker;
        private Lazy<IResultStatusTypeState> _resultStatusTypeState;
        private Lazy<IServerInvoker> _serverInvoker;
        private Lazy<IServerStatus> _serverStatus;
        private Lazy<IServiceWrapper> _serviceWrapper;
        private Lazy<IStatusTypeInvoker> _statusTypeInvoker;
        private Lazy<IStatusTypeState> _statusTypeState;

        public Factory(InvokerToken token)
        {
            _httpClient = new HttpClient() { BaseAddress = new Uri("https://localhost:7206") };
            _client = new Lazy<IClient>(() => new Client(_httpClient));
            _fileDownload = new Lazy<IFileDownload>(() => new FileDownload(_httpClient));
            _invoiceDetailInvoker = new Lazy<IInvoiceDetailInvoker>(() => new InvoiceDetailInvoker(ServerInvoker, ServiceWrapper, InvoiceDetailState));
            _invoiceDetailState = new Lazy<IInvoiceDetailState>(() => new InvoiceDetailState());
            _invoiceDetailUpdateState = new Lazy<IInvoiceDetailUpdateState>(() => new InvoiceDetailUpdateState(InvoiceDetailState));
            _invoiceHeaderInvoker = new Lazy<IInvoiceHeaderInvoker>(() => new InvoiceHeaderInvoker(ServerInvoker, ServiceWrapper, InvoiceHeaderState));
            _invoiceHeaderState = new Lazy<IInvoiceHeaderState>(() => new InvoiceHeaderState());
            _invoiceSearchInvoker = new Lazy<IInvoiceSearchInvoker>(() => new InvoiceSearchInvoker(ServerInvoker, ServiceWrapper, InvoiceSearchState));
            _invoiceSearchState = new Lazy<IInvoiceSearchState>(() => new InvoiceSearchState());
            _invoiceUploaderInvoker = new Lazy<IInvoiceUploaderInvoker>(() => new InvoiceUploaderInvoker(ServerInvoker, ServiceWrapper, InvoiceUploaderState));
            _invoiceUploaderState = new Lazy<IInvoiceUploaderState>(() => new InvoiceUploaderState());
            _modalNavigation = new Lazy<IModalNavigation>(() => new ModalNavigation(this, token));
            _resultStatusInvoker = new Lazy<IResultStatusInvoker>(() => new ResultStatusInvoker(ServerInvoker, ServiceWrapper, ResultStatusTypeState));
            _resultStatusTypeState = new Lazy<IResultStatusTypeState>(() => new ResultStatusTypeState());
            _serverInvoker = new Lazy<IServerInvoker>(() => new ServerInvoker(ServerStatus));
            _serverStatus = new Lazy<IServerStatus>(() => new ServerStatus());
            _serviceWrapper = new Lazy<IServiceWrapper>(() => new ServiceWrapper(Client));
            _statusTypeInvoker = new Lazy<IStatusTypeInvoker>(() => new StatusTypeInvoker(ServerInvoker, ServiceWrapper, StatusTypeState));
            _statusTypeState = new Lazy<IStatusTypeState>(() => new StatusTypeState());
        }
        public HttpClient HClient => _httpClient;

        public IClient Client => _client.Value;
        public IFileDownload FileDownload => _fileDownload.Value;
        public IInvoiceDetailInvoker InvoiceDetailInvoker => _invoiceDetailInvoker.Value;
        public IInvoiceDetailState InvoiceDetailState => _invoiceDetailState.Value;
        public IInvoiceDetailUpdateState InvoiceDetailUpdateState => _invoiceDetailUpdateState.Value;
        public IInvoiceHeaderInvoker InvoiceHeaderInvoker => _invoiceHeaderInvoker.Value;
        public IInvoiceHeaderState InvoiceHeaderState => _invoiceHeaderState.Value;
        public IInvoiceSearchInvoker InvoiceSearchInvoker => _invoiceSearchInvoker.Value;
        public IInvoiceSearchState InvoiceSearchState => _invoiceSearchState.Value;
        public IInvoiceUploaderInvoker InvoiceUploaderInvoker => _invoiceUploaderInvoker.Value;
        public IInvoiceUploaderState InvoiceUploaderState => _invoiceUploaderState.Value;
        public IModalNavigation ModalNavigation => _modalNavigation.Value;
        public IResultStatusInvoker ResultStatusInvoker => _resultStatusInvoker.Value;
        public IResultStatusTypeState ResultStatusTypeState => _resultStatusTypeState.Value;
        public IStatusTypeInvoker StatusTypeInvoker => _statusTypeInvoker.Value;
        public IStatusTypeState StatusTypeState => _statusTypeState.Value;
        public IServerInvoker ServerInvoker => _serverInvoker.Value;
        public IServerStatus ServerStatus => _serverStatus.Value;
        public IServiceWrapper ServiceWrapper => _serviceWrapper.Value;
    }
}
