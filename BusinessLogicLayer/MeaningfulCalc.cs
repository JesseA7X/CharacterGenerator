using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer
{
    // this class was created for unit testing my meaningful calculation, it is not dependent on the DAL, and it is also used by the context BLL to actually preform the calculations
    public class MeaningfulCalc
    {
        static Random diceRoll = new Random();
        public int Roll()
        {
            List<int> rolls = new List<int>();
            for (int x = 0; x < 4; x++)
            {
                rolls.Add(diceRoll.Next(1, 7));
            }
            return rolls.Sum() - rolls.Min();
        }

        // orignal code below updated in order to roll 4 dice and drop the low
        //{
        //    int diceRoll1 = diceRoll.Next(1, 7);
        //    int diceRoll2 = diceRoll.Next(1, 7);
        //    int diceRoll3 = diceRoll.Next(1, 7);
        //    int diceRoll4 = diceRoll.Next(1, 7);
        //    int totalRoll = (diceRoll1 + diceRoll2 + diceRoll3 + diceRoll4);
        //    return totalRoll;
        //}

        public void CharacterModification(CharacterBLL u, List<Modifier> Modifiers)
        {
            foreach (Modifier m in Modifiers)
            {
                // this switch statement is taking the base stats of a character and adding in the race and class modifiers (if any) to each stat and returning the total value
                switch ((m.StatID))
                {
                    // due to the offset of the stats the cases start at 6 and end at 11
                    case (6):
                        u.StrengthScore = u.StrengthScore + m.ModifierAmount;
                        break;
                    case (7):
                        u.DexterityScore = u.DexterityScore + m.ModifierAmount;
                        break;
                    case (8):
                        u.ConstitutionScore = u.ConstitutionScore + m.ModifierAmount;
                        break;
                    case (9):
                        u.IntelligenceScore = u.IntelligenceScore + m.ModifierAmount;
                        break;
                    case (10):
                        u.WisdomScore = u.WisdomScore + m.ModifierAmount;
                        break;
                    case (11):
                        u.CharismaScore = u.CharismaScore + m.ModifierAmount;
                        break;
                }

            }
        }
    }
}
