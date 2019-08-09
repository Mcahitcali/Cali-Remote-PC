using Cali_Remote_Pc.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Action = Cali_Remote_Pc.Entity.Action;

namespace Cali_Remote_Pc.Models
{
    public class ApiModel
    {
        public Action CurrentAction { get; set; }
        public Client MyProperty { get; set; }
    }
}
