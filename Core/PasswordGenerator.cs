using System;
using System.Linq;
using System.Security.Cryptography;

namespace Core
{
    [Flags]
    public enum PasswordRequirements
    {
        None = 0,
        UpperCaseRequired = 1,
        NoSymbol = 2,
        NoNumber = 4,
        SymbolRequired = 8,
        NumberRequired = 16
    }

    public class PasswordGenerator
    {
        PasswordRequirements Requirements;
        string Alphabet = "abcdefghijklmnopqrstuvwxyz";
        string Uppercase = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        string Numbers = "012345679";
        string Symbols = "!@#$%^&*()-_";         

        public PasswordGenerator(PasswordRequirements req)
        {
            Requirements = req;
        }

        public string GeneratePassword(string value, string salt, int length)
        {
            var rawPassword = GenerateRawPassword(value, salt).Substring(0, length);
            var library = Alphabet + Uppercase;
            var testRequirements = Requirements & PasswordRequirements.NoNumber;
            if ((Requirements & PasswordRequirements.NoNumber) == 0)
            {
                library += Numbers;
            }

            if ((Requirements & PasswordRequirements.NoSymbol) == 0)
            {
                library += Symbols;
            }
            var processedPassword = BuildPasswordFromMap(rawPassword, library);
            return VerifyPassword(processedPassword, Requirements);
        }

        private string VerifyPassword(string sourcePassword, PasswordRequirements requirements)
        {
            bool success = true;
            var password = sourcePassword;

            if ((requirements & PasswordRequirements.NumberRequired) > 0)
            {
                success &= (sourcePassword.Any(x => Numbers.Contains(x)));
                if (!success)
                {
                    password += Numbers[GetAscii(sourcePassword[0]) % Numbers.Length];
                }
            }

            if ((requirements & PasswordRequirements.SymbolRequired) > 0)
            {
                success &= (sourcePassword.Any(x => Symbols.Contains(x)));
                password += Symbols[GetAscii(sourcePassword[0]) % Symbols.Length];
            }

            if ((requirements & PasswordRequirements.UpperCaseRequired) > 0)
            {
                success &= (sourcePassword.Any(x => Uppercase.Contains(x)));
                password += Uppercase[GetAscii(sourcePassword[0]) % Uppercase.Length];
            }

            return password;
        }

        private string BuildPasswordFromMap(string raw, string map)
        {
            var mapLength = map.Length;
            var result = string.Empty;
            foreach (var letter in raw)
            {
                var ascii = GetAscii(letter);
                result += map[ascii % mapLength];
            }

            return result;
        }

        private int GetAscii(char c)
        {
            var bytes = System.Text.Encoding.ASCII.GetBytes(new char[] { c });
            return bytes[0];
        }

        private string GenerateRawPassword(string value, string salt)
        {
            using (var sha = SHA512.Create())
            {
                return Convert.ToBase64String(sha.ComputeHash(System.Text.Encoding.UTF8.GetBytes(value + salt)));
            }
        }
    }
}
