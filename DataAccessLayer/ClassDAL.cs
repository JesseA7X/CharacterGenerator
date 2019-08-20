using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class ClassDAL
    {
        #region Direct Properties
        public int ClassID { get; set; }
        public string ClassName { get; set; }
        public string Description { get; set; }
        public int ClassModifier { get; set; }
        #endregion

        #region Indirect Properties
        // this class does not have any indirect properties
        // because the Classes Table does not have any Foreign keys
        #endregion 

        public override string ToString()
        {
            return $"ClassID: {ClassID} ClassName: {ClassName} Description: {Description} ClassModifier: {ClassModifier}";
        }
    }
}
