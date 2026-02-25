using Core.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IBusiness<T>
    {
        ResultWrapper Add(T item);
        ResultWrapper Update(T item);
        ResultWrapper Delete(T item);
        List<T> GetAll();
        T GetById(int id);
    }
}
