using CsvHelper;
using CsvHelper.Configuration;
using Sales.Core.Application.Interfaces;
using Sales.Core.Domain.Model;
using System.Globalization;

namespace Sales.Infrastructure.Repository
{
    public class SalesRepository : ISalesRepository
    {
        public IEnumerable<SalesData> GetSalesData(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath))
            {
                throw new ArgumentException("File path cannot be empty.", filePath);
            }

            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException($"The file '{filePath}' does not exist.");
            }

            return ReadDataFromFile(filePath);
        }

        private static List<SalesData> ReadDataFromFile(string filePath)
        {
            try
            {
                using var reader = new StreamReader(filePath, System.Text.Encoding.UTF8);
                using var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    HasHeaderRecord = true,
                    Delimiter = ",",
                    PrepareHeaderForMatch = args => args.Header.Trim().Replace(" ", "")
                });

                return csv.GetRecords<SalesData>().ToList();
            }

            catch (Exception ex)
            {
                throw new InvalidOperationException("An error occurred while reading the sales data CSV file.", ex);
            }
        }
    }
}
