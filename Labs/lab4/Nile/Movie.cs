/*
 * ITSE 1430
 * lab4
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
            get => _description ?? "";
            set => _description = value ?? "";
        }

        /// <summary>Gets or sets the name.</summary>
        /// <value></value>
        public string Title
        {
            get => _title ?? "";
            set => _title = value;
        }
        
        /// <summary>Gets or sets the ength.</summary>
        public int Length { get; set; }

        /// <summary>Determines if the movie is discontinued.</summary>
        public bool IsOwned { get; set; }
        
        public IEnumerable<ValidationResult> Validate( ValidationContext validationContext )
        {
            var errors = new List<ValidationResult>();

            //Name is required
            if (String.IsNullOrEmpty(_title))
                errors.Add(new ValidationResult("Name cannot be empty", 
                             new[] { nameof(Title) }));

            //lenght >= 0
            if (Length < 0)
                errors.Add(new ValidationResult("length must be >= 0",
                            new[] { nameof(Length) }));

            return errors;
        }

        #region Private Members

        private string _title;
        private string _description;

        #endregion
    }
}
