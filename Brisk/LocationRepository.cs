using Brisk.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Brisk
{
    public class LocationRepository
    {

        private static string connectionString = Environment.GetEnvironmentVariable("SQL_CONN");

        public static Location GetLocationById(int id)
        {
            var conn = new MySqlConnection(connectionString);
            var l = new Location();

            using (conn)
            {
                conn.Open();

                var cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT creator, zip, name FROM locations WHERE id = @id";
                cmd.Parameters.AddWithValue("id", id);

                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    l.Id = id;
                    l.Creator = (int)reader["creator"];
                    l.Zip = (int)reader["zip"];
                    l.Name = reader["name"].ToString();
                }

                return l;
            }
        }

        public static List<Location> GetLocations()
        {
            var conn = new MySqlConnection(connectionString);
            var locations = new List<Location>();

            using (conn)
            {
                conn.Open();

                var cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT id, creator, zip, name FROM locations ORDER BY zip;";

                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    var l = new Location()
                    {
                        Id = (int)reader["id"],
                        Creator = (int)reader["creator"],
                        Zip = (int)reader["zip"],
                        Name = reader["name"].ToString()
                };

                    locations.Add(l);
                }

                return locations;
            }
        }

        public static int CreateLocation(Location l)
        {
            var conn = new MySqlConnection(connectionString);

            using (conn)
            {
                conn.Open();

                var cmd = conn.CreateCommand();
                cmd.CommandText = "INSERT INTO locations (creator, zip, name) " +
                    "VALUES (@creator, @zip, @name);";
                cmd.Parameters.AddWithValue("creator", l.Creator);
                cmd.Parameters.AddWithValue("zip", l.Zip);
                cmd.Parameters.AddWithValue("name", l.Name);

                return cmd.ExecuteNonQuery();
            }
        }

        public static int DeleteLocation(int id)
        {
            var conn = new MySqlConnection(connectionString);

            using (conn)
            {
                conn.Open();

                var cmd = conn.CreateCommand();
                cmd.CommandText = "DELETE FROM locations WHERE id = @id;";
                cmd.Parameters.AddWithValue("id", id);

                return cmd.ExecuteNonQuery();
            }
        }

        public static void UpdateLocation(Location l)
        {
            var conn = new MySqlConnection(connectionString);

            using (conn)
            {
                conn.Open();

                var cmd = conn.CreateCommand();
                cmd.CommandText = "UPDATE locations SET creator = @creator, zip = @zip, name = @name WHERE id = @id";
                cmd.Parameters.AddWithValue("creator", l.Creator);
                cmd.Parameters.AddWithValue("zip", l.Zip);
                cmd.Parameters.AddWithValue("name", l.Name);
                cmd.Parameters.AddWithValue("id", l.Id);

                cmd.ExecuteNonQuery();
            }
        }
    }
}
