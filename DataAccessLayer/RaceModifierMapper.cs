using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace DataAccessLayer
{
    public class RaceModifierMapper : Mapper
    {
        int OffsetToRaceModifierID; // expected to be 0
        int OffsetToRaceID; // expected to be 1
        int OffsetToStatID; // expected to be 2
        int OffsetToModifier; // expected to be 3
        int OffsetToRaceName; // expected to be 4

        public RaceModifierMapper(SqlDataReader reader)
        {
            OffsetToRaceModifierID = reader.GetOrdinal("RaceModifierID");
            Assert(0 == OffsetToRaceModifierID, "The RaceModifierID is not 0 as expected");
            OffsetToRaceID = reader.GetOrdinal("RaceID");
            Assert(1 == OffsetToRaceID, "The RaceID is not 1 as expected");
            OffsetToStatID = reader.GetOrdinal("StatID");
            Assert(2 == OffsetToStatID, "The StatID is not 2 as expected");
            OffsetToModifier = reader.GetOrdinal("Modifier");
            Assert(3 == OffsetToModifier, "The Modifier is not 3 as expected");
            OffsetToRaceName = reader.GetOrdinal("RaceName");
            Assert(4 == OffsetToRaceName, "The RaceName is not 4 as expected");
        }

        public RaceModifierDAL RaceModifierFromReader(SqlDataReader reader)
        {
            RaceModifierDAL proposedReturnValue = new RaceModifierDAL();
            proposedReturnValue.RaceModifierID = GetInt32OrDefault(reader, OffsetToRaceModifierID);
            proposedReturnValue.RaceID = GetInt32OrDefault(reader, OffsetToRaceID);
            proposedReturnValue.StatID = GetInt32OrDefault(reader, OffsetToStatID);
            proposedReturnValue.Modifier = GetInt32OrDefault(reader, OffsetToModifier);
            proposedReturnValue.RaceName = GetStringOrDefault(reader, OffsetToRaceName);
            return proposedReturnValue;
        }
    }
}
