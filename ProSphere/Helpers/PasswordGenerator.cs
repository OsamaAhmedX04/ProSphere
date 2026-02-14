using ProSphere.Domain.Constants;
using ProSphere.Domain.Enums;

namespace ProSphere.Helpers
{
    public static class PasswordGenerator
    {
        public static string Generate(PasswordDificulty dificulty)
        {
            string password = "";
            int numberOfChars = dificulty switch
            {
                PasswordDificulty.Low => 3,
                PasswordDificulty.Medium => 6,
                PasswordDificulty.High => 9,
                _ => 0
            };

            password += RandomDataGenerator.GenerateString(CharacterSets.numbers, numberOfChars);
            password += RandomDataGenerator.GenerateString(CharacterSets.specialChars, numberOfChars);
            password += RandomDataGenerator.GenerateString(CharacterSets.lowerCaseLetters, numberOfChars);

            return password;
        }
    }
}
