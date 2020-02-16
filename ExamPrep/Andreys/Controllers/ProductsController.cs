namespace Andreys.Controllers
{
    using Andreys.Services;
    using Andreys.ViewModels.Products;
    using SIS.HTTP;
    using SIS.MvcFramework;

    public class ProductsController : Controller
    {
        private readonly IProductsService productsService;

        public ProductsController(IProductsService productsService)
        {
            this.productsService = productsService;
        }

        public HttpResponse Add()
        {
            if (!this.IsUserLoggedIn())
            {
                this.Redirect("/Users/Login");
            }

            return this.View();
        }

        [HttpPost]
        public HttpResponse Add(ProductAddInputModel inputModel)
        {
            if (!this.IsUserLoggedIn())
            {
                this.Redirect("/Users/Login");
            }

            if (inputModel.Name.Length < 4 || inputModel.Name.Length > 20)
            {
                return this.Error("Name should be between 4 and 20 charecters!");
            }

            if (inputModel.Description.Length > 10)
            {
                return this.Error("Description should be up to 10 charecters!");
            }

            this.productsService.Create(inputModel.Name, inputModel.Description,
                inputModel.ImageUrl, inputModel.Category, inputModel.Gender, inputModel.Price);

            return this.Redirect("/");
        }

        public HttpResponse Details(int id)
        {
            if (!this.IsUserLoggedIn())
            {
                this.Redirect("/Users/Login");
            }

            var inputModel = this.productsService.GetProduct(id);

            if (inputModel == null)
            {
                return this.Error("Invalid Product Id!");
            }

            return this.View(inputModel, "Details");
        }

        public HttpResponse Delete(int id)
        {
            if (!this.IsUserLoggedIn())
            {
                this.Redirect("/Users/Login");
            }

            var result = this.productsService.Delete(id);

            if (result == 0)
            {
                return this.Error("Invalid Product Id!");
            }

            return this.Redirect("/");
        }
    }
}
