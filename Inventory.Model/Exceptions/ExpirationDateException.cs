using System;

namespace Inventory.Model.Exceptions
{
    public class ExpirationDateException : Exception
    {
        public ExpirationDateException(string message): base(message) { }
    }
}
