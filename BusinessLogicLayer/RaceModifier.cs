using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;

namespace BusinessLogicLayer
{
    public class RaceModifier : Modifier
    {
        public int RaceID { get; set; }
        public int RaceModifierID { get; set; }

        public RaceModifier()
        {

        }

        public RaceModifier(RaceModifierDAL dal)
        {
            this.RaceID = dal.RaceID;
            this.RaceModifierID = dal.RaceModifierID;
            this.ModifierAmount = dal.Modifier;
            this.StatID = dal.StatID;
        }

        public override string ToString()
        {
            return $"RaceID: {RaceID} RaceModifierID: {RaceModifierID} ModifierAmount: {ModifierAmount} StatID: {StatID}";
        }
    }
}
