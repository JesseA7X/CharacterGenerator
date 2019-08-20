using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Lumberjack;

namespace DataAccessLayer
{
    public class ContextDAL : IDisposable
    {
        #region Context Stuff
        SqlConnection _connection;
        public ContextDAL()
        {
            _connection = new SqlConnection();
        }
        public string ConnectionString
        {
            get { return _connection.ConnectionString; }
            set { _connection.ConnectionString = value; }
        }
        void EnsureConnected()
        {
            if (_connection.State == System.Data.ConnectionState.Open)
            {
                // there is nothing to do if i am connected
            }
            else if (_connection.State == System.Data.ConnectionState.Broken)
            {
                // if the state is broken close then open
                _connection.Close();
                _connection.Open();
            }
            else if (_connection.State == System.Data.ConnectionState.Closed)
            {
                // if the state is closed open it
                _connection.Open();
            }
            else
            {
                // other states need no processing
            }
        }

        bool Log(Exception ex)
        {
            Console.WriteLine(ex.ToString());
            Logger.Log(ex);
            return false;
        }
        public void Dispose()
        {
            _connection.Dispose();
        }
        #endregion

        #region Role Stuff
        public RoleDAL FindRoleByRoleID(int RoleID)
        {
            RoleDAL ProposedReturnValue = null;
            try
            {
                EnsureConnected();
                using (SqlCommand command
                    = new SqlCommand("FindRoleByRoleID", _connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@RoleID", RoleID);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        RoleMapper m = new RoleMapper(reader);
                        int count = 0;
                        while (reader.Read())
                        {
                            ProposedReturnValue = m.RoleFromReader(reader);
                            count++;
                        }
                        if (count > 1)
                        {
                            throw new
                              Exception($"Found more than 1 Role with key {RoleID}");

                        }
                    }
                }
            }
            catch (Exception ex) when (Log(ex))
            {

            }
            return ProposedReturnValue;

        }

        public List<RoleDAL> GetRoles(int skip, int take)
        {
            List<RoleDAL> proposedReturnValue = new List<RoleDAL>();
            try
            {
                EnsureConnected();
                using (SqlCommand command = new SqlCommand("GetRoles", _connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Skip", skip);
                    command.Parameters.AddWithValue("@Take", take);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        RoleMapper m = new RoleMapper(reader);
                        while (reader.Read())
                        {
                            RoleDAL r = m.RoleFromReader(reader);
                            proposedReturnValue.Add(r);
                        }
                    }
                }
            }
            catch (Exception ex) when (Log(ex))
            {

            }
            return proposedReturnValue;
        }

