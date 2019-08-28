using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BusinessLogicLayer
{
    // modifier is here to pass in the ID's and modifier amounts from their respective classes into the meaningful calculation
    public class Modifier
    {
        public int StatID { get; set; }
        public int ModifierAmount { get; set; }

        public Modifier()
        {

        }

         
    }
}
