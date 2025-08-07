using Ambev.DeveloperEvaluation.Domain.Common;

namespace Ambev.DeveloperEvaluation.Domain.Entities
{
    /// <summary>
    /// Represents a product included in a sale, with quantity, pricing, and discount information.
    /// </summary>
    public class SaleItem : BaseEntity
    {
        /// <summary>
        /// Gets the product id.
        /// </summary>
        public Guid ProductId { get; set; }

        public string ProductName { get;  set; } = string.Empty;

        /// <summary>
        /// Gets the number of units sold.
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// Gets the unit price of the product.
        /// </summary>
        public decimal UnitPrice { get; set; }

        /// <summary>
        /// Gets the discount amount applied to this item (absolute value, not percentage).
        /// </summary>
        public decimal Discount { get; set; }

        /// <summary>
        /// Indicates whether the item was cancelled.
        /// </summary>
        public bool Cancelled { get; private set; }

        /// <summary>
        /// Gets the total amount for this item after applying discount, or 0 if cancelled.
        /// </summary>
        public decimal TotalAmount => Cancelled ? 0 : (Quantity * UnitPrice) - Discount;


        /// <summary>
        /// Initializes a new instance of the Sale Item class.
        /// </summary>
        private SaleItem() { }

        // <summary>
        /// Initializes a new instance of the <see cref="SaleItem"/> class.
        /// Throws an exception if the quantity exceeds 20.
        /// </summary>
        /// <param name="productId">The ID of the product.</param>
        /// <param name="productName">The name of the product.</param>
        /// <param name="quantity">The quantity of the product in the sale (max 20).</param>
        /// <param name="unitPrice">The unit price of the product.</param>
        /// <exception cref="ArgumentException">Thrown when quantity is greater than 20.</exception>
        public SaleItem(Guid productId, string productName, int quantity, decimal unitPrice)
        {
            if (quantity > 20)
                throw new ArgumentException("Cannot sell more than 20 items");

            ProductId = productId;
            ProductName = productName;
            Quantity = quantity;
            UnitPrice = unitPrice;

            Discount = CalculateDiscount(quantity, unitPrice);
        }

        // <summary>
        /// Updates the quantity and unit price of the sale item.
        /// Resets the cancellation status and recalculates the discount.
        /// Throws an exception if the quantity exceeds 20.
        /// </summary>
        /// <param name="quantity">The new quantity of the product (max 20).</param>
        /// <param name="unitPrice">The new unit price of the product.</param>
        /// <exception cref="ArgumentException">Thrown when quantity is greater than 20.</exception>
        public void Update(int quantity, decimal unitPrice)
        {
            if (quantity > 20)
                throw new ArgumentException("Cannot sell more than 20 items");

            Quantity = quantity;
            UnitPrice = unitPrice;
            Discount = CalculateDiscount(quantity, unitPrice);
            Cancelled = false;
        }

        /// <summary>
        /// Calculate discounts
        /// </summary>
        /// <param name="quantity"></param>
        /// <param name="price"></param>
        /// <returns></returns>
        private static decimal CalculateDiscount(int quantity, decimal price)
        {
            if (quantity >= 10 && quantity <= 20)
                return quantity * price * 0.2m;
            if (quantity >= 4 && quantity < 10)
                return quantity * price * 0.1m;
            return 0;
        }

        /// <summary>
        /// Cancels the item, setting its total to 0.
        /// </summary>
        public void Cancel() => Cancelled = true;

    }
}
