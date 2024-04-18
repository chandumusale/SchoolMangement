using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SchoolMangement2.Models;
using SchoolMangement2.ViewModel;

namespace SchoolMangement2.Controllers
{
    [Authorize]
    public class EmployeesController : Controller
    {
        private readonly RelContext _context;

        public EmployeesController(RelContext context)
        {
            _context = context;
        }
        
        public IActionResult MasterView()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        /*  public async Task<IActionResult> MasterView(EmployeeView model)
          {
              if (ModelState.IsValid)
              {
                  Employee employee = new Employee
                  {
                      FirstName = model.FirstName,
                      LastName = model.LastName,
                      Gender = model.Gender,
                      Salary = model.Salary,
                      Email = model.Email,
                      AddressList = new List<Address> // Initializing AddressList
              {
                  new Address
                  {
                      AddressLine = model.AddressLine,
                      City = model.City,
                      State = model.State,
                      PinCode = model.PinCode,
                  }
              }
                  };

                  _context.Add(employee);
                  await _context.SaveChangesAsync();

                  return RedirectToAction("Index");
              }

              return View(model);
          }
          */
        public async Task<IActionResult> MasterView(EmployeeView model)
        {
            if (ModelState.IsValid)
            {
                Employee employee = new Employee
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Salary = model.Salary,
                    Gender =model.Gender,
                    Email = model.Email,
                    AddressList = new List<Address> 
            {
                new Address
                {
                    AddressLine = model.AddressLine,
                    City = model.City,
                    State = model.State,
                    PinCode = model.PinCode
                }
            }
                };

                _context.Add(employee);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Form Submitted Sucessfully";

                return RedirectToAction(nameof(Success));
            }
            return View(model);
        }














        public async Task<IActionResult> Index(int minSalary, string gender)
        {
            IQueryable<Employee> employees = _context.Employees.Include(e => e.AddressList);

            employees = employees.Where(e => e.Salary >= minSalary);

     
            if (!string.IsNullOrEmpty(gender))
            {
                employees = employees.Where(e => e.Gender == gender);
            }

            
            employees = employees.OrderBy(e => e.Salary);
            // above all operations on query diffrent diff condition

            var employeeList = await employees.ToListAsync(); //here actual query excutes
            

            return View(employeeList);
        }

        /*        public async Task<IActionResult> Index1(EmployeeViewModel model)
        {
            if (ModelState.IsValid)
            {
                Employee employee = new Employee
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Salary = model.Salary,
                    Email = model.Email,
                   
                    Address = new List<Address>
            {
                new Address
                {
                    AddressLine = model.AddressLine,
                    City = model.City,
                    State = model.State,
                    PinCode = model.PinCode
                }
            }
                };

                _context.Add(employee);
                await _context.SaveChangesAsync();

                
                return RedirectToAction("Index");
            }

            model.CityList = GetCityList();
            model.StateList = GetStateList();

            return View(model);
        }
*/



        // GET: Employees/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees
                .FirstOrDefaultAsync(m => m.EmployeeId == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // GET: Employees/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Employees/Create
      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EmployeeId,FirstName,LastName,Gender,Salary,Email")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                _context.Add(employee);
               
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Form Submitted Sucessfully";
                return RedirectToAction(nameof(Success));
            }
            return View(employee);
        }
        //get method
        public IActionResult Success()
        {
            return View();
        }

        // GET: Employees/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }

        // POST: Employees/Edit/5
       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EmployeeId,FirstName,LastName,Gender,Salary,Email")] Employee employee)
        {
            if (id != employee.EmployeeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(employee);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeExists(employee.EmployeeId))
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
            return View(employee);
        }

        // GET: Employees/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees
                .FirstOrDefaultAsync(m => m.EmployeeId == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee != null)
            {
                _context.Employees.Remove(employee);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeeExists(int id)
        {
            return _context.Employees.Any(e => e.EmployeeId == id);
        }
    }
}
