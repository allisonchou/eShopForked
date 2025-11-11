using eShop.ClientApp.Models.Basket;

namespace ClientApp.UnitTests.Models;

[TestClass]
public class CustomerBasketTests
{
    [TestMethod]
    public async Task UpdateQuantityAsync_ExistingProduct_UpdatesQuantity()
    {
        // Arrange
        var basket = new CustomerBasket();
        var item = new BasketItem { ProductId = 1, Quantity = 5 };
        basket.AddItemToBasket(item);

        // Act
        var result = await basket.UpdateQuantityAsync(1, 10);

        // Assert
        Assert.IsTrue(result);
        Assert.AreEqual(1, basket.Items.Count);
        Assert.AreEqual(10, basket.Items.First().Quantity);
    }

    [TestMethod]
    public async Task UpdateQuantityAsync_ExistingProduct_ZeroQuantity_RemovesItem()
    {
        // Arrange
        var basket = new CustomerBasket();
        var item = new BasketItem { ProductId = 1, Quantity = 5 };
        basket.AddItemToBasket(item);

        // Act
        var result = await basket.UpdateQuantityAsync(1, 0);

        // Assert
        Assert.IsTrue(result);
        Assert.AreEqual(0, basket.Items.Count);
    }

    [TestMethod]
    public async Task UpdateQuantityAsync_NonExistingProduct_ReturnsFalse()
    {
        // Arrange
        var basket = new CustomerBasket();
        var item = new BasketItem { ProductId = 1, Quantity = 5 };
        basket.AddItemToBasket(item);

        // Act
        var result = await basket.UpdateQuantityAsync(999, 10);

        // Assert
        Assert.IsFalse(result);
        Assert.AreEqual(1, basket.Items.Count);
        Assert.AreEqual(5, basket.Items.First().Quantity);
    }

    [TestMethod]
    public async Task UpdateQuantityAsync_NegativeQuantity_ThrowsArgumentOutOfRangeException()
    {
        // Arrange
        var basket = new CustomerBasket();
        var item = new BasketItem { ProductId = 1, Quantity = 5 };
        basket.AddItemToBasket(item);

        // Act & Assert
        await Assert.ThrowsExceptionAsync<ArgumentOutOfRangeException>(
            async () => await basket.UpdateQuantityAsync(1, -1));
    }

    [TestMethod]
    public async Task UpdateQuantityAsync_EmptyBasket_ReturnsFalse()
    {
        // Arrange
        var basket = new CustomerBasket();

        // Act
        var result = await basket.UpdateQuantityAsync(1, 10);

        // Assert
        Assert.IsFalse(result);
        Assert.AreEqual(0, basket.Items.Count);
    }

    [TestMethod]
    public async Task UpdateQuantityAsync_MultipleItems_UpdatesCorrectItem()
    {
        // Arrange
        var basket = new CustomerBasket();
        basket.AddItemToBasket(new BasketItem { ProductId = 1, Quantity = 5 });
        basket.AddItemToBasket(new BasketItem { ProductId = 2, Quantity = 3 });
        basket.AddItemToBasket(new BasketItem { ProductId = 3, Quantity = 7 });

        // Act
        var result = await basket.UpdateQuantityAsync(2, 15);

        // Assert
        Assert.IsTrue(result);
        Assert.AreEqual(3, basket.Items.Count);
        Assert.AreEqual(5, basket.Items.First(i => i.ProductId == 1).Quantity);
        Assert.AreEqual(15, basket.Items.First(i => i.ProductId == 2).Quantity);
        Assert.AreEqual(7, basket.Items.First(i => i.ProductId == 3).Quantity);
    }

    [TestMethod]
    public async Task UpdateQuantityAsync_MultipleItems_RemovesMiddleItem()
    {
        // Arrange
        var basket = new CustomerBasket();
        basket.AddItemToBasket(new BasketItem { ProductId = 1, Quantity = 5 });
        basket.AddItemToBasket(new BasketItem { ProductId = 2, Quantity = 3 });
        basket.AddItemToBasket(new BasketItem { ProductId = 3, Quantity = 7 });

        // Act
        var result = await basket.UpdateQuantityAsync(2, 0);

        // Assert
        Assert.IsTrue(result);
        Assert.AreEqual(2, basket.Items.Count);
        Assert.IsTrue(basket.Items.Any(i => i.ProductId == 1));
        Assert.IsFalse(basket.Items.Any(i => i.ProductId == 2));
        Assert.IsTrue(basket.Items.Any(i => i.ProductId == 3));
    }
}
