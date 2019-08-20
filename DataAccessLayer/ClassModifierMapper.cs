using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace DataAccessLayer
{
    public class ClassModifierMapper : Mapper
    {
        int OffsetToClassModifierID; // expected to be 0
        int OffsetToClassID; // expected to be 1
        int OffsetToStatID; // expected to be 2
        int OffsetToModifier; // expected to be 3
        int OffsetToClassName; // expected to be 4

        public ClassModifierMapper(SqlDataReader reader)
        {
            OffsetToClassModifierID = reader.GetOrdinal("ClassModifierID");
            Assert(0 == OffsetToClassModifierID, "The ClassModifierID is not 0 as expected");
            OffsetToClassID = reader.GetOrdinal("ClassID");
            Assert(1 == OffsetToClassID, "The ClassID is not 1 as expected");
            OffsetToStatID = reader.GetOrdinal("StatID");
            Assert(2 == OffsetToStatID, "The StatID is not 2 as expected");
            OffsetToModifier = reader.GetOrdinal("Modifier");
            Assert(3 == OffsetToModifier, "The Modifier is not 3 as expected");
            OffsetToClassName = reader.GetOrdinal("ClassName");
            Assert(4 == OffsetToClassName, "The ClassName is not 4 as expected");
        }

        public ClassModifierDAL ClassModifierFromReader(SqlDataReader reader)
        {
            ClassModifierDAL proposedReturnValue = new ClassModifierDAL();
            proposedReturnValue.ClassModifierID = GetInt32OrDefault(reader, OffsetToClassModifierID);
            proposedReturnValue.ClassID = GetInt32OrDefault(reader, OffsetToClassID);
            proposedReturnValue.StatID = GetInt32OrDefault(reader, OffsetToStatID);
            proposedReturnValue.Modifier = GetInt32OrDefault(reader, OffsetToModifier);
            proposedReturnValue.ClassName = GetStringOrDefault(reader, OffsetToClassName);
            return proposedReturnValue;
        }
    }
}
