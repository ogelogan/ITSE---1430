/*
 * ITSE 1430
 * lab4
 * logan oge
 */
using System;
using System.Linq;
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

        public MovieDetailForm( string title ) : this()
        {
            Text = title;
        }

        public MovieDetailForm( Movie movie ) :this("Edit Movie")
        {
            Movie = movie;
        }
        #endregion

        /// <summary>Gets or sets the movie being edited.</summary>
        public Movie Movie { get; set; }
        
        protected override void OnLoad( EventArgs e )
        {
            base.OnLoad(e);

            //Load movie
            if (Movie != null)
            {
                _txtTitle.Text = Movie.Title;
                _txtDescription.Text = Movie.Description;
                _txtLength.Text = Movie.Length.ToString();
                _chkIsOwned.Checked = Movie.IsOwned;
            };

            ValidateChildren();
        }

        #region Event Handlers

        private void OnCancel( object sender, EventArgs e )
        {
        }

        private void OnSave( object sender, EventArgs e )
        {
            //Force validation of child controls
            if (!ValidateChildren())
                return;

            // Create movie - using object initializer syntax
            var movie = new Movie() {
                Title = _txtTitle.Text,
                Description = _txtDescription.Text,
                Length = ConvertToLength(_txtLength),
                IsOwned = _chkIsOwned.Checked,
            };
            var errors = ObjectValidator.TryValidate(movie);
            if (errors.Count() > 0)
            {
                //Get first error
                DisplayError(errors.ElementAt(0).ErrorMessage);
                return;
            };            
            
            //Return from form
            Movie = movie;
            DialogResult = DialogResult.OK;

            Close();
        }
        #endregion
        
        private void DisplayError ( string message )
        {
            MessageBox.Show(this, message, "Error", MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
        }
        private int ConvertToLength ( TextBox control )
        {
            if (Int32.TryParse(control.Text, out var length))
                return length;

            return -1;
        }

        private void _txtTitle_Validating( object sender, System.ComponentModel.CancelEventArgs e )
        {
            var textbox = sender as TextBox;

            if (String.IsNullOrEmpty(textbox.Text))
            {

                _errorProvider.SetError(textbox, "title is required");
                e.Cancel = true;
            } else
                _errorProvider.SetError(textbox, "");
        }

        private void _txtLength_Validating( object sender, System.ComponentModel.CancelEventArgs e )
        {
            var textbox = sender as TextBox;

            var length = ConvertToLength(textbox);
            if (length < 0)
            {
                _errorProvider.SetError(textbox, "Length must be >= 0");
                e.Cancel = true;
            } else
                _errorProvider.SetError(textbox, "");
        }
    }
}
