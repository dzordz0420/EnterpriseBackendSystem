using Entity;
using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface IUserBusiness : IBusiness<User>
    {
        User GetByEmail(string email);
    }
}
