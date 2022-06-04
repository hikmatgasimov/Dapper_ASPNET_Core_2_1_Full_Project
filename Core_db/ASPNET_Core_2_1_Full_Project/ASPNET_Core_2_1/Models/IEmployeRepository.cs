using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASPNET_Core_2_1.Models
{
    public interface IEmployeRepository
    {
        List<Isci> GetAll();
        //List<Cinsler> GetCins();
    }
}
