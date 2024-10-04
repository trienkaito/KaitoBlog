using FA.JustBlog.Core.Models;

namespace FA.JustBlog.Core
{
    public class CategoryRepository : Repository<Category>, ICategoryRepositories
    {
        public CategoryRepository(JustBlogContext context) : base(context)
        {
           
        }

    }

}
