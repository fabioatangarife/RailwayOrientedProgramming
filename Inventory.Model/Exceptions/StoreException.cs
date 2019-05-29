using System;

namespace Inventory.Model.Exceptions
{
    public class StoreException : Exception
    {
        public StoreException(string message) : base(message) { }
    }
}
