namespace LLama.Prompts
{
    public class Prompt
    {
        public string Context;
        public int[]? PromptTokens;

        public Prompt(string prompt) =>
            (Context, PromptTokens) = (prompt, new int[prompt.Length]);
       
        public static Prompt Create(string prompt) =>
            new Prompt(prompt);
    }
}
