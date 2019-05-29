using System;

namespace Inventory.Model.Exceptions
{
    public class StatusException : Exception
    {
        public StatusException(string message) : base(message) { }
    }
}
