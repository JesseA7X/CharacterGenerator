using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
using Lumberjack;

namespace BusinessLogicLayer
{
    public class ContextBLL : IDisposable
    {
        ContextDAL _context = new ContextDAL();

        public void Dispose()
        {
            // IDisposable is being used to dispose of heavy loads
            ((IDisposable)_context).Dispose();
        }

        bool Log(Exception ex)
        {
            // acessing Lumberjack to log exceptions
            Console.WriteLine(ex);
            Logger.Log(ex);
            return false;
        }

        // this is a random dice roller to assign the stats to the created characters
        Random diceRoll = new Random();
        public int Roll()
        {
            MeaningfulCalc calc = new MeaningfulCalc();
            return calc.Roll();
        }
        

        public void CharacterModification(CharacterBLL u, List<Modifier> Modifiers)
        {
            MeaningfulCalc calc = new MeaningfulCalc();
            calc.CharacterModification(u, Modifiers);
        }                          

        public ContextBLL()
        {
            try
            {
                string connectionstring;
                connectionstring = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnectionString"].ConnectionString;
                _context.ConnectionString = connectionstring;
            }
            catch (Exception ex) when(Log(ex))
            {
                //this exception does not have a reasonable handler, simply log it
            }
        }

        #region Role Stuff
        public RoleBLL FindRoleByRoleID(int RoleID)
        {
            RoleBLL proposedReturnValue = null;
            RoleDAL DataLayerObject = _context.FindRoleByRoleID(RoleID);
            if (null != DataLayerObject)
            {
                proposedReturnValue = new RoleBLL(DataLayerObject);
            }
            return proposedReturnValue;
        }

        public List<RoleBLL> GetRoles(int skip, int take)
        {
            List<RoleBLL> proposedReturnValue = new List<RoleBLL>();
            List<RoleDAL> ListOfDataLayerObjects = _context.GetRoles(skip, take);
            foreach(RoleDAL role in ListOfDataLayerObjects)
            {
                RoleBLL BusinessObject = new RoleBLL(role);
                proposedReturnValue.Add(BusinessObject);
            }
            return proposedReturnValue;
        }

        public int ObtainRoleCount()
        {
            int proposedReturnValue = 0;
            proposedReturnValue = _context.ObtainRoleCount();
            return proposedReturnValue;
        }

        public int CreateRole(string RoleName)
        {
            int proposedReturnValue = -1;
            proposedReturnValue = _context.CreateRole(RoleName);
            return proposedReturnValue;
        }

        public int CreateRole(RoleBLL role) // this is needed for posting in mvc
        {
            int proposedReturnValue = -1;
            proposedReturnValue = _context.CreateRole(role.RoleName);
            return proposedReturnValue;
        }

        public void JustUpdateRole(int RoleID, string RoleName)
        {

            _context.JustUpdateRole(RoleID, RoleName);

        }

        public void JustUpdateRole(RoleBLL Role)
        {

            _context.JustUpdateRole(Role.RoleID, Role.RoleName);

        }

        public void DeleteRole(int RoleID)
        {
            _context.DeleteRole(RoleID);
        }

        public void DeleteRole(RoleBLL Role)
        {
            _context.DeleteRole(Role.RoleID);
        }

        #endregion

        #region User Stuff

        public UserBLL FindUserByUserID(int UserID)
        {
            UserBLL proposedReturnValue = null;
            UserDAL DataLayerObject = _context.FindUserByUserID(UserID);
            if (null != DataLayerObject)
            {
                proposedReturnValue = new UserBLL(DataLayerObject);
            }
            return proposedReturnValue;
        }

        public UserBLL FindUserByUserName(string UserName)
        {
            UserBLL proposedReturnValue = null;
            UserDAL DataLayerObject = _context.FindUserByUserName(UserName);
            if (null != DataLayerObject)
            {
                proposedReturnValue = new UserBLL(DataLayerObject);
            }
            return proposedReturnValue;
        }

        public UserBLL FindUserByEmail(string Email)
        {
            UserBLL proposedReturnValue = null;
            UserDAL DataLayerObject = _context.FindUserByEmail(Email);
            if (null != DataLayerObject)
            {
                proposedReturnValue = new UserBLL(DataLayerObject);
            }
            return proposedReturnValue;
        }

