using FruitCart_API.Data;
using FruitCart_API.Models;
using FruitCart_API.Models.Dto;
using FruitCart_API.Services;
using FruitCart_API.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace FruitCart_API.Controllers
{
    [Route("api/MenuItem")]
    [Authorize(Roles = SD.Role_Admin)]
    [ApiController]
    public class MenuItemController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly ApiResponse _response;
        private readonly IWebHostEnvironment _env;
        //private readonly ICloudImages _cloudImages;

        public MenuItemController(ApplicationDbContext db, IWebHostEnvironment env)
        {
            _db = db;
            _response = new ApiResponse();
            _env = env;
            //_cloudImages = cloudImages;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult GetMenuItems()
        {
            List<MenuItem> menuItems = _db.MenuItems.ToList();

            List<OrderDetail> orderDetailsWithRatings = _db.OrderDetails.Where(u => u.Rating != null).ToList();

            foreach (var menuItem in menuItems)
            {
                var ratings = orderDetailsWithRatings.Where(u => u.MenuItemId == menuItem.Id).Select(u => u.Rating.Value);
                double avgRating = ratings.Any() ? ratings.Average() : 0;
                menuItem.Rating = avgRating;
            }
            _response.Result = menuItems;
            _response.StatusCode = HttpStatusCode.OK;
            return Ok(_response);
        }

        [HttpGet("{id:int}", Name = "GetMenuItem")]
        [AllowAnonymous]
        public IActionResult GetMenuItem(int id)
        {
            if (id == 0)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                return BadRequest(_response);
            }
            MenuItem? menuItem = _db.MenuItems.FirstOrDefault(u => u.Id == id);
            List<OrderDetail> orderDetailsWithRatings = _db.OrderDetails.Where(u => u.Rating != null && u.MenuItemId == menuItem.Id).ToList();


            var ratings = orderDetailsWithRatings.Select(u => u.Rating.Value);
            double avgRating = ratings.Any() ? ratings.Average() : 0;
            menuItem.Rating = avgRating;
            _response.Result = menuItem;
            _response.StatusCode = HttpStatusCode.OK;
            return Ok(_response);
        }


        [HttpPost]
        public async Task<ActionResult<ApiResponse>> CreateMenuItem([FromForm] MenuItemCreateDTO menuItemCreateDTO)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (menuItemCreateDTO.File == null || menuItemCreateDTO.File.Length == 0)
                    {
                        _response.IsSuccess = false;
                        _response.StatusCode = HttpStatusCode.BadRequest;
                        _response.ErrorMessages = ["File is required"];
                        return BadRequest(_response);
                    }

                    var imagesPath = Path.Combine(_env.WebRootPath, "images");
                    if (!Directory.Exists(imagesPath))
                    {
                        Directory.CreateDirectory(imagesPath);
                    }
                    var filePath = Path.Combine(imagesPath, menuItemCreateDTO.File.FileName);
                    if (System.IO.File.Exists(filePath))
                    {
                        System.IO.File.Delete(filePath);
                    }
                    //uploading the image
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await menuItemCreateDTO.File.CopyToAsync(stream);
                    }
                    //var imageUrl = await _cloudImages.UploadAsync(menuItemCreateDTO.File);


                    MenuItem menuItem = new()
                    {
                        Name = menuItemCreateDTO.Name,
                        Description = menuItemCreateDTO.Description,
                        Price = menuItemCreateDTO.Price,
                        Category = menuItemCreateDTO.Category,
                        SpecialTag = menuItemCreateDTO.SpecialTag,
                        //Image = imageUrl
                        Image = "images/" + menuItemCreateDTO.File.FileName
                    };

                    _db.MenuItems.Add(menuItem);
                    await _db.SaveChangesAsync();

                    _response.Result = menuItem;
                    _response.StatusCode = HttpStatusCode.Created;
                    return CreatedAtRoute("GetMenuItem", new { id = menuItem.Id }, _response);

                }
                else
                {
                    _response.IsSuccess = false;
                }
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages
                     = [ex.ToString()];
            }

            return BadRequest(_response);
        }

        [HttpPut]
        public async Task<ActionResult<ApiResponse>> UpdateMenuItem(int id, [FromForm] MenuItemUpdateDTO menuItemUpdateDTO)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (menuItemUpdateDTO == null || menuItemUpdateDTO.Id != id)
                    {
                        _response.IsSuccess = false;
                        _response.StatusCode = HttpStatusCode.BadRequest;
                        return BadRequest(_response);
                    }

                    MenuItem? menuItemFromDb = await _db.MenuItems.FirstOrDefaultAsync(u => u.Id == id);

                    if (menuItemFromDb == null)
                    {
                        _response.IsSuccess = false;
                        _response.StatusCode = HttpStatusCode.NotFound;
                        return NotFound(_response);
                    }

                    menuItemFromDb.Name = menuItemUpdateDTO.Name;
                    menuItemFromDb.Description = menuItemUpdateDTO.Description;
                    menuItemFromDb.Price = menuItemUpdateDTO.Price;
                    menuItemFromDb.Category = menuItemUpdateDTO.Category;
                    menuItemFromDb.SpecialTag = menuItemUpdateDTO.SpecialTag;

                    if (menuItemUpdateDTO.File != null && menuItemUpdateDTO.File.Length > 0)
                    {
                        var imagesPath = Path.Combine(_env.WebRootPath, "images");
                        if (!Directory.Exists(imagesPath))
                        {
                            Directory.CreateDirectory(imagesPath);
                        }
                        var filePath = Path.Combine(imagesPath, menuItemUpdateDTO.File.FileName);
                        if (System.IO.File.Exists(filePath))
                        {
                            System.IO.File.Delete(filePath);
                        }
                        var filePath_OldFile = Path.Combine(_env.WebRootPath, menuItemFromDb.Image);
                        if (System.IO.File.Exists(filePath_OldFile))
                        {
                            System.IO.File.Delete(filePath_OldFile);
                        }
                        //uploading the image
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await menuItemUpdateDTO.File.CopyToAsync(stream);
                        }
                        menuItemFromDb.Image = "images/" + menuItemUpdateDTO.File.FileName;

                        //var imageUrl = await _cloudImages.UploadAsync(menuItemUpdateDTO.File);
                        //menuItemFromDb.Image = imageUrl;

                    }

                    _db.MenuItems.Update(menuItemFromDb);
                    await _db.SaveChangesAsync();

                    _response.StatusCode = HttpStatusCode.NoContent;
                    return Ok(_response);

                }
                else
                {
                    _response.IsSuccess = false;
                }
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages
                     = [ex.ToString()];
            }

            return BadRequest(_response);
        }


        [HttpDelete]
        public async Task<ActionResult<ApiResponse>> DeleteMenuItem(int id)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (id == 0)
                    {
                        _response.IsSuccess = false;
                        _response.StatusCode = HttpStatusCode.BadRequest;
                        return BadRequest(_response);
                    }

                    MenuItem? menuItemFromDb = await _db.MenuItems.FirstOrDefaultAsync(u => u.Id == id);

                    if (menuItemFromDb == null)
                    {
                        _response.IsSuccess = false;
                        _response.StatusCode = HttpStatusCode.NotFound;
                        return NotFound(_response);
                    }

                    //var filePath_OldFile = Path.Combine(_env.WebRootPath, menuItemFromDb.Image);
                    //if (System.IO.File.Exists(filePath_OldFile))
                    //{
                    //    System.IO.File.Delete(filePath_OldFile);
                    //}_db.MenuItems.Remove(menuItemFromDb);

                    _db.MenuItems.Remove(menuItemFromDb);
                    await _db.SaveChangesAsync();

                    _response.StatusCode = HttpStatusCode.NoContent;
                    return Ok(_response);

                }
                else
                {
                    _response.IsSuccess = false;
                }
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages
                     = [ex.ToString()];
            }

            return BadRequest(_response);
        }
    }
}
//using FruitCart_API.Data;
//using FruitCart_API.Models;
//using FruitCart_API.Models.Dto;
//using FruitCart_API.Services;
//using FruitCart_API.Utility;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using System.Net;

