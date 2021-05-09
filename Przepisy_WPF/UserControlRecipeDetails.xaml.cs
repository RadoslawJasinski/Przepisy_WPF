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
        public UserControlRecipeDetails(List<Recipe> recipe, List<Recipe> images, List<Ingredient> quantity, List<Ingredient> ingredientName)
        {
            InitializeComponent();

            RecipeName.ItemsSource = recipe;
            DescriptionBind.ItemsSource = recipe;
            ImagesList.ItemsSource = images;
            IngredientQuantityBind.ItemsSource = quantity;
            IngredientNameBind.ItemsSource = ingredientName;
            SpicesBind.ItemsSource = recipe;
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        { 
            MainWindow mw = new MainWindow();
            var Recipe = mw.RecipesAllList;
            var Ingre = mw.IngredientList;
            var BreakfastList = mw.BreakfastList;
            var DinnerList = mw.DinnerList;
            var SnackList = mw.SnackList;
            var DessertList = mw.DessertList;

            UserControlHome uscH = new UserControlHome(Recipe, Ingre, BreakfastList, DinnerList, SnackList, DessertList);
            this.Content = uscH;
        }
    }
}
