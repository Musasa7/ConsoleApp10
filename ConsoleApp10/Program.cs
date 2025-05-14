using System; // Basic functionalities for console input/output
using System.Collections.Generic; // Allows collections like lists and dictionaries
using System.Text.RegularExpressions; // Regular expression functionality for input validation
using NAudio.Wave; // Audio playback functionalities for the chatbot's greeting
using System.Linq; // LINQ for accessing elements by index

class CybersecurityChatbot
{
    // User information variables
    static string firstName = "";
    static string lastName = "";
    static string favoriteTopic = "";

    // Tips for specific keywords
    static Dictionary<string, string> keywordResponses = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
    {
       {"password", "A strong password is like a sturdy lock for your online accounts. Avoid using easily guessed passwords, and aim for a mix of letters, numbers, and symbols."},
{"scam", "Watch out for scams! Always double-check links and the identities of senders before clicking on anything."},
{"privacy", "To keep your personal information safe, think about using VPNs and take a moment to adjust your social media privacy settings."}
    };

    // Responses based on user feelings
    static Dictionary<string, string> sentimentResponses = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
    {
        {"Troubled", "It's okay to feel this way, {firstName}. I'm here to help you."},
        {"Curious", "I love your curiosity! Feel free to ask anything, {firstName}."},
        {"Frustrated", "It's okay to feel frustrated. Let's tackle this together!"}
    };

    static void Main(string[] args)
    {
        // Start the chatbot experience
        PlayGreeting();//first
        DisplayAsciiArt();//second method
        GetUserName();//third
        HandleUserInput();//last
    }

    static void PlayGreeting()
    {
        // Path to the greeting audio file
        string filePath = @"C:\\Users\\hustl\\source\\repos\\ConsoleApp8\\ConsoleApp8\\audio\\synthesize.wav";
        try
        {
            // Play the greeting audio
            using (var audioFile = new AudioFileReader(filePath))
            using (var outputDevice = new WaveOutEvent())
            {
                outputDevice.Init(audioFile);
                outputDevice.Play();
                Console.ForegroundColor = ConsoleColor.Green;//color
                Console.WriteLine("Playing audio... Press any key to continue.");
                Console.ReadKey();
            }
        }
        catch (Exception ex)
        {
            // Handle any errors during audio playback
            Console.WriteLine($"Oops! There was an issue playing the audio: {ex.Message}");
        }
    }

