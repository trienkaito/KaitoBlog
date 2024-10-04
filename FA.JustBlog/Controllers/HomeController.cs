using FA.JustBlog.Core;
using FA.JustBlog.Core.Models;
using FA.JustBlog.Core.Repositories;
using FA.JustBlog.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Linq;
using System.Net.WebSockets;

namespace FA.JustBlog.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IPostRepository _postRepository;
        private readonly ICommentRepository _commentRepository;

        public HomeController(ILogger<HomeController> logger, IPostRepository postRepository, ICommentRepository commentRepository)
        {
            _logger = logger;
            _postRepository = postRepository;
            _commentRepository = commentRepository;
        }
        public ActionResult AboutCard()
        {
            return PartialView("_PartialAboutCard");
        }
        private  IEnumerable<PostViewModel> GetMostViewedPosts()
        {
            var posts =  _postRepository.GetMostViewedPost(5);
            if (posts == null)
            {
                return Enumerable.Empty<PostViewModel>();
            }
            return posts.Select(post =>
                new PostViewModel(
                    post.Id,
                    post.Title,
                    post.ShortDescription,
                    post.PostContent,
                    post.Meta,
                    post.UrlSlug,
                    post.Published,
                    post.PostedOn,
                    post.Modified,
                    post.ViewCount,
                    post.RateCount,
                    post.TotalRate,
                    post.TagValues
                )
            );
        }

        public async Task<IActionResult> IndexAsync(string Name)
        {
            IEnumerable<Post> posts;

            if (string.IsNullOrEmpty(Name))
            {
               
                posts = await _postRepository.GetAllAsync();
               
            }
            else
            {
               
                posts = _postRepository.GetPostsByCategory(Name);
            }

            var data = posts.Select(post => new PostViewModel
            (
                post.Id,
                post.Title,
                post.ShortDescription,
                post.PostContent,
                post.Meta,
                post.UrlSlug,
                post.Published,
                post.PostedOn,
                post.Modified,
                post.ViewCount,
                post.RateCount,
                post.TotalRate,
                post.TagValues
            )).OrderByDescending(p => p.PostedOn);

            return View(data);
        }


        [Route("Category/{Name}")]
        public IActionResult Categories(string Name)
        {
            var posts = _postRepository.GetPostsByCategory(Name);
            var data = posts.Select(post =>
            {
                return new PostViewModel
                (
                    post.Id,
                    post.Title,
                    post.ShortDescription,
                    post.PostContent,
                    post.Meta,
                    post.UrlSlug,
                    post.Published,
                    post.PostedOn,
                    post.Modified,
                    post.ViewCount,
                    post.RateCount,
                    post.TotalRate,
                    post.TagValues
                    );
            }
              );

            return View(data);
        }
      
        public IActionResult Privacy()
        {
            return View();
        }
    
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
