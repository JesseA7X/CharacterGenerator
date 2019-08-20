using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;

namespace BusinessLogicLayer
{
    public class RaceBLL
    {
        public int RaceID { get; set; }
        public string RaceName { get; set; }
        public int RaceModifier { get; set; }

        public RaceBLL()
        {

        }

        public RaceBLL(RaceDAL dal)
        {
            this.RaceID = dal.RaceID;
            this.RaceName = dal.RaceName;
            this.RaceModifier = dal.RaceModifier;
        }

        public override string ToString()
        {
            return $"RaceID: {RaceID} RaceName: {RaceName} RaceModifier: {RaceModifier}";
        }
    }
}
