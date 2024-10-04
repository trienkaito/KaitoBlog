using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FA.JustBlog.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace FA.JustBlog.Core.Repositories
{

    public class TagRepository : Repository<Tag>, ITagRepository
    {
        private new readonly JustBlogContext _context;
        public TagRepository(JustBlogContext context) : base(context)
        {
            _context = context;
        }

        public Tag? GetTagByUrlSlug(string urlSlug)
        {
            if(urlSlug != null)
            {
                return _context.Tags.First(t => t.UrlSlug.Equals(urlSlug));
            }
            return null;
        }

        public  IEnumerable<Tag> GetTop10Tags()
        {
            return _context.Tags.OrderByDescending(t => t.Id)
                                     .Take(10)
                                     .ToList();
        }
    }
}
