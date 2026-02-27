using BusinessLayer.Abstract;
using Core.Result;
using Core.Security;
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
    public class UserBusiness : IUserBusiness
    {
        private readonly IUserRepository userRepository;
        private readonly IRecipeRepository recipeRepository;
        public UserBusiness(IUserRepository userRepository, IRecipeRepository recipeRepository) 
        {
            this.userRepository = userRepository;
            this.recipeRepository = recipeRepository;
        }
        public ResultWrapper Add(User item)
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
            return userRepository.Add(item)
                ? new ResultWrapper { Message = "Successfuly added user", Success = true }
                : new ResultWrapper { Message = "Error while attempting to add a user", Success = false };
        }

        public ResultWrapper Delete(User item)
        {
            if (recipeRepository.NumberOfByUserId(item.Id) > 0)
            {
                return new ResultWrapper
                {
                    Message = "User account is coonected with table recipies",
                    Success = false
                };
            }
            return userRepository.Delete(item)
                ? new ResultWrapper { Message = "Successfuly deleted user", Success = true }
                : new ResultWrapper { Message = "Error while attempting to delete a user", Success = false };
        }

        public List<User> GetAll()
        {
            return userRepository.GetAll();
        }

        public User GetByEmail(string email)
        {
            var x = userRepository.GetUserByEmail(email);
            return x ?? new User();
        }

        public User GetById(int id)
        {
            var x = userRepository.GetUserById(id);
            return x ?? new User();
        }

        public ResultWrapper Login(LoginDTO loginDTO)
        {
            User korisnik = userRepository.GetUserByEmail(loginDTO.Email);
            Console.WriteLine(korisnik);
            if (korisnik == null)
            {
                return new ResultWrapper
                {
                    Success = false,
                    Message = "Incorrect email or password"
                };
            }
            return HashingHelper.VerifyHash(loginDTO.Password, korisnik.Password)
                ? new ResultWrapper { Message = "Login successful", Success = true }
                : new ResultWrapper { Message = "Incorrect email or password", Success = false };
        }

        public ResultWrapper Update(User item)
        {
            return userRepository.Update(item)
                ? new ResultWrapper { Message = "Successfuly updated user", Success = true }
                : new ResultWrapper { Message = "Error while attempting to update a user", Success = false };
        }
    }
}
