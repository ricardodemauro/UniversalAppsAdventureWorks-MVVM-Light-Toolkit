using AdventureWorksCatalog.DataSources;
using AdventureWorksCatalog.Interfaces.DataSources;
using AdventureWorksCatalog.Locator;
using AdventureWorksCatalog.Portable.Model;
using AdventureWorksCatalog.ViewModel.Services;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using System;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Xml;
using Windows.ApplicationModel.DataTransfer;
using Windows.Storage.Streams;

namespace AdventureWorksCatalog.ViewModel
{
    public class ProductPageViewModel : AWViewModelBase, ISupportSharing
    {
        public ICommand NavigateHomeCommand { get; private set; }

        public ICommand ShareProductCommand { get; set; }

        public IWindowsDataSource DataSource { get; private set; }

        private Product _Product;
        public Product Product
        {
            get { return _Product; }
            set { Set(ref this._Product, value); }
        }

        private Category _Category;
        public Category Category
        {
            get { return _Category; }
            set { Set(ref this._Category, value); }
        }

        private Company _Company;
        public Company Company
        {
            get { return _Company; }
            set { Set(ref this._Company, value); }
        }

        public ProductPageViewModel(IWindowsDataSource datasource, INavigationService navigationService)
            : base(navigationService)
        {
            this.DataSource = datasource;

            NavigateHomeCommand = new RelayCommand(OnNavigateHomeCommand);
            ShareProductCommand = new RelayCommand(OnShareProductCommand);

#if DEBUG
            if (IsInDesignMode)
            {
                this.LoadAsync(1);
            }
#endif
        }

        private void OnShareProductCommand()
        {
            DataTransferManager.ShowShareUI();
        }

        private void OnNavigateHomeCommand()
        {
            this.NavigationService.NavigateTo(PagesName.HomePageName);
        }

        public async Task LoadAsync(int productId)
        {
            Company = await DataSource.GetCompanyAsync();

            var product = await DataSource.GetProductAsync(productId);
            if (product != null)
            {
                Category = product.Category;
            }
            Product = product;
        }

      

        public override void Initialize(object parameter)
        {
            Product product = parameter as Product;
            if (product == null)
            {
                throw new ArgumentNullException("parameter", "parameter cannot be null");
            }
            this.LoadAsync(product.Id);
            base.Initialize(parameter);
        }


        #region Share
        public void OnShareRequested(DataRequest dataRequest)
        {
            if (this.Product != null)
            {
                dataRequest.Data.Properties.Title = this.ShareTitle;
                dataRequest.Data.Properties.Description = this.Product.Description;
                dataRequest.Data.SetText(this.Product.Description);

                var imageStreamRef = RandomAccessStreamReference.CreateFromUri(new Uri(new Uri("ms-appx:///Data/"), this.Product.PhotoPath));
                if (imageStreamRef != null)
                {
                    dataRequest.Data.Properties.Thumbnail = imageStreamRef;
                    dataRequest.Data.SetBitmap(imageStreamRef);
                }

                var htmlToShare = this.GetHtmlToShare();
                dataRequest.Data.SetHtmlFormat(HtmlFormatHelper.CreateHtmlFormat(htmlToShare));
            }
            else
            {
                throw new Exception("Select the item you want to share and try again.");
            }
        }

        public string ShareTitle
        {
            get
            {
                if (this.Product == null || this.Company == null)
                    return null;
                return string.Format("{0} - {1}", this.Company.Name, this.Product.Name);
            }
        }

        public Uri GetUriToShare()
        {
            if (Product != null && !String.IsNullOrEmpty(Product.ProductUrl))
            {
                return new Uri(Product.ProductUrl);
            }

            return new Uri(Company.Website);
        }

        public string GetHtmlToShare()
        {
            var stringBuilder = new StringBuilder();
            var settings = new XmlWriterSettings() { OmitXmlDeclaration = true };
            using (var xmlWriter = XmlWriter.Create(stringBuilder, settings))
            {
                xmlWriter.WriteStartElement("div");

                xmlWriter.WriteElementString("p", Company.Name);

                xmlWriter.WriteStartElement("p");
                xmlWriter.WriteElementString("span", Company.Website);
                xmlWriter.WriteStartElement("br");
                xmlWriter.WriteEndElement();
                xmlWriter.WriteElementString("span", Company.ContactEmail);
                xmlWriter.WriteStartElement("br");
                xmlWriter.WriteEndElement();
                xmlWriter.WriteElementString("span", Company.Address);
                xmlWriter.WriteStartElement("br");
                xmlWriter.WriteEndElement();
                xmlWriter.WriteElementString("span", Company.Telephone);
                xmlWriter.WriteStartElement("br");
                xmlWriter.WriteEndElement();
                xmlWriter.WriteEndElement();

                xmlWriter.WriteStartElement("hr");
                xmlWriter.WriteEndElement();

                if (Product != null)
                {
                    xmlWriter.WriteElementString("h2", Product.Category.Name);

                    xmlWriter.WriteStartElement("br");
                    xmlWriter.WriteEndElement();

                    xmlWriter.WriteElementString("b", string.Format("R$ {0:0.00}", Product.Price));

                    xmlWriter.WriteElementString("p", Product.Description);
                }

                xmlWriter.WriteEndElement();
                xmlWriter.Flush();
            }
            return stringBuilder.ToString();
        }
        #endregion
    }
}
