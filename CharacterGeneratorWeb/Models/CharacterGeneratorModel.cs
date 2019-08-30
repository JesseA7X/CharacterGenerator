using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessLogicLayer;
using System.ComponentModel.DataAnnotations;

namespace CharacterGeneratorWeb.Models
{
    public class CharacterGeneratorModel
    {
        [Display(Name ="Str")]
        public int StrengthScore { get; set; }  public int ModStr { get; set; }
        [Display(Name = "Dex")]
        public int DexterityScore { get; set; } public int ModDex { get; set; }
        [Display(Name = "Con")]
        public int ConstitutionScore { get; set; } public int ModCon { get; set; }
        [Display(Name = "Int")]
        public int IntelligenceScore { get; set; } public int ModInt { get; set; }
        [Display(Name = "Wis")]
        public int WisdomScore { get; set; } public int ModWis { get; set; }
        [Display(Name = "Cha")]
        public int CharismaScore { get; set; } public int ModCha { get; set; }
        public int ClassID { get; set; }
        public int RaceID { get; set; }
        [Display(Name = "Class")]
        [Required]
        public List<SelectListItem> ClassNames { get; set; }
        [Display(Name = "Race")]
        [Required]
        public List<SelectListItem> RaceNames { get; set; }
        [Display(Name = "Name")]
        [Required]
        public string CharacterName { get; set; }
        public CharacterBLL BaseCharacter
        {
            get { CharacterBLL proposedReturnValue = new CharacterBLL();
                proposedReturnValue.StrengthScore = this.StrengthScore;
                proposedReturnValue.DexterityScore = this.DexterityScore;
                proposedReturnValue.ConstitutionScore = this.ConstitutionScore;
                proposedReturnValue.IntelligenceScore = this.IntelligenceScore;
                proposedReturnValue.WisdomScore = this.WisdomScore;
                proposedReturnValue.CharismaScore = this.CharismaScore;
                proposedReturnValue.CharacterName = this.CharacterName;
                proposedReturnValue.ClassID = this.ClassID;
                proposedReturnValue.RaceID = this.RaceID;
                return proposedReturnValue;
            }
            set { this.StrengthScore = value.StrengthScore;
                this.DexterityScore = value.DexterityScore;
                this.ConstitutionScore = value.ConstitutionScore;
                this.IntelligenceScore = value.IntelligenceScore;
                this.WisdomScore = value.WisdomScore;
                this.CharismaScore = value.CharismaScore;
                this.CharacterName = value.CharacterName;
                this.ClassID = value.ClassID;
                this.RaceID = value.RaceID;
            }
        }

        public CharacterBLL ModifiedCharacter
        {
            get
            {
                CharacterBLL proposedReturnValue = new CharacterBLL();
                proposedReturnValue.StrengthScore = ModStr;
                proposedReturnValue.DexterityScore = ModDex;
                proposedReturnValue.ConstitutionScore = ModCon;
                proposedReturnValue.IntelligenceScore = ModInt;
                proposedReturnValue.WisdomScore = ModWis;
                proposedReturnValue.CharismaScore = ModCha;
                proposedReturnValue.RaceID = RaceID;
                proposedReturnValue.ClassID = ClassID;
                proposedReturnValue.CharacterName = CharacterName;
                return proposedReturnValue;
            }
            set
            {
                ModStr = value.StrengthScore;
                ModDex = value.DexterityScore;
                ModCon = value.ConstitutionScore;
                ModInt = value.IntelligenceScore;
                ModWis = value.WisdomScore;
                ModCha = value.CharismaScore;
            }
        }

    }
}