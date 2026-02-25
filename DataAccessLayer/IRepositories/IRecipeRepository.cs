using Core.Interfaces;
using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.IRepositories
{
    public interface IRecipeRepository:IRepository<Recipe>
    {
        Recipe GetRecipeById(int id);
        int NumberOfByUserId(int id);

        int AddAndGetId(Recipe item);
        List<Recipe> GetByIds(List<int> ids);
    }
}
