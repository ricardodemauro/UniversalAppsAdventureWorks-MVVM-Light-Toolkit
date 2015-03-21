using AdventureWorksCatalog.Interfaces.DataSources;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AdventureWorksCatalog.Design
{
    public class DesignDataSourceWindows : IWindowsDataSource
    {
        public Task<List<Portable.Model.Category>> GetCategoriesAndItemsAsync(int? maxItemsPerCategory = null)
        {
            throw new NotImplementedException();
        }

        public Task<Portable.Model.Category> GetCategoryAsync(int categoryId)
        {
            throw new NotImplementedException();
        }

        public Task<Portable.Model.Company> GetCompanyAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Portable.Model.Product> GetProductAsync(int productId)
        {
            throw new NotImplementedException();
        }

        public Task<List<Portable.Model.Category>> SearchCategoriesAndItemsAsync(string query)
        {
            throw new NotImplementedException();
        }
    }
}
