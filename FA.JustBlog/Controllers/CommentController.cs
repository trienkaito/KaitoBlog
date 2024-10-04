using FA.JustBlog.Core.Models;
using FA.JustBlog.Core;
using FA.JustBlog.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.HttpResults;

namespace FA.JustBlog.Controllers
{
    public class CommentController : Controller
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;

        public CommentController(ICommentRepository commentRepository, IConfiguration configuration)
        {
            _commentRepository = commentRepository;
            _configuration = configuration;
            _httpClient = new HttpClient();
        }
        [HttpPost]
        public async Task<IActionResult> AddComment(CommentViewModel model)
        {
            if (ModelState.IsValid)
            {
                var comment = new Comment
                {
                    Name = model.Name,
                    Email = model.Email,
                    PostId = model.PostId,
                    CommentHeader = model.CommentHeader,
                    CommentText = model.CommentText,
                    CommentTime = DateTime.Now
                };

                await _commentRepository.AddAsync(comment); 

                return RedirectToAction("Index", "Post", new { id = model.PostId });
            }

            return RedirectToAction("Index", "Post", new { id = model.PostId });
        }


    }
}