        public List<UserBLL> GetUsers(int skip, int take)
        {
            List<UserBLL> ProposedReturnValue = new List<UserBLL>();
            List<UserDAL> ListOfDataLayerObjects = _context.GetUsers(skip, take);
            foreach (UserDAL user in ListOfDataLayerObjects)
            {
                UserBLL BusinessObject = new UserBLL(user);
                ProposedReturnValue.Add(BusinessObject);
            }
            return ProposedReturnValue;
        }

        public List<UserBLL> GetUsersRelatedToRoles(int RoleID, int skip, int take)
        {
            List<UserBLL> ProposedReturnValue = new List<UserBLL>();
            List<UserDAL> ListOfDataLayerObjects = _context.GetUsersRelatedToRoles(RoleID, skip, take);
            // converting all datalayer objects into business layer objects
            foreach (UserDAL user in ListOfDataLayerObjects)
            {
                UserBLL BusinessObject = new UserBLL(user);
                ProposedReturnValue.Add(BusinessObject);
            }
            return ProposedReturnValue;
        }

        public int ObtainUserCount()
        {
            int proposedReturnValue = 0;
            proposedReturnValue = _context.ObtainUserCount();
            return proposedReturnValue;
        }

        public int CreateUser( string UserName, string Email, int RoleID,string Hash, string Salt, string RoleName)
        {
            int proposedReturnValue = -1;
            proposedReturnValue = _context.CreateUser(UserName,Email, RoleID, Hash, Salt, RoleName);
            return proposedReturnValue;
        }

        // this is a way mvc action methods can be called
        public int CreateUser(UserBLL user)
        {
            int proposedReturnValue = -1;
            proposedReturnValue = _context.CreateUser(user.UserName, user.Email, user.RoleID, user.Hash, user.Salt, user.RoleName);
            return proposedReturnValue;
        }

        public void JustUpdateUser(int UserID,string UserName, string Email, int RoleID, string Hash, string Salt, string RoleName)
        {
            _context.JustUpdateUser(UserID, UserName, Email, RoleID, Hash, Salt, RoleName);
        }

        public void JustUpdateUser(UserBLL user)
        {
            _context.JustUpdateUser(user.UserID, user.UserName, user.Email, user.RoleID, user.Hash, user.Salt, user.RoleName);
        }

        public void DeleteUser(int UserID)
        {
            _context.DeleteUser(UserID);
        }

        public void DeleteUser(UserBLL User)
        {
            _context.DeleteUser(User.UserID);
        }
        #endregion

        #region Character Stuff
        public CharacterBLL FindByCharacterID(int CharacterID)
        {
            CharacterBLL proposedReturnValue = null;
            CharacterDAL DataLayerObject = _context.FindByCharacterID(CharacterID);
            if (null != DataLayerObject)
            {
                proposedReturnValue = new CharacterBLL(DataLayerObject);
            }
            return proposedReturnValue;
        }

        public List<CharacterBLL> GetCharacters(int skip, int take)
        {
            List<CharacterBLL> ProposedReturnValue = new List<CharacterBLL>();
            List<CharacterDAL> ListOfDataLayerObjects = _context.GetCharacters(skip, take);
            foreach (CharacterDAL Character in ListOfDataLayerObjects)
            {
                CharacterBLL BusinessObject = new CharacterBLL(Character);
                ProposedReturnValue.Add(BusinessObject);
            }
            return ProposedReturnValue;
        }

        public List<CharacterBLL> GetCharactersRelatedToClassID(int ClassID, int skip, int take)
        {
            List<CharacterBLL> ProposedReturnValue = new List<CharacterBLL>();
            List<CharacterDAL> ListOfDataLayerObjects = _context.GetCharactersRelatedToClassID(ClassID, skip, take);
            foreach (CharacterDAL Character in ListOfDataLayerObjects)
            {
                CharacterBLL BusinessObject = new CharacterBLL(Character);
                ProposedReturnValue.Add(BusinessObject);
            }
            return ProposedReturnValue;
        }

        public List<CharacterBLL> GetCharactersRelatedToRaceID(int RaceID, int skip, int take)
        {
            List<CharacterBLL> ProposedReturnValue = new List<CharacterBLL>();
            List<CharacterDAL> ListOfDataLayerObjects = _context.GetCharactersRelatedToRaceID(RaceID, skip, take);
            foreach (CharacterDAL Character in ListOfDataLayerObjects)
            {
                CharacterBLL BusinessObject = new CharacterBLL(Character);
                ProposedReturnValue.Add(BusinessObject);
            }
            return ProposedReturnValue;
        }

