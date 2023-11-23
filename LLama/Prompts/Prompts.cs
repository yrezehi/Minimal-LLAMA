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

        public Prompts 
    }
}
