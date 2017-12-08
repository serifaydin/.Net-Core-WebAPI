# .Net-Core-WebAPI
.Net Core WebAPI & Entity FrameworkCore

Added Nuget Package :Microsoft.EntityFrameworkCore.DbContext

PersonController

using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Core_WebApp.Models;
using System.Collections.Generic;

namespace Core_WebApp.Controllers
{
    [Route("api/[controller]")]
    public class PersonController : Controller
    {
        private readonly PersonContext _context;

        public PersonController(PersonContext context)
        {
            _context = context;

            if (_context.Person.Count() == 0)
            {
                _context.Person.Add(new Person { Firstname = "Serif", Surname = "Aydin", password = "123", isActive = true });
                _context.Person.Add(new Person { Firstname = "Alex", Surname = "de Souza", password = "1907", isActive = true });
                _context.SaveChanges();
            }
        }

        [HttpGet]
        public IEnumerable<Person> GetAll()
        {
            return _context.Person.ToList();
        }

        [HttpGet("{id}", Name = "GetPerson")]
        public IActionResult GetById(long id)
        {
            var item = _context.Person.FirstOrDefault(t => t.Id == id);
            if (item == null)
                return NotFound();

            return new ObjectResult(item);
        }

        [HttpPost]
        public IActionResult Create([FromBody] Person item)
        {
            if (item == null)
                return BadRequest();

            _context.Person.Add(item);
            _context.SaveChanges();

            return CreatedAtRoute("GetPerson", new { id = item.Id }, item);
        }

        [HttpPut("{id}")]
        public IActionResult Update(long id, [FromBody] Person item)
        {
            if (item == null || item.Id != id)
                return BadRequest();

            var model = _context.Person.FirstOrDefault(t => t.Id == id);
            if (model == null)
                return NotFound();

            model.Firstname = item.Firstname;
            model.Surname = item.Surname;
            model.isActive = item.isActive;
            model.password = item.password;

            _context.Person.Update(model);
            _context.SaveChanges();

            return new NoContentResult();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var model = _context.Person.FirstOrDefault(t => t.Id == id);
            if (model == null)
                return NotFound();

            _context.Person.Remove(model);
            _context.SaveChanges();

            return CreatedAtRoute("GetPerson", new { id }, model);
        }
    }
}