using System;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Collections.Concurrent;

namespace Dane
{
    internal class DataWriter
    {
        DataWriter(string subdir, string fileName)
        {
            if (subdir == null || fileName == null)
            {
                throw new ArgumentNullException();
            }
            _fileName = fileName;
            _directory = Directory.GetCurrentDirectory() + "\\" + subdir;

            if (Directory.Exists(_directory))
            {
                Directory.Delete(_directory, true);
            }
            Directory.CreateDirectory(_directory);

            string fullPath = _directory + "\\" + _fileName + ".json";
            lock (_lock)
            {
                File.WriteAllText(fullPath, "[]");
            }

            DataToWrite = new ConcurrentQueue<BallRecord>();
            _first = true;
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

        private string _directory { get; }
        private string _fileName { get; }
        private bool _first { get; set; }
        private ConcurrentQueue<BallRecord> DataToWrite { get; set; }


        private void WriteData()
        {
            BallRecord record;
            DataToWrite.TryDequeue(out record);

            //int ballId;
            //BallsIdDict.TryGetValue(record.Ball, out ballId);

            //ulong offset;
            //IdOffsetDict.TryGetValue(ballId, out offset);

            string fullPath = _directory + "\\" + _fileName + ".json";
            lock (_lock)
            {
                FileStream fs = new FileStream(fullPath, FileMode.Open, FileAccess.ReadWrite);
                fs.SetLength(fs.Length - 1);
                fs.Close();
                if (!_first)
                {
                    File.AppendAllText(fullPath, "," + record.SerializedData + "]");
                }
                else
                {
                    File.AppendAllText(fullPath, record.SerializedData + "]");
                    _first = false;
                }
            }
            //IdOffsetDict.Remove(ballId);
            //IdOffsetDict.Add(ballId, offset + 1);
        }
        public async void WriteBallPosition(BallAbstract ball, DateTime time)
        {
            DataToWrite.Enqueue(new BallRecord(ball, time));
            Task.Factory.StartNew(
                WriteData
                );
        }
    }
}