    static void DisplayAsciiArt()
    {
        // Display ASCII art for visual appeal
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(@" 
 ▄▄▄▄    ██▀███  ▓█████ ▄▄▄       ██ ▄█▀  
▓█████▄ ▓██ ▒ ██▒▓█   ▀▒████▄     ██▄█▒   
▒██▒ ▄██▓██ ░▄█ ▒▒███  ▒██  ▀█▄  ▓███▄░   
▒██░█▀  ▒██▀▀█▄  ▒▓█  ▄░██▄▄▄▄██ ▓██ █▄   
░▓█  ▀█▓░██▓ ▒██▒░▒████▒▓█   ▓██▒▒██▒ █▄  
░▒▓███▀▒░ ▒▓ ░▒▓░░░ ▒░ ░▒▒   ▓▒█░▒ ▒▒ ▓▒  
▒░▒   ░   ░▒ ░ ▒░ ░ ░  ░ ▒   ▒▒ ░░ ░▒ ▒░  
 ░    ░   ░░   ░    ░    ░   ▒   ░ ░░ ░   
 ░         ░        ░  ░     ░  ░░  ░     
      ░                                   
 ██▓███   ▒█████   ██▓ ███▄    █ ▄▄▄█████▓
▓██░  ██▒▒██▒  ██▒▓██▒ ██ ▀█   █ ▓  ██▒ ▓▒
▓██░ ██▓▒▒██░  ██▒▒██▒▓██  ▀█ ██▒▒ ▓██░ ▒░
▒██▄█▓▒ ▒▒██   ██░░██░▓██▒  ▐▌██▒░ ▓██▓ ░ 
▒██▒ ░  ░░ ████▓▒░░██░▒██░   ▓██░  ▒██▒ ░ 
▒▓▒░ ░  ░░ ▒░▒░▒░ ░▓  ░ ▒░   ▒ ▒   ▒ ░░   
░▒ ░       ░ ▒ ▒░  ▒ ░░ ░░   ░ ▒░    ░    
░░       ░ ░ ░ ▒   ▒ ░   ░   ░ ░   ░      
             ░ ░   ░           ░          
");
    }

    static void GetUserName()
    {
        // Loop to get valid first name
        while (true)
        {
            Console.ForegroundColor = ConsoleColor.Blue; // Change text color
            Console.WriteLine("What’s your first name?"); // Prompt for first name
            firstName = Console.ReadLine(); // Read user input

            if (!IsValidName(firstName)) // Validate first name
            {
                Console.ForegroundColor = ConsoleColor.Red; // Change text color for error
                Console.WriteLine("Uh-oh! First name cannot contain special characters. Let's try again."); // Error message
                continue; // Restart loop
            }

            // Loop to get valid last name
            Console.ForegroundColor = ConsoleColor.Blue; // Change text color
            Console.WriteLine("Now, what’s your last name?"); // Prompt for last name
            lastName = Console.ReadLine(); // Read user input

            if (!IsValidName(lastName)) // Validate last name
            {
                Console.ForegroundColor = ConsoleColor.Red; // Change text color for error
                Console.WriteLine("Oops! Last name must not have special characters. Please enter it again."); // Error message
                continue; // Restart loop
            }

            // Welcome message
            Console.WriteLine($"Hey, {firstName} {lastName}! It’s great to meet you!");
            break; // Exit loop if both names are valid
        }

        // Ask for favorite topic
        Console.ForegroundColor = ConsoleColor.Blue; // Change text color
        Console.WriteLine("What’s your favorite topic in cybersecurity?"); // Prompt for favorite topic
        favoriteTopic = Console.ReadLine(); // Read user input
        Console.ForegroundColor = ConsoleColor.Green; // Change text color
        Console.WriteLine($"Got it! I’ll remember that you like {favoriteTopic}, {firstName}."); // Confirmation message

        AskAboutFeelings(); // Ask how the user is feeling
    }

    static bool IsValidName(string name)
    {
        // Regular expression for valid names (only letters and spaces)
        string pattern = @"^[a-zA-Z0-9\s]*$";
        return Regex.IsMatch(name, pattern); // Check if name matches the pattern
    }

    static void HandleUserInput()
    {
        // Main loop to handle user input
        while (true)
        {
            Console.ForegroundColor = ConsoleColor.Green; // Change text color
            TypeWithDelay("Here are some questions you can ask me (or type 'exit' to leave):"); // Prompt for questions
            // List of topics user can inquire about
            Console.WriteLine("\n- Cybersecurity\n- Importance\n- Threats\n- Safety\n- Malware\n- Firewalls\n- Encryption\n- Updates\n- VPN\n- Phishing");

            string question = Console.ReadLine().ToLower(); // Read user input

            if (question == "exit") // Check if user wants to exit
            {
                Console.ForegroundColor = ConsoleColor.Red; // Change text color for exit message
                Console.WriteLine($"Have a Good day, {firstName}! Goodbye!"); // Exit message
                break; // Exit loop
            }

            // Process user input for sentiments and keywords
            if (DetectSentiment(question)) continue; // Checkng  sentiments
            if (RecognizeKeyword(question)) continue; // keyword checking

            RespondToQuestion(question); // Responding 

            // Ask if the user wants tips
            Console.WriteLine("Do you want tips? (yes/no)"); // choose
            string wantTips = Console.ReadLine().ToLower(); // Reading users
            if (wantTips == "yes") // If user wants tips
            {
                DisplayTips(); // Displaying tips
            }
            else if (wantTips == "no") // If user does not want tips
            {
                TypeWithDelay("Okay! If you change your mind, just let me know."); // Acknowledge response
            }
            else // If input is not recognized
            {
                TypeWithDelay("I am not following you, Please Try. Let's continue!"); // Handle unrecognized input
            }
        }
    }

    static void DisplayTips()
    {
        // Display tips to the user
        Console.ForegroundColor = ConsoleColor.Green; // Change text color
        TypeWithDelay("Here are some tips you might find useful:"); // Introduction to tips

        foreach (var tip in keywordResponses)
        {
            TypeWithDelay($"-{tip.Key}: {tip.Value}"); // Displaying tips
        }
    }

    static void AskAboutFeelings()
    {
        Console.ForegroundColor = ConsoleColor.Blue; // Change text color
        Console.WriteLine("How are you feeling today? (\n-Troubled,\n- Curious, \n-Frustrated,\n- Sad, \n-Good)"); // Prompt for feelings

        string userFeeling = Console.ReadLine(); // Read user input

        // Respond based on the user's feeling
        switch (userFeeling.ToLower())
        {
            case "troubled":
                Console.ForegroundColor = ConsoleColor.Red; // Change text color
                TypeWithDelay("I am  sorry to hear that you’re feeling troubled. Remember, it's okay to seek help and talk about it."); // Response for troubled
                break;
            case "curious":
                Console.ForegroundColor = ConsoleColor.White; // Change text color
                TypeWithDelay(" curiosity is good! It’s a great way to learn and grow."); // Responding
                break;
            case "frustrated":
                Console.ForegroundColor = ConsoleColor.Red; // Change text color
                TypeWithDelay("It's completely okay to feel frustrated. Let’s work through it together!"); // Responding
                break;
            case "sad":
                Console.ForegroundColor = ConsoleColor.White; // Change text color
                TypeWithDelay("I am really sorry to hear that you're feeling sad. It's okay to feel this way, and I'm here for you."); // Response for sad
                break;
            case "good":
                Console.ForegroundColor = ConsoleColor.Green; //text color
                TypeWithDelay("I'm glad to hear you're feeling good!"); // Responding
                break;
            default:
                Console.ForegroundColor = ConsoleColor.Red; // Change text color for error
                TypeWithDelay("Sorry, but I'm here to help!"); // Response for invalid feelings
                break;
        }
    }

    static bool RecognizeKeyword(string input)
    {
        // Check if the input have any keywords from the tips
        foreach (var keyword in keywordResponses.Keys)
        {
            if (input.Contains(keyword)) // If input contains the keyword
            {
                Console.ForegroundColor = ConsoleColor.Blue; // Change text color
                TypeWithDelay(keywordResponses[keyword]);
                return true; //if a keyword is recognized Return true
            }
        }
        return false;
    }

    static bool DetectSentiment(string input)
    {
        // Checking if the input have any user feelings
        foreach (var mood in sentimentResponses.Keys)
        {
            if (input.Contains(mood)) // feelings
            {
                Console.ForegroundColor = ConsoleColor.White; // 
                TypeWithDelay(sentimentResponses[mood].Replace("{firstName}", firstName)); // Display the corresponding response
                return true; // Return true if a sentiment is recognized
            }
        }
        return false; // if no sentiment is found Return false if no 
    }

    static void RespondToQuestion(string question)
    {
        // Responding to various questions based on the input
        if (question.Contains("how are you"))
        {
            TypeWithDelay("I'm doing well, thanks for asking! I'm here to help you with all your cybersecurity questions.");
        }
        else if (question.Contains("what's your purpose"))
        {
            TypeWithDelay("My purpose is to help you stay safe online by sharing advice on passwords, phishing, and safe browsing.");
        }
        else if (question.Contains("cybersecurity"))
        {
            TypeWithDelay("Cybersecurity involves protecting systems and networks from digital attacks and threats.");
        }
        else if (question.Contains("importance"))
        {
            TypeWithDelay("It's crucial to safeguard your data and ensure system integrity to prevent breaches.");
        }
        else if (question.Contains("threats"))
        {
            TypeWithDelay("Common threats include viruses, phishing scams, and ransomware.");
        }
        else if (question.Contains("safety"))
        {
            TypeWithDelay("To stay safe online, use strong passwords and enable two-factor authentication.");
        }
        else if (question.Contains("malware"))
        {
            TypeWithDelay("Malware is harmful software like viruses and spyware that can damage your system.");
        }
        else if (question.Contains("firewalls"))
        {
            TypeWithDelay("Firewalls protect your network by filtering incoming and outgoing traffic.");
        }
        else if (question.Contains("encryption"))
        {
            TypeWithDelay("Encryption secures your data by converting it into a code that only authorized users can read.");
        }
        else if (question.Contains("updates"))
        {
            TypeWithDelay("Always keep your software updated to fix vulnerabilities and improve security.");
        }
        else if (question.Contains("vpn"))
        {
            TypeWithDelay("A VPN creates a secure connection over the internet, protecting your privacy.");
        }
        else if (question.Contains("phishing")) // If user asks 
        {
            TypeWithDelay("Phishing involves tricking you into revealing personal information by pretending to be trustworthy.");
        }
        else if (question.Contains("favorite"))
        {
            TypeWithDelay($"You said your favorite topic is {favoriteTopic}. interesting!");
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red; // Color
            TypeWithDelay("This question is not found. Try again?");
        }
    }

    static void TypeWithDelay(string message)// this Method is displaying text by typing effect
    {

        Console.ForegroundColor = ConsoleColor.White; // Changed  colour
        foreach (char c in message) // Loop to every character in message
        {
            Console.Write(c); // Displaying character
            System.Threading.Thread.Sleep(30); // Delaying 
        }
        Console.ForegroundColor = ConsoleColor.White; // Change text color
        Console.WriteLine(); // moving to next line
    }
}