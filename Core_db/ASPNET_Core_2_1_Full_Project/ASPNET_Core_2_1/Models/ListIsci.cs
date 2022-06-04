using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASPNET_Core_2_1.Models
{
    public class ListIsci
    {
        public List<Isci> LI { get; set; }
        public Isci ISC { get; set; }

        public ListIsci()
        {
            LI = new List<Isci>();
        }
    }
}
