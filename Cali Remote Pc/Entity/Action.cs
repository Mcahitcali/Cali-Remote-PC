using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Cali_Remote_Pc.Entity
{
    public class Action:BaseEntity
    {
        public string Command { get; set; }
        public TimeSpan Timeout { get; set; }
    }
}
