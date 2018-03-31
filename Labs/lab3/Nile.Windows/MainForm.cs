/*
 * ITSE 1430
 * lab 3
 * logan oge
 */
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Nile.Data;
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

            RefreshUI();
        }

        #region Event Handlers

        private void OnFileExit( object sender, EventArgs e )
        {
            Close();
        }

        private void OnMovieAdd ( object sender, EventArgs e )
        {
            var button = sender as ToolStripMenuItem;
            var form = new MovieDetailForm("Add Movie");
            //Show form modally
            var result = form.ShowDialog(this);
            if (result != DialogResult.OK)
                return;
            //Add to database
            _database.Add(form.Movie, out var message);
            if (!String.IsNullOrEmpty(message))
                MessageBox.Show(message);
            RefreshUI();           
        }

        private void OnMovieEdit( object sender, EventArgs e )
        {
            //Get selected movie
            var movie = GetSelectedMovie();
            if (movie == null)
            {
                MessageBox.Show(this, "NO movie selected", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            EditMovie(movie);
        }
        private void EditMovie(Movie movie)
        {
            var form = new MovieDetailForm(movie);
            var result = form.ShowDialog(this);
            if (result != DialogResult.OK)
                return;
            //Update the movie
            form.Movie.Id = movie.Id;
            _database.Update(form.Movie, out var message);
            if (!String.IsNullOrEmpty(message))
                MessageBox.Show(message);
            RefreshUI();
        }
        private void OnMovieRemove( object sender, EventArgs e )
        {
            var movie = GetSelectedMovie();
            if (movie == null)
            {
                MessageBox.Show(this, "NO movie selected", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            DeleteMovie(movie);
        }
        private void DeleteMovie( Movie movie )
        {
            if (!ShowConfirmation("Are you sure?", "Remove Movie"))
                return;
            //Remove movie
            _database.Remove(movie.Id);          
            RefreshUI();
        }
        private void OnHelpAbout( object sender, EventArgs e )
        {
            MessageBox.Show(this, "Not implemented", "Help About", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        }
        #endregion

        private Movie GetSelectedMovie ( )
        {
            //Get the first selected row in the grid, if any
            if (dataGridView1.SelectedRows.Count > 0)
                return dataGridView1.SelectedRows[0].DataBoundItem as Movie;
            return null;
        }
        private void RefreshUI ()
        {
            //Get movies
            var movies = _database.GetAll();
            //Bind to grid
            movieBindingSource.DataSource = new List<Movie>(movies);
        }

        private bool ShowConfirmation ( string message, string title )
        {
            return MessageBox.Show(this, message, title
                             , MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                           == DialogResult.Yes;
        }
        private IMovieDatabase _database = new MemoryMovieDatabase();

        private void OnCellDoubleClick( object sender, DataGridViewCellEventArgs e )
        {
            var movie = GetSelectedMovie();
            if (movie == null)
                return;
            EditMovie(movie);
        }
        private void OnCellKeyDown( object sender, KeyEventArgs e )
        {
            var movie = GetSelectedMovie();
            if (movie == null)
                return;
            if (e.KeyCode == Keys.Delete)
            {
                e.Handled = true;
                DeleteMovie(movie);
            } else if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                EditMovie(movie);
            }
        }
    }
}
