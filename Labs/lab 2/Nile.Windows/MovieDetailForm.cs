/*
 * ITSE 1430
 * lab2
 * logan oge
 */
using System;
using System.Windows.Forms;

namespace Nile.Windows
{
    /// <summary>Provides a form for adding/editing <see cref="Movie"/>.</summary>
    public partial class MovieDetailForm : Form
    {
        #region Construction

        public MovieDetailForm()
        {
            InitializeComponent();
        }
        public MovieDetailForm(string title) : this()
        {
            //InitializeComponent();

            Text = title;
        }

        public MovieDetailForm( Movie movie ) :this("Edit Movie")
        {
            //InitializeComponent();

            //Text = "Edit Movie";
            Movie = movie;
        }
        #endregion

        /// <summary>Gets or sets the movie being edited.</summary>
        public Movie Movie { get; set; }

        protected override void OnLoad( EventArgs e )
        {
            base.OnLoad(e);

            //load movie
            if (this.Movie != null)
            {
                _txtName.Text = Movie.Name;
                _txtDescription.Text = Movie.Description;
                _txtPrice.Text = Movie.Price.ToString();
                _chkIsOwned.Checked = Movie.IsOwned;
            }
            ValidateChildren();
        }

        #region Event Handlers

        private void OnCancel( object sender, EventArgs e )
        {
            //Don't need this method as DialogResult set on button
        }

        private void OnSave( object sender, EventArgs e )
        {
            if (!ValidateChildren())
                return;
            // Create movie
            var movie = new Movie();
            movie.Name = _txtName.Text;
            movie.Description = _txtDescription.Text;
            movie.Price = ConvertToPrice(_txtPrice);
            movie.IsOwned = _chkIsOwned.Checked;


            //Validate
            var message = movie.Validate();
            if (!String.IsNullOrEmpty(message))
            {     
                DisplayError(message);
                return;
            }; 

            //Return from form
            Movie = movie;
            DialogResult = DialogResult.OK;
            //DialogResult = DialogResult.None;
            Close();
        }
        #endregion
        
        private void DisplayError ( string message )
        {
            MessageBox.Show(this, message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private decimal ConvertToPrice ( TextBox control )
        {
            if (Decimal.TryParse(control.Text, out var price))
                return price;

            return -1;
        }

        private void _txtPrice_Validating( object sender, System.ComponentModel.CancelEventArgs e )
        {
            var textbox = sender as TextBox;

            var price = ConvertToPrice(textbox);
            if (price < 0)
            {
                _errorProvider.SetError(textbox, "Price is required");
                e.Cancel = true;
            } else
                _errorProvider.SetError(textbox, "");
        }

        private void _txtName_Validating( object sender, System.ComponentModel.CancelEventArgs e )
        {
            var textbox = sender as TextBox;

            if (String.IsNullOrEmpty(textbox.Text))
            {
                _errorProvider.SetError(textbox, "Name is required");
                e.Cancel = true;
            } else
                _errorProvider.SetError(textbox, "");
        }
    }
}
