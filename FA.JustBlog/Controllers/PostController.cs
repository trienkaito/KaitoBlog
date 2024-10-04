using FA.JustBlog.Core.Models;
using FA.JustBlog.Core.Repositories;
using FA.JustBlog.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using System.Linq;
using System.Xml.Linq;

namespace FA.JustBlog.Controllers
{
    public class PostController : Controller
    {
        private readonly IPostRepository _postRepository;

        public PostController(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }
        /*  public  IActionResult MostViewedPosts()
          {
              var posts =  _postRepository.GetMostViewedPost(5);
              if (posts == null)
              {
                  return NotFound();
              }
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
              return PartialView("_ListPosts", data);
          }*/
        // GET: PostController/Details/5
        private IEnumerable<PostViewModel> GetMostViewedPosts()
        {
            var posts = _postRepository.GetMostViewedPost(5);
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

        private IEnumerable<PostViewModel> LatestPosts()
        {
            var posts = _postRepository.GetLatestPost(5);
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

        [Route("Post/{year}/{month}/{urlSlug}")]
        public IActionResult Index(int year, int month, string urlSlug)
        {
            var post = _postRepository.FindPost(year, month, urlSlug);

            if (post == null)
            {
                return NotFound();
            }

            var data = new PostViewModel
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
                post.TagValues,
                new CategoryViewModel(post.Category.Id, post.Category.Name),
                post.Tags.Select(tag => new TagViewModel(tag.Id, tag.Name)).ToList(),
                post.Comments.Select(comment => new CommentViewModel(
                    comment.Id,
                    comment.Name,
                    comment.Email,
                    comment.PostId,
                    comment.CommentHeader,
                    comment.CommentText,
                    comment.CommentTime
                )).ToList()) ;

            return View(data);
        }


        // GET: PostController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PostController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PostController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: PostController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PostController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: PostController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
