using System;
using System.Collections.Generic;

namespace ApiForReact.Services
{
    public class TextGeneratorService : ITextGeneratorService
    {
        public string GenerateText(int length, int wordCount = 1)
        {
            List<string> words = new List<string>();

            for (int i = 0; i < wordCount; i++)
            {
                Random r = new Random();
                string[] consonants = { "b", "c", "d", "f", "g", "h", "j", "k", "l", "m", "l", "n", "p", "q", "r", "s", "sh", "zh", "t", "v", "w", "x" };
                string[] vowels = { "a", "e", "i", "o", "u", "ae", "y" };
                string Name = "";
                Name += consonants[r.Next(consonants.Length)].ToUpper();
                Name += vowels[r.Next(vowels.Length)];
                int b = 2; //b tells how many times a new letter has been added. It's 2 right now because the first two letters are already in the name.
                while (b < length)
                {
                    Name += consonants[r.Next(consonants.Length)];
                    b++;
                    Name += vowels[r.Next(vowels.Length)];
                    b++;
                }

                words.Add(Name);
            }
            return String.Join(" ", words);
        }
    }
}
