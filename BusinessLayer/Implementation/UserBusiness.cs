using BusinessLayer.Abstract;
using Core.Result;
using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Implementation
{
    public class UserBusiness : IUserBusiness
    {
        public ResultWrapper Add(User item)
        {
            throw new NotImplementedException();
        }

        public ResultWrapper Delete(User item)
        {
            throw new NotImplementedException();
        }

        public List<User> GetAll()
        {
            throw new NotImplementedException();
        }

        public User GetByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public User GetById(int id)
        {
            throw new NotImplementedException();
        }

        public ResultWrapper Update(User item)
        {
            throw new NotImplementedException();
        }
    }
}
