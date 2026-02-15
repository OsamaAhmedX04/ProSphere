namespace ProSphere.Helpers.Generators
{
    public static class RandomDataGenerator
    {
        public static string GenerateString(char[] array, int numberOfChars)
        {
            string text = "";
            Random random = new Random();
            for (int i = 0; i < numberOfChars; i++)
            {
                var randomNumber = random.NextInt64(0, array.Length);
                char randomChar = array[randomNumber];
                text += randomChar;
            }
            return text;
        }
    }
}
