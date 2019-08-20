using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace DataAccessLayer
{
    public class RaceMapper : Mapper
    {
        int OffsetToRaceID; // should be 0
        int OffsetToRaceName; // should be 1
        int OffsetToRaceModifier; // should be 2

        public RaceMapper(SqlDataReader reader)
        {
            OffsetToRaceID = reader.GetOrdinal("RaceID");
            Assert(0 == OffsetToRaceID, "The RaceID is not 0 as expected");
            OffsetToRaceName = reader.GetOrdinal("RaceName");
            Assert(1 == OffsetToRaceName, "The RaceName is not 1 as expected");
            OffsetToRaceModifier = reader.GetOrdinal("RaceModifier");
            Assert(2 == OffsetToRaceModifier, "The RaceModifier is not 2 as expected");
        }
        public RaceDAL RaceFromReader(SqlDataReader reader)
        {
            RaceDAL proposedReturnValue = new RaceDAL();
            proposedReturnValue.RaceID = GetInt32OrDefault(reader, OffsetToRaceID);
            proposedReturnValue.RaceName = GetStringOrDefault(reader, OffsetToRaceName);
            proposedReturnValue.RaceModifier = GetInt32OrDefault(reader, OffsetToRaceModifier);
            return proposedReturnValue;
        }
    }
}
