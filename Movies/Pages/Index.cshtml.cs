using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Movies.Pages
{
    public class IndexModel : PageModel
    {

        public IEnumerable<Movie> Movies { get; protected set; }

        [BindProperty(SupportsGet = true)]
        public string[] MPAARatings { get; set; }

        [BindProperty(SupportsGet = true)]
        public string[] Genres { get; set; }

        [BindProperty (SupportsGet = true)]
        public string SearchTerms { get; set; } = "";

        [BindProperty(SupportsGet = true)]
        public double? IMDBMin { get; set; }

        [BindProperty(SupportsGet = true)]
        public double? IMDBMax { get; set; }

        [BindProperty(SupportsGet = true)]
        public double? RottenMax { get; set; }

        [BindProperty(SupportsGet = true)]
        public double? RottenMin { get; set; }

        public void OnGet(double? imdbmin, double? imdbmax, double? rottenmin, double? rottenmax)
        {
            /*this.IMDBMax = imdbmax;
            this.IMDBMin = imdbmin;
            this.RottenMin = rottenmin;
            this.RottenMax = rottenmax;
            SearchTerms = Request.Query["SearchTerms"];
            MPAARatings = Request.Query["MPAARatings"];
            Genres = Request.Query["Genres"];
            Movies = MovieDatabase.Search(SearchTerms);
            Movies = MovieDatabase.FilterByMPAARating(Movies, MPAARatings);
            Movies = MovieDatabase.FilterByGenre(Movies, Genres);
            Movies = MovieDatabase.FilterByIMDBRating(Movies, IMDBMin, IMDBMax);
            Movies = MovieDatabase.FilterByRottenTomatoesRating(Movies, RottenMin, RottenMax);*/

            Movies = MovieDatabase.All;
            if (SearchTerms != null) {
                Movies = Movies.Where(movie => movie.Title != null && movie.Title.Contains(SearchTerms, StringComparison.CurrentCultureIgnoreCase));      
            }

            if (MPAARatings != null && MPAARatings.Length != 0)
            {
                Movies = Movies.Where(movie => movie.MPAARating != null && 
                    MPAARatings.Contains(movie.MPAARating));
            }

            if (Genres != null && Genres.Count() != 0)
            {
                Movies = Movies.Where(movie => movie.MajorGenre != null &&
                    Genres.Contains(movie.MajorGenre));
            }

            if (IMDBMin != null)
            {
                if (IMDBMax != null)
                {
                    Movies = Movies.Where(movie => movie.IMDBRating != null &&
                        (movie.IMDBRating >= IMDBMin && movie.IMDBRating <= IMDBMax));
                }
                else
                {
                    Movies = Movies.Where(movie => movie.IMDBRating != null &&
                        movie.IMDBRating >= IMDBMin);
                }
            }
            else
            {
                if (IMDBMax != null)
                {
                    Movies = Movies.Where(movie => movie.IMDBRating != null &&
                        movie.IMDBRating <= IMDBMax);
                }
            }

            if (RottenMin != null)
            {
                if (RottenMax != null)
                {
                    Movies = Movies.Where(movie => movie.RottenTomatoesRating != null &&
                        (movie.RottenTomatoesRating >= RottenMin && movie.RottenTomatoesRating <= RottenMax));
                }
                else
                {
                    Movies = Movies.Where(movie => movie.RottenTomatoesRating != null &&
                        movie.RottenTomatoesRating >= RottenMin);
                }
            }
            else
            {
                if (RottenMax != null)
                {
                    Movies = Movies.Where(movie => movie.RottenTomatoesRating != null &&
                        movie.RottenTomatoesRating <= RottenMax);
                }
            }
        }
    }
}
