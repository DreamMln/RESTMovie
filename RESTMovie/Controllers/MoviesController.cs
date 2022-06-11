using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieLib;
using RESTMovie.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace RESTMovie.Controllers
{
    /// <summary>
    ///  RestController er brugt til restful web services 
    ///  med de her @RestController annotation. 
    ///  De her annotation er brugt til class level og
    ///  tillader classen at håndtere de requests der kommer fra en client (JS).
    ///  
    ///  RestControlleren returnere objekter og data direkte skrevet som et 
    ///  http response, i JSON-format.
    /// </summary>

    //route - controllers bruger Routing til at matche
    // URL'er for indkommende requests, og actions.
    [Route("api/[controller]")]
    //gør den til en apicontroller
    [ApiController]
    public class MoviesController : ControllerBase
    {
        //reference til manager class
        //instans
        //opretter objektet - for at anvende metoderne
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
