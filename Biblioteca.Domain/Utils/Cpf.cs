using System.Linq;
using System.Text.RegularExpressions;

namespace Biblioteca.Domain.Utils
{
    public static class Cpf {

        public static bool Validate(string cpf) {

            var regex = new Regex(@"\d{3}.\d{3}.\d{3}-\d{2}");

            //verifica se a 
            if (!regex.IsMatch(cpf)) return false;
            if(IsKnownInvalidCpf(cpf)) return false;

            var firstNineNights = cpf.Split('-')[0].Replace(".", "").Select(c => int.Parse($"{c}")).ToArray();
            var firstVerificationDigit = int.Parse($"{cpf.Split('-')[1][0]}");
            var secondVerificationDigit = int.Parse($"{cpf.Split('-')[1][1]}");
            var isFirstDigitValid = ValidateFirstDigit(firstNineNights, firstVerificationDigit);
            var isSecondDigitValid = ValidateSecondDigit(firstNineNights, firstVerificationDigit, secondVerificationDigit);

            return isFirstDigitValid && isSecondDigitValid;

        }

        private static bool IsKnownInvalidCpf(string cpf) {

            var knownInvalidCpfs = new[] {
                "000.000.000-00",
                "111.111.111-11",
                "222.222.222-22",
                "333.333.333-33",
                "444.444.444-44",
                "555.555.555-55",
                "666.666.666-66",
                "777.777.777-77",
                "888.888.888-88",
                "999.999.999-99"
            };

            return knownInvalidCpfs.Contains(cpf);

        }

        private static bool ValidateFirstDigit(int[] firstNineDigits, int firstVerificationDigit) {
            var prodSum = 0;
            for (var i = 0; i < firstNineDigits.Length; i++) {
                prodSum += firstNineDigits[i] * (10 - i);
            }

            var remainder = (prodSum * 10) % 11;
            return remainder == firstVerificationDigit;
        }

        private static bool ValidateSecondDigit(int[] firstNineDigits, int firstVerificationDigit, int secondVerificationDigit) {
            var firstTenDigits = firstNineDigits.Concat(new[] { firstVerificationDigit }).ToArray();
            
            var prodSum = 0;
            for (var i = 0; i < firstTenDigits.Length; i++) {
                prodSum += firstTenDigits[i] * (11 - i);
            }

            var remainder = (prodSum * 10) % 11;
            return remainder == secondVerificationDigit;
        }

    }
}
