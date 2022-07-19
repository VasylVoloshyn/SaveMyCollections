using Microsoft.AspNetCore.Identity;
using MyCollection.Data;
using MyCollection.Enums;
using MyCollection.Models;

namespace MyCollection.Service
{
    public class UserPhotoServise
    {
        private const string ImageLocation = "Images";
                
        public static async Task<UserPhoto> CreateImageAsync(IWebHostEnvironment _hostingEnv, MyColectionType type,
            IFormFile file, ApplicationUser user)
        {
            var childDirectory = GetPath(_hostingEnv, type, user);

            var fileName = file.FileName;
            var newFileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            
            var filePath = Path.Combine(childDirectory, fileName);
            var newFilePath = Path.Combine(childDirectory, newFileName);

            using (FileStream fs = File.Create(filePath))
            {                
                await file.CopyToAsync(fs);
            }
            File.Copy(filePath, newFilePath);
            File.Delete(filePath);

            var photo = new UserPhoto();
            photo.FileName = newFileName;
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
                if (Directory.GetFiles(childDirectory).Count() > 5)
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
            return Task.Run(() =>
            {
                File.Delete(filePath);
            });

        }
    }
}
 