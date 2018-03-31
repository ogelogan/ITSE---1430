/*
 * ITSE 1430
 * lab 3
 * logan oge
 */
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Nile
{
    /// <summary>Provides information about a movie.</summary>
    public class Movie : IValidatableObject
    {   
        /// <summary>Gets or sets the movie ID.</summary>
        public int Id { get; set; }

        /// <summary>Gets or sets the description.</summary>
        public string Description
        {
            get { return _description ?? ""; }
            set { _description = value ?? ""; }
        }

        /// <summary>Gets or sets the name.</summary>
        /// <value></value>
        public string Name
        {
            get { return _name ?? ""; }
            set { _name = value; }
        }        
        /// <summary>Gets or sets the price.</summary>
        public decimal Price { get; set; }
        /// <summary>Determines if the movie is discontinued.</summary>
        public bool IsDiscontinued { get; set; }
        
        public IEnumerable<ValidationResult> Validate( ValidationContext validationContext )
        {
            var errors = new List<ValidationResult>();

            //Name is required
            if (String.IsNullOrEmpty(_name))
                errors.Add(new ValidationResult("Name cannot be empty", 
                             new[] { nameof(Name) }));

            //Price >= 0
            if (Price < 0)
                errors.Add(new ValidationResult("Price must be >= 0",
                            new[] { nameof(Price) }));

            return errors;
        }
        #region Private Members
        private string _name;
        private string _description;

        #endregion
    }
}
