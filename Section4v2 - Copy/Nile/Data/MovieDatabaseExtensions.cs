/*
 * ITSE 1430
 * lab4
 * logan oge
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nile.Data
{
    public static class MovieDatabaseExtensions
    {
        public static void Seed ( this IMovieDatabase source)
        {
            var message = "";
            source.Add(new Movie() {
                Title = "Inception",
                IsOwned = true, Length = 128,}, out message);
            source.Add(new Movie() {
                Title = "batman 3",
                IsOwned = true, Length = 152, }, out message);
            source.Add(new Movie() {
                Title = "Transformers",
                IsOwned = false, Length = 258
            }, out message);
        }
    }
}
