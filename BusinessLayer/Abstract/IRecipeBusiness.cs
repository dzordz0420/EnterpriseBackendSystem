using Core.Interfaces;
using Core.Result;
using Entity;
using Entity.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface IRecipeBusiness : IBusiness<Recipe>
    {
        RecipeDetailsDTO GetDetailsById(int id);
    }
}