//namespace FruitCart_API.Controllers
//{
//    [Route("api/MenuItem")]
//    [Authorize(Roles = SD.Role_Admin)]
//    [ApiController]
//    public class MenuItemController : Controller
//    {
//        private readonly ApplicationDbContext _db;
//        private readonly ApiResponse _response;
//        private readonly ICloudImages _cloudImages;

//        public MenuItemController(ApplicationDbContext db, ICloudImages cloudImages)
//        {
//            _db = db;
//            _response = new ApiResponse();
//            _cloudImages = cloudImages;
//        }

//        [HttpGet]
//        [AllowAnonymous]
//        public IActionResult GetMenuItems()
//        {
//            List<MenuItem> menuItems = _db.MenuItems.ToList();

//            List<OrderDetail> orderDetailsWithRatings =
//                _db.OrderDetails.Where(u => u.Rating != null).ToList();

//            foreach (var menuItem in menuItems)
//            {
//                var ratings = orderDetailsWithRatings
//                    .Where(u => u.MenuItemId == menuItem.Id)
//                    .Select(u => u.Rating.Value);

//                menuItem.Rating = ratings.Any() ? ratings.Average() : 0;
//            }

//            _response.Result = menuItems;
//            _response.StatusCode = HttpStatusCode.OK;
//            return Ok(_response);
//        }

