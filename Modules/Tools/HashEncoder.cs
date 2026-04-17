using hub.demon.UI;
using System;
using System.Collections.Generic;
using System.Text;

namespace hub.demon.Modules.Tools
{
    internal class HashEncoder
    {
        public static void HashEncoderMain()
        {
            int? choice = ConsoleNavigator.Navigation("HASH TOOL", new string[] { "Encode (MD5)", "Decode / Compare", "Back" });
            
            if (choice == null || choice == 2)
                return;

            if (choice == 0)
                RunEncoderFlow();
        }

        public static void RunEncoderFlow()
        {
            Console.Clear();
            Console.WriteLine("Enter the text to hash (MD5):");
            string input = Console.ReadLine() ?? "";
            using (var md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] inputBytes = Encoding.UTF8.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                    sb.Append(hashBytes[i].ToString("x2"));
                string hashResult = sb.ToString();
                Console.Clear();
                Console.WriteLine("MD5 Hash result:\n");
                Console.WriteLine(hashResult);
                Console.WriteLine("\nPress any key to continue...");
                Console.ReadKey();
            }
        }
    }
}