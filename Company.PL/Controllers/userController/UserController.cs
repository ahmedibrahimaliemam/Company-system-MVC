using Company.DL.Models;
using Company.PL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Company.PL.Controllers.userController
{
	public class UserController : Controller
	{
		private readonly UserManager<ApplicationModel> userManager;
		private readonly SignInManager<ApplicationModel> signInManager;

		public UserController(UserManager<ApplicationModel> userManager , SignInManager<ApplicationModel> signInManager)
        {
			this.userManager = userManager;
			this.signInManager = signInManager;
		}
     
		public async Task<IActionResult> Index(string Email)
		{
			if(string.IsNullOrEmpty(Email))
			{
				var users = await userManager.Users.Select(x => new UserViewModel
				{
				
					id = x.Id,
					Email = x.Email,
					FName = x.FName,
					LName = x.LName,
					PhoneNumber = x.PhoneNumber,
					Roles = userManager.GetRolesAsync(x).Result
				}).ToListAsync() ;
				return View(users);
			}
			else
			{
				var user=await userManager.FindByEmailAsync(Email);
				var MappedUser = new UserViewModel
				{
					id=user.Id,
					Email = user.Email,
					FName = user.FName,
					LName = user.LName,
					PhoneNumber = user.PhoneNumber,
					Roles=userManager.GetRolesAsync(user).Result
				};
				return View(new List<UserViewModel>() { MappedUser });
			}

		}
		public async Task<IActionResult> Details(string id , string ViewName="Details")
		{
			if(id is not null)
			{
				var user= await userManager.FindByIdAsync(id);
				if(user is null)
				{
					return NotFound();
				}
				var MappedUser = new UserViewModel
				{
					id = user.Id,
					Email = user.Email,
					FName = user.FName,
					LName = user.LName,
					PhoneNumber = user.PhoneNumber,

				};
				return View(ViewName,MappedUser);
			}
			return BadRequest();
		}
		public async Task< IActionResult> Delete(string id)
		{
			return await Details(id,"Delete");
		}
		public async Task<IActionResult> Edit(string id)
		{
			return await Details(id, "Edit");
		}
		[ValidateAntiForgeryToken]
		[HttpPost]
		public async Task<IActionResult> Delete([FromRoute]string id , UserViewModel model)
		{
			if (id == model.id)
			{
				if(model is not null)
				{
					var user= await userManager.FindByIdAsync(model.id);
			
					var result= await userManager.DeleteAsync(user);
					if (result.Succeeded)
					{
						return RedirectToAction(nameof(Index));
					}
					foreach (var err in result.Errors)
						ModelState.AddModelError(string.Empty, err.Description);
				}
				
			}
			return BadRequest();
			
		}
		[HttpPost,ValidateAntiForgeryToken]
		
		public async Task<IActionResult> Edit([FromRoute]string id , UserViewModel model)
		{
			if (id == model.id)
			{
				if(model is not null)
				{
					var user =await userManager.FindByIdAsync(model.id);
					user.PhoneNumber = model.PhoneNumber;
					user.FName = model.FName;
					user.LName = model.LName;
					var result= await userManager.UpdateAsync(user);
					if (result.Succeeded)
					{
						return RedirectToAction(nameof(Index));
					}
					foreach (var err in result.Errors)
						ModelState.AddModelError(string.Empty, err.Description);
				}
			}
			return NotFound();
		}
	}
}
