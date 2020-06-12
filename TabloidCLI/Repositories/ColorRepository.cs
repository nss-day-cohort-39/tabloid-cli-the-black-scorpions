using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Text;
using TabloidCLI.Models;

namespace TabloidCLI.Repositories
{
    class ColorRepository : DatabaseConnector
    {
        public ColorRepository(string connectionString) : base(connectionString) { }

        public Color Get()
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT Color
                                          FROM Color";

                    Color color = new Color();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        color.ActiveColor = reader.GetString(reader.GetOrdinal("Color"));    
                    }

                    reader.Close();

                    return color;
                }
            }
        }

        public void Update(Color color)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"UPDATE Color 
                                           SET Color = @color
                                         WHERE id = 1";
                    cmd.Parameters.AddWithValue("@color", color.ActiveColor);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
