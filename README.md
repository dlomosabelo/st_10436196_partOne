st_10436196_partOne
README FILE

  Project details
. Project name: St10436196_partOne
. Netframe: 4.0.8
. Templates: Console App C# Netframework

Features and Functionalities
1. Voice Greeting (voice_greeting Class)
The voice_greeting class is implemented to play a welcome greeting sound when the user launches the application. This feature enhances user interaction by providing an auditory introduction. The class plays an audio file named Welcome-to-the-Cyber.wav, which must be located in the root directory of the project.
How it works:
The program first retrieves the full path of the current project directory.
It then removes the bin\Debug\ part of the path to locate the root directory where the audio file is stored.
Using the SoundPlayer class, the program loads the audio file and plays it .
This feature serves as an engaging introduction to the program, giving users a friendly and welcoming first impression.

2. ASCII Art Display (ascii_art Class)
The ascii_art class is responsible for transforming an image (in this case, logo.jpg) into ASCII art, which is then displayed in the console. The image is first resized to fit the console dimensions before converting each pixel into an ASCII character.
How it works:
The program loads the image from the root directory of the project.
It resizes the image to a predefined size (150x120 pixels) to ensure it fits within the console window.
Each pixel's color is analyzed by calculating its grayscale value. This grayscale value is used to determine which ASCII character will represent the pixel in the output.
The pixel's grayscale value is mapped to a specific set of characters based on brightness:


3. Interactive Chatbot (input_validation Class)
The main functionality of the program resides in the input_validation class. This class handles the user input and provides appropriate responses based on the user's questions related to cybersecurity.
How it works:
The chatbot begins by greeting the user and asking for their name. It displays the greeting in a friendly manner and uses a typewriter effect to gradually reveal the text.
After the user inputs their name, the chatbot enters an interactive loop where the user can ask questions about cybersecurity. The questions should relate to topics such as password safety, phishing attacks, and safe browsing.
The chatbot listens to the user input, analyzes the text, and provides responses based on keywords contained within the question.
For example:
If the question contains the word "password," the chatbot will respond with tips related to password safety.
If the question contains the word "phishing," the chatbot will provide advice on how to identify and avoid phishing scams.
If the question contains the word "browsing," the chatbot will provide recommendations for secure browsing practices.
If the user's question does not contain any of these keywords, the chatbot will prompt them to rephrase the question or ensure it is related to cybersecurity.
If at any time the user types "exit," the chatbot will thank them for using the service and exit the program.
The chatbot uses predefined arrays to store responses for each of the topics mentioned above. It ensures that users receive valuable and relevant information while interacting with the chatbot.

4. Typewriter Effect
To make the user interaction more engaging and realistic, the chatbot displays its responses using a typewriter effect. This effect gradually prints each character of the chatbot’s response with a small delay between each character. This gives the user the sensation of the chatbot typing in real-time, which makes the interaction feel more personal and natural.
How it works:
The TypeEffect method iterates through each character in a string and uses Thread.Sleep to introduce a small delay between each character's display.
The speed of the effect can be adjusted by changing the delay time in the Thread.Sleep method (currently set to 30 milliseconds).

5. Exit Command
The user can exit the chatbot at any time by typing the command "exit". When this command is entered, the chatbot will print a thank-you message and exit the conversation. This feature allows the user to exit the program gracefully when they are finished interacting with the chatbot.
How it works:
The chatbot listens for the "exit" command within the user’s input. If it detects this command, the program breaks out of the conversation loop and ends the session.
A farewell message is displayed before the application exits, providing a polite and friendly conclusion to the user’s session.

6. Responses Based on User Input
The input_validation class is designed to analyze user input and provide relevant responses based on keywords. The class maintains arrays of responses for specific topics like password safety, phishing, and browsing.
How it works:
The chatbot first converts the user’s input to lowercase, ensuring that the analysis is case-insensitive.
It then checks for specific keywords (e.g., "password," "phishing," or "browsing") in the user’s query.
If a match is found, the chatbot selects and displays the relevant responses from the predefined arrays.
If no match is found, the chatbot asks the user to rephrase their question or specify that it should be related to cybersecurity.
This feature ensures that the chatbot remains focused on cybersecurity topics while still being flexible enough to handle a variety of questions.

7. Exception Handling
The code uses try-catch blocks in the voice_greeting and ascii_art classes to handle potential errors, such as missing files or invalid paths.
How it works:
When attempting to load the audio file or image, the program wraps the file-loading process in a try-catch block.
If any error occurs (e.g., the file is missing or the path is incorrect), an error message is displayed to the user, ensuring that the program does not crash unexpectedly.
This exception handling makes the program more robust and user-friendly by ensuring that it can gracefully handle errors.
Flow of Execution
Here’s a step-by-step breakdown of how the program runs:
Program Start:
The program first plays a voice greeting to welcome the user.
Then, it displays an ASCII art logo.
User Interaction:
The chatbot greets the user and asks for their name.
After receiving the user’s name, the chatbot invites the user to ask questions about cybersecurity.
Handling User Questions:
The chatbot analyzes the user’s question and matches it with predefined topics (passwords, phishing, and browsing).
The chatbot provides relevant cybersecurity advice based on the question.
If the question does not match any of the predefined topics, the chatbot asks the user to rephrase the question.


Exit:
The user can exit the conversation by typing "exit," and the chatbot will thank them before ending the program.
Conclusion
This Cyber Security Awareness Chatbot is an educational tool that helps users learn about crucial cybersecurity topics such as password security, phishing, and safe browsing. By combining interactive conversation, voice greetings, ASCII art, and engaging text effects, this chatbot creates an enjoyable and informative user experience. 

