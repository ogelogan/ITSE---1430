/*
 * ITSE 1430
 * lab 3
 * logan oge
 */
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nile.Data.Memory
{
    /// <summary>Provides an in-memory movie database.</summary>
    public class MemoryMovieDatabase : MovieDatabase
    {
        /// <summary>Initializes an instance of the <see cref="MemoryMovieDatabase"/> class.</summary>
        public MemoryMovieDatabase()
        {
            _movies = new List<Movie>() 
            {
                new Movie() { Id = _nextId++, Name = "Batman",
                                IsDiscontinued = true, Price = 15, },
                new Movie() { Id = _nextId++, Name = "Inception",
                                IsDiscontinued = true, Price = 55, },
                new Movie() { Id = _nextId++, Name = "Transformers",
                                IsDiscontinued = false, Price = 8 }
            };
        }

        /// <summary>Add a new movie.</summary>
        /// <param name="movie">The movie to add.</param>
        /// <param name="message">Error message.</param>
        /// <returns>The added movie.</returns>
        protected override Movie AddCore ( Movie movie)
        {           
            movie.Id = _nextId++;
            _movies.Add(Clone(movie));

            // Return a copy
            return movie;
        }

        /// <summary>Edits an existing movie.</summary>
        /// <param name="movie">The movie to update.</param>
        /// <param name="message">Error message.</param>
        /// <returns>The updated movie.</returns>
        protected override Movie UpdateCore ( Movie movie)
        {
            var existing = GetCore(movie.Id);
            // Clone the object
            Copy(existing, movie);
            //Return a copy
            return movie;
        }

        /// <summary>Gets all movies.</summary>
        /// <returns>The list of movies.</returns>
        protected override IEnumerable<Movie> GetAllCore ()
        {
            foreach (var movie in _movies)
            {
                if (movie != null)                
                    yield return Clone(movie);
            };
        }
        /// <summary>Removes a movie.</summary>
        /// <param name="id">The movie ID.</param>
        protected override void RemoveCore ( int id )
        {
            var existing = GetCore(id);
            if (existing != null)
                _movies.Remove(existing);
        }

        #region Private Members

        //Clone a movie
        private Movie Clone ( Movie item )
        {
            var newMovie = new Movie();
            Copy(newMovie, item);
            return newMovie;
        }
        //Copy a movie from one object to another
        private void Copy ( Movie target, Movie source )
        {
            target.Id = source.Id;
            target.Name = source.Name;
            target.Description = source.Description;
            target.Price = source.Price;
            target.IsDiscontinued = source.IsDiscontinued;
        }
        //Find a movie by its ID
        protected override Movie GetCore ( int id )
        {
            foreach (var movie in _movies)
            {
                if (movie.Id == id)
                    return movie;
            };

            return null;
        }
        protected override Movie GetMovieByNameCore ( string name)
        {
            foreach (var movie in _movies)
            {               
                if (String.Compare(movie.Name, name, true) == 0)
                    return movie;
            };

            return null;
        }

        private readonly List<Movie> _movies = new List<Movie>();
        private int _nextId = 1;

        #endregion
    }
}
