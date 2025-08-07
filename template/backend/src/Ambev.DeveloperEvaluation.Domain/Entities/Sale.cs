using Ambev.DeveloperEvaluation.Domain.Common;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Ambev.DeveloperEvaluation.Domain.Entities
{
    /// <summary>
    /// Represents a sales transaction containing customer, branch and item details.
    /// Handles business rules such as discounts and cancellation.
    /// </summary>
    public class Sale : BaseEntity
    {
        /// <summary>
        /// Gets the sale number used for external reference.
        /// </summary>
        public string SaleNumber { get; set; } = Guid.NewGuid().ToString("N")[..8];

        /// <summary>
        /// Gets the date and time the sale was created (UTC).
        /// </summary>
        public DateTime Date { get; set; } = DateTime.UtcNow;


        /// <summary>
        /// Gets the ID of the customer who made the purchase.
        /// </summary>
        public Guid CustomerId { get;  set; }

        /// <summary>
        /// Gets the name of the customer who made the purchase.
        /// </summary>
        public string CustomerName { get;  set; }

        /// <summary>
        /// Gets the customer name who made the purchase.
        /// </summary>
       // public string UserName { get; private set; }

        // <summary>
        /// Gets the id of the branch where the sale was made.
        /// </summary>
        public Guid BranchId { get; set; }

        // <summary>
        /// Gets the name of the branch where the sale was made.
        /// </summary>
        public string BranchName { get; set; } = string.Empty;

        /// <summary>
        /// Indicates whether the sale was cancelled.
        /// </summary>
        public bool Cancelled { get; set; } = false;

        /// <summary>
        /// Gets the list of items included in the sale.
        /// </summary>
        public List<SaleItem> Items { get; set; } = [];

        /// <summary>
        /// Gets the total amount of the sale, considering discounts and cancelled items.
        /// </summary>
        public decimal TotalAmount { get; private set; }

        /// <summary>
        /// Initializes a new instance of the Sale class.
        /// </summary>
        private Sale() { }


        public Sale(Guid customerId, string customerName, Guid branchId, string branchName)
        {
            CustomerId = customerId;
            CustomerName = customerName;
            BranchId = branchId;
            BranchName = branchName;
        }

        public void AddItem(Guid productId, string productName, int quantity, decimal unitPrice)
        {
            var item = new SaleItem(productId, productName, quantity, unitPrice);
            Items.Add(item);
            RecalculateTotal();
        }

        public void UpdateItems(List<(Guid ProductId, string ProductName, int Quantity, decimal UnitPrice)> updatedItems)
        {
            foreach (var existingItem in Items)
            {
                var stillExists = updatedItems.Any(i => i.ProductId == existingItem.ProductId);
                if (!stillExists)
                {
                    existingItem.Cancel();
                }
            }

            foreach (var item in updatedItems)
            {
                var existing = Items.FirstOrDefault(i => i.ProductId == item.ProductId);
                if (existing != null)
                {
                    existing.Update(item.Quantity, item.UnitPrice);
                }
                else
                {
                    AddItem(item.ProductId, item.ProductName, item.Quantity, item.UnitPrice);
                }
            }

            RecalculateTotal();
        }

        private void RecalculateTotal()
        {
            TotalAmount = Items.Sum(x => x.TotalAmount);
        }

        /// <summary>
        /// Cancels the entire sale.
        /// </summary>
        public void CancelSale() => Cancelled = true;

        /// <summary>
        /// Cancels a specific item in the sale.
        /// </summary>
        public void CancelItem(Guid itemId)
        {
            var item = Items.FirstOrDefault(x => x.Id == itemId);
            item?.Cancel();
        }

    }
}
