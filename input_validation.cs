using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.IO;

namespace st_10436196_partTwo
{
    // Delegate for selecting responses based on sentiment
    public delegate string SentimentResponseSelector(string sentiment, List<string> responses);

    public class input_validation
    {
        // User information
        private string userName = string.Empty;
        private Dictionary<string, string> userMemory = new Dictionary<string, string>();

        // File path for memory storage
        private const string MEMORY_FILE = "memory.txt";

        // Dictionary to store cybersecurity topics and their responses
        private Dictionary<string, List<string>> topicResponses = new Dictionary<string, List<string>>();

        // Dictionary for sentiment detection and appropriate responses
        private Dictionary<string, string> sentimentResponses = new Dictionary<string, string>();

        // List to track conversation history
        private List<string> conversationHistory = new List<string>();

        // Random number generator for selecting random responses
        private Random random = new Random();

        // SentimentResponseSelector delegate instance
        private SentimentResponseSelector sentimentSelector;

        // Constructor
        public input_validation()
        {
            // Initialize the sentiment response selector delegate
            sentimentSelector = (sentiment, responses) =>
            {
                // Get a random response from the list
                int index = random.Next(responses.Count);

                // If sentiment is detected, prepend an empathetic response
                if (!string.IsNullOrEmpty(sentiment) && sentimentResponses.ContainsKey(sentiment))
                {
                    return sentimentResponses[sentiment] + " " + responses[index];
                }

                return responses[index];
            };

            // Initialize topic responses
            InitializeResponses();

            // Load user memory if it exists
            LoadMemory();

            // Welcome the user and start the conversation
            StartConversation();
        }

        // Initialize all responses
        private void InitializeResponses()
        {
            // Password safety responses
            topicResponses["password"] = new List<string>
            {
                "It's important to use strong passwords. A good password includes a mix of uppercase, lowercase, numbers, and special characters.",
                "Always use different passwords for each account. This way, if one account is compromised, your others remain safe.",
                "Consider using a password manager to store and generate strong, unique passwords for all your accounts.",
                "Avoid using personal information in your passwords, like birthdays or names, as these can be easy to guess.",
                "Aim for passwords that are at least 12 characters long for better security."
            };

            // Phishing responses
            topicResponses["phishing"] = new List<string>
            {
                "Phishing attacks often look like legitimate emails or websites. Be cautious and always double-check the sender's email address.",
                "If you receive an email asking for personal information or login credentials, don't click on any links. Legitimate organizations won't ask for such details through email.",
                "Use two-factor authentication (2FA) wherever possible to add an extra layer of security to your accounts.",
                "Look for spelling mistakes or poor grammar in emails, as these can be indicators of phishing attempts.",
                "Hover over links before clicking to see where they actually lead to."
            };

            // Safe browsing responses
            topicResponses["browsing"] = new List<string>
            {
                "Always check for 'https' in the URL before entering sensitive information on a website. The 's' indicates that the site uses encryption to protect your data.",
                "Avoid downloading files from untrusted websites. These could contain malware or viruses that harm your device.",
                "A VPN (Virtual Private Network) can help protect your privacy while browsing by masking your IP address and encrypting your internet traffic.",
                "Keep your browser and its plugins updated to protect against security vulnerabilities.",
                "Consider using browser extensions that block trackers and ads for safer browsing."
            };

            // Privacy responses
            topicResponses["privacy"] = new List<string>
            {
                "Regularly review privacy settings on your social media accounts to control who can see your information.",
                "Be mindful of what personal information you share online. Once it's out there, it can be difficult to remove.",
                "Use private browsing modes when using public computers or networks.",
                "Consider using privacy-focused search engines like DuckDuckGo that don't track your searches.",
                "Regularly clear cookies and browsing history to minimize tracking."
            };

            // Malware responses
            topicResponses["malware"] = new List<string>
            {
                "Install reputable antivirus software and keep it updated to protect against malware.",
                "Be cautious when downloading free software as it might bundle unwanted programs or malware.",
                "Regularly scan your computer for malware to catch any threats early.",
                "Avoid clicking on pop-up ads, which may contain malicious code.",
                "Keep your operating system and applications updated to patch security vulnerabilities."
            };

            // Scam responses
            topicResponses["scam"] = new List<string>
            {
                "If an offer seems too good to be true, it probably is. Be skeptical of unbelievable deals.",
                "Never send money or provide personal information to someone you've only met online.",
                "Research companies and products before making purchases, especially from unfamiliar websites.",
                "Be wary of urgent requests for money or information, even if they appear to come from friends or family.",
                "Check reviews and ratings before engaging with new online services or merchants."
            };

            // General cybersecurity responses
            topicResponses["security"] = new List<string>
            {
                "Regularly back up your important data to prevent loss in case of a security incident.",
                "Use secure Wi-Fi networks whenever possible, especially when handling sensitive information.",
                "Consider using a password manager to help maintain unique, strong passwords for all your accounts.",
                "Enable two-factor authentication on all accounts that offer it for an extra layer of security.",
                "Stay informed about current cybersecurity threats and best practices."
            };

            // Sentiment responses
            sentimentResponses["worried"] = "I understand your concern. It's normal to feel worried about online threats.";
            sentimentResponses["scared"] = "It's completely natural to feel scared about these issues. Let me help ease your concerns.";
            sentimentResponses["confused"] = "I can see you're a bit confused. Let me try to explain this more clearly.";
            sentimentResponses["frustrated"] = "I hear your frustration. Cybersecurity can sometimes be overwhelming.";
            sentimentResponses["curious"] = "Your curiosity is great! Learning about cybersecurity is the first step to staying safe online.";
            sentimentResponses["interested"] = "I'm glad you're interested in this topic! Knowledge is your best defense in the digital world.";
        }

