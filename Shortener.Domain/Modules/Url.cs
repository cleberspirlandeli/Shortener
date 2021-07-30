using System;
using System.Linq;

namespace Shortener.Domain.Modules
{
    public class Url: Entity
    {
        // Properties
        public string MainDestinationUrl { get; private set; }
        public string KeyUrl { get; private set; }
        public int DayCounter { get; private set; }
        public int WeekCounter { get; private set; }
        public int AmountCounter { get; private set; }
        public int YearCounter { get; private set; }

        // Constructors
        public Url(string mainDestinationUrl)
        {
            MainDestinationUrl = mainDestinationUrl;
            KeyUrl = GenerateKeyUrl();
            // TODO: Validations
        }

        // Methods
        public string GetId() => Id.ToString("N");
        public void ResetDayCounter() => DayCounter = 10;
        public void ResetWeekCounter() => WeekCounter = 50;
        public void ResetAmountCounter() => AmountCounter = 100;
        public void ResetYearCounter() => YearCounter = 250;
        public void Increment()
        {
            DayCounter += 1;
            WeekCounter += 1;
            AmountCounter += 1;
            YearCounter += 1;
        }

        private protected string GenerateKeyUrl()
        {
            var text = string.Empty;

            for (int i = 0; i < 10; i++)
            {
                if (IsNumber())
                {
                    text += GetRandomNumber();
                } else
                {
                    if (IsCapital()) text += GetRandomString().ToUpper();
                    else text += GetRandomString().ToLower();
                }
            }

            if (int.TryParse(text, out int _)) return GenerateKeyUrl();

            return text;
        }

        private static Random random => new Random();
        private bool IsNumber() => Randomic();
        private bool IsCapital() => Randomic();
        private bool Randomic() => random.Next(2) == 1;
        private int GetRandomNumber() => random.Next(10);
        
        private string GetRandomString()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            return new string(Enumerable.Repeat(chars, 1)
                .Select(str => str[random.Next(str.Length)]).ToArray());
        }
    }
}
