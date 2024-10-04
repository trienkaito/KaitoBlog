using FA.JustBlog.Core;
using FA.JustBlog.Core.Repositories;
using FA.JustBlog.Models;
using Microsoft.AspNetCore.Mvc;

namespace FA.JustBlog.Components
{
    [ViewComponent(Name = "Categories")]
    public class CategoriesViewComponent : ViewComponent
    {
        private readonly ICategoryRepositories _categoryRepositories;

        public CategoriesViewComponent(ICategoryRepositories categoryRepositories)
        {
            _categoryRepositories = categoryRepositories;
        }

        // Ensure there is exactly one public Invoke method
        
        public  async Task<IViewComponentResult> InvokeAsync()
        {
            var categories = await _categoryRepositories.GetAllAsync();
            var data = categories.Select(cate => {
                return new CategoryViewModel(
                cate.Id,
                cate.Name
                );
                }
              );
            if(data != null)
            {
                return View(data);
            }
            return View();
        }
    }
}
