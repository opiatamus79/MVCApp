using System;
using MVCApp.DataAccess;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;



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
                Email = user.Email;
                FirstName = user.FirstName;
                LastName = user.LastName;
                Roles = user.Roles;
            }
        }
    }
