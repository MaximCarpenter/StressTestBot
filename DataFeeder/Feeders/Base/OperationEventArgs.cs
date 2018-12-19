using System;

namespace DataFeeder
{
    public class OperationEventArgs : OperationEventStartArgs
    {
        public string State { get; set; }
        public string Error { get; set; }
        public long Milliseconds { get; set; }
        public bool IsError => !string.IsNullOrEmpty(Error);
    }

    public class OperationEventStartArgs : EventArgs
    {
        public int AmountOfRows { get; set; }
        public string TableName { get; set; }
    }
}