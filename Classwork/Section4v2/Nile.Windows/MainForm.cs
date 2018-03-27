/*
 * ITSE 1430
 * Classwork
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Nile.Data;
using Nile.Data.IO;
using Nile.Data.Memory;

namespace Nile.Windows
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        protected override void OnLoad( EventArgs e )
        {
            base.OnLoad(e);
            _database = new FileProductDatabase("products.csv");
            RefreshUI();
        }

        #region Event Handlers

        private void OnFileExit( object sender, EventArgs e )
        {
            Close();
        }

        private void OnProductAdd ( object sender, EventArgs e )
        {
            var button = sender as ToolStripMenuItem;

            var form = new ProductDetailForm("Add Product");

            //Show form modally
            var result = form.ShowDialog(this);
            if (result != DialogResult.OK)
                return;

            //Add to database
            _database.Add(form.Product, out var message);
            if (!String.IsNullOrEmpty(message))
                MessageBox.Show(message);

            RefreshUI();
            //Find empty array element
            //var index = FindEmptyProductIndex();
            //if (index >= 0)
            //_products[index] = form.Product;                    
        }

        private void OnProductEdit( object sender, EventArgs e )
        {
            //Get selected product
            var product = GetSelectedProduct();
            if (product == null)
            {
                MessageBox.Show(this, "NO product selected", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            EditProduct(product);
        }

        private void EditProduct(Product product)
        {
            var form = new ProductDetailForm(product);
            var result = form.ShowDialog(this);
            if (result != DialogResult.OK)
                return;

            //Update the product
            form.Product.Id = product.Id;
            _database.Update(form.Product, out var message);
            if (!String.IsNullOrEmpty(message))
                MessageBox.Show(message);

            RefreshUI();
        }

        private void OnProductRemove( object sender, EventArgs e )
        {
            var product = GetSelectedProduct();
            if (product == null)
            {
                MessageBox.Show(this, "NO product selected", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            DeleteProduct(product);
        }

        private void DeleteProduct( Product product )
        {
            if (!ShowConfirmation("Are you sure?", "Remove Product"))
                return;

            //Remove product
            _database.Remove(product.Id);
            //_products[index] = null;

            RefreshUI();
        }

        private void OnHelpAbout( object sender, EventArgs e )
        {
            MessageBox.Show(this, "Not implemented", "Help About", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        }
        #endregion

        private Product GetSelectedProduct ( )
        {
            //Get the first selected row in the grid, if any
            if (dataGridView1.SelectedRows.Count > 0)
                return dataGridView1.SelectedRows[0].DataBoundItem as Product;

            return null;
        }

        private void RefreshUI ()
        {
            //Get products
            var products = _database.GetAll();
            //products[0].Name = "Product A";

            //Bind to grid
            //productBindingSource.DataSource = new List<Product>(products);
            //productBindingSource.DataSource = Enumerable.ToList(products);
            productBindingSource.DataSource = products.ToList();
        }

        private bool ShowConfirmation ( string message, string title )
        {
            return MessageBox.Show(this, message, title
                             , MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                           == DialogResult.Yes;
        }

        //this might be important***
        private IProductDatabase _database;

        private void OnCellDoubleClick( object sender, DataGridViewCellEventArgs e )
        {
            var product = GetSelectedProduct();
            if (product == null)
                return;

            EditProduct(product);
        }

        private void OnCellKeyDown( object sender, KeyEventArgs e )
        {
            var product = GetSelectedProduct();
            if (product == null)
                return;
            if (e.KeyCode == Keys.Delete)
            {
                e.Handled = true;
                DeleteProduct(product);
            } else if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                EditProduct(product);
            }
        }
    }
}
