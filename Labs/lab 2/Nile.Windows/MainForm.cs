/*
 * ITSE 1430
 * lab2
 * logan oge
 */
using System;
using System.Windows.Forms;

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

        }
        #region Event Handlers

        private void OnFileExit( object sender, EventArgs e )
        {
            Close();
        }

        private void OnMovieAdd ( object sender, EventArgs e )
        {
            //test thing
            var button = sender as ToolStripMenuItem;

            var form = new MovieDetailForm("Add Movie");

            //Show form modally
            var result = form.ShowDialog(this);
            if (result != DialogResult.OK)
                return;

            //"Add" the movie
            _movie = form.Movie;
        }

        private void OnMovieEdit( object sender, EventArgs e )
        {
            if (_movie == null)
                return;

            var form = new MovieDetailForm(_movie);

            //Show form modally
            var result = form.ShowDialog(this);
            if (result != DialogResult.OK)
                return;

            //"Editing" the movie
            _movie = form.Movie;
        }

        private void OnMovieDelete( object sender, EventArgs e )
        {
            if (!ShowConfirmation("Are you sure?", "Delete Movie"))
                return;
            _movie = null;
        }        
        
        private void OnHelpAbout( object sender, EventArgs e )
        {
            var about = new AboutBox1();
            about.ShowDialog(this);

        }
        #endregion

        private bool ShowConfirmation ( string message, string title )
        {
            return MessageBox.Show(this, message, title
                             , MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                           == DialogResult.Yes;
        }

        private Movie _movie;
    }
}
