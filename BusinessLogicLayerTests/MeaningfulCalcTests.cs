using Microsoft.VisualStudio.TestTools.UnitTesting;
using BusinessLogicLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Tests
{
    [TestClass()]
    public class MeaningfulCalcTests
    {
        [TestMethod()]
        public void RollTest()
        {
            // arrange
            MeaningfulCalc calc = new MeaningfulCalc();


            // act
            int result = calc.Roll();

            // assert
            Assert.IsTrue(result > 3 && result < 18);
        }

        [TestMethod()]
        public void CharacterModificationTestSTR()
        {
            // arrange
            MeaningfulCalc calc = new MeaningfulCalc();

            CharacterBLL dude = new CharacterBLL();
            dude.StrengthScore = 10;
            dude.DexterityScore = 10;
            dude.ConstitutionScore = 10;
            dude.IntelligenceScore = 10;
            dude.WisdomScore = 10;
            dude.CharismaScore = 10;

            List<Modifier> listMods = new List<Modifier>();

            Modifier mod = new Modifier();
            mod.StatID = 6;
            mod.ModifierAmount = 2;

            listMods.Add(mod);

            var expected = 12;
          
            // act
            calc.CharacterModification(dude, listMods);
            var actual = dude.StrengthScore;

            // assert

            Assert.AreEqual(expected, actual);
            
        }

        [TestMethod()]
        public void CharacterModificationTestDEX()
        {
            // arrange
            MeaningfulCalc calc = new MeaningfulCalc();

            CharacterBLL dude = new CharacterBLL();
            dude.StrengthScore = 10;
            dude.DexterityScore = 10;
            dude.ConstitutionScore = 10;
            dude.IntelligenceScore = 10;
            dude.WisdomScore = 10;
            dude.CharismaScore = 10;

            List<Modifier> listMods = new List<Modifier>();

            Modifier mod = new Modifier();
            mod.StatID = 7;
            mod.ModifierAmount = 2;

            listMods.Add(mod);

            var expected = 12;

            // act
            calc.CharacterModification(dude, listMods);
            var actual = dude.DexterityScore;

            // assert

            Assert.AreEqual(expected, actual);

        }

        [TestMethod()]
        public void CharacterModificationTestCON()
        {
            // arrange
            MeaningfulCalc calc = new MeaningfulCalc();

            CharacterBLL dude = new CharacterBLL();
            dude.StrengthScore = 10;
            dude.DexterityScore = 10;
            dude.ConstitutionScore = 10;
            dude.IntelligenceScore = 10;
            dude.WisdomScore = 10;
            dude.CharismaScore = 10;

            List<Modifier> listMods = new List<Modifier>();

            Modifier mod = new Modifier();
            mod.StatID = 8;
            mod.ModifierAmount = 2;

            listMods.Add(mod);

            var expected = 12;

            // act
            calc.CharacterModification(dude, listMods);
            var actual = dude.ConstitutionScore;

            // assert

            Assert.AreEqual(expected, actual);

        }

        [TestMethod()]
        public void CharacterModificationTestINT()
        {
            // arrange
            MeaningfulCalc calc = new MeaningfulCalc();

            CharacterBLL dude = new CharacterBLL();
            dude.StrengthScore = 10;
            dude.DexterityScore = 10;
            dude.ConstitutionScore = 10;
            dude.IntelligenceScore = 10;
            dude.WisdomScore = 10;
            dude.CharismaScore = 10;

            List<Modifier> listMods = new List<Modifier>();

            Modifier mod = new Modifier();
            mod.StatID = 9;
            mod.ModifierAmount = 2;

            listMods.Add(mod);

            var expected = 12;

            // act
            calc.CharacterModification(dude, listMods);
            var actual = dude.IntelligenceScore;

            // assert

            Assert.AreEqual(expected, actual);

        }

        [TestMethod()]
        public void CharacterModificationTestWIS()
        {
            // arrange
            MeaningfulCalc calc = new MeaningfulCalc();

            CharacterBLL dude = new CharacterBLL();
            dude.StrengthScore = 10;
            dude.DexterityScore = 10;
            dude.ConstitutionScore = 10;
            dude.IntelligenceScore = 10;
            dude.WisdomScore = 10;
            dude.CharismaScore = 10;

            List<Modifier> listMods = new List<Modifier>();

            Modifier mod = new Modifier();
            mod.StatID = 10;
            mod.ModifierAmount = 2;

            listMods.Add(mod);

            var expected = 12;

            // act
            calc.CharacterModification(dude, listMods);
            var actual = dude.WisdomScore;

            // assert

            Assert.AreEqual(expected, actual);

        }

        [TestMethod()]
        public void CharacterModificationTestCHA()
        {
            // arrange
            MeaningfulCalc calc = new MeaningfulCalc();

            CharacterBLL dude = new CharacterBLL();
            dude.StrengthScore = 10;
            dude.DexterityScore = 10;
            dude.ConstitutionScore = 10;
            dude.IntelligenceScore = 10;
            dude.WisdomScore = 10;
            dude.CharismaScore = 10;

            List<Modifier> listMods = new List<Modifier>();

            Modifier mod = new Modifier();
            mod.StatID = 11;
            mod.ModifierAmount = 2;

            listMods.Add(mod);

            var expected = 12;

            // act
            calc.CharacterModification(dude, listMods);
            var actual = dude.CharismaScore;

            // assert

            Assert.AreEqual(expected, actual);

        }
    }
}