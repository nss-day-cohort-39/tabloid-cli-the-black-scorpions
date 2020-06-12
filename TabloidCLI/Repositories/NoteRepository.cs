using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Text;
using TabloidCLI.Models;
using TabloidCLI.UserInterfaceManagers;

namespace TabloidCLI.Repositories
{
    class NoteRepository : DatabaseConnector, IRepository<Note>
    {
        public NoteRepository(string connectionString) : base(connectionString) { }

        public List<Note> GetAll()
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT n.Id as NoteId,
                                        n.Title as NoteTitle,
                                        n.Content,
                                        n.CreateDateTime,
                                        p.Title as PostTitle
                                        FROM Note n
                                        JOIN Post p on n.PostId = p.Id";
                    List<Note> notes = new List<Note>();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Note note = new Note()
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("NoteId")),
                            Title = reader.GetString(reader.GetOrdinal("NoteTitle")),
                            Content = reader.GetString(reader.GetOrdinal("Content")),
                            CreateDateTime = reader.GetDateTime(reader.GetOrdinal("CreateDateTime")),
                            Post = new Post
                            {
                                Title = reader.GetString(reader.GetOrdinal("PostTitle")),
                            }
                        };
                        notes.Add(note);
                    }
                    reader.Close();
                    return notes;
                }
            }
        }

        public Note Get(int id)
        {
            throw new NotImplementedException();
        }


        public void Insert(Note note)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"INSERT INTO Note (Title, Content, CreateDateTime, PostId )
                                                     VALUES (@title, @content, @createDateTime, @postId)";
                    cmd.Parameters.AddWithValue("@title", note.Title);
                    cmd.Parameters.AddWithValue("@content", note.Content);
                    cmd.Parameters.AddWithValue("@createDateTime", note.CreateDateTime);
                    cmd.Parameters.AddWithValue("@postId", note.Post.Id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        
        public void Update(Note note)
        {
            throw new NotImplementedException();
        }


        public void Delete(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"DELETE FROM Note WHERE Id = @id";
                    cmd.Parameters.AddWithValue("@id", id);

                    cmd.ExecuteNonQuery();
                }
            }
        }
        
    }
}
