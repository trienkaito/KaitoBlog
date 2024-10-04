using FA.JustBlog.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FA.JustBlog.Core.Repositories
{
    public interface IPostRepository: IDisposable, IRepository<Post>
    {
       Post FindPost(int? year, int? month, string? urlSlug);
       IEnumerable<Post> GetPublisedPosts();
       IEnumerable<Post> GetUnpublisedPosts();
       IEnumerable<Post> GetLatestPost(int size);
       IEnumerable<Post> GetPostsByMonth(DateTime monthYear);
        int CountPostsForCategory(string category);
       IEnumerable<Post> GetPostsByCategory(string categoryName);
        int CountPostsForTag(string tag);
       IEnumerable<Post> GetPostsByTag(string tag);
       IEnumerable<Post> GetMostViewedPost(int size);
       IEnumerable<Post> GetHighestPosts(int size);
        Task<IEnumerable<Post>> SearchPostsAsync(string searchQuery);
    }
}
