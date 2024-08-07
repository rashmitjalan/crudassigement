using CrudOperation.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace CrudOperation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserDbContext _context;

        public UsersController(UserDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers(int pageNumber = 1, int pageSize = 8)
        {
            return await _context.Users.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        [HttpPost]
        public async Task<ActionResult<User>> PostUser([FromForm] UserDto userDto)
        {
            var user = new User
            {
                Id = 0,
                FullName = userDto.FullName,
                Gender = userDto.Gender,
                Address = userDto.Address,
                City = userDto.City,
                Pin = userDto.Pin,
                State = userDto.State,
                Country = userDto.Country,
                Email = userDto.Email,
                Contact = userDto.Contact,
                EducationQualification = userDto.EducationQualification,
                Designation = userDto.Designation,
            };

            if (userDto.ProfileImage != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await userDto.ProfileImage.CopyToAsync(memoryStream);
                    user.ProfileImageBase64 = Convert.ToBase64String(memoryStream.ToArray());
                }
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, [FromForm] UserDto userDto)
        {
            if (id != userDto.Id)
            {
                return BadRequest();
            }

            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            user.FullName = userDto.FullName;
            user.Gender = userDto.Gender;
            user.Address = userDto.Address;
            user.City = userDto.City;
            user.Pin = userDto.Pin;
            user.State = userDto.State;
            user.Country = userDto.Country;
            user.Email = userDto.Email;
            user.Contact = userDto.Contact;
            user.EducationQualification = userDto.EducationQualification;
            user.Designation = userDto.Designation;

            if (userDto.ProfileImage != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await userDto.ProfileImage.CopyToAsync(memoryStream);
                    user.ProfileImageBase64 = Convert.ToBase64String(memoryStream.ToArray());
                }
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }

    public class UserDto
    {
        public int Id { get; set; }
        public string? FullName { get; set; }
        public IFormFile? ProfileImage { get; set; } // Changed to IFormFile for file upload
        public string? Gender { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public int Pin { get; set; }
        public string? State { get; set; }
        public string? Country { get; set; }
        public string? Email { get; set; }
        public string? Contact { get; set; }
        public string? EducationQualification { get; set; }
        public string? Designation { get; set; }
    }
}
