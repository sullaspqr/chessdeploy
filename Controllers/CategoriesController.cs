using Farkas_Zoltán_backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Farkas_Zoltán_backend.Controllers
{
    [Route("api/categories")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        [HttpGet("feladat11")]
        public IActionResult Get()
        {
            using (var context = new LibrarydbContext())
            {
                try
                {
                    var result = context.Categories.Include(x => x.Books).ToList();

                    if (result != null)
                    {
                        return Ok(result);
                    }

                    return NotFound();

                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
        }
    }
}
