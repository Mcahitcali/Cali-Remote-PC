using Cali_Remote_Pc.Entity;
using Cali_Remote_Pc.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cali_Remote_Pc.Models
{
    public class IndexModel
    {
        public IEnumerable<Entity.Action> viewActions { get; set; }

        public IEnumerable<Client> viewClients { get; set; }

        public string Message { get; set; }
    }
}
