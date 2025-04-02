namespace Sales.Core.Application.Utility
{
    public static class CurrencyParser
    {
        public static  string GetCurrencySymbol(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return ApplicationCoreConstants.DefaultCurrency;

            string trimmedValue = value.Trim();
            string currencySymbol = trimmedValue[..1] ?? ApplicationCoreConstants.DefaultCurrency;

            return currencySymbol;
        }

        public static decimal GetAmount(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return 0m;

            string trimmedValue = value.Trim();
            string numericPart = trimmedValue[1..].Trim();

            if (!decimal.TryParse(numericPart, out decimal amount))
                return 0m;

            return Math.Round(amount, 2);
        }
    }


}
