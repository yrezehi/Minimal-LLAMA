namespace LLama.Prompts
{
    public class Prompt
    {
        public string Context;
        
        public int[] Tokens;
        public int NumberOfTokens;

        public Prompt(string prompt)
        {
            Context = prompt; 
            Tokens = new int[prompt.Length];
        }

        public static Prompt Create(string prompt) =>
            new Prompt(prompt);
    }
}
