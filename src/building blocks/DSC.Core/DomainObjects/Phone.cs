using System.Text.RegularExpressions;

namespace DSC.Core.DomainObjects
{
    public class Phone
    {
        public const int PhoneMaxLength = 13;

        public string Number { get; private set; }

        public Phone(string number)
        {
            if (!Validate(number)) throw new DomainException("Telefone inválido");
            Number = number;
        }

        public static bool Validate(string phone)
        {
            // Valida qualquer telefone ou celular, com ou sem DDD. O traço é opcional. Sem parênteses.
            var regexPhone = new Regex(@"(^[0-9]{2})?(\s|-)?(9?[0-9]{4})-?([0-9]{4}$)");
            return regexPhone.IsMatch(phone);
        }

        public void Update(string number)
        {
            Number = number;
        }

    }
}