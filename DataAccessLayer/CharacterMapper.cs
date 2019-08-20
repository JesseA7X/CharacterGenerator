using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace DataAccessLayer
{
    class CharacterMapper : Mapper
    {
        int OffsetToCharacterID; // expected to be 0
        int OffsetToUserID; // expected to be 1
        int OffsetToCharacterName; // expected to be 2
        int OffsetToClassID; // expected to be 3
        int OffsetToRaceID; // expected to be 4
        int OffsetToStrengthScore; // expected to be 5
        int OffsetToDexterityScore; // expected to be 6
        int OffsetToConstitutionScore; // expected to be 7
        int OffsetToIntelligenceScore; // expected to be 8
        int OffsetToWisdomScore; // expected to be 9
        int OffsetToCharismaScore; // expected to be 10
        int OffsetToUserName; // expected to be 11
        int OffsetToClassName; // expected to be 12
        int OffsetToRaceName; // expected to be 13

        public CharacterMapper(SqlDataReader reader)
        {
            OffsetToCharacterID = reader.GetOrdinal("CharacterID");
            Assert(0 == OffsetToCharacterID, $"CharacterID is {OffsetToCharacterID} not 0 as expected");
            OffsetToUserID = reader.GetOrdinal("UserID");
            Assert(1 == OffsetToUserID, $"UserID is {OffsetToUserID} not 1 as expected");
            OffsetToCharacterName = reader.GetOrdinal("CharacterName");
            Assert(2 == OffsetToCharacterName, $"CharacterName is {OffsetToCharacterName} not 2 as expected");
            OffsetToClassID = reader.GetOrdinal("ClassID");
            Assert(3 == OffsetToClassID, $"ClassID is {OffsetToClassID} not 3 as expected");
            OffsetToRaceID = reader.GetOrdinal("RaceID");
            Assert(4 == OffsetToRaceID, $"RaceID is {OffsetToRaceID} not 4 as expected");
            OffsetToStrengthScore = reader.GetOrdinal("StrengthScore");
            Assert(5 == OffsetToStrengthScore, $"StrengthScore is {OffsetToStrengthScore} not 5 as expected");
            OffsetToDexterityScore = reader.GetOrdinal("DexterityScore");
            Assert(6 == OffsetToDexterityScore, $"DexterityScore is {OffsetToDexterityScore} not 6 as expected");
            OffsetToConstitutionScore = reader.GetOrdinal("ConstitutionScore");
            Assert(7 == OffsetToConstitutionScore, $"ConstitutionScore is {OffsetToConstitutionScore} not 7 as expected");
            OffsetToIntelligenceScore = reader.GetOrdinal("IntelligenceScore");
            Assert(8 == OffsetToIntelligenceScore, $"IntelligenceScore is {OffsetToIntelligenceScore} not 8 as expected");
            OffsetToWisdomScore = reader.GetOrdinal("WisdomScore");
            Assert(9 == OffsetToWisdomScore, $"WisdomScore is {OffsetToWisdomScore} not 9 as expected");
            OffsetToCharismaScore = reader.GetOrdinal("CharismaScore");
            Assert(10 == OffsetToCharismaScore, $"CharismaScore is {OffsetToCharismaScore} not 10 as expected");
            OffsetToUserName = reader.GetOrdinal("UserName");
            Assert(11 == OffsetToUserName, $"UserName is {OffsetToUserName} not 11 as expected");
            OffsetToClassName = reader.GetOrdinal("ClassName");
            Assert(12 == OffsetToClassName, $"ClassName is {OffsetToClassName} not 12 as expected");
            OffsetToRaceName = reader.GetOrdinal("RaceName");
            Assert(13 == OffsetToRaceName, $"RaceName is {OffsetToRaceName} not 13 as expected");

        }

        public CharacterDAL CharacterFromReader(SqlDataReader reader)
        {
            CharacterDAL proposedReturnValue = new CharacterDAL();
            proposedReturnValue.CharacterID = reader.GetInt32(OffsetToCharacterID);
            proposedReturnValue.UserID = reader.GetInt32(OffsetToUserID);
            proposedReturnValue.CharacterName = reader.GetString(OffsetToCharacterName);
            proposedReturnValue.ClassID = reader.GetInt32(OffsetToClassID);
            proposedReturnValue.RaceID = reader.GetInt32(OffsetToRaceID);
            proposedReturnValue.StrengthScore = reader.GetInt32(OffsetToStrengthScore);
            proposedReturnValue.DexterityScore = reader.GetInt32(OffsetToDexterityScore);
            proposedReturnValue.ConstitutionScore = reader.GetInt32(OffsetToConstitutionScore);
            proposedReturnValue.IntelligenceScore = reader.GetInt32(OffsetToIntelligenceScore);
            proposedReturnValue.WisdomScore = reader.GetInt32(OffsetToWisdomScore);
            proposedReturnValue.CharismaScore = reader.GetInt32(OffsetToCharismaScore);
            proposedReturnValue.UserName = reader.GetString(OffsetToUserName);
            proposedReturnValue.ClassName = reader.GetString(OffsetToClassName);
            proposedReturnValue.RaceName = reader.GetString(OffsetToRaceName);
            return proposedReturnValue;
        }


    }
}
