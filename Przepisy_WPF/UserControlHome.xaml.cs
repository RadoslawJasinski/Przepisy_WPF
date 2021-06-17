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

        public DbConnect Data { get; set; }

        public UserControlHome(DbConnect data)
        {
            InitializeComponent();
            Data = data;
            ItemsList.ItemsSource = Data.RecipesAllList.OrderByDescending(x => x.RecipeID);
            IngredientsList.ItemsSource = Data.IngredientList.OrderBy(x => x.IngredientName);
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
                    Data.SelectedRecipes = selectedItems;
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
                        var byIngredients = Data.SelectedRecipes.FindAll(x => x.Name.Contains(txt_Search.Text));
                        ItemsList.ItemsSource = byIngredients.OrderByDescending(x => x.RecipeID);
                        break;
                    }
                case "Śniadania":
                    {
                        var breakfastList = Data.BreakfastList.FindAll(x => x.Name.Contains(txt_Search.Text));
                        ItemsList.ItemsSource = breakfastList.OrderByDescending(x => x.RecipeID);
                        break;
                    }
                case "Obiady":
                    {
                        var dinnerList = Data.DinnerList.FindAll(x => x.Name.Contains(txt_Search.Text));
                        ItemsList.ItemsSource = dinnerList.OrderByDescending(x => x.RecipeID);
                        break;
                    }
                case "Przekąski":
                    {
                        var snackList = Data.SnackList.FindAll(x => x.Name.Contains(txt_Search.Text));
                        ItemsList.ItemsSource = snackList.OrderByDescending(x => x.RecipeID);
                        break;
                    }
                case "Desery":
                    {
                        var dessertList = Data.DessertList.FindAll(x => x.Name.Contains(txt_Search.Text));
                        ItemsList.ItemsSource = dessertList.OrderByDescending(x => x.RecipeID);
                        break;
                    }

                default:
                    {
                        var all = Data.RecipesAllList.FindAll(x => x.Name.Contains(txt_Search.Text));
                        ItemsList.ItemsSource = all.OrderByDescending(x => x.RecipeID);
                        break;
                    }
            }
        }

        private void Recipe_Click(object sender, RoutedEventArgs e)
        {
            var senderPanel = sender as Grid;
            var panelLabel = senderPanel.Children.OfType<Label>().First();
            int recipeID = (int)panelLabel.Content;
            
            var recipeDetails = Data.RecipesAllList.FindAll(x => x.RecipeID.Equals(recipeID)); //Get information on a specifed recipe

            DbConnect db = new DbConnect();
            var images = db.GetDetailImages(recipeID);
            var ingredientsQuantity = db.GetDetailQuantityIngredient(recipeID);
            var ingredientsName = db.GetSelectedIngredientsName(recipeID);

            UserControlRecipeDetails uscRecipeDet = new UserControlRecipeDetails(recipeDetails,images,ingredientsQuantity,ingredientsName,Data);

            this.Content = uscRecipeDet;
        }

    }
}
