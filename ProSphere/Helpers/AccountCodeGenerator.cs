using ProSphere.Domain.Constants;

namespace ProSphere.Helpers
{
    public static class AccountCodeGenerator
    {
        public static string Generate()
        {
            string code = "";
            code += RandomDataGenerator.GenerateString(CharacterSets.numbers, 4);
            code += RandomDataGenerator.GenerateString(CharacterSets.upperCaseLetters, 1);
            code += RandomDataGenerator.GenerateString(CharacterSets.lowerCaseLetters, 1);
            return code;
        }
    }
}
