using ProSphere.Domain.Constants.SetsConstants;
using ProSphere.Domain.Enums;

namespace ProSphere.Helpers.Generators
{
    public static class PasswordGenerator
    {
        public static string Generate(PasswordDificulty dificulty)
        {
            string password = "";
            int numberOfChars = dificulty switch
            {
                PasswordDificulty.Low => 2,
                PasswordDificulty.Medium => 6,
                PasswordDificulty.High => 9,
                _ => 0
            };

            password += RandomDataGenerator.GenerateString(CharacterSets.numbers, numberOfChars);
            password += RandomDataGenerator.GenerateString(CharacterSets.safeSpecialChars, numberOfChars);
            password += RandomDataGenerator.GenerateString(CharacterSets.lowerCaseLetters, numberOfChars);
            password += RandomDataGenerator.GenerateString(CharacterSets.upperCaseLetters, numberOfChars);

            return password;
        }
    }
}
