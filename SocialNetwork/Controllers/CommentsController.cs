using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SocialNetwork.Data;
using SocialNetwork.Models;

namespace SocialNetwork.Controllers
{
    public class CommentsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CommentsController(ApplicationDbContext context)
        {
            _context = context;
        }
        // GET: Comments/Create
        public IActionResult Create(int id)
        {
            Comment comment = new Comment();
            comment.PostID = id;

            return View(comment);
        }

        // POST: Comments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CommentID,PostID,Text")] Comment comment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(comment);
                await _context.SaveChangesAsync();

                //find the specific controller name - message or photo
                string controllerName = string.Empty;
                var message = _context.Messages.Find(comment.PostID);
                var photo = _context.Photos.Find(comment.PostID);
                if (message != null)
                {
                    controllerName = "MessagePosts";
                }
                if (photo != null)
                {
                    controllerName = "PhotoPosts";
                }
                return RedirectToAction("Details", controllerName, new { id = comment.PostID });
            }
            return View(comment);
        }
    }
}
