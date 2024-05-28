using AgriEnergyConnectPOE.Data;

namespace AgriEnergyConnectPOE.Services
{
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

        private string GetUniqueFileName(string fileName)
        {
            fileName = Path.GetFileName(fileName);
            return Path.GetFileNameWithoutExtension(fileName)
                   + "_"
                   + Guid.NewGuid().ToString().Substring(0, 4)
                   + Path.GetExtension(fileName);
        }
    }
}