        public List<CharacterBLL> GetCharactersRelatedToUserID(int UserID, int skip, int take)
        {
            List<CharacterBLL> ProposedReturnValue = new List<CharacterBLL>();
            List<CharacterDAL> ListOfDataLayerObjects = _context.GetCharactersRelatedToUserID(UserID, skip, take);
            foreach (CharacterDAL Character in ListOfDataLayerObjects)
            {
                CharacterBLL BusinessObject = new CharacterBLL(Character);
                ProposedReturnValue.Add(BusinessObject);
            }
            return ProposedReturnValue;
        }

        public int ObtainCharacterCount()
        {
            int proposedReturnValue = 0;
            proposedReturnValue = _context.ObtainCharacterCount();
            return proposedReturnValue;
        }

        public int ObtainCharactersRelatedToUserIDCount(int UserID)
        {
            int proposedReturnValue = 0;
            proposedReturnValue = _context.ObtainCharactersRelatedToUserIDCount(UserID);
            return proposedReturnValue;
        }

        public int CreateCharacter( int UserID, string CharacterName, int ClassID, int RaceID, int StrengthScore, int DexterityScore, int ConstitutionScore, int IntelligenceScore, int WisdomScore, int CharismaScore)
        {
            int proposedReturnValue = -1;
            proposedReturnValue = _context.CreateCharacter( UserID, CharacterName, ClassID, RaceID, StrengthScore, DexterityScore, ConstitutionScore, IntelligenceScore, WisdomScore, CharismaScore);
            return proposedReturnValue;
        }

        public int CreateCharacter(CharacterBLL character)
        {
            int proposedReturnValue = -1;
            proposedReturnValue = _context.CreateCharacter( character.UserID, character.CharacterName, character.ClassID, character.RaceID, character.StrengthScore, character.DexterityScore, character.ConstitutionScore, character.IntelligenceScore, character.WisdomScore, character.CharismaScore);
            return proposedReturnValue;
        }

        public void JustUpdateCharacter(int CharacterID, int UserID, string CharacterName, int ClassID, int RaceID, int StrengthScore, int DexterityScore, int ConstitutionScore, int IntelligenceScore, int WisdomScore, int CharismaScore, string UserName, string ClassName, string RaceName)
        {

            _context.JustUpdateCharacter(CharacterID, UserID, CharacterName, ClassID, RaceID, StrengthScore, DexterityScore, ConstitutionScore, IntelligenceScore, WisdomScore, CharismaScore, UserName, ClassName, RaceName);

        }

        public void JustUpdateCharacter(CharacterBLL character)
        {
            _context.JustUpdateCharacter(character.CharacterID, character.UserID, character.CharacterName, character.ClassID, character.RaceID, character.StrengthScore, character.DexterityScore, character.ConstitutionScore, character.IntelligenceScore, character.WisdomScore, character.CharismaScore, character.UserName, character.ClassName, character.RaceName);
        }

        public void DeleteCharacter(int CharacterID)
        {
            _context.DeleteCharacter(CharacterID);
        }

        public void DeleteCharacter(CharacterBLL character)
        {
            _context.DeleteCharacter(character.CharacterID);
        }
        #endregion

        #region Race Stuff
        public RaceBLL FindRaceByRaceID(int RaceID)
        {
            RaceBLL proposedReturnValue = null;
            RaceDAL DataLayerObject = _context.FindRaceByRaceID(RaceID);
            if (null != DataLayerObject)
            {
                proposedReturnValue = new RaceBLL(DataLayerObject);
            }
            return proposedReturnValue;
        }

        public List<RaceBLL> GetRaces(int skip, int take)
        {
            List<RaceBLL> ProposedReturnValue = new List<RaceBLL>();
            List<RaceDAL> ListOfDataLayerObjects = _context.GetRaces(skip, take);
            foreach (RaceDAL race in ListOfDataLayerObjects)
            {
                RaceBLL BusinessObject = new RaceBLL(race);
                ProposedReturnValue.Add(BusinessObject);
            }
            return ProposedReturnValue;
        }

        public int ObtainRaceCount()
        {
            int proposedReturnValue = 0;
            proposedReturnValue = _context.ObtainRaceCount();
            return proposedReturnValue;
        }

