using FA.JustBlog.Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FA.JustBlog.Core
{
    public class CommentRepository : Repository<Comment>, ICommentRepository
    {
        private new readonly JustBlogContext _context;

        public CommentRepository(JustBlogContext context) : base(context)
        {
            _context = context;
        }

        public void AddComment(int postId, string commentName, string commentEmail, string commentTitle, string commentBody)
        {
            var comment = new Comment
            {
                PostId = postId,
                Name = commentName,
                Email = commentEmail,
                CommentHeader = commentTitle,
                CommentText = commentBody,
                CommentTime = DateTime.UtcNow
            };

            _context.Comments.Add(comment);
            _context.SaveChanges();
        }

        public IEnumerable<Comment> GetCommentsForPost(int postId)
        {
            return _context.Comments.Where(c => c.PostId == postId).ToList();
        }

        public IEnumerable<Comment> GetCommentsForPost(Post post)
        {
            return _context.Comments.Where(c => c.PostId == post.Id).ToList();
        }
    }

}
