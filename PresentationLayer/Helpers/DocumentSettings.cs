//using Microsoft.AspNetCore.Http;
//using System;
//using System.IO;

//namespace PresentationLayer.Helpers
//{
//    public static class DocumentSettings
//    {
//        //public static string UploadFile(IFormFile file, string folderName)
//        //{
//        //    // Check if file is null or has no content
//        //    //if (file == null || file.Length == 0)
//        //    //{
//        //    //    throw new ArgumentException("File cannot be null or empty.", nameof(file));
//        //    //}

//        //    // Generate the path for storing the file
//        //    string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Files", folderName);

//        //    // Ensure that the directory exists
//        //    if (!Directory.Exists(folderPath))
//        //    {
//        //        Directory.CreateDirectory(folderPath);
//        //    }

//        //    // Generate a unique file name
//        //    string uniqueFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(file.FileName);

//        //    // Full path for saving the file
//        //    string filePath = Path.Combine(folderPath, uniqueFileName);

//        //    // Save the file to the server
//        //    using (var fileStream = new FileStream(filePath, FileMode.Create))
//        //    {
//        //        file.CopyTo(fileStream);
//        //    }

//        //    // Return the unique file name for storage in the database
//        //    return uniqueFileName;
//        //}


//    }
//}