        public int CreateRace(int RaceID, string RaceName, int RaceModifier)
        {
            int proposedReturnValue = -1;
            proposedReturnValue = _context.CreateRace(RaceID,RaceName,RaceModifier);
            return proposedReturnValue;
        }

        public int CreateRace(RaceBLL race)
        {
            int proposedReturnValue = -1;
            proposedReturnValue = _context.CreateRace(race.RaceID, race.RaceName, race.RaceModifier);
            return proposedReturnValue;
        }

        public void JustUpdateRace(int RaceID, string RaceName, int RaceModifier)
        {

            _context.JustUpdateRace(RaceID, RaceName, RaceModifier);

        }

        public void JustUpdateRace(RaceBLL race)
        {

            _context.JustUpdateRace(race.RaceID, race.RaceName, race.RaceModifier);

        }

        public void DeleteRace(int RaceID)
        {
            _context.DeleteRace(RaceID);
        }
        public void DeleteRace(RaceBLL race)
        {
            _context.DeleteRace(race.RaceID);
        }
        #endregion

        #region RaceModifier Stuff
        public RaceModifier FindRaceModifierByRaceModifierID(int RaceModifierID)
        {
            RaceModifier proposedReturnValue = null;
            RaceModifierDAL DataLayerObject = _context.FindRaceModifierByRaceModifierID(RaceModifierID);
            if (null != DataLayerObject)
            {
                proposedReturnValue = new RaceModifier(DataLayerObject);
            }
            return proposedReturnValue;
        }

        public List<RaceModifier> GetRaceModifiers(int skip, int take)
        {
            List<RaceModifier> ProposedReturnValue = new List<RaceModifier>();
            List<RaceModifierDAL> ListOfDataLayerObjects = _context.GetRaceModifiers(skip, take);
            foreach (RaceModifierDAL racemodifier in ListOfDataLayerObjects)
            {
                RaceModifier BusinessObject = new RaceModifier(racemodifier);
                ProposedReturnValue.Add(BusinessObject);
            }
            return ProposedReturnValue;
        }

        public List<Modifier> GetRaceModifiersRelatedToRaces(int RaceID, int skip, int take)
        {
            List<Modifier> ProposedReturnValue = new List<Modifier>();
            List<RaceModifierDAL> ListOfDataLayerObjects = _context.GetRaceModifiersRelatedToRaces(RaceID, skip, take);
            foreach (RaceModifierDAL racemodifier in ListOfDataLayerObjects)
            {
                RaceModifier BusinessObject = new RaceModifier(racemodifier);
                ProposedReturnValue.Add(BusinessObject);
            }
            return ProposedReturnValue;
        }

        public int ObtainRaceModifierCount()
        {
            int proposedReturnValue = 0;
            proposedReturnValue = _context.ObtainRaceModifierCount();
            return proposedReturnValue;
        }

        public int CreateRaceModifier(int RaceModifierID, int RaceID, int StatID, int Modifier)
        {
            int proposedReturnValue = -1;
            proposedReturnValue = _context.CreateRaceModifier(RaceModifierID, RaceID, StatID, Modifier);
            return proposedReturnValue;
        }

        public int CreateRaceModifier(RaceModifier rmod)
        {
            int proposedReturnValue = -1;
            proposedReturnValue = _context.CreateRaceModifier(rmod.RaceModifierID, rmod.RaceID, rmod.StatID, rmod.ModifierAmount);
            return proposedReturnValue;
        }

        public void DeleteRaceModifier(int RaceModifierID)
        {
            _context.DeleteRaceModifier(RaceModifierID);
        }

        public void DeleteRaceModifier(RaceModifier rmod)
        {
            _context.DeleteRaceModifier(rmod.RaceModifierID);
        }
        #endregion

        #region Class Stuff
        public ClassBLL FindClassByClassID(int ClassID)
        {
            ClassBLL proposedReturnValue = null;
            ClassDAL DataLayerObject = _context.FindClassByClassID(ClassID);
            if (null != DataLayerObject)
            {
                proposedReturnValue = new ClassBLL(DataLayerObject);
            }
            return proposedReturnValue;
        }

