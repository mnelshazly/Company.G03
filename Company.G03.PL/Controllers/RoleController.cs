﻿using Company.G03.DAL.Models;
using Company.G03.PL.Dtos;
using Company.G03.PL.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Company.G03.PL.Controllers
{
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public RoleController(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string? SearchInput)
        {
            IEnumerable<RoleToReturnDto> roles;

            if (string.IsNullOrEmpty(SearchInput))
            {
                roles = _roleManager.Roles.Select(R => new RoleToReturnDto()
                {
                    Id = R.Id,
                    Name = R.Name,

                });
            }
            else
            {
                roles = _roleManager.Roles.Select(R => new RoleToReturnDto()
                {
                    Id = R.Id,
                    Name = R.Name,

                }).Where(R => R.Name.ToLower().Contains(SearchInput.ToLower()));
            }

            return View(roles);
        }


        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(RoleToReturnDto model)
        {

            if (ModelState.IsValid)
            {
                var role = await _roleManager.FindByNameAsync(model.Name);

                if(role is null)
                {
                    role = new IdentityRole()
                    {
                        Name = model.Name,
                    };
                    var result = await _roleManager.CreateAsync(role);
                    if(result.Succeeded)
                    {
                        return RedirectToAction("Index");
                    }
                }
            }

            return View(model);
        }


        [HttpGet]
        public async Task<IActionResult> Details(string? id, string viewName = "Details")
        {
            if (id is null) return BadRequest("Invalid Id"); //400

            var role = await _roleManager.FindByIdAsync(id);

            if (role is null) return NotFound(new { StatusCode = 404, message = $"Role with Id: {id} was not found" });

            var dto = new RoleToReturnDto()
            {
                Id = role.Id,
                Name = role.Name

            };

            return View(viewName, dto);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string? id)
        {
            return await Details(id, "Edit");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute] string id, RoleToReturnDto model)
        {
            if (ModelState.IsValid)
            {
                if (id != model.Id) return BadRequest("Invalid Id");

                var role = await _roleManager.FindByIdAsync(id);

                if (role is null) return NotFound(new { StatusCode = 404, message = $"Role with Id: {id} was not found" });

                var roleResult = await _roleManager.FindByNameAsync(model.Name);

                if (roleResult is null)
                {
                    role.Name = model.Name;
                    
                    var result = await _roleManager.UpdateAsync(role);

                    if (result.Succeeded)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                }

                ModelState.AddModelError("", "Invalid Operations !!");
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> DeleteAsync(string? id)
        {
            return await Details(id, "Delete");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete([FromRoute] string id, RoleToReturnDto model)
        {
            if (ModelState.IsValid)
            {
                if (id != model.Id) return BadRequest("Invalid Id");

                var role = await _roleManager.FindByIdAsync(id);

                if (role is null) return NotFound(new { StatusCode = 404, message = $"Role with Id: {id} was not found" });

                var roleResult = await _roleManager.FindByNameAsync(model.Name);

                var result = await _roleManager.DeleteAsync(role);

                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(Index));
                }

                ModelState.AddModelError("", "Invalid Operations !!");
            }

            return View(model);
        }
    }
}
