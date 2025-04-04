using System;
using System.Collections;
using System.Threading;

namespace st_10436196_partOne
{
    public class input_validation
    {
        private string user_name = string.Empty;
        private string user_asking = string.Empty;

        // Arrays to hold responses
        private string[] passwordSafetyResponses = new string[]
        {
            "It's important to use strong passwords. A good password includes a mix of uppercase, lowercase, numbers, and special characters.",
            "Always use different passwords for each account. This way, if one account is compromised, your others remain safe.",
            "Consider using a password manager to store and generate strong, unique passwords for all your accounts."
        };

        private string[] phishingResponses = new string[]
        {
            "Phishing attacks often look like legitimate emails or websites. Be cautious and always double-check the sender's email address.",
            "If you receive an email asking for personal information or login credentials, don't click on any links. Legitimate organizations won't ask for such details through email.",
            "Use two-factor authentication (2FA) wherever possible to add an extra layer of security to your accounts."
        };

        private string[] safeBrowsingResponses = new string[]
        {
            "Always check for 'https' in the URL before entering sensitive information on a website. The 's' indicates that the site uses encryption to protect your data.",
            "Avoid downloading files from untrusted websites. These could contain malware or viruses that harm your device.",
            "A VPN (Virtual Private Network) can help protect your privacy while browsing by masking your IP address and encrypting your internet traffic."
        };

        // Constructor
        public input_validation()
        {
            // Welcome the user and prompt for their name
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("------------------------------------------------------------");
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("   |   Welcome to Cyber Security Awareness Chatbot |");
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("------------------------------------------------------------");

            // Ask for the user's name
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("ChatBot: ");
            Console.ForegroundColor = ConsoleColor.Gray;
            TypeEffect("Please Enter your name.");

            // Get the user's name
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("You: ");
            Console.ForegroundColor = ConsoleColor.White;
            user_name = Console.ReadLine();

            // Re-create the interface
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("ChatBot: ");
            Console.ForegroundColor = ConsoleColor.Gray;
            TypeEffect("Hey " + user_name + ", How can i Assist you?");

            // Start the conversation loop with do-while
            do
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write(user_name + ": ");
                Console.ForegroundColor = ConsoleColor.White;
                user_asking = Console.ReadLine();

                // Exit condition
                if (user_asking.ToLower() == "exit")
                {
                    TypeEffect("Thank you for using Cyber Security Awareness Chatbot , bye!");
                    break; // Exit the loop
                }

                // Answer the question based on user input
                answer(user_asking);

            } while (true); // Continue until the user types "exit"
        }

        // Method for answering the user's question
        private void answer(string asked)
        {
            // Get the responses from the input_validation logic
            ArrayList responses = GetResponses(asked);

            // If responses are found, display all of them
            if (responses.Count > 0)
            {
                Console.ForegroundColor = ConsoleColor.White;
                TypeEffect("ChatBot: Here are some tips for you:");

                // Loop through all responses and display each one
                foreach (string response in responses)
                {
                    TypeEffect("- " + response);
                }

                // Add the "exit" message in red color after the responses
                Console.ForegroundColor = ConsoleColor.Blue;
                TypeEffect("ChatBot: If you don't have any questions, type exit.");
                Console.ForegroundColor = ConsoleColor.White;
            }
            else
            {
                // If no relevant responses were found, display a beep and message
                Console.Beep(1900, 1000); // Beep sound when no response is found
                Console.ForegroundColor = ConsoleColor.Red;
                TypeEffect("ChatBot: I'm sorry, I didn't quite understand that. Could you rephrase? and the question must be based on Cyber security");
                Console.ForegroundColor = ConsoleColor.White;
            }
        }

        // Method for getting responses based on the user query
        private ArrayList GetResponses(string asked)
        {
            asked = asked.ToLower(); // Convert to lowercase for case insensitivity

            // Create an ArrayList to store responses
            ArrayList responses = new ArrayList();

            // Add responses based on the query content
            if (asked.Contains("password"))
            {
                responses.AddRange(passwordSafetyResponses);
            }
            else if (asked.Contains("phishing"))
            {
                responses.AddRange(phishingResponses);
            }
            else if (asked.Contains("browsing"))
            {
                responses.AddRange(safeBrowsingResponses);
            }

            // Return the responses
            return responses;
        }

        // Method for type effect (printing text with a delay)
        private void TypeEffect(string response)
        {
            foreach (char c in response)
            {
                Console.Write(c);
                Thread.Sleep(30); // Adjust speed as needed
            }
            Console.WriteLine();
        }// end of constructor
    }//end of class
}//end of namespace
