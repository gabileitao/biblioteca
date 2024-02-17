using Biblioteca.Domain.Utils;

namespace Biblioteca.Domain.UnitTest
{
    [TestClass]
    public class CpfTest {
        
        [TestMethod]
        public void HappyFlow() {

            var cpfTest = "431.711.978-12";
            var result = Cpf.Validate(cpfTest);
            Assert.IsTrue(result);

        }

        [TestMethod]
        public void BadFlowInvalidKnownCpf() {

            var cpfTest = "111.111.111-11";
            var result = Cpf.Validate(cpfTest);
            Assert.IsFalse(result);

        }

        [TestMethod]
        public void BadFlowInvalidStructureCpf() {

            var cpfTest = "356.111482-11";
            var result = Cpf.Validate(cpfTest);
            Assert.IsFalse(result);

        }

        [TestMethod]
        public void BadFlowInvalidCpf() {

            var cpfTest = "356.111.482-11";
            var result = Cpf.Validate(cpfTest);
            Assert.IsFalse(result);

        }
    }
}