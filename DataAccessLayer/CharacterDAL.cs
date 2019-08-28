using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class CharacterDAL
    {
        // the region tags arent needed but they were used as a learning tool to help visualize the shape of the code
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
            return $"CharacterID: {CharacterID,5} UserID: {UserID} CharacterName: {CharacterName} ClassID: {ClassID} RaceID: {RaceID} StrengthScore: {StrengthScore} DexterityScore: {DexterityScore} ConstitutionScore: {ConstitutionScore} IntelligenceScore: {IntelligenceScore} WisdomScore: {WisdomScore} CharismaScore: {CharismaScore} UserName: {UserName} ClassName: {ClassName} RaceName: {RaceName}";
        }
    }
}
