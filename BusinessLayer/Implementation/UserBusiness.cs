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

            if (userRepository.Add(item) == true)
            {
                return new ResultWrapper
                {
                    Message = "Successfully added user",
                    Success = true
                };
            }
            else
            {
                return new ResultWrapper
                {
                    Message = "Error user not added",
                    Success = false
                };
            }
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
            else
            {
                if (userRepository.Delete(item) == true)
                {
                    return new ResultWrapper
                    {
                        Message = "Successuflly deleted user account",
                        Success = true
                    };
                }
                else
                {
                    return new ResultWrapper
                    {
                        Message = "Error while attempting to delete user account",
                        Success = false
                    };
                }
            }
        }

        public List<User> GetAll()
        {
            return userRepository.GetAll();
        }

        public User GetByEmail(string email)
        {
            var x = userRepository.GetUserByEmail(email);
            if (x == null) return new User();
            return x;
        }

        public User GetById(int id)
        {
            var x = userRepository.GetUserById(id);
            if (x == null) return new User();
            return x;
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

            if (HashingHelper.VerifyHash(loginDTO.Password, korisnik.Password))
            {
                return new ResultWrapper
                {
                    Success = true,
                    Message = "Login successful"
                };
            }
            else
            {
                return new ResultWrapper
                {
                    Success = false,
                    Message = "Incorrect email or password"
                };
            }
        }

        public ResultWrapper Update(User item)
        {
            return userRepository.Update(item) == true ?
                new ResultWrapper
                {
                    Message = "Successfuly updated user account",
                    Success = true
                } : new ResultWrapper
                {
                    Message = "Error while updating user account",
                    Success = false
                };
        }
    }
}
