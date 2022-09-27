using SaveMyCollectionsEnums;
using SaveMyCollections.Models;
using ImageProcessor;
using ImageProcessor.Imaging;
using ImageProcessor.Imaging.Formats;
using System.Drawing;

namespace SaveMyCollections.Services
{
    public class UserPhotoServise
    {
        private const string ImageLocation = "Images";

        public static async Task<UserPhoto> CreateImageAsync(IWebHostEnvironment _hostingEnv, MyColectionType type,
            IFormFile file, ApplicationUser user)
        {
            var childDirectory = GetPath(_hostingEnv, type, user);            
            var newFileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            var prevFileName = "prev_" + newFileName;
            var newFilePath = Path.Combine(childDirectory, newFileName);
            var prevNewFilePath = Path.Combine(childDirectory, prevFileName);

            int quality = 80;
            var format = new JpegFormat(); 

            using (var memoryStream = new MemoryStream())
            {
                await file.CopyToAsync(memoryStream);
                                
                using (var imageFactory = new ImageFactory(preserveExifData: true))
                {
                    imageFactory.Load(memoryStream.ToArray()).                    
                        Resize(new ResizeLayer(new Size(1920, 1080), resizeMode: ResizeMode.Max))
                        .Format(format)
                        .Quality(quality)
                        .Save(newFilePath);

                    imageFactory.Load(memoryStream.ToArray()).
                        Resize(new ResizeLayer(new Size(320, 180), resizeMode: ResizeMode.Max))
                        .Format(format)
                        .Quality(quality)
                        .Save(prevNewFilePath);
                }
            }
            
            var photo = new UserPhoto();
            photo.FileName = newFileName;
            photo.PrevFileName = prevFileName;
            photo.FileExtension = Path.GetExtension(newFileName);
            photo.FileLocation = childDirectory.Substring(_hostingEnv.WebRootPath.Length);
            photo.Size = file.Length;

            return photo;
        }
        
        private static string GetPath(IWebHostEnvironment _hostingEnv, MyColectionType type, ApplicationUser user)
        {
            var UserParentDirectory = Path.Combine(_hostingEnv.WebRootPath, ImageLocation, type.ToString(), user.Id);

            var childDirectory = Path.Combine(UserParentDirectory, 0.ToString());
            if (Directory.Exists(UserParentDirectory))
            {
                var directories = Directory.GetDirectories(UserParentDirectory).Count();
                childDirectory = Path.Combine(UserParentDirectory, (directories - 1).ToString());
                if (Directory.GetFiles(childDirectory).Count() > 500)
                {
                    childDirectory = Path.Combine(UserParentDirectory, directories.ToString());
                    Directory.CreateDirectory(childDirectory);
                }
            }
            else
            {
                Directory.CreateDirectory(childDirectory);
            }

            return childDirectory;
        }

        public static Task DeletePhotoAsync(IWebHostEnvironment _hostingEnv, UserPhoto photo)
        {
            var filePath = Path.Combine(_hostingEnv.WebRootPath, photo.FileLocation.Substring(1), photo.FileName);
            var prevFilePath = Path.Combine(_hostingEnv.WebRootPath, photo.FileLocation.Substring(1), photo.PrevFileName);
            return Task.Run(() =>
            {
                File.Delete(prevFilePath);
                File.Delete(filePath);
            });
        }
    }
}
 