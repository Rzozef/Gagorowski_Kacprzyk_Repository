using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace Dane
{
    internal class DataWriter
    {
        private string subdirectory { get; }
        private string fileName { get; }
        private IList<Stream> DataToWrite { get; set; }
        private ulong DataOffset { get; set; }

        private void WriteData()
        {
            lock (DataToWrite)
            {
                using Stream data = DataToWrite[0];
                DataToWrite.Remove(data);

                string fullPath = Directory.GetCurrentDirectory() + "\\" + subdirectory + "\\" + fileName + DataOffset + ".json";
                using FileStream stream = File.Create(fullPath);
                DataOffset++;
                data.CopyTo(stream);
                data.Close();
            }
        }

        public DataWriter(string directory, string fileName)
        {
            if (directory == null || fileName == null)
            {
                throw new ArgumentNullException();
            }
            this.subdirectory = directory;
            this.fileName = fileName;

            if (Directory.Exists(directory))
            {
                Directory.Delete(directory, true);
            }
            Directory.CreateDirectory(directory);

            DataToWrite = new List<Stream>();
            DataOffset = 0;
        }

        public async void WriteBallsPosition(Stream data)
        {
            DataToWrite.Add(data);
            Task.Factory.StartNew(
                WriteData
                );
        }
    }
}
