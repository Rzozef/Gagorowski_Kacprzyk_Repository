using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Threading.Tasks;

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

            DataToWrite = new List<BallRecord>();
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
        private IList<BallRecord> DataToWrite { get; set; }
        private IReadOnlyDictionary<BallAbstract, int> BallsIdDict { get; }
        private IDictionary<int, ulong> IdOffsetDict { get; }


        private void WriteData()
        {
            lock (DataToWrite)
            {
                BallRecord record = DataToWrite[0];
                DataToWrite.RemoveAt(0);

                //int ballId;
                //BallsIdDict.TryGetValue(record.Ball, out ballId);

                //ulong offset;
                //IdOffsetDict.TryGetValue(ballId, out offset);

                string fullPath = _directory + "\\" + _fileName + ".json";
                File.AppendAllText(fullPath, record.SerializedData);
                //IdOffsetDict.Remove(ballId);
                //IdOffsetDict.Add(ballId, offset + 1);
            }
        }

        public DataWriter(string subdir, string fileName, IList<BallAbstract> balls)
        {
/*            if (subdir == null || fileName == null)
            {
                throw new ArgumentNullException();
            }
            this._fileName = fileName;
            _directory = Directory.GetCurrentDirectory() + "\\" + subdir;

            if (Directory.Exists(_directory))
            {
                Directory.Delete(_directory, true);
            }
            Directory.CreateDirectory(_directory);

            DataToWrite = new List<BallRecord>();

            Dictionary<BallAbstract, int> ballsIdDict = new Dictionary<BallAbstract, int>();
            BallsIdDict = ballsIdDict;
            for (int i = 0; i < balls.Count; ++i)
            {
                ballsIdDict.Add(balls[i], i);
            }

            IdOffsetDict = new Dictionary<int, ulong>();
            foreach (KeyValuePair<BallAbstract, int> pair in BallsIdDict)
            {
                IdOffsetDict.Add(pair.Value, 0);
            }*/
        }
        public async void WriteBallPosition(BallAbstract ball, DateTime time)
        {
            DataToWrite.Add(new BallRecord(ball, time));
            Task.Factory.StartNew(
                WriteData
                );
        }
    }
}
