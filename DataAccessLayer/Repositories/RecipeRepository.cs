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
    public class RecipeRepository : IRecipeRepository
    {
        private string GetStringSafe(SqlDataReader reader, int index)
        {
            return reader.IsDBNull(index) ? null : reader.GetString(index);
        }

        private decimal? GetDecimalSafe(SqlDataReader reader, int index)
        {
            return reader.IsDBNull(index) ? null : reader.GetDecimal(index);
        }
        public bool Add(Recipe item)
        {
            using (var sqlConnection = new SqlConnection(ConnectionBase.ConnectionString))
            {
                sqlConnection.Open();
                using (var sqlCommand = sqlConnection.CreateCommand())
                {
                    sqlCommand.CommandText = @"INSERT INTO Recipes (name, shortDescription, description, time, category, averageRating, autor, recipePicture) VALUES (@name, @shortDescription, @description, @time, @category, @averageRating, @author, @recipePicture)";
                    sqlCommand.Parameters.AddWithValue("@name", item.Name);
                    sqlCommand.Parameters.AddWithValue("@shortDescription", item.ShortDescription);
                    sqlCommand.Parameters.AddWithValue("@description", item.Description);
                    sqlCommand.Parameters.AddWithValue("@time", item.Time);
                    sqlCommand.Parameters.AddWithValue("@category", item.Category);
                    sqlCommand.Parameters.AddWithValue("@averageRating", (object)item.AverageRating ?? DBNull.Value);
                    sqlCommand.Parameters.AddWithValue("@author", item.Author);
                    sqlCommand.Parameters.AddWithValue("@recipePicture", (object)item.RecipePicture ?? DBNull.Value);

                    int result = sqlCommand.ExecuteNonQuery();
                    return result > 0;
                }
            }
        }

        public bool Delete(Recipe item)
        {
            using (var sqlConnection = new SqlConnection(ConnectionBase.ConnectionString))
            {
                sqlConnection.Open();
                using (var sqlCommand = sqlConnection.CreateCommand())
                {
                    sqlCommand.CommandText = "DELETE FROM Recepti WHERE idRecipe = @id";
                    sqlCommand.Parameters.AddWithValue("@id", item.IdRecipe);

                    int result = sqlCommand.ExecuteNonQuery();
                    return result > 0;
                }
            }
        }

        public List<Recipe> GetAll()
        {
            var list = new List<Recipe>();

            using (var sqlConnection = new SqlConnection(ConnectionBase.ConnectionString))
            {
                sqlConnection.Open();
                using (var sqlCommand = sqlConnection.CreateCommand())
                {
                    sqlCommand.CommandText = "SELECT * FROM Recipes";
                    using (var reader = sqlCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var recipe = new Recipe
                            {
                                IdRecipe = reader.GetInt32(0),
                                Name = GetStringSafe(reader, 1),
                                ShortDescription = GetStringSafe(reader, 2),
                                Description = GetStringSafe(reader, 3),
                                Time = reader.GetInt32(4),
                                Category = GetStringSafe(reader, 5),
                                AverageRating = GetDecimalSafe(reader, 6),
                                Author = reader.GetInt32(7),
                                RecipePicture = GetStringSafe(reader, 8),
                            };
                            list.Add(recipe);
                        }
                    }
                }
            }

            return list;
        }

        public List<Recipe> GetByIds(List<int> ids)
        {
            var recipes = new List<Recipe>();
            if (ids == null || ids.Count == 0)
                return recipes;

            using var sqlConnection = new SqlConnection(ConnectionBase.ConnectionString);
            sqlConnection.Open();

            var parameters = string.Join(", ", ids.Select((id, index) => $"@id{index}"));

            using var sqlCommand = sqlConnection.CreateCommand();
            sqlCommand.CommandText = $"SELECT * FROM Recipes WHERE idRecipe IN ({parameters})";

            for (int i = 0; i < ids.Count; i++)
                sqlCommand.Parameters.AddWithValue($"@id{i}", ids[i]);

            using var reader = sqlCommand.ExecuteReader();
            while (reader.Read())
            {
                var recipe = new Recipe
                {
                    IdRecipe = reader.GetInt32(reader.GetOrdinal("idRecepta")),
                    Name= reader.GetString(reader.GetOrdinal("Naziv")),
                    ShortDescription = reader.GetString(reader.GetOrdinal("KratakOpis")),
                    Time = reader.GetInt32(reader.GetOrdinal("Vreme")),
                    Category = reader.GetString(reader.GetOrdinal("Kategorija")),
                    RecipePicture = reader.GetString(reader.GetOrdinal("SlikaRecepta")),
                };
                recipes.Add(recipe);
            }

            return recipes;
        }

        public Recipe GetRecipeById(int id)
        {
            Recipe recipe = null;

            using (var sqlConnection = new SqlConnection(ConnectionBase.ConnectionString))
            {
                sqlConnection.Open();
                using (var sqlCommand = sqlConnection.CreateCommand())
                {
                    sqlCommand.CommandText = "SELECT * FROM Recipes WHERE idRecipe = @id";
                    sqlCommand.Parameters.AddWithValue("@id", id);

                    using (var reader = sqlCommand.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            recipe = new Recipe
                            {
                                IdRecipe = reader.GetInt32(0),
                                Name = GetStringSafe(reader, 1),
                                ShortDescription = GetStringSafe(reader, 2),
                                Description = GetStringSafe(reader, 3),
                                Time = reader.GetInt32(4),
                                Category = GetStringSafe(reader, 5),
                                AverageRating = GetDecimalSafe(reader, 6),
                                Author = reader.GetInt32(7),
                                RecipePicture = GetStringSafe(reader, 8),
                            };
                        }
                    }
                }
            }

            return recipe;
        }

        public int NumberOfByUserId(int id)
        {
            using (var sqlConnection = new SqlConnection(ConnectionBase.ConnectionString))
            {
                sqlConnection.Open();
                using (var sqlCommand = sqlConnection.CreateCommand())
                {
                    sqlCommand.CommandText = "SELECT COUNT(*) FROM Recipes WHERE autor = @idUser";
                    sqlCommand.Parameters.AddWithValue("@idUser", id);
                    return (int)sqlCommand.ExecuteScalar();
                }
            }
        }

        public bool Update(Recipe item)
        {
            using (var sqlConnection = new SqlConnection(ConnectionBase.ConnectionString))
            {
                sqlConnection.Open();
                using (var sqlCommand = sqlConnection.CreateCommand())
                {
                    sqlCommand.CommandText = @"UPDATE Recipes SET name = @name, shortDescription = @shortDescription, description = @description, time = @time, category = @category, averageRating = @averageRating, author = @author, recipePicture = @recipePicture WHERE idRecipe = @id";

                    sqlCommand.Parameters.AddWithValue("@name", item.Name);
                    sqlCommand.Parameters.AddWithValue("@shortDescription", item.ShortDescription);
                    sqlCommand.Parameters.AddWithValue("@description", item.Description);
                    sqlCommand.Parameters.AddWithValue("@time", item.Time);
                    sqlCommand.Parameters.AddWithValue("@category", item.Category);
                    sqlCommand.Parameters.AddWithValue("@averageRating", (object)item.AverageRating ?? DBNull.Value);
                    sqlCommand.Parameters.AddWithValue("@author", item.Author);
                    sqlCommand.Parameters.AddWithValue("@recipePicture", (object)item.RecipePicture ?? DBNull.Value);

                    int result = sqlCommand.ExecuteNonQuery();
                    return result > 0;
                }
            }
        }
    }
}
