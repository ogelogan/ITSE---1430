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

namespace Nile.Data
{
    /// <summary>Provides an in-memory movie database.</summary>
    public abstract class MovieDatabase : IMovieDatabase
    {
        /// <summary>Add a new movie.</summary>
        /// <param name="movie">The movie to add.</param>
        /// <param name="message">Error message.</param>
        /// <returns>The added movie.</returns>
        public Movie Add ( Movie movie, out string message )
        {
            //Check for null
            if (movie == null)
            {
                message = "Movie cannot be null.";
                return null;
            };
            var errors = ObjectValidator.Validate(movie);
            if (errors.Count() > 0)
            {
                //Get first error
                message = errors.ElementAt(0).ErrorMessage;
                return null;
            };
            //Verify unique movie
            var existing = GetMovieByNameCore(movie.Name);
            if (existing != null)
            {
                message = "Movie already exists.";
                return null;
            }
            message = null;
            return AddCore(movie);
        }

        /// <summary>Edits an existing movie.</summary>
        /// <param name="movie">The movie to update.</param>
        /// <param name="message">Error message.</param>
        /// <returns>The updated movie.</returns>
        public Movie Update ( Movie movie, out string message )
        {
            message = "";
            //Check for null
            if (movie == null)
            {
                message = "movie cannot be null.";
                return null;
            };
            var errors = ObjectValidator.Validate(movie);
            if (errors.Count() > 0)
            {
                //Get first error
                message = errors.ElementAt(0).ErrorMessage;
                return null;
            };
            //Verify unique movie
            var existing = GetMovieByNameCore(movie.Name);
            if (existing != null && existing.Id != movie.Id)
            {
                message = "Movie already exists.";
                return null;
            }
            //Find existing
            existing = existing ?? GetCore(movie.Id);
            if (existing == null)
            {
                message = "Movie not found.";
                return null;
            };
            return UpdateCore(movie);
        }
        /// <summary>Gets all movies.</summary>
        /// <returns>The list of movies.</returns>
        public IEnumerable<Movie> GetAll ()
        {
            return GetAllCore();
        }
        /// <summary>Removes a movie.</summary>
        /// <param name="id">The movie ID.</param>
        public void Remove ( int id )
        {
            //TODO: Return an error if id <= 0

            if (id > 0)
            {
                RemoveCore(id);
            };
        }

        protected abstract Movie AddCore( Movie movie );
        protected abstract IEnumerable<Movie> GetAllCore();
        protected abstract Movie GetCore( int id );
        protected abstract void RemoveCore( int id );
        protected abstract Movie UpdateCore( Movie movie );
        protected abstract Movie GetMovieByNameCore( string name );

        #region Private Members

        private readonly List<Movie> _movies = new List<Movie>();

        #endregion
    }
}
