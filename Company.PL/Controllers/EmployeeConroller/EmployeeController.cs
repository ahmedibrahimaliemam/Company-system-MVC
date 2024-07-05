using AutoMapper;
using Comoany.Bl.Interfaces;
using Company.DL.Models;
using Company.PL.Helper;
using Company.PL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace Company.PL.Controllers.Emplopyee
{
    [Authorize]
    public class EmployeeController : Controller
    {   
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public EmployeeController( IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public async Task< IActionResult> Index(string searchName)
        {
            if(string.IsNullOrEmpty(searchName))
            {
                var emp = await unitOfWork.EmployeeRepo.GetAll();
                var MappedEmp = mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeViewModel>>(emp);
                return View(MappedEmp);

            }
            else
            {
                IEnumerable<Employee> emp= unitOfWork.EmployeeRepo.SearchByName(searchName);
                var mappedEmp=mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeViewModel>>(emp); 
                return View(mappedEmp);
            }
            //var emp = employeeRepo.GetAll();
            //var MappedEmp = mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeViewModel>>(emp);
            //return View(MappedEmp);
        }
        public async Task< IActionResult> Details(int? id , string viewName)
        { 
 
            if(id is null)
            {
                return BadRequest();
            }
            var emp = await unitOfWork.EmployeeRepo.GetById(id.Value);
            var MappedEmp=mapper.Map<Employee,EmployeeViewModel>(emp);
            return View(viewName , MappedEmp);

        }
        [HttpGet]
        public  IActionResult Create()
        {
            
            return View();

        }
        [HttpPost]
        public async Task<IActionResult> Create(EmployeeViewModel employeeVM)
        {   if(ModelState.IsValid)
            {
                try
                {
                    var MappedEmp=mapper.Map<EmployeeViewModel,Employee>(employeeVM);
                    MappedEmp.ImageName = Documents.UploadFile(employeeVM.Image, "Images");
                    await unitOfWork.EmployeeRepo.Add(MappedEmp);
                    var result = await unitOfWork.Complete();

                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }
           
            return View(employeeVM);
            
        }
        [HttpGet]
        public async Task< IActionResult> Edit(int id)
        {
          //ViewBag.department=departmentRepo.GetAll();
            return await Details(id, "Edit");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task< IActionResult> Edit([FromRoute]int id,EmployeeViewModel employeeVM)
        {
            if (id == employeeVM.ID)
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        var MappedEmp = mapper.Map<EmployeeViewModel, Employee>(employeeVM);
                         unitOfWork.EmployeeRepo.Update(MappedEmp);
                        var result = await unitOfWork.Complete();
                        return RedirectToAction(nameof(Index));
                    }
                    catch (Exception ex)
                    {
                        ModelState.AddModelError(string.Empty, ex.Message);
                    }
                }
            }
            else
                return BadRequest();
          
            return View(employeeVM);
        }
        [HttpGet]
        public async Task< IActionResult> Delete(int id)
        {
            return await Details(id, "Delete");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task< IActionResult> Delete([FromRoute]int id,EmployeeViewModel employeeVM)
        {
            if (id == employeeVM.ID)
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        var MappedEmp=mapper.Map<EmployeeViewModel, Employee>(employeeVM);
                        Documents.RemoveFile( MappedEmp.ImageName , "Images");
                        unitOfWork.EmployeeRepo.Delete(MappedEmp);
                        var result=await unitOfWork.Complete();
                        return RedirectToAction(nameof(Index));
                    }
                    catch (Exception ex)
                    {
                        ModelState.AddModelError(string.Empty, ex.Message);
                    }
                }
            }
            else
                return BadRequest();
            return View(employeeVM);
        }
      
        
    }
}
