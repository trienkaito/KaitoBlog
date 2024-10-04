using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FA.JustBlog.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace FA.JustBlog.Core.Repositories
{
    public class PostRepository : Repository<Post>, IPostRepository
    {
        private new readonly JustBlogContext _context;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public PostRepository(JustBlogContext context) : base(context)
        {
            _context = context;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        public int CountPostsForCategory(string category)
        {
            return  _context.Posts.Count(p => p.Category.Name == category);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="tag"></param>
        /// <returns></returns>
        public int CountPostsForTag(string tag)
        {
            return _context.Posts.Count(p => p.Tags.Any(t => t.Name == tag));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="urlSlug"></param>
        /// <returns></returns>
        public Post FindPost(int? year, int? month, string urlSlug)
        {
            return _context.Posts
                .Include(p => p.Category) // Include the Category
                .Include(p => p.Tags)
                .Include(p => p.Comments)// Include the Tags
                .FirstOrDefault(p => p.Published &&
                                     p.PostedOn.Year == year &&
                                     p.PostedOn.Month == month &&
                                     p.UrlSlug == urlSlug);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Post> GetAllPosts()
        {
            return _context.Posts.OrderByDescending(p => p.PostedOn).ToList();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Post> GetPublisedPosts()
        {
            return _context.Posts.Where(p => p.Published).OrderByDescending(p => p.PostedOn).ToList();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Post> GetUnpublisedPosts()
        {
            return _context.Posts.Where(p => !p.Published).OrderByDescending(p => p.Modified).ToList();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="size"></param>
        /// <returns></returns>
        public IEnumerable<Post> GetHighestPosts(int size)
        {
            return _context.Posts.AsEnumerable()
                                 .OrderByDescending(p => p.Rate)
                                 .Take(size)
                                 .ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="size"></param>
        /// <returns></returns>
        public IEnumerable<Post> GetLatestPost(int size)
        {
            return _context.Posts.OrderByDescending(p => p.PostedOn).Take(size).ToList();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="size"></param>
        /// <returns></returns>
        public IEnumerable<Post> GetMostViewedPost(int size)
        {
            return _context.Posts.OrderByDescending(p => p.ViewCount).Take(size).ToList();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        public IEnumerable<Post> GetPostsByCategory(string categoryName)
        {
            return _context.Posts.Where(p => p.Category.Name == categoryName).ToList();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="monthYear"></param>
        /// <returns></returns>
        public IEnumerable<Post> GetPostsByMonth(DateTime monthYear)
        {;
            return _context.Posts.Where(p => p.PostedOn.Year == monthYear.Year && p.PostedOn.Month == monthYear.Month).ToList();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="tag"></param>
        /// <returns></returns>
        public IEnumerable<Post> GetPostsByTag(string tag)
        {
            return _context.Posts.Where(p => p.Tags.Any(t => t.UrlSlug == tag)).ToList();
        }
        public async Task<IEnumerable<Post>> SearchPostsAsync(string searchQuery)
        {
            if (string.IsNullOrEmpty(searchQuery))
            {
                return await _context.Posts.ToListAsync();
            }

            return await _context.Posts
                .Where(p => p.Title.Contains(searchQuery) ||
                            p.ShortDescription.Contains(searchQuery) ||
                            p.PostContent.Contains(searchQuery) ||
                            p.Meta.Contains(searchQuery) ||
                            p.UrlSlug.Contains(searchQuery))
                .ToListAsync();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="post"></param>
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
