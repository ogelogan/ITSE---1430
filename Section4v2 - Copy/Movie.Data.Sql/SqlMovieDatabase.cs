/*
 * ITSE 1430
 * lab4
 * logan oge
 */
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nile;
using Nile.Data;
//forces me to use abstract. figure out why.
//also figure out why i gotta use nile.Movie instead of movie
namespace Movie.Data.Sql
{
    public class SqlMovieDatabase : MovieDatabase
    {
        private readonly string _connectionString;

        public SqlMovieDatabase( string connectionString )
        {
            if (connectionString == null)
                throw new ArgumentNullException(nameof(connectionString));
            if (connectionString == "")
                throw new ArgumentException("Connection string cannot be empty.",
                                            nameof(connectionString));

            _connectionString = connectionString;
        }

        protected override Nile.Movie AddCore( Nile.Movie movie )
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                var cmd = new SqlCommand("AddMovie", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@title", movie.Title);
                cmd.Parameters.AddWithValue("@length", movie.Length);
                cmd.Parameters.AddWithValue("@description", movie.Description);

                var parm = cmd.CreateParameter();
                parm.ParameterName = "@isOwned";
                parm.DbType = System.Data.DbType.Boolean;
                parm.Value = movie.IsOwned;
                cmd.Parameters.Add(parm);

                conn.Open();
                var result = cmd.ExecuteScalar();

                var id = Convert.ToInt32(result);
                movie.Id = id;
            }
            return movie;
        }

        protected override IEnumerable<Nile.Movie> GetAllCore()
        {
            var items = new List<Nile.Movie>();

            using (var conn = new SqlConnection(_connectionString))
            {
                var cmd = new SqlCommand("GetAllMovies", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                conn.Open();

                var ds = new DataSet();

                var da = new SqlDataAdapter();
                da.SelectCommand = cmd;

                da.Fill(ds);

                if (ds.Tables.Count == 1)
                {
                    foreach (var row in ds.Tables[0].Rows.OfType<DataRow>())
                    {
                        var movie = new Nile.Movie() {
                            Id = Convert.ToInt32(row["Id"]),
                            Title = row.Field<string>("Title"),
                            IsOwned = row.Field<bool>("IsOwned"),
                            Description = row.Field<string>("Description"),
                            Length = row.Field<int>("Length")                            
                        };
                        items.Add(movie);
                    };
                };
            };

            return items;
        }

        protected override Nile.Movie GetCore( int id )
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                var cmd = new SqlCommand("GetMovie", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@id", id));
                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                        return ReadData(reader);
                }
            }
            return null;
        }

        private Nile.Movie ReadData( SqlDataReader reader )
        {
            return new Nile.Movie() {
                Id = Convert.ToInt32(reader["Id"]),
                Title = reader.GetFieldValue<string>(1),
                IsOwned = reader.GetBoolean(2),
                Description = reader.GetString(3),
                Length = reader.GetInt32(4)
            };
        }

        protected override Nile.Movie GetMovieBytitleCore( string title )
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                var cmd = new SqlCommand("GetAllMovies", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var movie = ReadData(reader);
                        if (String.Compare(movie.Title, title, true) == 0)
                            return movie;
                    }
                }
            }
            return null;
        }

        protected override void RemoveCore( int id )
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                var cmd = new SqlCommand("RemoveMovie", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@id", id));
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        protected override Nile.Movie UpdateCore( Nile.Movie movie )
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                var cmd = new SqlCommand("RemoveMovie", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@id", movie.Id);
                cmd.Parameters.AddWithValue("@title", movie.Title);

                var parm = cmd.CreateParameter();
                parm.ParameterName = "@isOwned";
                parm.DbType = System.Data.DbType.Boolean;
                cmd.Parameters.Add(parm);
                conn.Open();
                cmd.ExecuteNonQuery();

                cmd.Parameters.AddWithValue("@Description", movie.Description);
                cmd.Parameters.AddWithValue("@Length", movie.Length);
            };
            return movie;
        }
    }
}
