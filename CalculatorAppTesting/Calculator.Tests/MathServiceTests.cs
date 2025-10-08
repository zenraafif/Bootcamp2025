using NUnit.Framework;
using Calculator;

namespace Calculator.Tests
{
    [TestFixture]//Menandai class berisi test.
    public class MathServiceTests
    {
        private MathService _mathService;

        [SetUp]//Dijalankan sebelum tiap test (biasanya untuk inisialisasi).
        public void Setup()
        {
            _mathService = new MathService();
        }

        [Test]//Menandai satu test method.
        public void Add_WhenCalled_ReturnsSumOfArguments()
        {
            var svc = new MathService();
            var result = _mathService.Add(2, 3);
            Assert.That(result, Is.EqualTo(5));//Membandingkan nilai hasil dengan ekspektasi.
            // Assert.Throws<DivideByZeroException>(() => svc.Divide(1, 0)); //Mengecek apakah method melempar exception.
            // Assert.IsTrue(result > 0); //Mengecek apakah kondisi benar.
        }

        [Test]
        public void Subtract_WhenCalled_ReturnsDifferenceOfArguments()
        {
            // Arrange
            var result = _mathService.Subtract(10, 4);

            // Assert
            Assert.That(result, Is.EqualTo(6));
        }

        [Test]
        public void Multiply_WhenCalled_ReturnsProductOfArguments()
        {
            // Arrange
            var result = _mathService.Multiply(3, 4);

            // Assert
            Assert.That(result, Is.EqualTo(12));
        }

        [Test]
        public void Divide_WhenCalled_ReturnsQuotient()
        {
            // Arrange
            var result = _mathService.Divide(10, 2);

            // Assert
            Assert.That(result, Is.EqualTo(5));
        }

        [Test]
        public void Divide_ByZero_ThrowsException()
        {
            // Assert + Act
            Assert.Throws<DivideByZeroException>(() => _mathService.Divide(5, 0));
        }
    
    }

    
   
}

/* 
Prinsip Unit Test yang Baik

1.  Satu test = satu skenario logika.
    Jangan uji banyak hal dalam satu test.
2.  Nama test harus jelas.
    Gunakan format:
    
    [MethodName]_   [Condition]_    [ExpectedResult]
    Add_            WhenCalled_     ReturnsSumOfArguments
    Divide_         ByZero_         ThrowsException


3.  Isolasi – test tidak bergantung pada data atau urutan test lain.
4.  Repeatable – hasil test selalu sama tiap kali dijalankan.
*/