        public List<ClassBLL> GetClasses(int skip, int take)
        {
            List<ClassBLL> ProposedReturnValue = new List<ClassBLL>();
            List<ClassDAL> ListOfDataLayerObjects = _context.GetClasses(skip, take);
            foreach (ClassDAL @class in ListOfDataLayerObjects)
            {
                ClassBLL BusinessObject = new ClassBLL(@class);
                ProposedReturnValue.Add(BusinessObject);
            }
            return ProposedReturnValue;
        }

        public int ObtainClassCount()
        {
            int proposedReturnValue = 0;
            proposedReturnValue = _context.ObtainClassCount();
            return proposedReturnValue;
        }

        public int CreateClass(int ClassID, string ClassName, string Description, int ClassModifier)
        {
            int proposedReturnValue = -1;
            proposedReturnValue = _context.CreateClass(ClassID, ClassName, Description, ClassModifier);
            return proposedReturnValue;
        }

        public int CreateClass(ClassBLL @class)
        {
            int proposedReturnValue = -1;
            proposedReturnValue = _context.CreateClass(@class.ClassID, @class.ClassName, @class.Description, @class.ClassModifier);
            return proposedReturnValue;
        }

        public void JustUpdateClass(int ClassID, string ClassName, string Description, int ClassModifier)
        {

            _context.JustUpdateClass(ClassID, ClassName, Description, ClassModifier);

        }

        public void JustUpdateClass(ClassBLL @class)
        {

            _context.JustUpdateClass(@class.ClassID, @class.ClassName, @class.Description, @class.ClassModifier);

        }

        public void DeleteClass(int ClassID)
        {
            _context.DeleteClass(ClassID);
        }

        public void DeleteClass(ClassBLL @class)
        {
            _context.DeleteClass(@class.ClassID);
        }
        #endregion

        #region ClassModifier Stuff
        public ClassModifier FindClassModifierByClassModifierID(int ClassModifierID)
        {
            ClassModifier proposedReturnValue = null;
            ClassModifierDAL DataLayerObject = _context.FindClassModifierByClassModifierID(ClassModifierID);
            if (null != DataLayerObject)
            {
                proposedReturnValue = new ClassModifier(DataLayerObject);
            }
            return proposedReturnValue;
        }

        public List<ClassModifier> GetClassModifiers(int skip, int take)
        {
            List<ClassModifier> ProposedReturnValue = new List<ClassModifier>();
            List<ClassModifierDAL> ListOfDataLayerObjects = _context.GetClassModifiers(skip, take);
            foreach (ClassModifierDAL classmodifier in ListOfDataLayerObjects)
            {
                ClassModifier BusinessObject = new ClassModifier(classmodifier);
                ProposedReturnValue.Add(BusinessObject);
            }
            return ProposedReturnValue;
        }

        public List<Modifier> GetClassModifiersRelatedToClasses(int ClassID, int skip, int take)
        {
            List<Modifier> ProposedReturnValue = new List<Modifier>();
            List<ClassModifierDAL> ListOfDataLayerObjects = _context.GetClassModifiersRelatedToClasses(ClassID, skip, take);
            foreach (ClassModifierDAL classmodifier in ListOfDataLayerObjects)
            {
                ClassModifier BusinessObject = new ClassModifier(classmodifier);
                ProposedReturnValue.Add(BusinessObject);
            }
            return ProposedReturnValue;
        }

        public int ObtainClassModifierCount()
        {
            int proposedReturnValue = 0;
            proposedReturnValue = _context.ObtainClassModifierCount();
            return proposedReturnValue;
        }

        public int CreateClassModifier(int ClassModifierID, int ClassID, int StatID, int Modifier)
        {
            int proposedReturnValue = -1;
            proposedReturnValue = _context.CreateClassModifier(ClassModifierID, ClassID, StatID, Modifier);
            return proposedReturnValue;
        }

        public int CreateClassModifier(ClassModifier cmod)
        {
            int proposedReturnValue = -1;
            proposedReturnValue = _context.CreateClassModifier(cmod.ClassModifierID, cmod.ClassID, cmod.StatID, cmod.ModifierAmount);
            return proposedReturnValue;
        }

        public void DeleteClassModifier(int ClassModifierID)
        {
            _context.DeleteClassModifier(ClassModifierID);
        }

        public void DeleteClassModifier(ClassModifier cmod)
        {
            _context.DeleteClassModifier(cmod.ClassModifierID);
        }
        #endregion
    }
}
