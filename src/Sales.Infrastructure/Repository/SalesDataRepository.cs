using CsvHelper;
using CsvHelper.Configuration;
using Sales.Core.Application.Interfaces;
using Sales.Core.Domain.Model;
using System.Globalization;

namespace Sales.Infrastructure.Repository
{
    public class SalesDataRepository : ISalesDataRepository
    {
        public IEnumerable<SalesData> GetSalesData(string relativePath)
        {
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            if (string.IsNullOrWhiteSpace(relativePath))
            {
                relativePath = "Data/CleanData.csv";
            }

            string filePath = Path.Combine(baseDirectory, relativePath);

            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException($"The file '{filePath}' does not exist.");
            }

            using var reader = new StreamReader(filePath);
            using var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true,
                Delimiter = ",",
                PrepareHeaderForMatch = args => args.Header.Trim().Replace(" ", "")
            });

            return csv.GetRecords<SalesData>().ToList();
        }
    }
}
