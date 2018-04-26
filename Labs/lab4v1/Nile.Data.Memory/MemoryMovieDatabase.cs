/*
 * ITSE 1430
 * lab4
 * logan oge
 */
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//forces me to use abstract. figure out why


namespace Nile.Data.Memory
{
    /// <summary>Provides an in-memory movie database.</summary>
    public class MemoryMovieDatabase : MovieDatabase
    {
        /// <summary>Add a new movie.</summary>
        /// <param name="movie">The movie to add.</param>
        /// <param name="message">Error message.</param>
        /// <returns>The added movie.</returns>
        /// <remarks>
        /// Returns an error if movie is null, invalid or if a movie
        /// with the same name already exists.
        /// </remarks>
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
        /// <remarks>
        /// Returns an error if movie is null, invalid, movie name
        /// already exists or if the movie cannot be found.
        /// </remarks>
        protected override Movie UpdateCore ( Movie movie)
        {
            var existing = GetCore(movie.Id);
            Copy(existing, movie);

            //Return a copy
            return movie;
        }

        /// <summary>Gets all movies.</summary>
        /// <returns>The list of movies.</returns>
        protected override IEnumerable<Movie> GetAllCore ()
        {
            return from m in _movies
                   select Clone(m);
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
            target.Title = source.Title;
            target.Description = source.Description;
            target.Length = source.Length;
            target.IsOwned = source.IsOwned;
        }

        //Find a movie by its ID
        protected override Movie GetCore ( int id )
        {
            //LINQs and extension methods
            return (from m in _movies
                    where m.Id == id
                    select m).FirstOrDefault();
        }

        protected override Movie GetMovieBytitleCore ( string title)
        {
            return (from m in _movies
                    where String.Compare(m.Title, title, true) == 0
                    select m).FirstOrDefault();
        }

        private readonly List<Movie> _movies = new List<Movie>();
        private int _nextId = 1;

        #endregion
    }
}
