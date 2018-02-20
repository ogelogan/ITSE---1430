﻿using System;
using System.Windows.Forms;

namespace Nile.Windows
{
    /// <summary>Provides a form for adding/editing <see cref="Product"/>.</summary>
    public partial class ProductDetailForm : Form
    {
        #region Construction

        public ProductDetailForm()
        {
            InitializeComponent();
        }
        #endregion

        /// <summary>Gets or sets the product being edited.</summary>
        public Product Product { get; set; }

        protected override void OnLoad( EventArgs e )
        {
            base.OnLoad(e);

            //load product
            if (Product != null)
            {
                _txtName.Text = Product.Name;
                _txtDescription.Text = Product.Description;
                _txtPrice.Text = Product.Price.ToString();
                _chkIsDiscontinued.Checked = Product.IsDiscontinued;
            }
        }

        #region Event Handlers

        private void OnCancel( object sender, EventArgs e )
        {
            //Don't need this method as DialogResult set on button
        }

        private void OnSave( object sender, EventArgs e )
        {
            // Create product
            var product = new Product();
            product.Name = _txtName.Text;
            product.Description = _txtDescription.Text;
            product.Price = ConvertToPrice(_txtPrice);
            product.IsDiscontinued = _chkIsDiscontinued.Checked;

            //Return from form
            Product = product;
            DialogResult = DialogResult.OK;
            //DialogResult = DialogResult.None;
            Close();
        }
        #endregion
        
        private decimal ConvertToPrice ( TextBox control )
        {
            if (Decimal.TryParse(control.Text, out var price))
                return price;

            return -1;
        }

        private void ProductDetailForm_Load( object sender, EventArgs e )
        {

        }
    }
}
