using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace DataAccessLayer
{
    public class ClassMapper : Mapper
    {
        int OffsetToClassID; // expected to be 0
        int OffsetToClassName; // expected to be 1
        int OffsetToDescription; // expected to be 2
        int OffsetToClassModifier; // expected to be 3

        public ClassMapper(SqlDataReader reader)
        {
            OffsetToClassID = reader.GetOrdinal("ClassID");
            Assert(0 == OffsetToClassID, "The ClassID is not 0 as expected");
            OffsetToClassName = reader.GetOrdinal("ClassName");
            Assert(1 == OffsetToClassName, "The ClassName is not 1 as expected");
            OffsetToDescription = reader.GetOrdinal("Description");
            Assert(2 == OffsetToDescription, "The Description is not 2 as expected");
            OffsetToClassModifier = reader.GetOrdinal("ClassModifier");
            Assert(3 == OffsetToClassModifier, "The ClassModifier is not 3 as expected");
        }

        public ClassDAL ClassFromReader(SqlDataReader reader)
        {
            ClassDAL proposedReturnValue = new ClassDAL();
            proposedReturnValue.ClassID = GetInt32OrDefault(reader, OffsetToClassID);
            proposedReturnValue.ClassName = GetStringOrDefault(reader, OffsetToClassName);
            proposedReturnValue.Description = GetStringOrDefault(reader, OffsetToDescription);
            proposedReturnValue.ClassModifier = GetInt32OrDefault(reader, OffsetToClassModifier);
            return proposedReturnValue;
        }
    }
}
