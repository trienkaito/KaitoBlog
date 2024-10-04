using FA.JustBlog.Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FA.JustBlog.Core
{
    public interface ICommentRepository : IRepository<Comment>
    {
            /// <summary>
            /// 
            /// </summary>
            /// <param name="postId"></param>
            /// <param name="commentName"></param>
            /// <param name="commentEmail"></param>
            /// <param name="commentTitle"></param>
            /// <param name="commentBody"></param>
			void AddComment(int postId, string commentName, string commentEmail, string commentTitle, string commentBody);
            /// <summary>
            /// 
            /// </summary>
            /// <param name="postId"></param>
            /// <returns></returns>
            IEnumerable<Comment> GetCommentsForPost(int postId);
            /// <summary>
            /// 
            /// </summary>
            /// <param name="post"></param>
            /// <returns></returns>
            IEnumerable<Comment> GetCommentsForPost(Post post);

    }
}
