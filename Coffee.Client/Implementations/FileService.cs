using Coffee.Client.Interfaces;

namespace Coffee.Client.Implementations
{
    public class FileService : IFileService
    {
        private readonly IWebHostEnvironment _environment;

        public FileService(IWebHostEnvironment environment) => _environment = environment;

        public Tuple<int, string> SaveImage(IFormFile imageFile)
        {
            try
            {
                var wwwPath = _environment.WebRootPath;

                var path = Path.Combine(wwwPath, "Images");

                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                // Проверьте разрешенные расширения
                var ext = Path.GetExtension(imageFile.FileName);
                var allowedExtensions = new string[] { ".jpg", ".png", ".jpeg" };
                if (!allowedExtensions.Contains(ext))
                {
                    string msg = string.Format("Разрешены {0} только расширения", string.Join(",", allowedExtensions));

                    return new Tuple<int, string>(0, msg);
                }

                // guid для названия картинки
                string uniqueString = Guid.NewGuid().ToString();

                //  пытаемся создать здесь уникальное имя файла
                var newFileName = uniqueString + ext;

                var fileWithPath = Path.Combine(path, newFileName);
                var stream = new FileStream(fileWithPath, FileMode.Create);

                imageFile.CopyTo(stream);

                stream.Close();

                return new Tuple<int, string>(1, newFileName);
            }
            catch (Exception ex)
            {
                return new Tuple<int, string>(0, "Произошла ошибка");
            }
        }

        public bool DeleteImage(string imageFileName)
        {
            try
            {
                var wwwPath = _environment.WebRootPath;
                var path = Path.Combine(wwwPath, "Images\\", imageFileName);
                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);

                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
