using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Core.Test
{
    [TestClass]
    public class PasswordGeneratorTest
    {
        string Alphabet = "abcdefghijklmnopqrstuvwxyz";
        string Uppercase = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        string Numbers = "012345679";
        string Symbols = "!@#$%^&*()-_";
        Random random = new Random();

        [TestMethod]
        public void PasswordGenerator_GeneratePasswordAllReq()
        {
            var passwordGen = new PasswordGenerator(PasswordRequirements.SymbolRequired | PasswordRequirements.UpperCaseRequired | PasswordRequirements.NumberRequired);            
            var sourceMap = Alphabet + Uppercase + Numbers;            

            for (int i = 0; i < 50; i++)
            {
                var sourceKey = string.Empty;

                for (int j = 0; j < 10; j++)
                {
                    // Not testing for symbols in source since the goal here is to generate a
                    // password from common phrase that is easy to remember
                    sourceKey += sourceMap[random.Next(100) % sourceMap.Length];
                }

                var password = passwordGen.GeneratePassword(sourceKey, "reddit", 10);

                Assert.IsTrue(!String.IsNullOrEmpty(password));
                // Try five sets of password
                Assert.IsTrue(password.Any(x => Numbers.Contains(x)), "Must contain number");
                Assert.IsTrue(password.Any(x => Symbols.Contains(x)), "Must contain symbols");
                Assert.IsTrue(password.Any(x => Uppercase.Contains(x)), "Must contain Uppercase");
            }
        }

        [TestMethod]
        public void PasswordGenerator_GeneratePasswordNoNumber()
        {
            var passwordGen = new PasswordGenerator(PasswordRequirements.SymbolRequired | PasswordRequirements.UpperCaseRequired | PasswordRequirements.NoNumber);
            var sourceMap = Alphabet + Uppercase + Numbers;

            for (int i = 0; i < 50; i++)
            {
                var sourceKey = string.Empty;

                for (int j = 0; j < 10; j++)
                {
                    // Not testing for symbols in source since the goal here is to generate a
                    // password from common phrase that is easy to remember
                    sourceKey += sourceMap[random.Next(100) % sourceMap.Length];
                }

                var password = passwordGen.GeneratePassword(sourceKey, "reddit", 10);

                // Try five sets of password
                Assert.IsTrue(!String.IsNullOrEmpty(password));
                Assert.IsFalse(password.Any(x => Numbers.Contains(x)), "Must not contain number");
                Assert.IsTrue(password.Any(x => Symbols.Contains(x)), "Must contain symbols");
                Assert.IsTrue(password.Any(x => Uppercase.Contains(x)), "Must contain Uppercase");
            }
        }

        [TestMethod]
        public void PasswordGenerator_Consistency()
        {
            var passwordGen = new PasswordGenerator(PasswordRequirements.SymbolRequired | PasswordRequirements.UpperCaseRequired | PasswordRequirements.NumberRequired);
            string[] raw = { "adf353", "ged3gd%", "bkre" };
            string[] salt = { "gmail", "reddit", "bank" };
            string[] expected = { "Z-ofYKkX41&M", "FhVOGW)RyE7-S", "XBz04%XPkj%K" };

            for (int i = 0; i < raw.Length; i++)
            {
                var password = passwordGen.GeneratePassword(raw[i], salt[i], 10);
                Assert.AreEqual(expected[i], password);
            }
        }
    }
}
