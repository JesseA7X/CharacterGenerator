using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class UserDAL
    {
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
            return $"User: UserID:{UserID,5} UserName:{UserName} Email:{Email} RoleID:{RoleID} Hash:{Hash} Salt:{Salt} RoleName:{RoleName}";
        }
    }
}
