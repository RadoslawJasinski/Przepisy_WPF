using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Przepisy_WPF.Models;

namespace Przepisy_WPF
{
    public partial class UserControlRecipeDetails : UserControl
    {
        public DbConnect Data { get; set; }
        public List<Recipe> RecipeInfo { get; set; }
        public List<Ingredient> IngredientsQuantity { get; private set; }
        public List<Ingredient> IngredientsName { get; private set; }
        public UserControlRecipeDetails(List<Recipe> recipe, List<Recipe> images, List<Ingredient> quantity, List<Ingredient> ingredientName, DbConnect data)
        {
            InitializeComponent();

            RecipeName.ItemsSource = recipe;
            DescriptionBind.ItemsSource = recipe;
            ImagesList.ItemsSource = images;
            IngredientQuantityBind.ItemsSource = quantity;
            IngredientNameBind.ItemsSource = ingredientName;
            SpicesBind.ItemsSource = recipe;
            Data = data;
            RecipeInfo = recipe;
            IngredientsQuantity = quantity;
            IngredientsName = ingredientName;
        }


        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            UserControlHome uscH = new UserControlHome(Data);
            this.Content = uscH;
        }

        private void BtnPrintRecipe_Click(object sender, RoutedEventArgs e)
        {
            PrintRecipe printRec = new PrintRecipe(RecipeInfo, IngredientsQuantity, IngredientsName);
        }
    }
}
