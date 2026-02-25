using DataAccessLayer.Constant;
using DataAccessLayer.IRepositories;
using Entity;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public class UserRepository : IUserRepository
    {
        private string GetStringSafe(SqlDataReader reader, int index)
        {
            return reader.IsDBNull(index) ? "" : reader.GetString(index);
        }

        public bool Add(User item)
        {
            using (SqlConnection sqlConnection = new SqlConnection())
            {
                sqlConnection.ConnectionString = ConnectionBase.ConnectionString;
                sqlConnection.Open();

                SqlCommand sqlCommand = sqlConnection.CreateCommand();
                sqlCommand.CommandText = @"
                INSERT INTO Users(username, name, last_name, email, title, password, photo)
                VALUES (@username, @name, @lastName, @email, @title, @password, @photo);";

                sqlCommand.Parameters.AddWithValue("@username", item.Username);
                sqlCommand.Parameters.AddWithValue("@name", item.Name);
                sqlCommand.Parameters.AddWithValue("@lastName", item.LastName);
                sqlCommand.Parameters.AddWithValue("@email", item.Email);
                sqlCommand.Parameters.AddWithValue("@title", item.Title);
                sqlCommand.Parameters.AddWithValue("@password", item.Password);
                sqlCommand.Parameters.AddWithValue("@photo", item.Photo);

                int result = sqlCommand.ExecuteNonQuery();
                return result > 0;
            }
        }

        public bool Delete(User item)
        {
            using (var sqlConnection = new SqlConnection(ConnectionBase.ConnectionString))
            {
                sqlConnection.Open();
                using (var sqlCommand = sqlConnection.CreateCommand())
                {
                    sqlCommand.CommandText = "DELETE FROM Users WHERE id = @id";
                    sqlCommand.Parameters.AddWithValue("@id", item.Id);

                    int result = sqlCommand.ExecuteNonQuery();
                    return result > 0;
                }
            }
        }

        public List<User> GetAll()
        {
            var list = new List<User>();

            using (var sqlConnection = new SqlConnection(ConnectionBase.ConnectionString))
            {
                sqlConnection.Open();
                using (var sqlCommand = sqlConnection.CreateCommand())
                {
                    sqlCommand.CommandText = "SELECT * FROM Users";
                    using (var reader = sqlCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var user = new User
                            {
                                Id = reader.GetInt32(0),
                                Username = GetStringSafe(reader, 1),
                                Name = GetStringSafe(reader, 2),
                                LastName = GetStringSafe(reader, 3),
                                Email = GetStringSafe(reader, 4),
                                Title = GetStringSafe(reader, 5),
                                Password = GetStringSafe(reader, 6),
                                Photo = GetStringSafe(reader, 7)
                            };
                            list.Add(user);
                        }
                    }
                }
            }

            return list;
        }

        public User GetUserByEmail(string email)
        {
            User user = null;

            using (var sqlConnection = new SqlConnection(ConnectionBase.ConnectionString))
            {
                sqlConnection.Open();
                using (var sqlCommand = sqlConnection.CreateCommand())
                {
                    sqlCommand.CommandText = "SELECT * FROM Users WHERE email = @email";
                    sqlCommand.Parameters.AddWithValue("@email", email);

                    using (var reader = sqlCommand.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            user = new User
                            {
                                Id = reader.GetInt32(0),
                                Username = GetStringSafe(reader, 1),
                                Name = GetStringSafe(reader, 2),
                                LastName = GetStringSafe(reader, 3),
                                Email = GetStringSafe(reader, 4),
                                Title = GetStringSafe(reader, 5),
                                Password = GetStringSafe(reader, 6),
                                Photo = GetStringSafe(reader, 7)
                            };
                        }
                    }
                }
            }

            return user;
        }

        public User GetUserById(int id)
        {
            User user = null;

            using (var sqlConnection = new SqlConnection(ConnectionBase.ConnectionString))
            {
                sqlConnection.Open();
                using (var sqlCommand = sqlConnection.CreateCommand())
                {
                    sqlCommand.CommandText = "SELECT * FROM Users WHERE id = @id";
                    sqlCommand.Parameters.AddWithValue("@id", id);

                    using (var reader = sqlCommand.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            user = new User
                            {
                                Id = reader.GetInt32(0),
                                Username = GetStringSafe(reader, 1),
                                Name = GetStringSafe(reader, 2),
                                LastName = GetStringSafe(reader, 3),
                                Email = GetStringSafe(reader, 4),
                                Title = GetStringSafe(reader, 5),
                                Password = GetStringSafe(reader, 6),
                                Photo = GetStringSafe(reader, 7)
                            };
                        }
                    }
                }
            }

            return user;
        }

        public bool Update(User item)
        {
            using (var sqlConnection = new SqlConnection(ConnectionBase.ConnectionString))
            {
                sqlConnection.Open();
                using (var sqlCommand = sqlConnection.CreateCommand())
                {
                    sqlCommand.CommandText = @"UPDATE Users SET username = @username, name = @name, last_name = @lastName, email = @email, title = @title, password = @password, photo = @photo, slikarecepta = @slikarecepta WHERE id = @id";
                    sqlCommand.Parameters.AddWithValue("@username", item.Username);
                    sqlCommand.Parameters.AddWithValue("@name", item.Name);
                    sqlCommand.Parameters.AddWithValue("@lastName", item.LastName);
                    sqlCommand.Parameters.AddWithValue("@vreme", item.Email);
                    sqlCommand.Parameters.AddWithValue("@title", item.Title);
                    sqlCommand.Parameters.AddWithValue("@password", item.Password );
                    sqlCommand.Parameters.AddWithValue("@photo", (object)item.Photo ?? DBNull.Value);
                    sqlCommand.Parameters.AddWithValue("@id", item.Id);

                    int result = sqlCommand.ExecuteNonQuery();
                    return result > 0;
                }
            }
        }
    }
}
