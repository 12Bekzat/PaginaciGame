using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace GamePlayPagination
{
  class Program
  {
    static void Main(string[] args)
    {
      var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
          .AddJsonFile("appsettings.json", false, true);

      IConfigurationRoot configurationRoot = builder.Build();

      var connectionString = configurationRoot.GetConnectionString("DebugConnectionString");
      using (var contex = new Contex(connectionString))
      {
        var search = new SearchService();
        var pageNumber = 0;
        search.ShowGames(contex);
        while (true)
        {
          Console.Write("Введите номер страницы: ");
          if (int.TryParse(Console.ReadLine(), out pageNumber) && search.GetPageCount(contex) >= pageNumber && pageNumber > 0)
          {
            search.ShowGames(contex, pageNumber);
          }
          else
          {
            Console.Write("Неккоректный ввод! Повторите попытку: ");
          }
        }
      }
    }
  }
}
