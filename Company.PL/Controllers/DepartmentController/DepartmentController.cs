using AutoMapper;
using Comoany.Bl.Interfaces;
using Company.DL.Models;
using Company.PL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Company.PL.Controllers.DepartmentController
{
    [Authorize]
    public class DepartmentController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        public DepartmentController(IUnitOfWork UnitOfWork,IMapper mapper)
        {
            unitOfWork = UnitOfWork;
            this.mapper=mapper;
            
        }
        public async Task<IActionResult> Index()
        {
            var department= await unitOfWork.DepartmentRepo.GetAll();
            var MappedDept = mapper.Map<IEnumerable<Department>,IEnumerable<DepartmentViewModel>>(department);
            return View(MappedDept);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();

        }
        [HttpPost]
        public async Task<IActionResult> Create(DepartmentViewModel deptVM)
        {
            if(ModelState.IsValid)
            {
                var MappedDept=mapper.Map<DepartmentViewModel,Department>(deptVM); 
                
               await unitOfWork.DepartmentRepo.Add(MappedDept);
                int result = await unitOfWork.Complete();
                if (result > 0)
                    TempData["message"] = "The Department Added Successfully!";
                return RedirectToAction(nameof(Index));
            }
            return View(deptVM);

        }
        public async Task<IActionResult> Details(int? Id , string ViewName="Details")
        {
            if (Id is null)
                return BadRequest();
            var department= await unitOfWork.DepartmentRepo.GetById(Id.Value);
            var MappedDept=mapper.Map<Department,DepartmentViewModel>(department);
            if(department is null)
                return NotFound();
            return View(ViewName,MappedDept);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            return await Details(id, "Edit");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute]int id , DepartmentViewModel departmentVM)
        {
            if (id != departmentVM.id)
                return BadRequest();
            if (!ModelState.IsValid)
                return BadRequest();
            else
            {
                try
                {
                    var MappedDept=mapper.Map<DepartmentViewModel,Department>(departmentVM);
                    unitOfWork.DepartmentRepo.Update(MappedDept);
                    int result = await unitOfWork.Complete();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty , ex.Message); 
                }
            }
            return View(departmentVM);
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            return await Details(id, "Delete");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete([FromRoute]int id,DepartmentViewModel departmentVM)
        {
            if(ModelState.IsValid && id==departmentVM.id)
            {
                
                try
                {
                    var MappedDept=mapper.Map<DepartmentViewModel,Department>(departmentVM);
                    unitOfWork.DepartmentRepo.Delete(MappedDept);
                    int result =await unitOfWork.Complete();
                    return RedirectToAction(nameof(Index));
                }
                catch(Exception ex)
                {
                    ModelState.AddModelError (string.Empty , ex.Message);
                }
          
               
            }
            else 
                return BadRequest();
            return View(departmentVM);
            
        }
    }
}
