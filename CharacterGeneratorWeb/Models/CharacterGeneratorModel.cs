using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessLogicLayer;

namespace CharacterGeneratorWeb.Models
{
    public class CharacterGeneratorModel
    {
        public int StrengthScore { get; set; }  public int ModStr { get; set; }
        public int DexterityScore { get; set; } public int ModDex { get; set; }
        public int ConstitutionScore { get; set; } public int ModCon { get; set; }
        public int IntelligenceScore { get; set; } public int ModInt { get; set; }
        public int WisdomScore { get; set; } public int ModWis { get; set; }
        public int CharismaScore { get; set; } public int ModCha { get; set; }
        public int ClassID { get; set; } 
        public int RaceID { get; set; }
        public List<SelectListItem> ClassNames { get; set; }
        public List<SelectListItem> RaceNames { get; set; }
        public CharacterBLL BaseCharacter
        {
            get { CharacterBLL proposedReturnValue = new CharacterBLL();
                proposedReturnValue.StrengthScore = this.StrengthScore;
                proposedReturnValue.DexterityScore = this.DexterityScore;
                proposedReturnValue.ConstitutionScore = this.ConstitutionScore;
                proposedReturnValue.IntelligenceScore = this.IntelligenceScore;
                proposedReturnValue.WisdomScore = this.WisdomScore;
                proposedReturnValue.CharismaScore = this.CharismaScore;
                return proposedReturnValue;
            }
            set { this.StrengthScore = value.StrengthScore;
                this.DexterityScore = value.DexterityScore;
                this.ConstitutionScore = value.ConstitutionScore;
                this.IntelligenceScore = value.IntelligenceScore;
                this.WisdomScore = value.WisdomScore;
                this.CharismaScore = value.CharismaScore;
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