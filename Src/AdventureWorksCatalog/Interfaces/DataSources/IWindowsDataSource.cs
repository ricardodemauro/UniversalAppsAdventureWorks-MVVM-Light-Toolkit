using AdventureWorksCatalog.Portable.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventureWorksCatalog.Interfaces.DataSources
{
    public interface IWindowsDataSource
    {
        Task<List<Category>> GetCategoriesAndItemsAsync(int? maxItemsPerCategory = null);

        Task<List<Category>> SearchCategoriesAndItemsAsync(string query);

        Task LoadAsync();

        Task<Company> GetCompanyAsync();

        Task<Product> GetProductAsync(int productId);

        Task<Category> GetCategoryAsync(int categoryId);
    }
}
