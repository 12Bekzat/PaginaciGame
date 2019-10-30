using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GamePlayPagination

{
    public class SearchService
    {
        private const int INDEX_PAGE = 3;


        public void ShowGames(Contex contex, int pageNumber = 1)
        {
            UpdateAllGames(contex);
            var games = contex.VideoGames.Skip(INDEX_PAGE * --pageNumber).Take(INDEX_PAGE).ToList();
            Console.Clear();
            games.ForEach(x => Console.WriteLine($"Name: {x.Name}\nDescription: {x.Description}\nAvg rating: {x.AvgRating}\n"));
            ShowPages(contex, ++pageNumber);
        }

        public void ShowPages(Contex contex, int pageNumber = 1)
        {
            Console.WriteLine($" {pageNumber} | {GetPageCount(contex)}");
        }

        public int GetPageCount(Contex contex)
        {
            var allGames = contex.VideoGames.ToList();
            return (int)Math.Ceiling(allGames.Count / (double)INDEX_PAGE);
        }

        private void UpdateAllGames(Contex contex)
        {
            var games = contex.VideoGames.ToList();
            games.ForEach(x => UpdateAvgRating(contex, x));
        }

        private void UpdateAvgRating(Contex contex, Game game)
        {
            var ratings = contex.Ratings.Where(x=>x.VideoGame==game).ToList();
            var sumRating = 0;
            ratings.ForEach(x => sumRating += x.GameRating);
            game.AvgRating = sumRating / (double)ratings.Count;
            contex.VideoGames.Update(game);
        }
    }
}
