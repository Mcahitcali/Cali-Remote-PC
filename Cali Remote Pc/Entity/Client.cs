using Cali_Remote_Pc.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Cali_Remote_Pc.Entity
{
    public class Client:BaseEntity
    {
        public string MachineName { get; set; }
        public string Platform { get; set; }
        public long SystemUUID { get; set; }
    }

}