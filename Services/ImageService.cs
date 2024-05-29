using AgriEnergyConnectPOE.Data;

namespace AgriEnergyConnectPOE.Services
{
    /// <summary>
    /// Please note that you'll need to handle the case where the product doesn't exist in the GetImagePathAsync method.
    /// Also, you'll need to handle the case where the file upload fails in the SaveImageAsync method. 
    /// This is a basic implementation and might need to be adjusted based on your specific requirements.
    /// </summary>
    public class ImageService
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _env;

        public ImageService(ApplicationDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public async Task<string> SaveImageAsync(IFormFile file)
        {
            if (file == null || file.Length == 0)
                throw new ArgumentException("File is not valid.");

            if (!IsImage(file))
                throw new ArgumentException("File is not a valid image.");

            var uniqueFileName = GetUniqueFileName(file.FileName);
            var imagesFolder = Path.Combine(_env.WebRootPath, "images");
            var filePath = Path.Combine(imagesFolder, uniqueFileName);

            if (!Directory.Exists(imagesFolder))
            {
                Directory.CreateDirectory(imagesFolder);
            }

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }

            return "/images/" + uniqueFileName;
        }

        public async Task<string> GetImagePathAsync(int productId)
        {
            var product = await _context.Products.FindAsync(productId);
            return product?.ImagePath;
        }

        public async Task DeleteImageAsync(string imagePath)
        {
            var filePath = Path.Combine(_env.WebRootPath, imagePath.TrimStart('/'));

            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }

        private string GetUniqueFileName(string fileName)
        {
            fileName = Path.GetFileName(fileName);
            return Path.GetFileNameWithoutExtension(fileName)
                   + "_"
                   + Guid.NewGuid().ToString().Substring(0, 4)
                   + Path.GetExtension(fileName);
        }

        private bool IsImage(IFormFile file)
        {
            var imageTypes = new[] { ".jpg", ".jpeg", ".png", ".gif", ".bmp" };
            var fileExtension = Path.GetExtension(file.FileName).ToLowerInvariant();

            return imageTypes.Contains(fileExtension);
        }
    }
}
