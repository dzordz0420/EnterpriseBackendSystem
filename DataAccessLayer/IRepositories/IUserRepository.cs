using Core.Interfaces;
using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.IRepositories
{
    public interface IUserRepository:IRepository<User>
    {
        User GetUserByEmail(string email);
        User GetUserById(int id);
    }
}
