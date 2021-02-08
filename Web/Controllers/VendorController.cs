using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Common.ViewModels;
using Data.Repositories.Interfaces;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace Web.Controllers
{
    public class VendorController : Controller
    {
        private readonly IVendorRepository _vendorRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public VendorController(IVendorRepository vendorRepository, IWebHostEnvironment webHostEnvironment)
        {
            _vendorRepository = vendorRepository;
            _webHostEnvironment = webHostEnvironment;
        }
        public async Task<IActionResult> VendorView(int? Id = null)
        {
            if (Id != null)
            {
               await _vendorRepository.Delete(Id);
            }
            var vendors =await _vendorRepository.GetAll();
            return View(vendors);
        }
        [HttpGet]
        public IActionResult VendorAdd()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> VendorAdd(VendorAddViewModel model, IFormFile fileImage)
        {
            if (ModelState.IsValid)
            {
                if (fileImage != null)
                {
                    model.Image = fileImage.FileName;
                    await _vendorRepository.Add(model);
                    var folderPath = _webHostEnvironment.WebRootPath + "/images/VendorImg";
                    if (!Directory.Exists(folderPath))
                    {
                        Directory.CreateDirectory(folderPath);
                    }
                    var savePath = _webHostEnvironment.WebRootPath + $"/images/VendorImg/{fileImage.FileName}";
                    using (var stream = new FileStream(savePath, FileMode.Create))
                    {
                        fileImage.CopyTo(stream);
                    }
                }
            }
            return View(model);
        }
    }
}