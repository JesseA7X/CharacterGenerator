using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using DataAccessLayer;

namespace BusinessLogicLayer
{
    public class UserBLL
    {
        public UserBLL()
        {

        }
        public UserBLL(UserDAL dal)
        {
            this.UserID = dal.UserID;
            this.UserName = dal.UserName;
            this.Email = dal.Email;
            this.RoleID = dal.RoleID;
            this.Hash = dal.Hash;
            this.Salt = dal.Salt;
            this.RoleName = dal.RoleName;
        }
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
            return $"UserID: {UserID} UserName: {UserName} Email: {Email} RoleID: {RoleID} Hash: {Hash} Salt: {Salt} RoleName: {RoleName}";
        }
    }
}
