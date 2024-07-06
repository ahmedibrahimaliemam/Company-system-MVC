using Company.DL.Models;
using Company.PL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Company.PL.Controllers.RoleController
{
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> role;

        public RoleController(RoleManager<IdentityRole> role)
        {
            this.role = role;
        }
      
        public async Task< IActionResult> Index(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                var Roles = await role.Roles.Select(x => new RoleViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                }).ToListAsync();
                return View(Roles);
            }
            var Role = await role.FindByNameAsync(name);
            var mappedRole = new RoleViewModel
            {
                Id = Role.Id,
                Name = Role.Name,
            };
            return View(new List<RoleViewModel> { mappedRole});
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(RoleViewModel roleVM)
        {
            if (ModelState.IsValid)
            {
                var MappedRole = new IdentityRole
                {
                    Name = roleVM.Name,
                    Id = roleVM.Id
                };
                var result = await role.CreateAsync(MappedRole);
                if(result.Succeeded)
                {
                    return RedirectToAction(nameof(Index));
                }
                foreach(var err in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, err.Description);
                }
            }
            return View(roleVM);
        }
        public async Task< IActionResult >Details(string id , string ViewName="Details")
        {
            if(string.IsNullOrEmpty(id))
            {
                return View("Error");
            }
            var Role=await role.FindByIdAsync(id);
            if(Role == null)
                return NotFound();
            var mappedRole = new RoleViewModel
            {
                Id =  Role.Id,
                Name=Role.Name,
                
            };
            return View(mappedRole);

        }
        public async Task<IActionResult> Delete(string id)
        {
            return await Details(id, "Delete");
        }
        [HttpPost]
        public async Task<IActionResult> Delete([FromRoute] string id,RoleViewModel roleVM)
        {
            if (id == roleVM.Id)
            {
                var Role=await role.FindByIdAsync(id);
                if(Role is not null)
                {
                    var result=await role.DeleteAsync(Role);
                    if(result.Succeeded)
                       return RedirectToAction(nameof(Index));
                    foreach(var err in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, err.Description);
                    }
                }
                return NotFound();
            }
            return BadRequest();
        }
        public async Task<IActionResult> Edit(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest();
            }
            var Role= await role.FindByIdAsync(id);
            var MappedRole = new RoleViewModel
            {
                Id = Role.Id,
                Name = Role.Name,
            };
            return View(MappedRole);

        }
        [HttpPost]
        public async Task<IActionResult> Edit([FromRoute] string id, RoleViewModel roleVM)
        {
            if (id == roleVM.Id)
            {
                var Role = await role.FindByIdAsync(id);
                if (Role is not null)
                {
                    Role.Name = roleVM.Name;
                    var result=await role.UpdateAsync(Role);
                    if (result.Succeeded)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                    foreach (var err in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, err.Description);
                    }
                }
                return NotFound();

            }
            return BadRequest();
        }
    }
}
