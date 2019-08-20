using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace DataAccessLayer
{
    public class UserMapper : Mapper
    {
        int OffsetToUserID; // expected to be 0
        int OffsetToUserName; // expected to be 1
        int OffsetToEmail; // expected to be 2
        int OffsetToRoleID; // expected to be 3
        int OffsetToHash; // expected to be 4
        int OffsetToSalt; // expected to be 5
        int OffsetToRoleName; // expected to be 6

        public UserMapper(SqlDataReader reader)
        {
            OffsetToUserID = reader.GetOrdinal("UserID");
            Assert(0 == OffsetToUserID, $"UserID is {OffsetToUserID} not 0 as expected");
            OffsetToUserName = reader.GetOrdinal("UserName");
            Assert(1 == OffsetToUserName, $"UserName is {OffsetToUserName} not 1 as expected");
            OffsetToEmail = reader.GetOrdinal("Email");
            Assert(2 == OffsetToEmail, $"Email is {OffsetToEmail} not 2 as expected");
            OffsetToRoleID = reader.GetOrdinal("RoleID");
            Assert(3 == OffsetToRoleID, $"RoleID is {OffsetToRoleID} not 3 as expected");
            OffsetToHash = reader.GetOrdinal("Hash");
            Assert(4 == OffsetToHash, $"Hash is {OffsetToHash} not 4 as expected");
            OffsetToSalt = reader.GetOrdinal("Salt");
            Assert(5 == OffsetToSalt, $"Salt is {OffsetToSalt} not 5 as expected");
            OffsetToRoleName = reader.GetOrdinal("RoleName");
            Assert(6 == OffsetToRoleName, $"RoleName is {OffsetToRoleName} not 6 as expected");

        }

        public UserDAL UserFromReader(SqlDataReader reader)
        {
            UserDAL proposedReturnValue = new UserDAL();
            proposedReturnValue.UserID = reader.GetInt32(OffsetToUserID);
            proposedReturnValue.UserName = reader.GetString(OffsetToUserName);
            proposedReturnValue.Email = reader.GetString(OffsetToEmail);
            proposedReturnValue.RoleID = reader.GetInt32(OffsetToRoleID);
            proposedReturnValue.Hash = reader.GetString(OffsetToHash);
            proposedReturnValue.Salt = reader.GetString(OffsetToSalt);
            proposedReturnValue.RoleName = reader.GetString(OffsetToRoleName);
            return proposedReturnValue;
        }
    }


}
