namespace DSC.Core.DomainObjects
{
    public class Rg
    {
        public const int RgMaxLength = 10;
        public string Number { get; private set; }

        public Rg(string number)
        {
            if (!Validate(number)) throw new DomainException("RG inválido");
            Number = number;
        }

        public static bool Validate(string rg)
        {
            return true;
            //var regexRg = new Regex(@"(^\d{1,2}).?(\d{3}).?(\d{3})-?(\d{1}|X|x$)");
            //return regexRg.IsMatch(rg);
        }

        public void Update(string number)
        {
            Number = number;
        }

    }
}