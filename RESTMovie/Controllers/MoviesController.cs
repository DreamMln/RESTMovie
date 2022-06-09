using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieLib;
using RESTMovie.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RESTMovie.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        //reference til manager class
        //instans
        //opretter objektet 
        private MoviesManager _manager = new MoviesManager();

        // GET: api/<MoviesController>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet]
        //action result så statuskoderne kan anvendes
        public ActionResult<IEnumerable<Movie>> Get()
        {
            IEnumerable<Movie> result = _manager.GetAll();
            if (result.Count() == 0)
            {
                return NotFound();
            }
            return Ok(result);
        }

        // GET api/<MoviesController>/5
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{id}")]
        public ActionResult<IEnumerable<Movie>> Get(int id)
        {
            Movie result = _manager.GetById(id);
            if (result == null)
            {
                return NotFound("No such Movie with, Id: " + id);
            }
            return Ok(result);
        }

        // GET api/<MoviesController>/5
        //filter - lengthinminutes
        //Is able to filter the result by either:
        //Name containing the substring (case-insensitive)
        //from query - binding
        [HttpGet("search")]
        public IEnumerable<Movie> GetF([FromQuery] int lengthInMinutes)
        {
            return _manager.GetFilter(lengthInMinutes);
        }


        // POST api/<MoviesController>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [HttpPost]
        public ActionResult<Movie> Post([FromBody] Movie newMovie)
        {
            Movie result = _manager.AddMovie(newMovie);
            if (result == null)
            {
                return null;
            }
            //ellers så bliver der tilføjet/created en Movie til listen
            return Created($"/api/movies/{result.Id}", result);
        }

        // PUT api/<MoviesController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/<MoviesController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
