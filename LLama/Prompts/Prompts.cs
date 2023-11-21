using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LLama.Prompts
{
    public class Prompts
    {
        public string Prompt;

        public int[]? PromptTokens;

        public Prompts(string prompt) =>
            (Prompt, PromptTokens) = (prompt, new int[prompt.Length]);
       
        public static Prompts Create(string prompt) =>
            new Prompts(prompt);
    }
}
