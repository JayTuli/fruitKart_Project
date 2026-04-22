//namespace FruitCart_API.Services
//{
//    public class CloudinaryService
//    {

//    }
//}
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using System.Net;

namespace FruitCart_API.Services
{
    public class CloudinaryService : ICloudinaryService
    {
        private readonly Cloudinary _cloudinary;
        private readonly ILogger<CloudinaryService> _logger;

        public CloudinaryService(IConfiguration configuration, ILogger<CloudinaryService> logger)
        {
            _logger = logger;

            // Get Cloudinary credentials from appsettings.json
            var cloudName = configuration["Cloudinary:CloudName"];
            var apiKey = configuration["Cloudinary:APIKey"];
            var apiSecret = configuration["Cloudinary:ApiSecret"];

            // Validate credentials
            if (string.IsNullOrEmpty(cloudName) || string.IsNullOrEmpty(apiKey) || string.IsNullOrEmpty(apiSecret))
            {
                throw new InvalidOperationException(
                    "Cloudinary credentials not configured in appsettings.json");
            }

            // Initialize Cloudinary client
            var account = new Account(cloudName, apiKey, apiSecret);
            _cloudinary = new Cloudinary(account);
        }

        /// <summary>
        /// Upload image to Cloudinary
        /// Returns full HTTPS URL: https://res.cloudinary.com/{cloud_name}/image/upload/{public_id}.{format}
        /// </summary>
        public async Task<string> UploadImageAsync(IFormFile file)
        {
            try
            {
                // Validate file
                if (file == null || file.Length == 0)
                    throw new ArgumentException("File is empty or null");

                // Allowed formats
                var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif", ".webp" };
                var fileExtension = Path.GetExtension(file.FileName).ToLower();

                if (!allowedExtensions.Contains(fileExtension))
                    throw new ArgumentException(
                        $"File format '{fileExtension}' not allowed. Use: jpg, jpeg, png, gif, webp");

                // Validate file size (max 10MB)
                if (file.Length > 10 * 1024 * 1024)
                    throw new ArgumentException("File size exceeds 10MB limit");

                // Generate unique public_id
                var publicId = $"fruitcart/menu_{Guid.NewGuid().ToString().Substring(0, 8)}";

                // Upload to Cloudinary
                var uploadParams = new ImageUploadParams()
                {
                    File = new FileDescription(file.FileName, file.OpenReadStream()),
                    PublicId = publicId,
                    Overwrite = true,
                    Folder = "fruitcart/menu-items",
                    Transformation = new Transformation()
                        .Width(500)                    // Auto-resize to 500px width
                        .Height(500)                   // Max height 500px
                        .Crop("fill")                  // Fill the bounding box
                        .Quality("auto")               // Auto optimize quality
                        .FetchFormat("auto")           // Serve best format for browser
                };

                var uploadResult = await _cloudinary.UploadAsync(uploadParams);

                if (uploadResult.StatusCode == HttpStatusCode.OK)
                {
                    _logger.LogInformation(
                        $"Image uploaded successfully: {uploadResult.SecureUrl}");

                    // Return HTTPS URL
                    return uploadResult.SecureUrl.ToString();
                }
                else
                {
                    throw new Exception(
                        $"Upload failed: {uploadResult.Error?.Message}");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    $"Error uploading image to Cloudinary: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Delete image from Cloudinary using public_id
        /// Input can be full URL or public_id
        /// </summary>
        public async Task<bool> DeleteImageAsync(string imageUrl)
        {
            try
            {
                if (string.IsNullOrEmpty(imageUrl))
                    return false;

                // Extract public_id from URL if full URL provided
                // Example: https://res.cloudinary.com/dpz82wj2r/image/upload/v123/fruitcart/menu_abc123.jpg
                // We need: fruitcart/menu_abc123
                string publicId = ExtractPublicIdFromUrl(imageUrl);

                if (string.IsNullOrEmpty(publicId))
                    return false;

                var deleteParams = new DeletionParams(publicId);
                var deleteResult = await _cloudinary.DestroyAsync(deleteParams);

                if (deleteResult.StatusCode == HttpStatusCode.OK ||
                    deleteResult.Result == "ok")
                {
                    _logger.LogInformation(
                        $"Image deleted successfully: {publicId}");
                    return true;
                }
                else
                {
                    _logger.LogWarning(
                        $"Failed to delete image: {deleteResult.Error?.Message}");
                    return false;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    $"Error deleting image from Cloudinary: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Extract public_id from Cloudinary URL
        /// URL format: https://res.cloudinary.com/{cloud}/image/upload/v{version}/{public_id}.{format}
        /// </summary>
        private string ExtractPublicIdFromUrl(string imageUrl)
        {
            try
            {
                if (imageUrl.StartsWith("http"))
                {
                    // Full URL - extract public_id
                    var uri = new Uri(imageUrl);
                    var path = uri.LocalPath; // /image/upload/v123/fruitcart/menu_abc123.jpg

                    // Remove /image/upload part
                    var parts = path.Split('/');

                    // Find index of "upload"
                    var uploadIndex = Array.IndexOf(parts, "upload");
                    if (uploadIndex == -1) return imageUrl;

                    // Join parts after upload (skip version)
                    var remaining = string.Join("/", parts.Skip(uploadIndex + 2));

                    // Remove file extension
                    return Path.GetFileNameWithoutExtension(remaining);
                }
                else
                {
                    // Assume it's already a public_id
                    return imageUrl;
                }
            }
            catch
            {
                return null;
            }
        }
    }
}