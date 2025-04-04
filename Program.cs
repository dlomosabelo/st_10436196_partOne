using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace st_10436196_partOne
{
    public class Program
    {
        static void Main(string[] args)
        {
            // Create an instance of the voice_greeting class to play a welcome sound
            new voice_greeting() { };

            // Create an instance of the ascii_art class to display the image
            new ascii_art() { };

            // Create an instance of the user_interaction class to start a conversation with the user
            new input_validation() { };

        }
    }
}
