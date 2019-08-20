using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class RoleDAL
    {
        #region Direct Properties
        public int RoleID { get; set; }
        public string RoleName { get; set; }
        #endregion

        #region Indirect Properties
        // this class doesnt have any indirect properties
        // because the roles table doesnt have any foreign keys
        #endregion 

        public override string ToString()
        {
            return $"RoleID: {RoleID,5} RoleName:{RoleName}";
        }
    }
}
