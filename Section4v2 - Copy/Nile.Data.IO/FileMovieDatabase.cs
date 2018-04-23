/*
 * ITSE 1430
 * lab4
 * logan oge
 */
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//forces me to do absract. figure out why

namespace Nile.Data.IO
{
    public class FileMovieDatabase : MovieDatabase
    {
        #region Construction
        public FileMovieDatabase ( string filename)
        {
            _filename = filename;
        }
        #endregion

        protected override Movie AddCore( Movie movie )
        {
            EnsureInitialized();

            movie.Id = _id++;
            _items.Add(movie);

            SaveData();

            return movie;
        }

        protected override IEnumerable<Movie> GetAllCore()
        {
            EnsureInitialized();

            //LoadData();
            return _items;
        }
        //Ensure file is loaded
        private void EnsureInitialized ()
        {
            if (_items == null)
            {
                _items = LoadData();
                if (_items.Any())
                {
                    _id = _items.Max(i => i.Id);
                    ++_id;
                }
            }
        }

        private List<Movie> LoadData()
        {
            var items = new List<Movie>();

            try
            {
                //make sure the file exists
                if (!File.Exists(_filename))
                    return items;

                var lines = File.ReadAllLines(_filename);

                foreach (var line in lines)
                {
                    var fields = line.Split(',');

                    //not checking for missing fields  here
                    var movie = new Movie()
                    {
                        Id = ParseInt32(fields[0]),
                        Title = fields[1],
                        Description = fields[2],
                        Length = ParseInt32(fields[3]),
                        IsOwned = ParseInt32(fields[4]) != 0
                    };
                    items.Add(movie);
                }
                return items;
            } catch (Exception e)
            {
                throw new Exception("Failure loading data", e);
            }
        }

        private decimal ParseDecimal( string value )
        {
            if (Decimal.TryParse(value, out var result))
                return result;

            return -1;
        }
        private int ParseInt32 ( string value)
        {
            if (Int32.TryParse(value, out var result))
                return result;

            return -1;
        }

        protected override Movie GetCore( int id )
        {
            EnsureInitialized();
            return _items.FirstOrDefault(i => i.Id == id);
        }

        protected override Movie GetMovieBytitleCore( string title )
        {
            EnsureInitialized();
            return _items.FirstOrDefault(
                    i => String.Compare(i.Title, title, true) == 0);
        }

        private void SaveData()
        {
            using (var stream = File.OpenWrite(_filename))
            using (var writer = new StreamWriter(stream))
            {
                foreach (var item in _items)
                {
                    var line = $"{item.Id},{item.Title},{item.Description},{item.Length},{(item.IsOwned ? 1 : 0)}";
                    writer.WriteLine(line);
                };
            };
        }
        protected override void RemoveCore( int id )
        {
            EnsureInitialized();

            var movie = GetCore(id);
            if (movie != null)
            {
                _items.Remove(movie);
                SaveData();
            }
        }

        protected override Movie UpdateCore( Movie movie )
        {
            EnsureInitialized();

            var existing = GetCore(movie.Id);
            _items.Remove(existing);
            _items.Add(movie);
            SaveData();
            return movie;
        }

        public override Movie Add(Movie movie, out string message)
        {
            throw new NotImplementedException();
        }

        public override Movie Update(Movie movie, out string message)
        {
            throw new NotImplementedException();
        }

        private readonly string _filename;
        private List<Movie> _items;
        private int _id;
    }
}
