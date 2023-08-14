using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using prjSchaumburg.Data;
using prjSchaumburg.Models;
using prjSchaumburg.Models.Domain;
using System.Reflection.PortableExecutable;

namespace prjSchaumburg.Controllers
{
    public class MachinesController : Controller
    {
        private readonly MVCDbContext mVCDbContext; // be able to handle db transactions

        public MachinesController(MVCDbContext mVCDbContext)
        {
            this.mVCDbContext = mVCDbContext;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var machines = await mVCDbContext.Machines.ToListAsync(); // get all machines stored in table
            return View(machines);
        }

        [HttpGet]
        public async Task<IActionResult> View(Guid id) // handle viewing a machine
        {
            var machine = await mVCDbContext.Machines.FirstOrDefaultAsync(x => x.Id == id);

            if(machine != null) {
                var viewModel = new UpdateMachineViewModel()
                {
                    Id = machine.Id,
                    Make = machine.Make,
                    Model = machine.Model,
                    Year = machine.Year
                };
                return View(viewModel);
            }
 

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }


        // handle getting info from the form
        [HttpPost]
        public async Task<IActionResult> Add(AddMachineViewModel addMachineRequest)
        {
            var machine = new Models.Domain.Machine() // obj to hold data passed in from form
            {
                Id = Guid.NewGuid(),
                Make = addMachineRequest.Make,
                Model = addMachineRequest.Model,
                Year = addMachineRequest.Year
            };

            await mVCDbContext.Machines.AddAsync(machine); // add obj to table
            await mVCDbContext.SaveChangesAsync(); // commit changes to table
            return RedirectToAction("Add"); // redirect or reload page 
        }
    }
}
