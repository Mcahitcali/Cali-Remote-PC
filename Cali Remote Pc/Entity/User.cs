
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace Cali_Remote_Pc.Entity
{
    public class User:IdentityUser
    {
        public string Name { get; set; }
        public string LastName { get; set; }

        public virtual ICollection<IdentityUserClaim<string>> Claims { get; set; }
        public virtual ICollection<IdentityUserLogin<string>> Logins { get; set; }
        public virtual ICollection<IdentityUserToken<string>> Tokens { get; set; }
        public virtual ICollection<IdentityUserRole<string>> UserRoles { get; set; }

        public virtual ICollection<Action> Actions { get; set; }
        public virtual ICollection<Client> Clients { get; set; }
    }
}