        public int ObtainRoleCount()
        {
            int proposedReturnValue = -1;
            try
            {
                EnsureConnected();
                using (SqlCommand command = new SqlCommand("ObtainRoleCount", _connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    object answer = command.ExecuteScalar();
                    proposedReturnValue = (int)answer;
                }
            }
            catch (Exception ex) when (Log(ex))
            {

            }

            return proposedReturnValue;
        }

        public int CreateRole(string RoleName)
        {
            int proposedReturnValue = -1;
            try
            {
                EnsureConnected();
                using (SqlCommand command = new SqlCommand("CreateRole", _connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@RoleName", RoleName);
                    command.Parameters.AddWithValue("@RoleID", 0);
                    command.Parameters["@RoleID"].Direction = System.Data.ParameterDirection.Output;
                    command.ExecuteNonQuery();
                    proposedReturnValue =
                        Convert.ToInt32(command.Parameters["@RoleID"].Value);
                }
            }
            catch (Exception ex) when (Log(ex))
            {

            }
            return proposedReturnValue;
        }

        public void JustUpdateRole(int RoleID, string RoleName)
        {

            try
            {
                EnsureConnected();
                using (SqlCommand command = new SqlCommand("JustUpdateRole", _connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@RoleName", RoleName);
                    command.Parameters.AddWithValue("@RoleID", RoleID);

                    command.ExecuteNonQuery();

                }
            }
            catch (Exception ex) when (Log(ex))
            {

            }

        }

        public void DeleteRole(int RoleID)
        {

            try
            {
                EnsureConnected();
                using (SqlCommand command = new SqlCommand("DeleteRole", _connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@RoleID", RoleID);

                    command.ExecuteNonQuery();

                }
            }
            catch (Exception ex) when (Log(ex))
            {

            }

        }
        #endregion

        #region User Stuff
        public UserDAL FindUserByUserID(int UserID)
        {
            UserDAL proposedReturnValue = null;
            try
            {
                EnsureConnected();
                using (SqlCommand command = new SqlCommand("FindUserByUserID", _connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@UserID", UserID);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        UserMapper m = new UserMapper(reader);
                        int count = 0;
                        while (reader.Read())
                        {
                            proposedReturnValue = m.UserFromReader(reader);
                            count++;
                        }
                        if (count > 1)
                        {
                            throw new Exception($"Found more than 1 User with key {UserID}");
                        }
                    }
                }
            }
            catch (Exception ex) when (Log(ex))
            {

            }
            return proposedReturnValue;
        }

        public UserDAL FindUserByEmail(string Email)
        {
            UserDAL proposedReturnValue = null;
            try
            {
                EnsureConnected();
                using (SqlCommand command = new SqlCommand("FindUserByEmail", _connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Email", Email);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        UserMapper m = new UserMapper(reader);
                        int count = 0;
                        while (reader.Read())
                        {
                            proposedReturnValue = m.UserFromReader(reader);
                            count++;
                        }
                        if (count > 1)
                        {
                            throw new Exception($"Found more than 1 User with key       {Email}");
                        }
                    }
                }
            }
            catch (Exception ex) when (Log(ex))
            {

            }
            return proposedReturnValue;
        }

        public UserDAL FindUserByUserName(string UserName)
        {
            UserDAL proposedReturnValue = null;
            try
            {
                EnsureConnected();
                using (SqlCommand command = new SqlCommand("FindUserByUserName", _connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@UserName", UserName);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        UserMapper m = new UserMapper(reader);
                        int count = 0;
                        while (reader.Read())
                        {
                            proposedReturnValue = m.UserFromReader(reader);
                            count++;
                        }
                        if (count > 1)
                        {
                            throw new Exception($"Found more than 1 User with key       {UserName}");
                        }
                    }
                }
            }
            catch (Exception ex) when (Log(ex))
            {

            }
            return proposedReturnValue;
        }

        public List<UserDAL> GetUsers(int skip, int take)
        {
            List<UserDAL> proposedReturnValue = new List<UserDAL>();
            try
            {
                EnsureConnected();
                using (SqlCommand command = new SqlCommand("GetUsers", _connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Skip", skip);
                    command.Parameters.AddWithValue("@Take", take);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        UserMapper m = new UserMapper(reader);
                        while (reader.Read())
                        {
                            UserDAL r = m.UserFromReader(reader);
                            proposedReturnValue.Add(r);
                        }
                    }
                }
            }
            catch (Exception ex) when (Log(ex))
            {

            }
            return proposedReturnValue;
        }

        public List<UserDAL> GetUsersRelatedToRoles(int RoleID, int skip, int take)
        {
            List<UserDAL> proposedReturnValue = new List<UserDAL>();
            try
            {
                EnsureConnected();
                using (SqlCommand command = new SqlCommand("GetUsersRelatedToRoles", _connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@RoleID", RoleID);
                    command.Parameters.AddWithValue("@Skip", skip);
                    command.Parameters.AddWithValue("@Take", take);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        UserMapper m = new UserMapper(reader);
                        while (reader.Read())
                        {
                            UserDAL r = m.UserFromReader(reader);
                            proposedReturnValue.Add(r);
                        }
                    }
                }
            }
            catch (Exception ex) when (Log(ex))
            {

            }
            return proposedReturnValue;
        }

        public int ObtainUserCount()
        {
            int proposedReturnValue = -1;
            try
            {
                EnsureConnected();
                using (SqlCommand command = new SqlCommand("ObtainUserCount", _connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    object answer = command.ExecuteScalar();
                    proposedReturnValue = (int)answer;
                }
            }
            catch (Exception ex) when (Log(ex))
            {

            }

            return proposedReturnValue;
        }

        public int CreateUser(string UserName, string Email, int RoleID, string Hash, string Salt, string RoleName)
        {
            int proposedReturnValue = -1;
            try
            {
                EnsureConnected();
                using (SqlCommand command = new SqlCommand("CreateUser", _connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@UserID", 0);
                    command.Parameters.AddWithValue("@UserName", UserName);
                    command.Parameters.AddWithValue("@Email", Email);
                    command.Parameters.AddWithValue("@Hash", Hash);
                    command.Parameters.AddWithValue("@Salt", Salt);
                    command.Parameters.AddWithValue("@RoleName", RoleName);
                    command.Parameters["UserID"].Direction = System.Data.ParameterDirection.Output;
                    command.ExecuteNonQuery();
                    proposedReturnValue =
                        Convert.ToInt32(command.Parameters["@UserID"].Value);
                }
            }
            catch (Exception ex) when (Log(ex))
            {

            }
            return proposedReturnValue;

        }

        public void JustUpdateUser(int UserID, string UserName, string Email, int RoleID, string Hash, string Salt, string RoleName)
        {
            try
            {
                EnsureConnected();
                using (SqlCommand command = new SqlCommand("JustUpdateUser", _connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@UserID", UserID);
                    command.Parameters.AddWithValue("@UserName", UserName);
                    command.Parameters.AddWithValue("@Email", Email);
                    command.Parameters.AddWithValue("@Hash", Hash);
                    command.Parameters.AddWithValue("@Salt", Salt);
                    command.Parameters.AddWithValue("@RoleName", RoleName);

                    command.ExecuteNonQuery();

                }
            }
            catch (Exception ex) when (Log(ex))
            {

            }

        }

        public void DeleteUser(int UserID)
        {

            try
            {
                EnsureConnected();
                using (SqlCommand command = new SqlCommand("DeleteUser", _connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@UserID", UserID);

                    command.ExecuteNonQuery();

                }
            }
            catch (Exception ex) when (Log(ex))
            {

            }

        }
        #endregion

        #region Character Stuff
        public CharacterDAL FindByCharacterID(int CharacterID)
        {
            CharacterDAL proposedReturnValue = null;
            try
            {
                EnsureConnected();
                using (SqlCommand command = new SqlCommand("FindByCharacterID", _connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@CharacterID", CharacterID);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        CharacterMapper m = new CharacterMapper(reader);
                        int count = 0;
                        while (reader.Read())
                        {
                            proposedReturnValue = m.CharacterFromReader(reader);
                            count++;
                        }
                        if (count > 1)
                        {
                            throw new Exception($"Found more than 1 Character with key {CharacterID}");
                        }
                    }
                }
            }
            catch (Exception ex) when (Log(ex))
            {

            }
            return proposedReturnValue;
        }

        public List<CharacterDAL> GetCharacters(int skip, int take)
        {
            List<CharacterDAL> proposedReturnValue = new List<CharacterDAL>();
            try
            {
                EnsureConnected();
                using (SqlCommand command = new SqlCommand("GetCharacters", _connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Skip", skip);
                    command.Parameters.AddWithValue("@Take", take);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        CharacterMapper m = new CharacterMapper(reader);
                        while (reader.Read())
                        {
                            CharacterDAL r = m.CharacterFromReader(reader);
                            proposedReturnValue.Add(r);
                        }
                    }
                }
            }
            catch (Exception ex) when (Log(ex))
            {

            }
            return proposedReturnValue;
        }

        public List<CharacterDAL> GetCharactersRelatedToClassID(int ClassID, int skip, int take)
        {
            List<CharacterDAL> proposedReturnValue = new List<CharacterDAL>();
            try
            {
                EnsureConnected();
                using (SqlCommand command = new SqlCommand("GetCharactersRelatedToClassID", _connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@ClassID", ClassID);
                    command.Parameters.AddWithValue("@Skip", skip);
                    command.Parameters.AddWithValue("@Take", take);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        CharacterMapper m = new CharacterMapper(reader);
                        while (reader.Read())
                        {
                            CharacterDAL r = m.CharacterFromReader(reader);
                            proposedReturnValue.Add(r);
                        }
                    }
                }
            }
            catch (Exception ex) when (Log(ex))
            {

            }
            return proposedReturnValue;
        }

        public List<CharacterDAL> GetCharactersRelatedToRaceID(int RaceID, int skip, int take)
        {
            List<CharacterDAL> proposedReturnValue = new List<CharacterDAL>();
            try
            {
                EnsureConnected();
                using (SqlCommand command = new SqlCommand("GetCharactersRelatedToRaceID", _connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@RaceID", RaceID);
                    command.Parameters.AddWithValue("@Skip", skip);
                    command.Parameters.AddWithValue("@Take", take);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        CharacterMapper m = new CharacterMapper(reader);
                        while (reader.Read())
                        {
                            CharacterDAL r = m.CharacterFromReader(reader);
                            proposedReturnValue.Add(r);
                        }
                    }
                }
            }
            catch (Exception ex) when (Log(ex))
            {

            }
            return proposedReturnValue;
        }

        public List<CharacterDAL> GetCharactersRelatedToUserID(int UserID, int skip, int take)
        {
            List<CharacterDAL> proposedReturnValue = new List<CharacterDAL>();
            try
            {
                EnsureConnected();
                using (SqlCommand command = new SqlCommand("GetCharactersRelatedToUserID", _connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@UserID", UserID);
                    command.Parameters.AddWithValue("@Skip", skip);
                    command.Parameters.AddWithValue("@Take", take);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        CharacterMapper m = new CharacterMapper(reader);
                        while (reader.Read())
                        {
                            CharacterDAL r = m.CharacterFromReader(reader);
                            proposedReturnValue.Add(r);
                        }
                    }
                }
            }
            catch (Exception ex) when (Log(ex))
            {

            }
            return proposedReturnValue;
        }

        public int ObtainCharacterCount()
        {
            int proposedReturnValue = -1;
            try
            {
                EnsureConnected();
                using (SqlCommand command = new SqlCommand("ObtainCharacterCount", _connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    object answer = command.ExecuteScalar();
                    proposedReturnValue = (int)answer;
                }
            }
            catch (Exception ex) when (Log(ex))
            {

            }

            return proposedReturnValue;
        }

        public int ObtainCharactersRelatedToUserIDCount(int UserID)
        {
            int proposedReturnValue = -1;
            try
            {
                EnsureConnected();
                using (SqlCommand command = new SqlCommand("ObtainCharactersRelatedToUserIDCount", _connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@UserID", UserID);
                    object answer = command.ExecuteScalar();
                    proposedReturnValue = (int)answer;
                }
            }
            catch (Exception ex) when (Log(ex))
            {

            }

            return proposedReturnValue;
        }

        public int CreateCharacter(int CharacterID, int UserID, string CharacterName, int ClassID, int RaceID, int StrengthScore, int DexterityScore, int ConstitutionScore, int IntelligenceScore, int WisdomScore, int CharismaScore, string UserName, string ClassName, string RaceName)
        {
            int proposedReturnValue = -1;
            try
            {
                EnsureConnected();
                using (SqlCommand command = new SqlCommand("CreateCharacter", _connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@CharacterID", 0);
                    command.Parameters.AddWithValue("@UserID", UserID);
                    command.Parameters.AddWithValue("@CharacterName", CharacterName);
                    command.Parameters.AddWithValue("@RaceID", RaceID);
                    command.Parameters.AddWithValue("@StrengthScore", StrengthScore);
                    command.Parameters.AddWithValue("@DexterityScore", DexterityScore);
                    command.Parameters.AddWithValue("@ConstitutionScore", ConstitutionScore);
                    command.Parameters.AddWithValue("@IntelligenceScore", IntelligenceScore);
                    command.Parameters.AddWithValue("@WisdomScore", WisdomScore);
                    command.Parameters.AddWithValue("@CharismaScore", CharismaScore);
                    command.Parameters.AddWithValue("@UserName", UserName);
                    command.Parameters.AddWithValue("@ClassName", ClassName);
                    command.Parameters.AddWithValue("@RaceName", RaceName);
                    command.Parameters["CharacterID"].Direction = System.Data.ParameterDirection.Output;
                    command.ExecuteNonQuery();
                    proposedReturnValue =
                        Convert.ToInt32(command.Parameters["@CharacterID"].Value);
                }
            }
            catch (Exception ex) when (Log(ex))
            {

            }
            return proposedReturnValue;

        }

        public void JustUpdateCharacter(int CharacterID, int UserID, string CharacterName, int ClassID, int RaceID, int StrengthScore, int DexterityScore, int ConstitutionScore, int IntelligenceScore, int WisdomScore, int CharismaScore, string UserName, string ClassName, string RaceName)
        {
            try
            {
                EnsureConnected();
                using (SqlCommand command = new SqlCommand("JustUpdateCharacter", _connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@CharacterID", 0);
                    command.Parameters.AddWithValue("@UserID", UserID);
                    command.Parameters.AddWithValue("@CharacterName", CharacterName);
                    command.Parameters.AddWithValue("@RaceID", RaceID);
                    command.Parameters.AddWithValue("@StrengthScore", StrengthScore);
                    command.Parameters.AddWithValue("@DexterityScore", DexterityScore);
                    command.Parameters.AddWithValue("@ConstitutionScore", ConstitutionScore);
                    command.Parameters.AddWithValue("@IntelligenceScore", IntelligenceScore);
                    command.Parameters.AddWithValue("@WisdomScore", WisdomScore);
                    command.Parameters.AddWithValue("@CharismaScore", CharismaScore);
                    command.Parameters.AddWithValue("@UserName", UserName);
                    command.Parameters.AddWithValue("@ClassName", ClassName);
                    command.Parameters.AddWithValue("@RaceName", RaceName);

                    command.ExecuteNonQuery();

                }
            }
            catch (Exception ex) when (Log(ex))
            {

            }

        }

        public void DeleteCharacter(int CharacterID)
        {

            try
            {
                EnsureConnected();
                using (SqlCommand command = new SqlCommand("DeleteCharacter", _connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@CharacterID", CharacterID);

                    command.ExecuteNonQuery();

                }
            }
            catch (Exception ex) when (Log(ex))
            {

            }

        }
        #endregion

        #region Race Stuff
        public RaceDAL FindRaceByRaceID(int RaceID)
        {
            RaceDAL proposedReturnValue = null;
            try
            {
                EnsureConnected();
                using (SqlCommand command = new SqlCommand("FindRaceByRaceID", _connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@RaceID", RaceID);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        RaceMapper m = new RaceMapper(reader);
                        int count = 0;
                        while (reader.Read())
                        {
                            proposedReturnValue = m.RaceFromReader(reader);
                            count++;
                        }
                        if (count > 1)
                        {
                            throw new Exception($"Found more than 1 Race with key {RaceID}");
                        }
                    }
                }
            }
            catch (Exception ex) when (Log(ex))
            {

            }
            return proposedReturnValue;
        }

        public List<RaceDAL> GetRaces(int skip, int take)
        {
            List<RaceDAL> proposedReturnValue = new List<RaceDAL>();
            try
            {
                EnsureConnected();
                using (SqlCommand command = new SqlCommand("GetRaces", _connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Skip", skip);
                    command.Parameters.AddWithValue("@Take", take);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        RaceMapper m = new RaceMapper(reader);
                        while (reader.Read())
                        {
                            RaceDAL r = m.RaceFromReader(reader);
                            proposedReturnValue.Add(r);
                        }
                    }
                }
            }
            catch (Exception ex) when (Log(ex))
            {

            }
            return proposedReturnValue;
        }

        public int ObtainRaceCount()
        {
            int proposedReturnValue = -1;
            try
            {
                EnsureConnected();
                using (SqlCommand command = new SqlCommand("ObtainRaceCount", _connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    object answer = command.ExecuteScalar();
                    proposedReturnValue = (int)answer;
                }
            }
            catch (Exception ex) when (Log(ex))
            {

            }

            return proposedReturnValue;
        }

        public int CreateRace(int RaceID, string RaceName, int RaceModifier)
        {
            int proposedReturnValue = -1;
            try
            {
                EnsureConnected();
                using (SqlCommand command = new SqlCommand("CreateRace", _connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@RaceID", 0);
                    command.Parameters.AddWithValue("@RaceName", RaceName);
                    command.Parameters.AddWithValue("@Racemodifier", RaceModifier);
                    command.Parameters["RaceID"].Direction = System.Data.ParameterDirection.Output;
                    command.ExecuteNonQuery();
                    proposedReturnValue =
                        Convert.ToInt32(command.Parameters["@RaceID"].Value);
                }
            }
            catch (Exception ex) when (Log(ex))
            {

            }
            return proposedReturnValue;

        }

        public void JustUpdateRace(int RaceID, string RaceName, int RaceModifier)
        {
            try
            {
                EnsureConnected();
                using (SqlCommand command = new SqlCommand("JustUpdateRace", _connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@RaceID", 0);
                    command.Parameters.AddWithValue("@RaceName", RaceName);
                    command.Parameters.AddWithValue("@Racemodifier", RaceModifier);

                    command.ExecuteNonQuery();

                }
            }
            catch (Exception ex) when (Log(ex))
            {

            }

        }

        public void DeleteRace(int RaceID)
        {

            try
            {
                EnsureConnected();
                using (SqlCommand command = new SqlCommand("DeleteRace", _connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@RaceID", RaceID);

                    command.ExecuteNonQuery();

                }
            }
            catch (Exception ex) when (Log(ex))
            {

            }

        }
        #endregion

        #region Race Modifier Stuff
        public RaceModifierDAL FindRaceModifierByRaceModifierID(int RaceModifierID)
        {
            RaceModifierDAL proposedReturnValue = null;
            try
            {
                EnsureConnected();
                using (SqlCommand command = new SqlCommand("FindRaceModifierByRaceModifierID", _connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@RaceModifierID", RaceModifierID);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        RaceModifierMapper m = new RaceModifierMapper(reader);
                        int count = 0;
                        while (reader.Read())
                        {
                            proposedReturnValue = m.RaceModifierFromReader(reader);
                            count++;
                        }
                        if (count > 1)
                        {
                            throw new Exception($"Found more than 1 RaceModifier with key {RaceModifierID}");
                        }
                    }
                }
            }
            catch (Exception ex) when (Log(ex))
            {

            }
            return proposedReturnValue;
        }

        public List<RaceModifierDAL> GetRaceModifiers(int skip, int take)
        {
            List<RaceModifierDAL> proposedReturnValue = new List<RaceModifierDAL>();
            try
            {
                EnsureConnected();
                using (SqlCommand command = new SqlCommand("GetRaceModifiers", _connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Skip", skip);
                    command.Parameters.AddWithValue("@Take", take);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        RaceModifierMapper m = new RaceModifierMapper(reader);
                        while (reader.Read())
                        {
                            RaceModifierDAL r = m.RaceModifierFromReader(reader);
                            proposedReturnValue.Add(r);
                        }
                    }
                }
            }
            catch (Exception ex) when (Log(ex))
            {

            }
            return proposedReturnValue;
        }

        public List<RaceModifierDAL> GetRaceModifiersRelatedToRaces(int RaceID, int skip, int take)
        {
            List<RaceModifierDAL> proposedReturnValue = new List<RaceModifierDAL>();
            try
            {
                EnsureConnected();
                using (SqlCommand command = new SqlCommand("GetRaceModifiersRelatedToRaces", _connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@RaceID", RaceID);
                    command.Parameters.AddWithValue("@Skip", skip);
                    command.Parameters.AddWithValue("@Take", take);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        RaceModifierMapper m = new RaceModifierMapper(reader);
                        while (reader.Read())
                        {
                            RaceModifierDAL r = m.RaceModifierFromReader(reader);
                            proposedReturnValue.Add(r);
                        }
                    }
                }
            }
            catch (Exception ex) when (Log(ex))
            {

            }
            return proposedReturnValue;
        }

        public int ObtainRaceModifierCount()
        {
            int proposedReturnValue = -1;
            try
            {
                EnsureConnected();
                using (SqlCommand command = new SqlCommand("ObtainRaceModifierCount", _connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    object answer = command.ExecuteScalar();
                    proposedReturnValue = (int)answer;
                }
            }
            catch (Exception ex) when (Log(ex))
            {

            }

            return proposedReturnValue;
        }

        public int CreateRaceModifier(int RaceModifierID, int RaceID, int StatID, int Modifier)
        {
            int proposedReturnValue = -1;
            try
            {
                EnsureConnected();
                using (SqlCommand command = new SqlCommand("CreateRaceModifier", _connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@RaceModifier", 0);
                    command.Parameters.AddWithValue("@RaceID", RaceID);
                    command.Parameters.AddWithValue("@StatID", StatID);
                    command.Parameters.AddWithValue("@Modifier", Modifier);
                    command.Parameters["RaceModifierID"].Direction = System.Data.ParameterDirection.Output;
                    command.ExecuteNonQuery();
                    proposedReturnValue =
                        Convert.ToInt32(command.Parameters["@RaceModifierID"].Value);
                }
            }
            catch (Exception ex) when (Log(ex))
            {

            }
            return proposedReturnValue;

        }

        public void DeleteRaceModifier(int RaceModifierID)
        {

            try
            {
                EnsureConnected();
                using (SqlCommand command = new SqlCommand("DeleteRaceModifier", _connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@RaceModifierID", RaceModifierID);

                    command.ExecuteNonQuery();

                }
            }
            catch (Exception ex) when (Log(ex))
            {

            }

        }


        #endregion

        #region Class Stuff
        public ClassDAL FindClassByClassID(int ClassID)
        {
            ClassDAL proposedReturnValue = null;
            try
            {
                EnsureConnected();
                using (SqlCommand command = new SqlCommand("FindClassByClassID", _connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@ClassID", ClassID);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        ClassMapper m = new ClassMapper(reader);
                        int count = 0;
                        while (reader.Read())
                        {
                            proposedReturnValue = m.ClassFromReader(reader);
                            count++;
                        }
                        if (count > 1)
                        {
                            throw new Exception($"Found more than 1 Class with key {ClassID}");
                        }
                    }
                }
            }
            catch (Exception ex) when (Log(ex))
            {

            }
            return proposedReturnValue;
        }

        public List<ClassDAL> GetClasses(int skip, int take)
        {
            List<ClassDAL> proposedReturnValue = new List<ClassDAL>();
            try
            {
                EnsureConnected();
                using (SqlCommand command = new SqlCommand("GetClasses", _connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Skip", skip);
                    command.Parameters.AddWithValue("@Take", take);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        ClassMapper m = new ClassMapper(reader);
                        while (reader.Read())
                        {
                            ClassDAL r = m.ClassFromReader(reader);
                            proposedReturnValue.Add(r);
                        }
                    }
                }
            }
            catch (Exception ex) when (Log(ex))
            {

            }
            return proposedReturnValue;
        }

        public int ObtainClassCount()
        {
            int proposedReturnValue = -1;
            try
            {
                EnsureConnected();
                using (SqlCommand command = new SqlCommand("ObtainClassCount", _connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    object answer = command.ExecuteScalar();
                    proposedReturnValue = (int)answer;
                }
            }
            catch (Exception ex) when (Log(ex))
            {

            }

            return proposedReturnValue;
        }

        public int CreateClass(int ClassID, string ClassName, string Description, int ClassModifier)
        {
            int proposedReturnValue = -1;
            try
            {
                EnsureConnected();
                using (SqlCommand command = new SqlCommand("CreateClass", _connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@ClassID", 0);
                    command.Parameters.AddWithValue("@ClassName", ClassName);
                    command.Parameters.AddWithValue("@Description", Description);
                    command.Parameters.AddWithValue("@ClassModifier", ClassModifier);
                    command.Parameters["ClassID"].Direction = System.Data.ParameterDirection.Output;
                    command.ExecuteNonQuery();
                    proposedReturnValue =
                        Convert.ToInt32(command.Parameters["@ClassID"].Value);
                }
            }
            catch (Exception ex) when (Log(ex))
            {

            }
            return proposedReturnValue;

        }

        public void JustUpdateClass(int ClassID, string ClassName, string Description, int ClassModifier)
        {
            try
            {
                EnsureConnected();
                using (SqlCommand command = new SqlCommand("JustUpdateClass", _connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@ClassID", 0);
                    command.Parameters.AddWithValue("@ClassName", ClassName);
                    command.Parameters.AddWithValue("@Description", Description);
                    command.Parameters.AddWithValue("@Classmodifier", ClassModifier);

                    command.ExecuteNonQuery();

                }
            }
            catch (Exception ex) when (Log(ex))
            {

            }

        }

        public void DeleteClass(int ClassID)
        {

            try
            {
                EnsureConnected();
                using (SqlCommand command = new SqlCommand("DeleteClass", _connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@ClassID", ClassID);

                    command.ExecuteNonQuery();

                }
            }
            catch (Exception ex) when (Log(ex))
            {

            }

        }
        #endregion

        #region Class Modifier Stuff
        public ClassModifierDAL FindClassModifierByClassModifierID(int ClassModifierID)
        {
            ClassModifierDAL proposedReturnValue = null;
            try
            {
                EnsureConnected();
                using (SqlCommand command = new SqlCommand("FindClassModifierByClassModifierID", _connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@ClassModifierID", ClassModifierID);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        ClassModifierMapper m = new ClassModifierMapper(reader);
                        int count = 0;
                        while (reader.Read())
                        {
                            proposedReturnValue = m.ClassModifierFromReader(reader);
                            count++;
                        }
                        if (count > 1)
                        {
                            throw new Exception($"Found more than 1 ClassModifier with key {ClassModifierID}");
                        }
                    }
                }
            }
            catch (Exception ex) when (Log(ex))
            {

            }
            return proposedReturnValue;
        }

        public List<ClassModifierDAL> GetClassModifiers(int skip, int take)
        {
            List<ClassModifierDAL> proposedReturnValue = new List<ClassModifierDAL>();
            try
            {
                EnsureConnected();
                using (SqlCommand command = new SqlCommand("GetClassModifiers", _connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Skip", skip);
                    command.Parameters.AddWithValue("@Take", take);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        ClassModifierMapper m = new ClassModifierMapper(reader);
                        while (reader.Read())
                        {
                            ClassModifierDAL r = m.ClassModifierFromReader(reader);
                            proposedReturnValue.Add(r);
                        }
                    }
                }
            }
            catch (Exception ex) when (Log(ex))
            {

            }
            return proposedReturnValue;
        }

        public List<ClassModifierDAL> GetClassModifiersRelatedToClasses(int ClassID, int skip, int take)
        {
            List<ClassModifierDAL> proposedReturnValue = new List<ClassModifierDAL>();
            try
            {
                EnsureConnected();
                using (SqlCommand command = new SqlCommand("GetClassModifiersRelatedToClasses", _connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@ClassID", ClassID);
                    command.Parameters.AddWithValue("@Skip", skip);
                    command.Parameters.AddWithValue("@Take", take);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        ClassModifierMapper m = new ClassModifierMapper(reader);
                        while (reader.Read())
                        {
                            ClassModifierDAL r = m.ClassModifierFromReader(reader);
                            proposedReturnValue.Add(r);
                        }
                    }
                }
            }
            catch (Exception ex) when (Log(ex))
            {

            }
            return proposedReturnValue;
        }

        public int ObtainClassModifierCount()
        {
            int proposedReturnValue = -1;
            try
            {
                EnsureConnected();
                using (SqlCommand command = new SqlCommand("ObtainClassModifierCount", _connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    object answer = command.ExecuteScalar();
                    proposedReturnValue = (int)answer;
                }
            }
            catch (Exception ex) when (Log(ex))
            {

            }

            return proposedReturnValue;
        }

        public int CreateClassModifier(int ClassModifierID, int ClassID, int StatID, int Modifier)
        {
            int proposedReturnValue = -1;
            try
            {
                EnsureConnected();
                using (SqlCommand command = new SqlCommand("CreateClassModifier", _connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@ClassModifier", 0);
                    command.Parameters.AddWithValue("@ClassID", ClassID);
                    command.Parameters.AddWithValue("@StatID", StatID);
                    command.Parameters.AddWithValue("@Modifier", Modifier);
                    command.Parameters["ClassModifierID"].Direction = System.Data.ParameterDirection.Output;
                    command.ExecuteNonQuery();
                    proposedReturnValue =
                        Convert.ToInt32(command.Parameters["@ClassModifierID"].Value);
                }
            }
            catch (Exception ex) when (Log(ex))
            {

            }
            return proposedReturnValue;

        }

        public void DeleteClassModifier(int ClassModifierID)
        {

            try
            {
                EnsureConnected();
                using (SqlCommand command = new SqlCommand("DeleteClassModifier", _connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@ClassModifierID", ClassModifierID);

                    command.ExecuteNonQuery();

                }
            }
            catch (Exception ex) when (Log(ex))
            {

            }

        }
        #endregion 
    }
}
