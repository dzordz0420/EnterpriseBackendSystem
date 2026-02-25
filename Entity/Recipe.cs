using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class Recipe
    {
        public int IdRecipe { get; set; }
        public string Name { get; set; }
        public string ShortDescription {  get; set; }
        public string Description { get; set; }
        public int Time {  get; set; }
        public string Category { get; set; }
        public decimal? AverageRating { get; set; }
        public int Author { get; set; }
        public string? RecipePicture { get; set; }

    }
}
