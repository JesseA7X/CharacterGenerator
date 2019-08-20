using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class ClassModifierDAL
    {
        #region Direct Properties
        public int ClassModifierID { get; set; }
        public int ClassID { get; set; }
        public int StatID { get; set; }
        public int Modifier { get; set; }
        #endregion

        #region Indirect Properties
        public string ClassName { get; set; }
        #endregion 

        public override string ToString()
        {
            return $"ClassModifierID: {ClassModifierID} RaceID: {ClassID} StatID: {StatID} Modifier: {Modifier} RaceName: {ClassName}";
        }
    }
}
