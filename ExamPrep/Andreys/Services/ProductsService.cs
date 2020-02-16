namespace Andreys.Services
{
    using Andreys.Data;
    using Andreys.Models;
    using Andreys.Models.Enums;
    using Andreys.ViewModels.Products;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class ProductsService : IProductsService
    {
        private readonly AndreysDbContext db;

        public ProductsService(AndreysDbContext db)
        {
            this.db = db;
        }

        public void Create(string name, string description, string imageUrl, string category, string gender, decimal price)
        {
            var categoryEnum = (Category)Enum.Parse(typeof(Category), category);
            var genderEnum = (Gender)Enum.Parse(typeof(Gender), gender);

            var product = new Product
            {
                Name = name,
                Description = description,
                ImageUrl = imageUrl,
                Category = categoryEnum,
                Gender = genderEnum,
                Price = price,
            };

            this.db.Products.Add(product);
            this.db.SaveChanges();
        }

        public IEnumerable<ProductInfoViewModel> GetAllProducts()
        {
            var items = this.db.Products
                .Select(p => new ProductInfoViewModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    ImageUrl = p.ImageUrl,
                    Price = p.Price.ToString("F2"),
                })
                .ToList();

            return items;
        }

        public ProductDetailsViewModel GetProduct(int id)
        {
            var viewModel = this.db.Products
                .Where(p => p.Id == id)
                .Select(p => new ProductDetailsViewModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    ImageUrl = p.ImageUrl,
                    Price = p.Price.ToString("F2"),
                    Category = p.Category.ToString(),
                    Gender = p.Gender.ToString(),
                })
                .FirstOrDefault();

            return viewModel;
        }

        public int Delete(int id)
        {
            var product = this.db.Products.Find(id);
            this.db.Products.Remove(product);
            var result = db.SaveChanges();

            return result;
        }
    }
}
