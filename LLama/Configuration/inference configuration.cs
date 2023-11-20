using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LLama.Configuration
{
    public class InferenceConfiguration
    {
        public float Temperature = 1.0f;
        public float Topp = 0.9f;
        int Steps = 256;
        
        public static InferenceConfiguration Create() =>
            new InferenceConfiguration();

    }
}
