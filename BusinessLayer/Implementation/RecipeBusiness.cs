using BusinessLayer.Abstract;
using Core.Result;
using DataAccessLayer.IRepositories;
using DataAccessLayer.Repositories;
using Entity;
using Entity.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Implementation
{
    public class RecipeBusiness : IRecipeBusiness
    {
        public readonly IUserRepository userRepository = new UserRepository();
        public readonly IRecipeRepository recipeRepository = new RecipeRepository();

        public RecipeBusiness(IUserRepository userRepository, IRecipeRepository recipeRepository)
        {
            this.userRepository = userRepository;
            this.recipeRepository = recipeRepository;
        }

        public ResultWrapper Add(Recipe item)
        {
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(item, null, null);

            if (!Validator.TryValidateObject(item, validationContext, validationResults, true))
            {
                return new ResultWrapper
                {
                    Message = string.Join(", ", validationResults.Select(vr => vr.ErrorMessage)),
                    Success = false
                };
            }
            return recipeRepository.Add(item)
                ? new ResultWrapper{Message = "Successfuly added recipe", Success = true}
                : new ResultWrapper{Message = "Error while attempting to add a recipe",Success = false};
        }

        public ResultWrapper Delete(Recipe item)
        {
            return recipeRepository.Delete(item)
               ? new ResultWrapper { Message = "Successfuly deleted recipe", Success = true }
               : new ResultWrapper { Message = "Error while attempting to delete a recipe", Success = false };
        }

        public List<Recipe> GetAll()
        {
            return recipeRepository.GetAll();
        }

        public Recipe GetById(int id)
        {
            var x = recipeRepository.GetRecipeById(id);
            return x ?? new Recipe();
        }

        public RecipeDetailsDTO GetDetailsById(int id)
        {
            var recept = recipeRepository.GetRecipeById(id);
            if (recept == null || recept.IdRecipe == 0)
                return null;

            var author = userRepository.GetUserById(recept.Author);
            return new RecipeDetailsDTO
            {
                IdRecipe = recept.IdRecipe,
                Name = recept.Name,
                Description = recept.Description,
                Time = recept.Time,
                Category = recept.Category,
                RecipePicture = recept.RecipePicture,
                Author = new AuthorDTO
                {
                    Id = author.Id,
                    Name = author.Name,
                    LastName = author.LastName
                }
            };
        }

        public ResultWrapper Update(Recipe item)
        {
            return recipeRepository.Update(item)
               ? new ResultWrapper { Message = "Successfuly updated recipe", Success = true }
               : new ResultWrapper { Message = "Error while attempting to update a recipe", Success = false };
        }
    }
}
