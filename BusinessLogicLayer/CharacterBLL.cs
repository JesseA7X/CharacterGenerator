using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
using System.ComponentModel.DataAnnotations;

namespace BusinessLogicLayer
{
    public class CharacterBLL
    {
        // this constructer is used to initialize an object within this class
        public CharacterBLL()
        {

        }
        // in this section we are setting objects in the BLL to their data from the DAL
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
            this.CharismaScore = dal.CharismaScore;
            this.UserName = dal.UserName;
            this.ClassName = dal.ClassName;
            this.RaceName = dal.RaceName;
        }
        #region Direct Properties
        // the following data annotations are being used to set the displayed information to a simpler cleaner view for the user
        public int CharacterID { get; set; }
        public int UserID { get; set; }
        [Display(Name="Character")]
        public string CharacterName { get; set; }
        public int ClassID { get; set; }
        public int RaceID { get; set; }
        [Display(Name = "Strength")]
        public int StrengthScore { get; set; }
        [Display(Name = "Dexterity")]
        public int DexterityScore { get; set; }
        [Display(Name = "Constitution")]
        public int ConstitutionScore { get; set; }
        [Display(Name = "Intelligence")]
        public int IntelligenceScore { get; set; }
        [Display(Name = "Wisdom")]
        public int WisdomScore { get; set; }
        [Display(Name = "Charisma")]
        public int CharismaScore { get; set; }
        #endregion

        #region Indirect Properties
        [Display(Name ="User")]
        public string UserName { get; set; }
        [Display(Name = "Class")]
        public string ClassName { get; set; }
        [Display(Name = "Race")]
        public string RaceName { get; set; }
        #endregion 

        public override string ToString()
        {
            return $"CharacterID: {CharacterID} UserID: {UserID} CharacterName: {CharacterName} ClassID: {ClassID} RaceID: {RaceID} StrengthScore: {StrengthScore} DexterityScore: {DexterityScore} ConstitutionScore: {ConstitutionScore} IntelligenceScore: {IntelligenceScore} WisdomScore: {WisdomScore} CharismaScore: {CharismaScore} UserName: {UserName} ClassName: {ClassName} RaceName: {RaceName}";
        }
        
    }
}
