using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class UserDAL
    {
        // the region tags arent needed but they were used as a learning tool to help visualize the shape of the code
        #region Direct Properties
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public int RoleID { get; set; }
        public string Hash { get; set; }
        public string Salt { get; set; }
        #endregion

        #region Indirect Properties
        public string RoleName { get; set; }
        #endregion

        public override string ToString()
        {
            // this is only used by a console app for testing not used by mvc
            return $"User: UserID:{UserID,5} UserName:{UserName} Email:{Email} RoleID:{RoleID} Hash:{Hash} Salt:{Salt} RoleName:{RoleName}";
        }
    }
}