        // Start the conversation
        private void StartConversation()
        {
            // Welcome message
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("------------------------------------------------------------");
            Console.WriteLine("   |   Welcome to Enhanced Cyber Security Awareness Chatbot |");
            Console.WriteLine("------------------------------------------------------------");

            // Ask for the user's name
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("ChatBot: ");
            Console.ForegroundColor = ConsoleColor.Gray;

            // Check if we already know the user
            if (userMemory.TryGetValue("name", out string storedName))
            {
                TypeEffect($"Welcome back, {storedName}! Is that still you? (yes/no)");

                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("You: ");
                Console.ForegroundColor = ConsoleColor.White;
                string response = Console.ReadLine().ToLower();

                if (response == "yes" || response == "y")
                {
                    userName = storedName;
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write("ChatBot: ");
                    Console.ForegroundColor = ConsoleColor.Gray;

                    // Recall their interests if any are stored
                    if (userMemory.TryGetValue("interest", out string interest))
                    {
                        TypeEffect($"Great to see you again! I remember you were interested in {interest}. Would you like to continue learning about that topic?");
                    }
                    else
                    {
                        TypeEffect($"Great to see you again! How can I help you today with cybersecurity?");
                    }
                }
                else
                {
                    // Ask for the new user's name
                    TypeEffect("Please enter your name.");
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("You: ");
                    Console.ForegroundColor = ConsoleColor.White;
                    userName = Console.ReadLine();
                    userMemory["name"] = userName;
                    SaveMemory(); // Save the new name

                    // Welcome the new user
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write("ChatBot: ");
                    Console.ForegroundColor = ConsoleColor.Gray;
                    TypeEffect($"Nice to meet you, {userName}! I'm your Cybersecurity Awareness Chatbot. You can ask me about topics like password safety, phishing, safe browsing, privacy, malware, or scams. How can I assist you today?");
                }
            }
            else
            {
                // First-time user
                TypeEffect("Please enter your name.");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("You: ");
                Console.ForegroundColor = ConsoleColor.White;
                userName = Console.ReadLine();
                userMemory["name"] = userName;
                SaveMemory(); // Save the name

                // Welcome the user
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("ChatBot: ");
                Console.ForegroundColor = ConsoleColor.Gray;
                TypeEffect($"Nice to meet you, {userName}! I'm your Cybersecurity Awareness Chatbot. You can ask me about topics like password safety, phishing, safe browsing, privacy, malware, or scams. How can I assist you today?");
            }

            // Main conversation loop
            string userInput;
            do
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write($"{userName}: ");
                Console.ForegroundColor = ConsoleColor.White;
                userInput = Console.ReadLine();

                // Add to conversation history
                conversationHistory.Add(userInput);

                // Check if user wants to exit
                if (userInput.ToLower() == "exit")
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write("ChatBot: ");
                    Console.ForegroundColor = ConsoleColor.Gray;
                    TypeEffect($"Thank you for chatting with me, {userName}! Stay safe online!");
                    SaveMemory(); // Save memory before exiting
                    break;
                }

