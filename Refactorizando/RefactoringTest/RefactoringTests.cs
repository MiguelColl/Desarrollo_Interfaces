using RefactoringExercise;

namespace RefactoringTest
{
    [TestClass]
    public class RefactoringTests
    {
        [TestMethod]
        public void PriceIngredients_ValidOption()
        {
            // Arrange
            int option = 1;
            double expectedPrice = 5;
            Pizza pizza = new Pizza();

            // Act
            double actualPrice = pizza.AddIngredient(option);

            // Assert
            Assert.AreEqual(expectedPrice, actualPrice, "Wrong price");
        }

        [TestMethod]
        public void PriceIngredients_InvalidOption()
        {
            // Arrange
            int option = 10;
            Pizza pizza = new Pizza();


            // Assert
            Assert.ThrowsException<InvalidOptionsException>(() => pizza.AddIngredient(option));
        }

        [TestMethod]
        public void PriceSizes_ValidOption()
        {
            // Arrange
            int option = 3;
            double expectedPrice = 7.2;
            Pizza pizza = new Pizza();

            // Act
            pizza.AddIngredient(option);
            double actualPrice = pizza.AddSize(option);

            // Assert
            Assert.AreEqual(expectedPrice, actualPrice, 0.01, "Wrong price");
        }

        [TestMethod]
        public void PriceSizes_InvalidOption()
        {
            // Arrange
            int option = 6;
            Pizza pizza = new Pizza();

            // Act
            pizza.AddIngredient(option);

            // Assert
            Assert.ThrowsException<InvalidOptionsException>(() => pizza.AddSize(option));
        }

        [TestMethod]
        public void PriceDelivery_ValidTest()
        {
            // Arrange
            int option = 1;
            double expectedPrice = 6;
            Pizza pizza = new Pizza();

            // Act
            pizza.AddIngredient(option);
            pizza.AddSize(option);
            double actualPrice = pizza.AddDelivery(option);

            // Assert
            Assert.AreEqual(expectedPrice, actualPrice, "Wrong price");
        }

        [TestMethod]
        public void PriceDelivery_InvalidTest()
        {
            // Arrange
            int option = 3;
            Pizza pizza = new Pizza();

            // Act
            pizza.AddIngredient(option);
            pizza.AddSize(option);

            // Assert
            Assert.ThrowsException<InvalidOptionsException>(() => pizza.AddDelivery(option));
        }
    }
}