using System.Drawing;
using System.IO;
using System;

namespace st_10436196_partTwo
{
    public class ascii_art
    {
        // Constructor for generating ASCII art from an image
        public ascii_art()
        {
            // Get the full directory path of the current project
            string full_location = AppDomain.CurrentDomain.BaseDirectory;

            // to get the root directory of the project
            string new_location = full_location.Replace("bin\\Debug\\", "");

            // Combine the root directory path with the image filename 
            string full_path = Path.Combine(new_location, "logo.jpg");

            // Load the image from the file path using Bitmap
            Bitmap image = new Bitmap(full_path);

            // Resize the image to fit the console dimensions 
            image = new Bitmap(image, new Size(150, 120));

            // Loop through each row (height) of the image
            for (int height = 0; height < image.Height; height++)
            {
                // Loop through each column (width) of the image
                for (int width = 0; width < image.Width; width++)
                {
                    // Get the pixel color at the current position
                    Color pixelColor = image.GetPixel(width, height);

                    // Convert the pixel color to grayscale by averaging the RGB values
                    int gray = (pixelColor.R + pixelColor.G + pixelColor.B) / 3;

                    // Determine which ASCII character to use based on the grayscale value
                    char asciiChar = gray > 200 ? '-' :
                                     gray > 150 ? '*' :
                                     gray > 100 ? 'o' :
                                     gray > 50 ? '#' : '@';

                    // Print the corresponding ASCII character to the console
                    Console.Write(asciiChar);
                }

                // Move to the next line after processing a row of pixels
                Console.WriteLine();
            }
        } // End of constructor
    } //end of class
}//end of namespace
