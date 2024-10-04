﻿using FA.JustBlog.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FA.JustBlog.Core.Repositories
{
    public interface ITagRepository : IRepository<Tag>
    {
        Tag? GetTagByUrlSlug(string urlSlug);
       IEnumerable<Tag> GetTop10Tags();
    }
}
