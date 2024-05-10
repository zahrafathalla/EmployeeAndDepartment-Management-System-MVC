namespace Demo.PL.Helper
{
    public static class Documentsetting
    {
        public static string UploadFile(IFormFile file, string folderName)
        {
            var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Files", folderName);

            var fileName = $"{Guid.NewGuid()}-{Path.GetFileName(file.FileName)}";
            //var fileName = Path.GetFileName(file.FileName);


            var filePath = Path.Combine(folderPath, fileName);

            using var fileStream = new FileStream(filePath, FileMode.Create);

            file.CopyTo(fileStream);

            return fileName;


        }
        public static void DeleteFile(string fileName, string folderName)
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Files", folderName, fileName);

            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }
        public static string UpdateFile(IFormFile file, string folderName, string fileName)
        {
            DeleteFile(fileName, folderName);
            return UploadFile(file, folderName);
        }
    }

}
