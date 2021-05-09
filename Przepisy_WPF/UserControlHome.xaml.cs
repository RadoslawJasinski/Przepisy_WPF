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
using MySql.Data.MySqlClient;
using Przepisy_WPF.Models;

namespace Przepisy_WPF
{
    
    public partial class UserControlHome : UserControl
    {
        public List<Recipe> RecipesAllList { get; set; }
        public List<Ingredient> IngredientList { get; set; }
        public List<Recipe> BreakfastList { get; set; }
        public List<Recipe> DinnerList { get; set; }
        public List<Recipe> SnackList { get; set; }
        public List<Recipe> DessertList { get; set; }
        public List<Recipe> SelectedRecipes { get; set; }

        public UserControlHome(List<Recipe> RecipesAllList,List<Ingredient> IngredientList, List<Recipe> BreakfastList, List<Recipe> DinnerList,List<Recipe> SnackList, List<Recipe> DessertList)
        {
            InitializeComponent();
            ItemsList.ItemsSource = RecipesAllList;
            IngredientsList.ItemsSource = IngredientList;

            this.RecipesAllList = RecipesAllList;
            this.IngredientList = IngredientList;
            this.BreakfastList = BreakfastList;
            this.DinnerList = DinnerList;
            this.SnackList = SnackList;
            this.DessertList = DessertList;  
        }

        private void BtnOpenIng_Click(object sender, RoutedEventArgs e) //Button method open "search by ingredients"
        {
            btn_CloseIngredients.Visibility = Visibility.Visible;
            btn_OpenIngredients.Visibility = Visibility.Collapsed;
        }

        private void BtnCloseIng_Click(object sender, RoutedEventArgs e) //Button method close "search by ingredients"
        {
            btn_CloseIngredients.Visibility = Visibility.Collapsed;
            btn_OpenIngredients.Visibility = Visibility.Visible;
        }

        private void BtnIngSearch_Click(object sender, RoutedEventArgs e)
        {
            DbConnect db = new DbConnect();
            string ingredientsID = "";

            foreach (Ingredient check in IngredientsList.Items)
            {
                if (check.IsSelected)
                {
                    ingredientsID += check.IngredientID.ToString() + ",";
                }
            }
            
            if(ingredientsID.Length>0)
            {
                ingredientsID = ingredientsID.Remove(ingredientsID.Length - 1);
                
                var recipeID = db.GetSelectedRecipeID(ingredientsID);
                var selectedItems = db.GetSelected(recipeID);


                if (selectedItems.Capacity == 0)
                {
                    MessageBox.Show("Zaznacz więcej składników", "Brak wyników", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    ItemsList.ItemsSource = selectedItems;
                    SelectedRecipes = selectedItems;
                    lb_Category.Content = "Według składników";
                }
            }
            else
            {
                MessageBox.Show("Zaznacz którykolwiek składnik", "Brak zaznaczonych", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            
        }

        private void TxtSearch_TextChanged(object sender, TextChangedEventArgs e) //Method for the search engine on the top bar
        {

            switch (lb_Category.Content)
            {
                case "Według składników":
                    {
                        var byIngredients = SelectedRecipes.FindAll(x => x.Name.Contains(txt_Search.Text));
                        ItemsList.ItemsSource = byIngredients;
                        break;
                    }
                case "Śniadania":
                    {
                        var breakfastList = BreakfastList.FindAll(x => x.Name.Contains(txt_Search.Text));
                        ItemsList.ItemsSource = breakfastList;
                        break;
                    }
                case "Obiady":
                    {
                        var dinnerList = DinnerList.FindAll(x => x.Name.Contains(txt_Search.Text));
                        ItemsList.ItemsSource = dinnerList;
                        break;
                    }
                case "Przekąski":
                    {
                        var snackList = SnackList.FindAll(x => x.Name.Contains(txt_Search.Text));
                        ItemsList.ItemsSource = snackList;
                        break;
                    }
                case "Desery":
                    {
                        var dessertList = DessertList.FindAll(x => x.Name.Contains(txt_Search.Text));
                        ItemsList.ItemsSource = dessertList;
                        break;
                    }

                default:
                    {
                        var all = RecipesAllList.FindAll(x => x.Name.Contains(txt_Search.Text));
                        ItemsList.ItemsSource = all;
                        break;
                    }
            }
        }

        private void Recipe_Click(object sender, RoutedEventArgs e)
        {
            var senderPanel = sender as Grid;
            var panelLabel = senderPanel.Children.OfType<Label>().First();
            var tag = panelLabel.Content.ToString();
            
            var recipeDetails = RecipesAllList.FindAll(x => x.Name.Contains(tag)); //Get information on a specifed recipe

            var recID = recipeDetails.First();
            var id = recID.RecipeID;

            DbConnect db = new DbConnect();
            var images = db.GetDetailImages(id);
            var ingredientsQuantity = db.GetDetailQuantityIngredient(id);
            var ingredientsName = db.GetSelectedIngredientsName(id);

            //var result = IngredientList.Union(ingredientsQuantity).Where(w => (IngredientList.Contains(w) && ingredientsQuantity.Contains(w))).ToList<Ingredient>();

            //var commonIngredients = from x in IngredientList join det in ingredientsQuantity on x.IngredientID equals det.IngredientID select x.IngredientName;

            UserControlRecipeDetails uscRecipeDet = new UserControlRecipeDetails(recipeDetails,images,ingredientsQuantity,ingredientsName);

            this.Content = uscRecipeDet;
        }

        //private List<Ingredient> GetCommonList(List<Ingredient> A, List<Ingredient> B)
        //{
        //    var result = A.Intersect(B).ToList<Ingredient>();
            
        //    return result;
        //}
    }
}
