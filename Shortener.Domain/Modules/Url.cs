﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Linq;

namespace Shortener.Domain.Modules
{
    public class Url : Entity
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
            MainDestinationUrl = GenerateUrl(mainDestinationUrl);
            KeyUrl = GenerateKeyUrl();
        }


        // Methods
        public string GetId() => Id.ToString();
        public void ResetDayCounter() => DayCounter = 0;
        public void ResetWeekCounter() => WeekCounter = 0;
        public void ResetAmountCounter() => AmountCounter = 0;
        public void ResetYearCounter() => YearCounter = 0;

        public void IncrementDayCounter() => DayCounter += 1;
        public void IncrementWeekCounter() => WeekCounter += 1;
        public void IncrementAmountCounter() => AmountCounter += 1;
        public void IncrementYearCounter() => YearCounter += 1;

        public void UpdateWeeklyTotalCounter() => WeekCounter += DayCounter;

        private string GenerateUrl(string url)
            => (url.StartsWith("https://") || url.StartsWith("http://")) ? url : $"http://{url}";

        private protected string GenerateKeyUrl()
        {
            var text = string.Empty;

            for (int i = 0; i < 10; i++)
            {
                if (IsNumber())
                {
                    text += GetRandomNumber();
                }
                else
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
