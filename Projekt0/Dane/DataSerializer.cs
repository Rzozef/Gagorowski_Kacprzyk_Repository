using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Text.Json;

namespace Dane
{
    internal class DataSerializer
    {
        DataSerializer()
        {
            BallsIdDict = new Dictionary<BallAbstract, int>();
        }
        private static readonly object _lock = new object();
        private static DataSerializer? _instance = null;
        private IDictionary<BallAbstract, int> BallsIdDict { get; }
        private int HighestIdCounter { get; set; }
        public static DataSerializer Instance
        {
            get
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new DataSerializer();
                    }
                    return _instance;
                }
            }
        }

        public string Serialize(BallRecord ball, BallAbstract orgBall)
        {
            JsonSerializerOptions options = new JsonSerializerOptions() { WriteIndented = true };
            options.Converters.Add(new BallConverter());

            string output;
            if (!BallsIdDict.ContainsKey(orgBall))
            {
                lock (_lock)
                {
                    BallsIdDict.Add(orgBall, HighestIdCounter + 1);
                    HighestIdCounter++;
                }
            }
            output = JsonSerializer.Serialize(new { ball, ID = BallsIdDict[orgBall] }, options);

            return output;
        }
    }
}
