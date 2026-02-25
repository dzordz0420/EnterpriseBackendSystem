using DataAccessLayer.IRepositories;
using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public class RecipeRepository : IRecipeRepository
    {
        public bool Add(Recipe item)
        {
            throw new NotImplementedException();
        }

        public int AddAndGetId(Recipe item)
        {
            throw new NotImplementedException();
        }

        public bool Delete(Recipe item)
        {
            throw new NotImplementedException();
        }

        public List<Recipe> GetAll()
        {
            throw new NotImplementedException();
        }

        public List<Recipe> GetByIds(List<int> ids)
        {
            throw new NotImplementedException();
        }

        public Recipe GetRecipeById(int id)
        {
            throw new NotImplementedException();
        }

        public int NumberOfByUserId(int id)
        {
            throw new NotImplementedException();
        }

        public bool Update(Recipe item)
        {
            throw new NotImplementedException();
        }
    }
}
