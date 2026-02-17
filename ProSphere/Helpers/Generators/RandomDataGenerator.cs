namespace ProSphere.Helpers.Generators
{
    public static class RandomDataGenerator
    {
        public static string GenerateString(char[] array, int numberOfChars)
        {
            if (array == null) throw new ArgumentNullException(nameof(array));
            if (array.Length == 0 || numberOfChars <= 0) return string.Empty;

            var sb = new System.Text.StringBuilder(numberOfChars);
            var random = System.Random.Shared; // .NET 6+; or use a thread-safe static Random
            for (int i = 0; i < numberOfChars; i++)
            {
                int idx = random.Next(0, array.Length);
                sb.Append(array[idx]);
            }
            return sb.ToString();
        }
    }
}
