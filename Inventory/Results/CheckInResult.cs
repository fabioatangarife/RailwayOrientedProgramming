using Inventory.Model;
using System;

namespace Inventory.Results
{
    public class CheckInResult
    {
        public bool Success { get; set; }

        public InventoryItem Item { get; set; }

        public Exception Error { get; set; }
    }
}
