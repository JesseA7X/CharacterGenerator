using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;

namespace BusinessLogicLayer
{
    public class ClassModifier : Modifier
    {
        public int ClassID { get; set; }
        public int ClassModifierID { get; set; }

        public ClassModifier()
        {

        }

        public ClassModifier(ClassModifierDAL dal)
        {
            this.ClassID = dal.ClassID;
            this.ClassModifierID = dal.ClassModifierID;
            this.ModifierAmount = dal.Modifier;
            this.StatID = dal.StatID;
        }

        public override string ToString()
        {
            return $"ClassID: {ClassID} ClassModifierID: {ClassModifierID} ModifierAmount: {ModifierAmount} StatID: {StatID}";
        }
    }
}
