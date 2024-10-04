using FA.JustBlog.Core.Repositories;
using FA.JustBlog.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace FA.JustBlog.Components
{
    // LatestPostsViewComponent.cs
    [ViewComponent(Name = "ListPost")]
    public class ListPostsViewComponent : ViewComponent
    {
        private readonly IPostRepository _postRepository;

        public ListPostsViewComponent(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        // Ensure there is exactly one public Invoke method
        public IViewComponentResult Invoke(string type)
        {
            IEnumerable<PostViewModel> data = null;

            if (type == "MostViewedPosts")
            {
                var mostViewedPosts = _postRepository.GetMostViewedPost(5);
                data = mostViewedPosts.Select(post =>
                    new PostViewModel
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
                    )
                );
            }
            else if (type == "LatestPosts")
            {
                var latestPosts = _postRepository.GetLatestPost(5);
                data = latestPosts.Select(post =>
                    new PostViewModel
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
                    )
                );
            }

            return View(data);
        }
    }
}
