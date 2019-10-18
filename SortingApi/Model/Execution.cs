using System;
using System.Collections.Generic;

namespace SortingApi.Model
{
    public class Execution
    {
        // These are made public for simplicity, to work with JsonConverter
        public int Id { get; set; }
        public DateTime TimeStamp { get; set; }
        public int Duration { get; set; }
        public ExecutionStatus Status { get; set; }
        public List<int> Input { get; set; }
        public List<int> Output { get; set; }

        internal Execution(int id, List<int> input)
        {
            Id = id;
            TimeStamp = DateTime.Now;
            Duration = 0;
            Status = ExecutionStatus.Pending;
            Input = input;
            Output = input;
        }

        // Constructor used by JSON Converter
        public Execution(int id, DateTime timeStamp, int duration, ExecutionStatus status, List<int> input, List<int> output)
        {
            Id = id;
            TimeStamp = timeStamp;
            Duration = duration;
            Status = status;
            Input = input;
            Output = output;
        }
    }
}
