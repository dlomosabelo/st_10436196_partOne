using System.IO;
using System.Media;
using System;

namespace st_10436196_partTwo
{
    public class voice_greeting
    {
        // Constructor to play a welcome greeting sound
        public voice_greeting()
        {
            // Retrieve the full directory path of the current project
            string full_location = AppDomain.CurrentDomain.BaseDirectory;

            // Modify the path to get the root directory of the project by removing "bin\\Debug\\"
            string new_path = full_location.Replace("bin\\Debug\\", "");

            // Try-catch block to handle any potential errors during file loading and playback
            try
            {
                // Combine the base directory with the sound file name to get the full path of the audio file
                string full_path = Path.Combine(new_path, "Welcome-to-the-Cyber.wav");

                // Create a SoundPlayer instance to handle playing the audio file
                using (SoundPlayer play = new SoundPlayer(full_path))
                {
                    // Play the sound file asynchronously
                    play.Play();
                } // End of using block, ensuring the SoundPlayer is disposed of properly

            }
            catch (Exception error)
            {
                // Catch any exceptions that occur and display the error message
                Console.WriteLine(error.Message);
            }

        } // End of constructor
    } // End of class
} // End of namespace
