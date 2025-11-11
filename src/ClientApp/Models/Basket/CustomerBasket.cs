namespace eShop.ClientApp.Models.Basket;

public class CustomerBasket
{
    private readonly List<BasketItem> _items = new();
    public string BuyerId { get; set; }
    public IReadOnlyList<BasketItem> Items => _items;

    public int ItemCount => _items.Sum(x => x.Quantity);

    public void AddItemToBasket(BasketItem basketItem)
    {
        foreach (var item in _items)
        {
            if (item.ProductId == basketItem.ProductId)
            {
                item.Quantity++;
                return;
            }
        }

        _items.Add(basketItem);
    }

    public void RemoveItemFromBasket(BasketItem basketItem)
    {
        for (var i = _items.Count - 1; i >= 0; i--)
        {
            if (_items[i].ProductId == basketItem.ProductId)
            {
                _items.RemoveAt(i);
                return;
            }
        }
    }

    /// <summary>
    /// Asynchronously updates the quantity of a specific product in the basket.
    /// </summary>
    /// <param name="productId">The unique identifier of the product whose quantity should be updated.</param>
    /// <param name="quantity">The new quantity for the product. Must be a non-negative value. If set to 0, the item will be removed from the basket.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation that returns <c>true</c> if the product was found and updated; otherwise, <c>false</c>.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="quantity"/> is negative.</exception>
    /// <remarks>
    /// This method searches for an existing item in the basket with the specified <paramref name="productId"/>.
    /// If found and the <paramref name="quantity"/> is greater than 0, the item's quantity is updated.
    /// If the <paramref name="quantity"/> is 0, the item is removed from the basket entirely.
    /// If the product is not found in the basket, the method returns <c>false</c> and no changes are made.
    /// </remarks>
    public async Task<bool> UpdateQuantityAsync(int productId, int quantity)
    {
        if (quantity < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(quantity), "Quantity cannot be negative.");
        }

        await Task.CompletedTask; // Simulate async operation for future extensibility

        for (var i = 0; i < _items.Count; i++)
        {
            if (_items[i].ProductId == productId)
            {
                if (quantity > 0)
                {
                    _items[i].Quantity = quantity;
                }
                else
                {
                    _items.RemoveAt(i);
                }
                return true;
            }
        }

        return false;
    }

    public void ClearBasket()
    {
        _items.Clear();
    }
}
