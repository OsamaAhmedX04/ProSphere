namespace ProSphere.Helpers
{
    public static class RandomDataGenerator
    {
        public static string GetRandomString(char[] array, int numberOfChars)
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
