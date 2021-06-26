using System.Text.RegularExpressions;

namespace DSC.Core.DomainObjects
{
    public class Cnpj
    {
        public const int CnpjMaxLength = 14;
        public string Number { get; private set; }

        public Cnpj(string number)
        {
            if (!Validate(number)) throw new DomainException("CNPJ inválido");
            Number = number;
        }

        public static bool Validate(string cnpj)
        {
            // Valida tanto 12.345.678/0001-00 quanto 12345678000100
            var regexCpnj = new Regex(@"(^\d{2}\.\d{3}\.\d{3}\/\d{4}\-\d{2}$)");
            return regexCpnj.IsMatch(cnpj);
        }
        public void Update(string number)
        {
            Number = number;
        }

    }
}