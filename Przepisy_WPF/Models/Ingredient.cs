using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Przepisy_WPF.Models
{
    public class Ingredient
    {
        public int IngredientID { get; set; }
        public string IngredientName { get; set; }
        public bool IsSelected { get; set; }
        public string Quantity { get; set; }
        public int RecipeID { get; set; }


        public Ingredient(string name)
        {
            this.IngredientName = name;
        }
        public Ingredient(int id, string name)
        {
            this.IngredientID = id;
            this.IngredientName = name;
        }
        public Ingredient(string quantity,int idIngredient, int idRecipe)
        {
            this.Quantity = quantity;
            this.IngredientID = idIngredient;
            this.RecipeID = idRecipe;
        }
    }
}
