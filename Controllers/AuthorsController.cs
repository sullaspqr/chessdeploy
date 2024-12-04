using Farkas_Zoltán_backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Farkas_Zoltán_backend.Controllers
{
    [Route("api/authors")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        [HttpGet("feladat9/{authorname}")]
        public IActionResult Get(string authorname)
        {
            using (var context = new LibrarydbContext())
            {
                try
                {
                    var result = context.Authors.Include(x => x.Books).FirstOrDefault(x => x.AuthorName == authorname);

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

        [HttpGet("feladat12")]
        public IActionResult GetAuthorNumbers()
        {
            using (var context = new LibrarydbContext())
            {
                try
                {
                    var result = context.Authors.Count();

                    if (result != null)
                    {
                        return StatusCode(200, "Szerzők száma: " + result);
                    }

                    return NotFound();

                }
                catch (Exception ex)
                {
                    return BadRequest("Nem lehet csatlakozni az adatbázishoz!");
                }
            }
        }
    }
}
