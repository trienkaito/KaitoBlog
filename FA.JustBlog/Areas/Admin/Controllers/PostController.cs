using FA.JustBlog.Areas.Admin.Models;
using FA.JustBlog.Core.Models;
using FA.JustBlog.Core.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FA.JustBlog.Core;
using Microsoft.AspNetCore.Authorization;

namespace FA.JustBlog.Areas.Admin.Controllers
{
    [Authorize(Policy = "RequireContributorOrHigher")]
    [Area("Admin")]
    public class PostController : Controller
    {
        private readonly ILogger<PostController> _logger;
        private readonly IPostRepository _postRepository;
        private readonly ICategoryRepositories _categoryRepositories;

        public PostController(ILogger<PostController> logger, IPostRepository postRepository, ICategoryRepositories categoryRepositories)
        {
            _logger = logger;
            _postRepository = postRepository;
            _categoryRepositories = categoryRepositories;
        }
        public async Task<IActionResult> Index(string searchQuery, string Type)
        {
            IEnumerable<Post> posts;

            if (!string.IsNullOrEmpty(searchQuery))
            {
                posts = await _postRepository.SearchPostsAsync(searchQuery);
            }
            else if (!string.IsNullOrEmpty(Type))
            {
                switch (Type)
                {
                    case "Latest Posts":
                        posts = _postRepository.GetLatestPost(5);
                        break;
                    case "Most Viewed Posts":
                        posts = _postRepository.GetMostViewedPost(5);
                        break;
                    case "Most Interesting Posts":
                        posts = _postRepository.GetHighestPosts(5);
                        break;
                    case "Published Posts":
                        posts = _postRepository.GetPublisedPosts();
                        break;
                    case "Un-published Posts":
                        posts = _postRepository.GetUnpublisedPosts();
                        break;
                    default:
                        posts = await _postRepository.GetAllAsync();
                        break;
                }
            }
            else
            {
                posts = await _postRepository.GetAllAsync();
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
                post.TagValues,
                post.CategoryId
            ));

            return View(data);
        }


        public async Task<IActionResult> Create()
        {
            var categories = await _categoryRepositories.GetAllAsync();

            var categorySelectList = categories.Select(cate => new SelectListItem
            {
                Value = cate.Id.ToString(),
                Text = cate.Name
            }).ToList();

            ViewBag.CategoryList = categorySelectList;

            return View(new PostAddModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PostAddModel model)
        {
            if (ModelState.IsValid)
            {
                var post = new Post
                {
                    Title = model.Title,
                    ShortDescription = model.ShortDescription,
                    PostContent = model.PostContent,
                    Meta = model.Meta,
                    UrlSlug = model.UrlSlug,
                    Published = model.Published,
                    PostedOn = model.PostedOn,
                    Modified = model.Modified,
                    ViewCount = model.ViewCount,
                    RateCount = model.RateCount,
                    TotalRate = model.TotalRate,
                    TagValues = model.TagValues,
                    CategoryId = model.CategoryId
                };

                await _postRepository.AddAsync(post);

                return RedirectToAction("Index", "Post");
            }

            // If ModelState is invalid, repopulate the category list
            var categories = await _categoryRepositories.GetAllAsync();
            var categorySelectList = categories.Select(cate => new SelectListItem
            {
                Value = cate.Id.ToString(),
                Text = cate.Name
            }).ToList();

            ViewBag.CategoryList = categorySelectList;

            return View(model);
        }

        public async Task<ActionResult> Details(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var post = await _postRepository.FindIdAsync(id);
            if (post == null)
            {
                return NotFound();
            }
            var data = new PostViewModel(
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
               post.CategoryId
               );
            return View(data);
        }

        // Only allow Blog Owners to delete
        [Authorize(Policy = "RequireBlogOwnerRole")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var post = await _postRepository.FindIdAsync(id);

            if (post == null)
            {
                return NotFound();
            }
            var data = new PostViewModel(
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
                post.CategoryId
                );
            return View(data);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "RequireBlogOwnerRole")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _postRepository.DeleteAsync(id);
            return RedirectToAction("Index");
        }

        // GET: Posts/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }

            var post = await _postRepository.FindIdAsync(id);
            if (post == null)
            {
                return NotFound();
            }

            var categories = await _categoryRepositories.GetAllAsync();
            ViewBag.CategoryList = categories.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Name
            }).ToList();

            var data = new PostAddModel
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
                post.CategoryId
            );

            return View(data);
        }

        // POST: Posts/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(PostAddModel model)
        {
            if (ModelState.IsValid)
            {
                var data = await _postRepository.FindIdAsync(model.Id);
                if (data == null)
                {
                    return NotFound();
                }

                // Update post properties
                data.Title = model.Title;
                data.ShortDescription = model.ShortDescription;
                data.PostContent = model.PostContent;
                data.Meta = model.Meta;
                data.UrlSlug = model.UrlSlug;
                data.Published = model.Published;
                data.PostedOn = model.PostedOn;
                data.Modified = model.Modified;
                data.ViewCount = model.ViewCount;
                data.RateCount = model.RateCount;
                data.TotalRate = model.TotalRate;
                data.TagValues = model.TagValues;
                data.CategoryId = model.CategoryId;

                await _postRepository.UpdateAsync(data);
                return RedirectToAction("Index");
            }

            // Repopulate the ViewBag in case of an error
            var categories = await _categoryRepositories.GetAllAsync();
            ViewBag.CategoryList = categories.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Name
            }).ToList();

            return View(model);
        }
    }
}
