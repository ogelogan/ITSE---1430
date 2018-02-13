﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nile
{
    /// <summary> Provides information about a product.</summary>
    public class Product
    {
        internal decimal DiscountPercentage = .10m;

        /// <summary>Gets or sets the name.</summary>
        public string Name
        {
            get { return _name ?? ""; }
            set { _name = value; }
        }

        /// <summary>Gets or sets the description.</summary>
        public string Description
        {
            get { return _description ?? ""; }
            set { _description = value ?? ""; }
        }

        //using auto property here
        /// <summary>Gets or sets the price.</summary>
        public decimal Price
        {
            //get { return _price; }
            //set { _price = value; }
            get; set;
        } = 0;

        /// <summary>Gets or sets isDiscontinued.</summary>
        public bool IsDiscontinued { get; set; }
        //{
        //    get { return _isDiscontinued; }
        //    set { _isDiscontinued = value; }
        //}

        //public int ShowingOffAccessibility
        //{
        //    get { }
        //    internal set { }
        //}

        /// <summary>Gets the price, with any discontinued discounts.</summary>
        public decimal ActualPrice
        {
            get
            {
                if (IsDiscontinued)
                    return Price - (Price * DiscountPercentage);
                return Price;
            }
            //set { }
        }

        ///// <summary>Get the product name.</summary>
        ///// <returns>The name.</returns>
        //public string GetName ()
        //{
        //    return _name ?? "";
        //}

        /// <summary>Sets the product name.</summary>
        /// <param name="value">The name.</param>
        public void setName (string value)
        {
            _name = value ?? "";
        }

        /// <summary>Validates the product.</summary>
        /// <returns>Error message, if any.</returns>
        public string Validate()
        {
            //Name is required
            if (String.IsNullOrEmpty(_name))
                return "Name cannot be empty";

            //Price >= 0
            if (Price < 0)
                return "Price must be >= 0;";

            return "";
        }

        private string _name;
        private string _description;
        //private decimal _price;
        private bool _isDiscontinued;

    }
}
