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

        [BindProperty]
        public string[] MPAARatings { get; set; }

        [BindProperty]
        public string[] Genres { get; set; }

        [BindProperty]
        public string SearchTerms { get; set; } = "";

        [BindProperty]
        public double? IMDBMin { get; set; }

        [BindProperty]
        public double? IMDBMax { get; set; }

        [BindProperty]
        public double? RottenMax { get; set; }

        [BindProperty]
        public double? RottenMin { get; set; }

        public void OnGet(double? imdbmin, double? imdbmax, double? rottenmin, double? rottenmax)
        {
            this.IMDBMax = imdbmax;
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
            Movies = MovieDatabase.FilterByRottenTomatoesRating(Movies, RottenMin, RottenMax);
        }
    }
}
