// namespace PrimeService.Tests;

// public class Tests
// {
//     [SetUp]
//     public void Setup()
//     {
//     }

//     [Test]
//     public void Test1()
//     {
//         Assert.Pass();
//     }
// }


using NUnit.Framework;
using Prime.Services;

namespace Prime.UnitTests.Services
{
    [TestFixture] // Marks the class as containing unit tests
    public class PrimeService_IsPrimeShould
    {
        private PrimeService _primeService;

        [SetUp] // This method is executed before each test method in the fixture
        public void SetUp()
        {
            _primeService = new PrimeService(); // Initialize the service instance for each test
        }
        [Test] // Marks this method as a unit test
        public void IsPrime_InputIs1_ReturnFalse()
        {
            // Arrange (setup the test conditions)
            var result = _primeService.IsPrime(1);

            // Assert (verify the outcome)
            Assert.That(result, Is.False, "1 should not be prime");
        }

        // ... existing code ...

        [TestCase(2)]
        [TestCase(3)]
        [TestCase(5)]
        [TestCase(8)]
        public void IsPrime_ValuesLessThan2_ReturnFalse(int value)
        {
            // Arrange
            var result = _primeService?.IsPrime(value); // Use null-conditional operator for safety, though _primeService is initialized by [SetUp]

            // Assert
            Assert.That(result, Is.False, $"{value} should not be prime........!!!!.....");
        }

        // ... rest of the class ...

    }
}
