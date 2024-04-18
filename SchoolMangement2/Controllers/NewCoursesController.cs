using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SchoolMangement2.Models;

namespace SchoolMangement2.Controllers
{
    [Authorize]
    public class NewCoursesController : Controller
    {
        private readonly EnrollContext _context;

        public NewCoursesController(EnrollContext context)
        {
            _context = context;
        }
       
        // GET: NewCourses
        public async Task<IActionResult> Index(string SerchStr)
            {


                var enrollContext = _context.NewCourses.Include(n => n.Course).Include(n => n.Student).AsQueryable();
            
                if (!string.IsNullOrEmpty(SerchStr))
                {
                    SerchStr = SerchStr.ToLower();
                    enrollContext = enrollContext.Where(c => c.CourseName.ToLower().Contains(SerchStr));
                }
   

            return View(await enrollContext.ToListAsync());
        }

        // GET: NewCourses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var newCourse = await _context.NewCourses
                .Include(n => n.Course)
                .Include(n => n.Student)
                .FirstOrDefaultAsync(m => m.CourseId == id);
            if (newCourse == null)
            {
                return NotFound();
            }

            return View(newCourse);
        }

        // GET: NewCourses/Create
        [Authorize]
        public IActionResult Create()
        {
            ViewData["CourseId"] = new SelectList(_context.Courses, "CourseId", "CourseId");
            ViewData["StudentId"] = new SelectList(_context.Students, "StudentId", "StudentId");
            ViewData["CourseName"] = new SelectList(_context.Courses, "CourseName", "CourseName");

            return View();
        }

        // POST: NewCourses/Create
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CourseId,CourseName,StudentId")] NewCourse newCourse)
        {
            if (ModelState.IsValid)
            {
                var course = await _context.Courses.FirstOrDefaultAsync(c => c.CourseId == newCourse.CourseId && c.CourseName == newCourse.CourseName);
                if (course != null)
                {


                    _context.Add(newCourse);

                    await _context.SaveChangesAsync();



                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    // Handle case where CourseId and CourseName do not match
                    ModelState.AddModelError(string.Empty, "CourseId and CourseName do not match any existing course.");
                }
            }
            ViewData["CourseId"] = new SelectList(_context.Courses, "CourseId", "CourseId", newCourse.CourseId);
            ViewData["StudentId"] = new SelectList(_context.Students, "StudentId", "StudentId", newCourse.StudentId);
            ViewData["CourseName"] = new SelectList(_context.Courses, "CourseName", "CourseName",newCourse.CourseName);

            return View(newCourse);
        }

        // GET: NewCourses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var newCourse = await _context.NewCourses.FindAsync(id);
            if (newCourse == null)
            {
                return NotFound();
            }
            ViewData["CourseId"] = new SelectList(_context.Courses, "CourseId", "CourseId", newCourse.CourseId);
            ViewData["StudentId"] = new SelectList(_context.Students, "StudentId", "StudentId", newCourse.StudentId);
            return View(newCourse);
        }

        // POST: NewCourses/Edit/5
       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CourseId,CourseName,StudentId")] NewCourse newCourse)
        {
            if (id != newCourse.CourseId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(newCourse);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NewCourseExists(newCourse.CourseId))
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
            ViewData["CourseId"] = new SelectList(_context.Courses, "CourseId", "CourseId", newCourse.CourseId);
            ViewData["StudentId"] = new SelectList(_context.Students, "StudentId", "StudentId", newCourse.StudentId);
            return View(newCourse);
        }

        // GET: NewCourses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var newCourse = await _context.NewCourses
                .Include(n => n.Course)
                .Include(n => n.Student)
                .FirstOrDefaultAsync(m => m.CourseId == id);
            if (newCourse == null)
            {
                return NotFound();
            }

            return View(newCourse);
        }

        // POST: NewCourses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var newCourse = await _context.NewCourses.FindAsync(id);
            if (newCourse != null)
            {
                _context.NewCourses.Remove(newCourse);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NewCourseExists(int id)
        {
            return _context.NewCourses.Any(e => e.CourseId == id);
        }
    }
}
