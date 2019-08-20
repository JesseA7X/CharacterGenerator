using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;

namespace BusinessLogicLayer
{
    public class CharacterBLL
    {
        public CharacterBLL()
        {

        }
        public CharacterBLL(CharacterDAL dal)
        {
            this.CharacterID = dal.CharacterID;
            this.UserID = dal.UserID;
            this.CharacterName = dal.CharacterName;
            this.ClassID = dal.ClassID;
            this.RaceID = dal.RaceID;
            this.StrengthScore = dal.StrengthScore;
            this.DexterityScore = dal.DexterityScore;
            this.ConstitutionScore = dal.ConstitutionScore;
            this.IntelligenceScore = dal.IntelligenceScore;
            this.WisdomScore = dal.WisdomScore;
            this.CharismaScore = dal.WisdomScore;
            this.UserName = dal.UserName;
            this.ClassName = dal.ClassName;
            this.RaceName = dal.RaceName;
        }
        #region Direct Properties
        public int CharacterID { get; set; }
        public int UserID { get; set; }
        public string CharacterName { get; set; }
        public int ClassID { get; set; }
        public int RaceID { get; set; }
        public int StrengthScore { get; set; }
        public int DexterityScore { get; set; }
        public int ConstitutionScore { get; set; }
        public int IntelligenceScore { get; set; }
        public int WisdomScore { get; set; }
        public int CharismaScore { get; set; }
        #endregion

        #region Indirect Properties
        public string UserName { get; set; }
        public string ClassName { get; set; }
        public string RaceName { get; set; }
        #endregion 

        public override string ToString()
        {
            return $"CharacterID: {CharacterID} UserID: {UserID} CharacterName: {CharacterName} ClassID: {ClassID} RaceID: {RaceID} StrengthScore: {StrengthScore} DexterityScore: {DexterityScore} ConstitutionScore: {ConstitutionScore} IntelligenceScore: {IntelligenceScore} WisdomScore: {WisdomScore} CharismaScore: {CharismaScore} UserName: {UserName} ClassName: {ClassName} RaceName: {RaceName}";
        }
        
    }
}
