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
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(Guid id)
        {
            // this is just reloading the page to include the machine ID from the selected machine
            var machine = await mVCDbContext.Machines.FirstOrDefaultAsync(x => x.Id == id);
            

            if (machine != null)
            {
                //machine is found in machine db
                var viewModel = new AddComponentViewModel()
                {
                    machineID = machine.Id
                };
                return await Task.Run(() => View("Add", viewModel));

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
            };

            await mVCDbContext.Components.AddAsync(newComponent);
            await mVCDbContext.SaveChangesAsync();

            return RedirectToAction("Index");

        }
    }
}
