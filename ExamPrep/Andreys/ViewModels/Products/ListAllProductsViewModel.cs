namespace Andreys.ViewModels.Products
{
    using System.Collections.Generic;

    public class ListAllProductsViewModel
    {
        public IEnumerable<ProductInfoViewModel> Items { get; set; }
    }
}
