/*
 * ITSE 1430
 * lab4
 * logan oge
 */
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Movie.Web.Mvc.Models
{
    /// <summary>Provides information about a movie.</summary>
    public class MovieModel
    {   

        public int Id { get; set; }

        public string Description { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string Title { get; set; }

        [Range(0, Double.MaxValue, ErrorMessage = "Length must be >= 0")]
        public int Length { get; set; }

        public bool IsOwned { get; set; }
    }
}
