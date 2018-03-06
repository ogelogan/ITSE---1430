/*
 * ITSE 1430
 * lab2
 * logan oge
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nile
{
    /// <summary>Provides information about a product.</summary>
    public class Movie
    {
        internal decimal DiscountPercentage = 0.10M;

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

        public decimal Price { get; set; } = 0;

        /// <summary>Gets the price, with any discontinued discounts.</summary>
        public decimal ActualPrice
        {
            get 
            {
                if (IsOwned)
                    return Price - (Price * DiscountPercentage);
                return Price;
            }
        }

        /// <summary>Determines if the product is discontinued.</summary>
        public bool IsOwned { get; set; }

        /// <summary>Validates the product.</summary>
        /// <returns>Error message, if any.</returns>
        public string Validate ()
        {
            if (String.IsNullOrEmpty(_name))
                return "Name cannot be empty";
            if (Price < 0)
                return "Price must be >= 0";
            return "";
        }
        private string _name;
        private string _description;
    }
}
