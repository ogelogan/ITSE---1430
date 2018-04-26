/*
 * ITSE 1430
 * lab4
 * logan oge
 */
using System.Collections.Generic;

namespace Nile.Data
{
    public interface IMovieDatabase
    {
        Movie Add( Movie movie);
        Movie Update( Movie movie);
        IEnumerable<Movie> GetAll();
        void Remove( int id );
    }
}