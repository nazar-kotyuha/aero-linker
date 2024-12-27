using System;
using System.Net;

namespace AeroLinker.DroneHub.DroneEmulator
{
    class Program
    {
        static void Main(string[] args)
        {
            // WebSocket server port
            int port = 4040;
            if (args.Length > 0)
                port = int.Parse(args[0]);
            // WebSocket server content path
            string www = @"C:\Users\nazar\OneDrive\Documents\grad-program\aero-linker\backend\AeroLinker.DroneHub.BLL\www\";
            if (args.Length > 1)
                www = args[1];

            Console.WriteLine($"WebSocket server port: {port}");
            Console.WriteLine($"WebSocket server static content path: {www}");
            Console.WriteLine($"WebSocket server website: http://localhost:{port}/chat/index.html");

            Console.WriteLine();

            // Create a new WebSocket server
            var server = new EmulatedCommunicationServer(IPAddress.Any, port);
            server.AddStaticContent(www, "/chat");

            // Start the server
            Console.Write("Server starting...");
            server.Start();
            Console.WriteLine("Done!");

            Console.WriteLine("Press Enter to stop the server or '!' to restart the server...");

            // Perform text input
            for (; ; )
            {
                string line = Console.ReadLine();
                if (string.IsNullOrEmpty(line))
                    break;

                // Restart the server
                if (line == "!")
                {
                    Console.Write("Server restarting...");
                    server.Restart();
                    Console.WriteLine("Done!");
                }

                // Multicast admin message to all sessions
                line = "(admin) " + line;
                server.MulticastText(line);
            }

            // Stop the server
            Console.Write("Server stopping...");
            server.Stop();
            Console.WriteLine("Done!");
        }
    }
}
