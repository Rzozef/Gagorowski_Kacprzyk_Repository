using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace Dane
{
    internal class BallRecord
    {
        internal string Data { get; }
        internal BallAbstract Ball { get; }

        public BallRecord(string data, BallAbstract ball)
        {
            Data = data;
            Ball = ball;
        }
    }
    internal class DataWriter
    {
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

                int ballId;
                BallsIdDict.TryGetValue(record.Ball, out ballId);

                ulong offset;
                IdOffsetDict.TryGetValue(ballId, out offset);

                string fullPath = _directory + "\\" + _fileName + ".json";
                File.AppendAllText(fullPath, ballId + record.Data);
                IdOffsetDict.Remove(ballId);
                IdOffsetDict.Add(ballId, offset + 1);
            }
        }

        public DataWriter(string subdir, string fileName, IList<BallAbstract> balls)
        {
            if (subdir == null || fileName == null)
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
            }
        }

        public async void WriteBallsPosition(string data, BallAbstract ball)
        {
            DataToWrite.Add(new BallRecord(data, ball));
            Task.Factory.StartNew(
                WriteData
                );
        }
    }
}
