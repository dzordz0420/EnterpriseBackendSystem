using BusinessLayer.Abstract;
using Core.Result;
using Entity;
using Entity.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Implementation
{
    public class RecipeBusiness : IRecipeBusiness
    {
        public ResultWrapper Add(Recipe item)
        {
            throw new NotImplementedException();
        }

        public ResultWrapper Delete(Recipe item)
        {
            throw new NotImplementedException();
        }

        public List<Recipe> GetAll()
        {
            throw new NotImplementedException();
        }

        public Recipe GetById(int id)
        {
            throw new NotImplementedException();
        }

        public RecipeDetailsDTO GetDetailsById(int id)
        {
            throw new NotImplementedException();
        }

        public ResultWrapper Update(Recipe item)
        {
            throw new NotImplementedException();
        }
    }
}
