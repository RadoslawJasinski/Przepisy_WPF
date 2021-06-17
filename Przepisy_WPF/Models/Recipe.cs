using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Przepisy_WPF.Models
{
    public class Recipe
    {
        public int RecipeID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageURL { get; set; }
        public string Spices { get; set; }
        public int CategoryID { get; set; }
        public string ImagesURL { get; set; }




        public Recipe(int id,string name,string desc,string image, string spices, int category)
        {
            this.RecipeID = id;
            this.Name = name;
            this.Description = desc;
            this.ImageURL = image;
            this.Spices = spices;
            this.CategoryID = category;
        }

        public Recipe(string imagesURL)
        {
            this.ImagesURL = imagesURL;
        }
    }

}
