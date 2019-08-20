using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class RaceDAL
    {
        #region Direct Properties
        public int RaceID { get; set; }
        public string RaceName { get; set; }
        public int RaceModifier { get; set; }
        #endregion

        #region Indirect Properties
        // this class doesnt have any indirect properties
        // because the Races table doesnt have any foreign keys
        #endregion 

        public override string ToString()
        {
            return $"RaceID: {RaceID} RaceName: {RaceName} RaceModifier: {RaceModifier}";
        }
    }
}
