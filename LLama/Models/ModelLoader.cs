using LLama.Configuration;
using LLama.Entities;
using LLama.Extensions;
using LLama.Layers;
using LLama.Memory;
using LLama.Prompts;
using LLama.Tokenizers;
using LLama.Transformers;
using System.IO;

namespace LLama.Models
{
    public class ModelLoader
    {

        private BinConfiguration Configuration;
        private MemoryFile Checkpoint;

        private TransformerWeights TransformerWeights;

        private Tokenizer Tokenizer;
        private State State;

        private ModelLoader(string path)
        {
            Configuration = ConfigurationLoader.Load(path);
            Checkpoint = MemoryFile.Load(path, Configuration.GetBytesSize());

            TransformerWeights = new TransformerWeights();

            Checkpoint.Initialize(ref Configuration, ref TransformerWeights, Configuration.vocab_size > 0);

            Tokenizer = Tokenizer.Create(Configuration.vocab_size).Load();
            State = new State().Populate(Configuration);
        }

        public static ModelLoader Load(string path) =>
            new ModelLoader(path);

        public void Inference(string prompt)
        {
            Prompt promptInstance = new Prompt(prompt);
            
            int sequencePosition = 0;
            int token = 0;

            while(sequencePosition < InferenceConfiguration.Steps)
            {
                Transformer.Transform(Configuration, TransformerWeights, State, token, sequencePosition);

                int nextState;

                if(sequencePosition < promptInstance.PromptTokensNumber)
                {
                    nextState = promptInstance.PromptTokens[sequencePosition];
                } else
                {
                    if(InferenceConfiguration.Temperature == 0.0f)
                    {
                    } else
                    {
                        for (int index = 0; index < Configuration.vocab_size; index++)
                        {
                            State.logits[index] /= InferenceConfiguration.Temperature;
                        }

                        Softmax.Normailize(State.logits, 0, Configuration.vocab_size);

                        if(InferenceConfiguration.Topp <= 0)
                        {
                            nextState = NucleusSampling.SampleTopp(State.logits, Configuration.vocab_size);
                        } else
                        {
                            nextState = NucleusSampling.SampleTopp(State.logits, Configuration.vocab_size, InferenceConfiguration.Topp, State.probindex);
                        }
                    }
                }

                sequencePosition++;

                if (nextState == 1)
                    break;

                string decodedToken = ( token == 1 && Tokenizer.Vocab[nextState][0] == ' ' ? Tokenizer.Vocab[nextState].TrimStart() : Tokenizer.Vocab[nextState]);

                token = nextState;

                return decodedToken;
            }
        }
    }
}
