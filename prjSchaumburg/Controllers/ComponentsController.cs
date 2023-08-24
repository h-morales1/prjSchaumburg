using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using prjSchaumburg.Data;
using prjSchaumburg.Models;

namespace prjSchaumburg.Controllers
{
    public class ComponentsController : Controller
    {

        private readonly MVCDbContext mVCDbContext; // handle db transactions

        public ComponentsController(MVCDbContext mVCDbContext)
        {
            this.mVCDbContext = mVCDbContext;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            // function gets list of all machines and all components from respective tables
            var comps = new ViewComponentsModel() { 
                Machines = await mVCDbContext.Machines.ToListAsync(),
                Components = await mVCDbContext.Components.ToListAsync()
            };

            return View(comps);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(Guid id)
        {
            // this is just reloading the page to include the machine ID from the selected machine

            var machine = await mVCDbContext.Machines.FirstOrDefaultAsync(x => x.Id == id); // search table for machine with id param
            

            if (machine != null)
            {
                //machine is found in machine db
                var viewModel = new AddComponentViewModel()
                {
                    machineID = machine.Id
                };
                return await Task.Run(() => View("Add", viewModel)); // reload page and fill in machine id

            }
            
            
            
            
            return RedirectToAction("Add");
        }

        [HttpPost]
        public async Task<IActionResult> AddSubmit(AddComponentViewModel componentViewModel)
        {
            // handle actually saving component from form in AddView

            var newComponent = new Models.Domain.Component
            {
                Id = Guid.NewGuid(),
                machineID = componentViewModel.machineID,
                Name = componentViewModel.Name,
                serialNum = componentViewModel.serialNum,
                price = componentViewModel.price,
                quantity = componentViewModel.quantity
            }; // component obj that holds data taken from form

            // commit changes to table
            await mVCDbContext.Components.AddAsync(newComponent);
            await mVCDbContext.SaveChangesAsync();

            return RedirectToAction("Index");

        }

        [HttpGet]
        public async Task<IActionResult> View(Guid compId)
        {
            // handle viewing specific component
            var component = await mVCDbContext.Components.FirstOrDefaultAsync(x => x.Id == compId); // search table for compId

            if (component != null)
            {
                // compId matched a componentId in table
                var viewModel = new UpdateComponentViewModel()
                {
                    Id = component.Id,
                    machineID = component.machineID,
                    Name = component.Name,
                    serialNum = component.serialNum,
                    price = component.price,
                    quantity = component.quantity

                };

                return await Task.Run(() => View("View", viewModel)); // fill in form with data
            }

            return RedirectToAction("Index");
        }
    }
}
