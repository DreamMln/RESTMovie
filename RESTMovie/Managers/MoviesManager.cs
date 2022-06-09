using MovieLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RESTMovie.Managers
{
    public class MoviesManager
    {
        /// <summary>
        /// hvis vi ikke kan oprette den statiske liste
        /// så kan vi lave en constructor og initialisere listen
        /// i. Det kan være en løsning
        /// </summary>
        
        private static int _nextId = 1;
        private static List<Movie> _movies = new List<Movie>
        {
            new Movie { Id = _nextId++, LengthInMinutes = 120, CountryOfOrigin = "USA", Name = "Dune"},
            new Movie { Id = _nextId++, LengthInMinutes = 155, CountryOfOrigin = "California", Name = "LOTR"},
            new Movie { Id = _nextId++, LengthInMinutes = 133, CountryOfOrigin = "Denmark", Name = "Kære Børn"}
        };
        
        public List<Movie> GetAll()
        {
            List<Movie> result = new List<Movie>(_movies);
            return result;
        }

        public Movie GetById(int id)
        {
           return _movies.Find(m => m.Id == id);
        }
        //filter
        public IEnumerable<Movie> GetFilter(int filter)
        {
            IEnumerable<Movie> result = _movies.Where(x => x.LengthInMinutes < filter);
            return result;
        }

        public Movie AddMovie(Movie addMovie)
        {
            // der er ikke nogle objekt
            if (addMovie == null)
            {
                return null;
            }
            addMovie.Id = _nextId++;
            _movies.Add(addMovie);
            return addMovie;
        }
        //Sletter Movie fra listen med det specifikke Id
        //derefter returnere den null, af ingen movies har det id
        public Movie DeleteMovie(int id)
        {
            Movie deleteMovie = _movies.Find(m => m.Id == id);
            if (deleteMovie == null)
            {
                return null;
            }
            //ellers slet en movie fra listen
            _movies.Remove(deleteMovie);
            return deleteMovie;
        }
    }
}
