using System;
using System.Collections.Generic;
using System.IO;
using System.IO.MemoryMappedFiles;

namespace LLama.Memory
{
    public class MemoryFile
    {
        private readonly MemoryMappedViewAccessor ViewAccessor;

        private MemoryFile(string modelPath)
        {
            if (File.Exists(modelPath))
            {
                throw new FileNotFoundException("Couldn't find the file at: " + modelPath);
            }

            var fileStream = new FileStream(modelPath, FileMode.Open, FileAccess.Read);

            ViewAccessor = MemoryMappedFile.CreateFromFile(fileStream, null, fileStream.Length, MemoryMappedFileAccess.Read, HandleInheritability.None, false).
                CreateViewAccessor(null, null, MemoryMappedFileAccess.Read);
        }

        public static MemoryFile Create(string pathFile) =>
            new MemoryFile(pathFile);

        public float[] Read(ref long offset, int size)
        {
            float[] buffer = new float[size];
            ViewAccessor.ReadArray(offset, buffer, 0, size);
            offset += sizeof(float) * (long)size;
            return buffer;
        }
    }
}
