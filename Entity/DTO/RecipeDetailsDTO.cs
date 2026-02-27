using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.DTO
{
    public class RecipeDetailsDTO
    {
        public int IdRecipe { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Time { get; set; }
        public string Category { get; set; }
        public string? RecipePicture { get; set; }
        public AuthorDTO Author { get; set; }
    }
}
