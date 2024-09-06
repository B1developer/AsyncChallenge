class Program
{
    static async Task Main(string[] args)
    {
        // URLs of images to download
        string[] imageUrls = new string[]
        {
            "https://images.unsplash.com/photo-1506748686214-e9df14d4d9d0",  // Links are safe, you can check it if you want
            "https://images.unsplash.com/photo-1501785888041-af3ef285b470",
            "https://images.unsplash.com/photo-1507525428034-b723cf961d3e"
        };
        // Folder where images will be saved
        string downloadFolder = Path.Combine(Directory.GetCurrentDirectory(), "Downloads");

        // Create the folder if it doesn't exist
        if (!Directory.Exists(downloadFolder))
        {
            Directory.CreateDirectory(downloadFolder);
        }

        // Start downloading images asynchronously
        await DownloadImagesAsync(imageUrls, downloadFolder);

        Console.WriteLine("All images downloaded successfully!");
    }

    // Method to download images asynchronously
    static async Task DownloadImagesAsync(string[] urls, string folderPath)
    {
        HttpClient httpClient = new HttpClient();

        for (int i = 0; i < urls.Length; i++)
        {
            string url = urls[i];

            try
            {
                // Display download start message
                Console.WriteLine($"Downloading image {i + 1}/{urls.Length} from {url}...");

                // Get the image data asynchronously
                byte[] imageBytes = await httpClient.GetByteArrayAsync(url);

                // Create a filename based on the image index
                string fileName = $"image{i + 1}.jpg";
                string filePath = Path.Combine(folderPath, fileName);

                // Save the image data to a file asynchronously
                await File.WriteAllBytesAsync(filePath, imageBytes);

                // Display success message
                Console.WriteLine($"Image {i + 1} downloaded and saved as {fileName}.\n");
            }
            catch (Exception ex)
            {
                // Handle any errors that occur during download
                Console.WriteLine($"Failed to download image {i + 1} from {url}. Error: {ex.Message}\n");
            }
        }
    }
}
