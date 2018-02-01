using AutoMapper;
using DomainLibrary.Meal;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using WebApi.Persistent.Meal;
using WebApi.Persistent.Meal.EntreePhotoRepo;
using WebApi.Persistent.Utility;
using WebApi.Resource.Meal.PhotoResource;

namespace WebApi.Controllers.ApiController.Meal
{
    [EnableCors("SiteCorsPolicy")]
    [Route("/api/entreePhoto/{entreeId}/photos")]
    public class EntreePhotoController : Controller
    {
        private readonly IHostingEnvironment _host;
        private readonly IUnitOfWork _uow;
        private readonly IEntreeRepository _entreeRepository;
        private readonly PhotoSettings _photoSettings;
        private readonly IEntreePhotoRepository _entreePhotoRepository;
        private readonly IMapper _mapper;

        public EntreePhotoController(
            IHostingEnvironment host,
            IMapper mapper,
            IEntreePhotoRepository entreePhotoRepository,
            IEntreeRepository entreeRepository,
            IOptionsSnapshot<PhotoSettings> options,
            IUnitOfWork uow)
        {
            this._host = host;
            this._entreePhotoRepository = entreePhotoRepository;
            this._entreeRepository = entreeRepository;
            this._photoSettings = options.Value;
            this._uow = uow;
            this._mapper = mapper;
        }

        //Upload one file use "IFormFile", upload multiple files use "IFormCollection"
        [HttpPost]
        public async Task<IActionResult> Upload(int entreeId, IFormFile file)
        {
            var entree = await _entreeRepository.GetEntree(entreeId, includeRelated: false);
            if (entree == null)
                return NotFound();

            if (file == null) return BadRequest("Null file");
            if (file.Length == 0) return BadRequest("Empty file");
            //if (file.Length > _photoSettings.MaxBytes) return BadRequest("Max file size exceed.");
            if (!_photoSettings.IsSupported(file.FileName)) return BadRequest("Invalid file type");

            // have to upload to web api wwwroot instead of client side asset because anything inside asset will only be included after webpack run
            // use staticfile in startup.cs
            var uploadsFolderPath = Path.Combine(_host.WebRootPath, "upload");
            // c:\wwwroot\..
            if (!Directory.Exists(uploadsFolderPath))
                Directory.CreateDirectory(uploadsFolderPath);

            //always generate new file name for security issue
            //because hacker may modify the request by changing the file name from "file.png" to "../../../windows/system/asd.dll"
            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            var filePath = Path.Combine(uploadsFolderPath, fileName);
            var entreePhoto = new EntreePhoto { FileName = fileName };

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);

                //using (var memoryStream = new MemoryStream())
                //{
                //    await file.CopyToAsync(memoryStream);
                //    entreePhoto.FileBlob = memoryStream.ToArray();
                //}
            }


            entree.EntreePhotos.Add(entreePhoto);
            await _uow.CompleteAsync();

            return Ok(entreePhoto);
        }

        [HttpGet]
        public async Task<IEnumerable<EntreePhotoResource>> GetPhotos(int entreeId)
        {
            var photos = await _entreePhotoRepository.GetPhotos(entreeId);

            return _mapper.Map<IEnumerable<EntreePhoto>, IEnumerable<EntreePhotoResource>>(photos);
        }
    }
}