using System;

namespace DSC.MessageBus
{
    public class Event
    {
        public DateTime Timestamp { get; private set; }

        protected Event()
        {
            Timestamp = DateTime.Now;
        }
    }
}