//        [HttpGet("{id:int}", Name = "GetMenuItem")]
//        [AllowAnonymous]
//        public IActionResult GetMenuItem(int id)
//        {
//            if (id == 0)
//            {
//                _response.StatusCode = HttpStatusCode.BadRequest;
//                _response.IsSuccess = false;
//                return BadRequest(_response);
//            }

//            var menuItem = _db.MenuItems.FirstOrDefault(u => u.Id == id);

//            if (menuItem == null)
//            {
//                _response.StatusCode = HttpStatusCode.NotFound;
//                _response.IsSuccess = false;
//                return NotFound(_response);
//            }

//            var ratings = _db.OrderDetails
//                .Where(u => u.Rating != null && u.MenuItemId == id)
//                .Select(u => u.Rating.Value);

//            menuItem.Rating = ratings.Any() ? ratings.Average() : 0;

//            _response.Result = menuItem;
//            _response.StatusCode = HttpStatusCode.OK;
//            return Ok(_response);
//        }

//        // 🔥 FIXED: multipart/form-data support
//        [HttpPost]
//        [Consumes("multipart/form-data")]
//        public async Task<ActionResult<ApiResponse>> CreateMenuItem(
//            [FromForm] MenuItemCreateDTO dto)
//        {
//            try
//            {
//                if (!ModelState.IsValid)
//                {
//                    _response.IsSuccess = false;
//                    return BadRequest(_response);
//                }

//                if (dto.File == null || dto.File.Length == 0)
//                {
//                    _response.IsSuccess = false;
//                    _response.StatusCode = HttpStatusCode.BadRequest;
//                    _response.ErrorMessages = new List<string> { "File is required" };
//                    return BadRequest(_response);
//                }

//                var imageUrl = await _cloudImages.UploadAsync(dto.File);

//                MenuItem menuItem = new()
//                {
//                    Name = dto.Name,
//                    Description = dto.Description,
//                    Price = dto.Price,
//                    Category = dto.Category,
//                    SpecialTag = dto.SpecialTag,
//                    Image = imageUrl
//                };

//                _db.MenuItems.Add(menuItem);
//                await _db.SaveChangesAsync();

//                _response.Result = menuItem;
//                _response.StatusCode = HttpStatusCode.Created;

//                return CreatedAtRoute("GetMenuItem",
//                    new { id = menuItem.Id }, _response);
//            }
//            catch (Exception ex)
//            {
//                _response.IsSuccess = false;
//                _response.ErrorMessages = new List<string> { ex.Message };
//                return BadRequest(_response);
//            }
//        }

//        // 🔥 FIXED: multipart for update
//        [HttpPut("{id:int}")]
//        [Consumes("multipart/form-data")]
//        public async Task<ActionResult<ApiResponse>> UpdateMenuItem(
//            int id,
//            [FromForm] MenuItemUpdateDTO dto)
//        {
//            try
//            {
//                if (!ModelState.IsValid || dto.Id != id)
//                {
//                    _response.IsSuccess = false;
//                    return BadRequest(_response);
//                }

//                var menuItem = await _db.MenuItems.FirstOrDefaultAsync(u => u.Id == id);

//                if (menuItem == null)
//                {
//                    _response.IsSuccess = false;
//                    return NotFound(_response);
//                }

//                menuItem.Name = dto.Name;
//                menuItem.Description = dto.Description;
//                menuItem.Price = dto.Price;
//                menuItem.Category = dto.Category;
//                menuItem.SpecialTag = dto.SpecialTag;

//                if (dto.File != null && dto.File.Length > 0)
//                {
//                    var imageUrl = await _cloudImages.UploadAsync(dto.File);
//                    menuItem.Image = imageUrl;
//                }

//                _db.MenuItems.Update(menuItem);
//                await _db.SaveChangesAsync();

//                _response.StatusCode = HttpStatusCode.NoContent;
//                return Ok(_response);
//            }
//            catch (Exception ex)
//            {
//                _response.IsSuccess = false;
//                _response.ErrorMessages = new List<string> { ex.Message };
//                return BadRequest(_response);
//            }
//        }

//        [HttpDelete("{id:int}")]
//        public async Task<ActionResult<ApiResponse>> DeleteMenuItem(int id)
//        {
//            try
//            {
//                if (id == 0)
//                {
//                    _response.IsSuccess = false;
//                    return BadRequest(_response);
//                }

//                var menuItem = await _db.MenuItems.FirstOrDefaultAsync(u => u.Id == id);

//                if (menuItem == null)
//                {
//                    _response.IsSuccess = false;
//                    return NotFound(_response);
//                }

//                _db.MenuItems.Remove(menuItem);
//                await _db.SaveChangesAsync();

//                _response.StatusCode = HttpStatusCode.NoContent;
//                return Ok(_response);
//            }
//            catch (Exception ex)
//            {
//                _response.IsSuccess = false;
//                _response.ErrorMessages = new List<string> { ex.Message };
//                return BadRequest(_response);
//            }
//        }
//    }
//}