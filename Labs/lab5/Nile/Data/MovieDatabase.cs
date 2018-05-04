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

namespace Nile.Data
{
    /// <summary>Provides an in-memory movie database.</summary>
    public abstract class MovieDatabase : IMovieDatabase
    {
        /// <summary>Add a new movie.</summary>
        /// <param name="movie">The movie to add.</param>
        /// <param name="message">Error message.</param>
        /// <returns>The added movie.</returns>
        /// <remarks>
        /// Returns an error if movie is null, invalid or if a movie
        /// with the same name already exists.
        /// </remarks>
        public Movie Add ( Movie movie)
        {
            //Check for null
            movie = movie ?? throw new ArgumentNullException(nameof(movie));

            //Validate movie
            movie.TryValidate();

            //Verify unique movie
            var existing = GetMovieBytitleCore(movie.Title);
            if (existing != null)
                throw new Exception("Movie already exists");
            return AddCore(movie);
        }

        /// <summary>Edits an existing movie.</summary>
        /// <param name="movie">The movie to update.</param>
        /// <param name="message">Error message.</param>
        /// <returns>The updated movie.</returns>
        /// <remarks>
        /// Returns an error if movie is null, invalid, movie name
        /// already exists or if the movie cannot be found.
        /// </remarks>
        public Movie Update ( Movie movie)
        {
            //Check for null
            if (movie == null)
                throw new ArgumentNullException(nameof(movie));
            //validating
            movie.TryValidate();
            //check if movie already exists
            var existing = GetMovieBytitleCore(movie.Title);
            if (existing != null && existing.Id != movie.Id)
                throw new Exception("Movie already exists.");

            existing = existing ?? GetCore(movie.Id);
            if (existing == null)
                throw new ArgumentException("Movie not found", nameof(movie));

            return UpdateCore(movie);
        }

        /// <summary>Gets all movies.</summary>
        /// <returns>The list of movies.</returns>
        public IEnumerable<Movie> GetAll ()
        {
            //using LINQ
            return from m in GetAllCore()
                   orderby m.Title, m.Id descending
                   select m;
        }

        /// <summary>Removes a movie.</summary>
        /// <param name="id">The movie ID.</param>
        public void Remove ( int id )
        {
            if (id <= 0)
                throw new ArgumentOutOfRangeException(nameof(id), "Id must be > 0");

            RemoveCore(id);
        }

        protected abstract Movie AddCore( Movie movie );
        protected abstract IEnumerable<Movie> GetAllCore();
        protected abstract Movie GetCore( int id );
        protected abstract void RemoveCore( int id );
        protected abstract Movie UpdateCore( Movie movie );
        protected abstract Movie GetMovieBytitleCore( string title );
    }
}
