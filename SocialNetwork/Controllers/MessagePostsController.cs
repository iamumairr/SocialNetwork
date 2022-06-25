using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SocialNetwork.Data;
using SocialNetwork.Models;

namespace SocialNetwork.Controllers
{
    public class MessagePostsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MessagePostsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: MessagePosts
        public async Task<IActionResult> Index()
        {
            return View(await _context.Messages.Include(a => a.Comments).ToListAsync());
        }

        // GET: MessagePosts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Messages == null)
            {
                return NotFound();
            }

            var messagePost = await _context.Messages.Include(a => a.Comments)
                .FirstOrDefaultAsync(m => m.PostId == id);
            if (messagePost == null)
            {
                return NotFound();
            }

            return View(messagePost);
        }

        // GET: MessagePosts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MessagePosts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Message,PostId,UserName")] MessagePost messagePost)
        {
            if (ModelState.IsValid)
            {
                _context.Add(messagePost);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(messagePost);
        }

        // GET: MessagePosts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Messages == null)
            {
                return NotFound();
            }

            var messagePost = await _context.Messages.FindAsync(id);
            if (messagePost == null)
            {
                return NotFound();
            }
            return View(messagePost);
        }

        // POST: MessagePosts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Message,PostId,UserName")] MessagePost messagePost)
        {
            if (id != messagePost.PostId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(messagePost);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MessagePostExists(messagePost.PostId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(messagePost);
        }

        // GET: MessagePosts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Messages == null)
            {
                return NotFound();
            }

            var messagePost = await _context.Messages.FindAsync(id);
            if (messagePost != null)
            {
                _context.Messages.Remove(messagePost);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Like(int id)
        {
            var message = _context.Messages.Find(id);
            if (message == null)
            {
                return NotFound();
            }

            message.Like();

            try
            {
                _context.Update(message);
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MessagePostExists(message.PostId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToAction(nameof(Details), new { id = id });
        }

        public IActionResult Unlike(int id)
        {
            var message = _context.Messages.Find(id);
            if (message == null)
            {
                return NotFound();
            }

            message.Unlike();

            try
            {
                _context.Update(message);
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MessagePostExists(message.PostId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToAction(nameof(Details), new { id = id });
        }

        private bool MessagePostExists(int id)
        {
            return _context.Messages.Any(e => e.PostId == id);
        }
    }
}
