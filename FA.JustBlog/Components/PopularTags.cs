using FA.JustBlog.Core.Repositories;
using FA.JustBlog.Models;
using Microsoft.AspNetCore.Mvc;

namespace FA.JustBlog.Components
{
    public class PopularTags : ViewComponent
    {
        private readonly ITagRepository _tagRepository;
        public PopularTags(ITagRepository tagRepository)
        {
            _tagRepository = tagRepository;
        }
        public IViewComponentResult Invoke()
        {
            var popularTags = _tagRepository.GetTop10Tags();

            var data = popularTags.Select(Tag => {
                return new TagViewModel(
                Tag.Name
                );
            }
            );         

            return View(data);
        }
    }
}
