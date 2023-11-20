using System;
using System.Collections.Generic;
using System.IO.MemoryMappedFiles;

namespace LLama.Memory
{
    public class MemoryFile
    {
        private readonly MemoryMappedFile MappedFile;

        private MemoryFile(string modelPath)
        {
            if (File.Exists(modelPath))
            {
                throw new FileNotFoundException("Couldn't find the file at: " + modelPath);
            }

            var fileStream = this.GetStream(modelPath);

            MappedFile = this.GetMappedFile(fileStream);
        }

        private FileStream GetStream(string modelPath) =>
            new FileStream(modelPath, FileMode.Open, FileAccess.Read);

        private MemoryMappedFile GetMappedFile(FileStream stream) =>
            MemoryMappedFile.CreateFromFile(stream, null, stream.Length, MemoryMappedFileAccess.Read, HandleInheritability.None, false);
    }
}
