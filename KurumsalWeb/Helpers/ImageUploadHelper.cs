using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Helpers;

namespace KurumsalWeb.Helpers
{
    public static class ImageUploadHelper
    {
        private static readonly HashSet<string> AllowedExtensions = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
        {
            ".jpg",
            ".jpeg",
            ".png",
            ".webp"
        };

        private const int MaxImageBytes = 5 * 1024 * 1024; // 5 MB

        public static bool TrySaveResizedImage(
            HttpServerUtilityBase server,
            HttpPostedFileBase file,
            string virtualDirectory,
            int width,
            int height,
            out string relativePath,
            out string errorMessage)
        {
            relativePath = null;
            errorMessage = null;

            if (file == null || file.ContentLength <= 0)
            {
                errorMessage = "Please select an image.";
                return false;
            }

            if (file.ContentLength > MaxImageBytes)
            {
                errorMessage = "Image size must be 5 MB or smaller.";
                return false;
            }

            var extension = Path.GetExtension(file.FileName);
            if (string.IsNullOrEmpty(extension) || !AllowedExtensions.Contains(extension))
            {
                errorMessage = "Only .jpg, .jpeg, .png and .webp files are allowed.";
                return false;
            }

            var safeFileName = Guid.NewGuid().ToString("N") + extension.ToLowerInvariant();
            var normalizedDirectory = virtualDirectory.TrimEnd('/');
            relativePath = normalizedDirectory + "/" + safeFileName;

            var absoluteDirectory = server.MapPath(normalizedDirectory);
            if (!Directory.Exists(absoluteDirectory))
            {
                Directory.CreateDirectory(absoluteDirectory);
            }

            var absolutePath = server.MapPath(relativePath);

            try
            {
                var image = new WebImage(file.InputStream);
                image.Resize(width, height);
                image.Save(absolutePath);
                return true;
            }
            catch
            {
                errorMessage = "Uploaded file is not a valid image.";
                relativePath = null;
                return false;
            }
        }
    }
}
