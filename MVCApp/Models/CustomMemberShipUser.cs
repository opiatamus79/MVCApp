using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using MVCApp.Models.DataAccess;

namespace MVCApp.Models
{
    namespace MVCApp.CustomAuthentication
    {
        public class CustomMembershipUser : MembershipUser
        {
            #region User Properties  

            public int UserId { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public ICollection<Role> Roles { get; set; }

            #endregion

            public CustomMembershipUser(Employee user) : base("CustomMembership", user.UserName, user.ID, user.Email, string.Empty, string.Empty, true, false, DateTime.Now, DateTime.Now, DateTime.Now, DateTime.Now, DateTime.Now)
            {
                UserId = user.ID;
                FirstName = user.FirstName;
                LastName = user.LastName;
                Roles = user.Roles;
            }
        }
    }
}