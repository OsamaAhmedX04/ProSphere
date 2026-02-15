namespace ProSphere.Domain.Constants.FileConstants
{
    public static class FileRestriction
    {
        #region Image
        public static int AllowableImageFileSize = 1 * 1024 * 1024; // 1MB
        public static List<string> AllowableImageExtensions = new List<string> { ".jpg", ".jpeg", ".png" };
        #endregion
    }
}
