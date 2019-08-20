using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;

namespace BusinessLogicLayer
{
    public class ClassBLL
    {
        public int ClassID { get; set; }
        public string ClassName { get; set; }
        public string Description { get; set; }
        public int ClassModifier { get; set; }

        public ClassBLL()
        {

        }

        public ClassBLL(ClassDAL dal)
        {
            this.ClassID = dal.ClassID;
            this.ClassName = dal.ClassName;
            this.Description = dal.Description;
            this.ClassModifier = dal.ClassModifier;
        }

        public override string ToString()
        {
            return $"ClassID: {ClassID} ClassName: {ClassName} Description: {Description} ClassModifier: {ClassModifier}";
        }
    }
}
