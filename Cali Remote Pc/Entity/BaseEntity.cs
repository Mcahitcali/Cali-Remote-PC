using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cali_Remote_Pc.Entity
{

    //Base prop for Action and Client entities
    public class BaseEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool State { get; set; }

        public string UserId { get; set; }
        public User User { get; set; }
    }
}
