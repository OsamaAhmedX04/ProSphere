namespace ProSphere.Domain.Constants.FileConstants
{
    public static class FileRestriction
    {
        #region Image
        public static int AllowableImageFileSize = 1 * 1024 * 1024;
        public static List<string> AllowableImageExtensions = new List<string> { ".jpg", ".jpeg", ".png" };
        #endregion

        #region CVFile
        public static int AllowableCVFileSize = 3 * 1024 * 1024;
        public static List<string> AllowableCVFileExtensions = new List<string> { ".pdf", ".docx" };
        #endregion
    }
}
