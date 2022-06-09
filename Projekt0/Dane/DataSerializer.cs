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
            BallsIdDict = new Dictionary<BallRecord, int>();
        }
        private static readonly object _lock = new object();
        private static DataSerializer? _instance = null;
        private IDictionary<BallRecord, int> BallsIdDict { get; }
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

        public string Serialize(BallRecord ball)
        {
            JsonSerializerOptions options = new JsonSerializerOptions() { WriteIndented = true };
            options.Converters.Add(new BallConverter());

            string output;
            if (!BallsIdDict.ContainsKey(ball))
            {
                lock (_lock)
                {
                    BallsIdDict.Add(ball, HighestIdCounter + 1);
                    HighestIdCounter++;
                }
            }
            output = JsonSerializer.Serialize(new { ball, ID = BallsIdDict[ball] }, options);

            return output;
        }
    }
}
