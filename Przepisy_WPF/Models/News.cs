using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Przepisy_WPF.Models
{
    public class News
    {
        public int NewsID { get; set; }
        public string Header { get; set; }
        public string Content { get; set; }
        public string ImageURL { get; set; }
        public bool IsRecipe { get; set; }
        public int RecipeID { get; set; }

        public News(int id,string header,string content,string imageURL,bool isRecipe,int recipeId)
        {
            this.NewsID = id;
            this.Header = header;
            this.Content = content;
            this.ImageURL = imageURL;
            this.IsRecipe = isRecipe;
            this.RecipeID = recipeId;
        }

    }
}
