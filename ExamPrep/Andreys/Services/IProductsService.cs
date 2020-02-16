namespace Andreys.Services
{
    using Andreys.ViewModels.Products;
    using System.Collections.Generic;

    public interface IProductsService
    {
        void Create(string name, string description, string imageUrl, string category, string gender, decimal price);

        IEnumerable<ProductInfoViewModel> GetAllProducts();

        ProductDetailsViewModel GetProduct(int id);

        int Delete(int id);
    }
}
