/*
 * ITSE 1430
 * lab4
 * logan oge
 */
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Windows.Forms;
using Movie.Data.Sql;
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
            var connString = ConfigurationManager.ConnectionStrings["MovieDatabase"];
            _database = new SqlMovieDatabase(connString.ConnectionString);

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
            try
            {
                _database.Add(form.Movie);
            } catch (NotImplementedException)
            {
                MessageBox.Show("not implemented yet");
            } catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

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
            try
            {
                _database.Update(form.Movie);
            } catch (Exception e)
            {
                MessageBox.Show(e.Message);
            };

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

        private Movie GetSelectedMovie ()
        {
            var items = (from r in dataGridView1.SelectedRows.OfType<DataGridViewRow>()
                         select new {
                             Index = r.Index,
                             Movie = r.DataBoundItem as Movie
                         }).FirstOrDefault();
            return items.Movie;
        }

        private void RefreshUI ()
        {
            //Get moveis
            var movies = _database.GetAll();
            movieBindingSource.DataSource = movies.ToList();
        }

        private bool ShowConfirmation ( string message, string title )
        {
            return MessageBox.Show(this, message, title
                             , MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                           == DialogResult.Yes;
        }

        //this might be important***
        private IMovieDatabase _database;

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
