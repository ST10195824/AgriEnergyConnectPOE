using AgriEnergyConnectPOE.Data;

namespace AgriEnergyConnectPOE.Services
{
    //------------------------------------------------------------------------------------//
    //                              *Image Service*                                       //
    //------------------------------------------------------------------------------------//

    /// <summary>
    /// Service for handling image operations.
    /// </summary>
    public class ImageService
    {
        //----------------------------------------------------------//
        //                          *Fields*                        //
        //----------------------------------------------------------//
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _env;

        //-------------------------------*Constructor*-------------------------------//
        /// <summary>
        /// Initializes a new instance of the <see cref="ImageService"/> class.
        /// </summary>
        /// <param name="context">The application database context.</param>
        /// <param name="env">The web host environment.</param>
        public ImageService(ApplicationDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        //-------------------------------*Save Image*-------------------------------//
        /// <summary>
        /// Saves the image asynchronously.
        /// </summary>
        /// <param name="file">The image file to be saved.</param>
        /// <returns>The file path of the saved image.</returns>
        /// <exception cref="ArgumentException">Thrown when the file is not valid or not an image.</exception>
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

        //-------------------------------*Generate Unique File Name*-------------------------------//
        /// <summary>
        /// Generates a unique file name for the image.
        /// </summary>
        /// <param name="fileName">The original file name.</param>
        /// <returns>The unique file name.</returns>
        private string GetUniqueFileName(string fileName)
        {
            fileName = Path.GetFileName(fileName);
            return Path.GetFileNameWithoutExtension(fileName)
                   + "_"
                   + Guid.NewGuid().ToString().Substring(0, 4)
                   + Path.GetExtension(fileName);
        }

        //-------------------------------*Check If File Is Image*-------------------------------//
        /// <summary>
        /// Checks if the file is a valid image.
        /// </summary>
        /// <param name="file">The file to check.</param>
        /// <returns><c>true</c> if the file is an image; otherwise, <c>false</c>.</returns>
        private bool IsImage(IFormFile file)
        {
            var imageTypes = new[] { ".jpg", ".jpeg", ".png", ".gif", ".bmp" };
            var fileExtension = Path.GetExtension(file.FileName).ToLowerInvariant();

            return imageTypes.Contains(fileExtension);
        }
    }
}
//----------------------------------------------------End_of_File----------------------------------------------------//
