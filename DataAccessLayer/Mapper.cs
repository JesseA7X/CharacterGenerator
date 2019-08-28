using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace DataAccessLayer
{
    public class Mapper
    {
        // we are using assert because we know the condition should be true but if the condition is not true we will show our exception message
        public void Assert(bool condition, string message)
        {
            if (!condition)
            {
                throw new Exception(message);
            }
        }

        // this code is used to protect against null values in the database
        public string GetStringOrDefault(SqlDataReader reader, int ordinal, string defaultValue = "")
        {
            if( reader.IsDBNull(ordinal))
            {
                return defaultValue;
            }
            else
            {
                return reader.GetString(ordinal);
            }
        }

        public int GetInt32OrDefault(SqlDataReader reader, int ordinal, int defaultValue = 0)
        {
            if (reader.IsDBNull(ordinal))
            {
                return defaultValue;
            }
            else
            {
                return reader.GetInt32(ordinal);
            }
        }
    }
}
