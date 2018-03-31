/*
 * ITSE 1430
 * lab 3
 * logan oge
 */
using System.Collections.Generic;

namespace Nile.Data
{
    public interface IMovieDatabase
    {
        Movie Add( Movie movie, out string message );
        Movie Update( Movie movie, out string message );
        IEnumerable<Movie> GetAll();
        void Remove( int id );
    }
}