                // Process the user input and provide a response
                ProcessUserInput(userInput);

            } while (true);
        }

        // Process user input and provide appropriate responses
        private void ProcessUserInput(string userInput)
        {
            string lowerInput = userInput.ToLower();

            // Check for sentiment first
            string detectedSentiment = DetectSentiment(lowerInput);

            // Check for topics the user is interested in and store in memory
            CheckForInterests(lowerInput);

            // Check if the input contains any known cybersecurity keywords
            List<string> relevantResponses = new List<string>();
            string matchedTopic = null;

            foreach (var topic in topicResponses.Keys)
            {
                if (lowerInput.Contains(topic))
                {
                    relevantResponses.AddRange(topicResponses[topic]);
                    matchedTopic = topic;

                    // Store the topic in user memory if they're asking about it
                    if (lowerInput.Contains("interested in") || lowerInput.Contains("want to learn about"))
                    {
                        userMemory["interest"] = topic;
                        SaveMemory(); // Save the memory as soon as an interest is detected
                    }
                }
            }

            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("ChatBot: ");
            Console.ForegroundColor = ConsoleColor.Gray;

            // Handle cases where we found relevant responses
            if (relevantResponses.Count > 0)
            {
                // Select a response using the delegate
                string response = sentimentSelector(detectedSentiment, relevantResponses);
                TypeEffect(response);

                // If this is a topic they've previously shown interest in, mention it
                if (userMemory.TryGetValue("interest", out string interest) && matchedTopic == interest)
                {
                    TypeEffect($"Since you previously expressed interest in {interest}, here's an additional tip: " +
                              $"Staying informed about {interest} is one of the best ways to protect yourself online.");
                }
            }
            // Check for greeting
            else if (lowerInput.Contains("hello") || lowerInput.Contains("hi") || lowerInput.Contains("hey"))
            {
                TypeEffect($"Hello {userName}! How can I help you with cybersecurity today?");
            }
            // Check for thank you
            else if (lowerInput.Contains("thank") || lowerInput.Contains("thanks"))
            {
                TypeEffect($"You're welcome, {userName}! I'm here to help with any cybersecurity questions you have.");
            }
            // Check for help request
            else if (lowerInput.Contains("help") || lowerInput.Contains("what can you do"))
            {
                TypeEffect($"I can provide information about various cybersecurity topics like passwords, phishing, safe browsing, privacy, malware, and scams. What would you like to learn about?");
            }
            // User wants to see their stored information
            else if (lowerInput.Contains("what do you know about me") || lowerInput.Contains("what do you remember") || lowerInput.Contains("my information"))
            {
                if (userMemory.Count > 0)
                {
                    TypeEffect($"Here's what I remember about you, {userName}:");
                    foreach (var item in userMemory)
                    {
                        if (item.Key != "name") // Skip name since we already addressed them by name
                        {
                            TypeEffect($"- Your {item.Key}: {item.Value}");
                        }
                    }
                }
                else
                {
                    TypeEffect($"I only know your name is {userName}. You can tell me about your interests in cybersecurity topics, and I'll remember them for next time!");
                }
            }
            // User is asking about a previous topic
            else if (lowerInput.Contains("tell me more") || lowerInput.Contains("more information") || lowerInput.Contains("more about"))
            {
                if (userMemory.TryGetValue("interest", out string interest))
                {
                    // Provide more information about their interest
                    if (topicResponses.ContainsKey(interest))
                    {
                        // Get a different response than they might have seen before
                        string additionalResponse = topicResponses[interest][random.Next(topicResponses[interest].Count)];
                        TypeEffect($"Here's more about {interest}: {additionalResponse}");
                    }
                }
                else
                {
                    TypeEffect("What specific cybersecurity topic would you like more information about?");
                }
            }
            // Default response for unknown inputs
            else
            {
                // Beep for unknown input
                Console.Beep(1900, 1000);
                Console.ForegroundColor = ConsoleColor.Red;
                TypeEffect("I'm sorry, I didn't quite understand that. Could you rephrase your question? You can ask me about cybersecurity topics like password safety, phishing, safe browsing, privacy, malware, or scams.");
                Console.ForegroundColor = ConsoleColor.Gray;

                // If they've shown interest in a topic before, suggest it
                if (userMemory.TryGetValue("interest", out string interest))
                {
                    TypeEffect($"Since you were interested in {interest} before, would you like to learn more about that?");
                }
            }

            // Add a reminder about exiting
            if (conversationHistory.Count % 3 == 0) // Every 3rd message
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                TypeEffect("ChatBot: If you don't have any more questions, type 'exit'.");
                Console.ForegroundColor = ConsoleColor.Gray;
            }
        }

        // Methods for saving and loading memory
        private void SaveMemory()
        {
            try
            {
                // Convert memory dictionary to a list of lines
                List<string> lines = new List<string>();
                foreach (var item in userMemory)
                {
                    lines.Add($"{item.Key}={item.Value}");
                }

                // Write all lines to the memory file
                File.WriteAllLines(MEMORY_FILE, lines);

                // Provide feedback (optional, can be removed if not needed)
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine("[Memory saved]");
                Console.ForegroundColor = ConsoleColor.Gray;
            }
            catch (Exception ex)
            {
                // Handle any errors (file permissions, etc.)
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine($"[Error saving memory: {ex.Message}]");
                Console.ForegroundColor = ConsoleColor.Gray;
            }
        }

        private void LoadMemory()
        {
            try
            {
                // Check if memory file exists
                if (File.Exists(MEMORY_FILE))
                {
                    // Read all lines from the memory file
                    string[] lines = File.ReadAllLines(MEMORY_FILE);

                    // Clear existing memory
                    userMemory.Clear();

                    // Process each line
                    foreach (string line in lines)
                    {
                        // Split the line at the first equals sign
                        int separatorIndex = line.IndexOf('=');
                        if (separatorIndex > 0)
                        {
                            string key = line.Substring(0, separatorIndex);
                            string value = line.Substring(separatorIndex + 1);

                            // Add to memory dictionary
                            userMemory[key] = value;
                        }
                    }

                    // Provide feedback (optional, can be removed if not needed)
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine("[Memory loaded]");
                    Console.ForegroundColor = ConsoleColor.Gray;
                }
            }
            catch (Exception ex)
            {
                // Handle any errors (file corruption, etc.)
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine($"[Error loading memory: {ex.Message}]");
                Console.ForegroundColor = ConsoleColor.Gray;

                // Initialize a fresh memory
                userMemory.Clear();
            }
        }

        // Detect user sentiment from input
        private string DetectSentiment(string input)
        {
            foreach (var sentiment in sentimentResponses.Keys)
            {
                if (input.Contains(sentiment))
                {
                    return sentiment;
                }
            }
            return string.Empty;
        }

        // Check for user interests and store them in memory
        private void CheckForInterests(string input)
        {
            // Check if user is expressing interest in a topic
            if (input.Contains("interested in") || input.Contains("want to learn about"))
            {
                foreach (var topic in topicResponses.Keys)
                {
                    if (input.Contains(topic))
                    {
                        userMemory["interest"] = topic;
                        break;
                    }
                }
            }
        }

        // Method for typing effect
        private void TypeEffect(string message)
        {
            foreach (char c in message)
            {
                Console.Write(c);
                Thread.Sleep(20); // Slightly faster typing for better UX
            }
            Console.WriteLine();
        }
    }

    
        
    
}