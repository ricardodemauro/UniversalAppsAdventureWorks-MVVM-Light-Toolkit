using AdventureWorksCatalog.DataSources;
using AdventureWorksCatalog.Interfaces.DataSources;
using AdventureWorksCatalog.Portable.Model;
using AdventureWorksCatalog.ViewModel.Messages;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using System;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Xml;

namespace AdventureWorksCatalog.ViewModel
{
    public class ProductPageViewModel : AWViewModelBase
    {
        public ICommand NavigateHomeCommand { get; private set; }

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
        }

        private void OnNavigateHomeCommand()
        {
            MessengerInstance.Send<NavigateMessage>(new NavigateMessage("HomePage"));
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

        #region Share
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


        public string GetTitleToShare()
        {
            if (Product == null || Company == null)
                return null;
            return string.Format("{0} - {1}", Company.Name, Product.Name);
        }
        #endregion

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
    }
}
