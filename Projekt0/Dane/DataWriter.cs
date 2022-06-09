using System;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Collections.Concurrent;

namespace Dane
{
    internal class DataWriter
    {
        internal static int MAX_ITEMS_NUM = 10000;
        DataWriter(string subdir, string fileName)
        {
            if (subdir == null || fileName == null)
            {
                throw new ArgumentNullException();
            }
            string _directory = Directory.GetCurrentDirectory() + "\\" + subdir;

            if (Directory.Exists(_directory))
            {
                Directory.Delete(_directory, true);
            }
            Directory.CreateDirectory(_directory);

            file = _directory + "\\" + fileName + ".json";
            File.WriteAllText(file, "[]");

            DataToWrite = new BlockingCollection<BallRecord>(new ConcurrentQueue<BallRecord>(), MAX_ITEMS_NUM);
            _first = true;
            Task.Factory.StartNew(Writer);
        }
        private static readonly object _lock = new object();
        private static DataWriter? _instance = null;
        public static DataWriter Instance
        {
            get
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new DataWriter("logs", "ball");
                    }
                    return _instance;
                }
            }
        }

        private string file { get; set; }
        private bool _first { get; set; }
        private BlockingCollection<BallRecord> DataToWrite { get; set; }


        private void WriteData()
        {
            BallRecord record;
            while (!DataToWrite.TryTake(out record)) { }

            FileStream fs = new FileStream(file, FileMode.Open, FileAccess.ReadWrite);
            fs.SetLength(fs.Length - 1);
            fs.Close();
            if (!_first)
            {
                File.AppendAllText(file, "," + record.SerializedData + "]");
            }
            else
            {
                File.AppendAllText(file, record.SerializedData + "]");
                _first = false;
            }
        }

        private async void Writer()
        {
            while (true)
            {
                while (DataToWrite.Count != 0)
                {
                    WriteData();
                }

                await Task.Delay(1000);
            }
        }

        public async void WriteBallPosition(BallAbstract ball, DateTime time)
        {
            DataToWrite.TryAdd(new BallRecord(ball, time), 10);
        }
    }
}
