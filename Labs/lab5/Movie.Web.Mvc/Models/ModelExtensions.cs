using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Movie.Web.Mvc.Models
{
    public static class ModelExtensions
    {
        public static MovieModel ToModel( this Nile.Movie source )
            => new MovieModel() {
                Id = source.Id,
                Title = source.Title,
                Description = source.Description,
                IsOwned = source.IsOwned,
                Length = source.Length
            };
        public static Nile.Movie ToDomain ( this MovieModel source)
            => new Nile.Movie() {
                Id = source.Id,
                Title = source.Title,
                Description = source.Description,
                IsOwned = source.IsOwned,
                Length = source.Length
            };
    }
}