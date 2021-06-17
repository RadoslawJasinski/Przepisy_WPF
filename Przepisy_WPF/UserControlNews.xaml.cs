using Przepisy_WPF.Models;
using System;
using System.Collections;
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

namespace Przepisy_WPF
{
    /// <summary>
    /// Logika interakcji dla klasy UserControlNews.xaml
    /// </summary>
    public partial class UserControlNews : UserControl
    {
        public List<News> News { get; set; }
        public DbConnect Data { get; set; }
        public object UcNews { get;set; }
        public UserControlNews(List<News> news,DbConnect data)
        {
            InitializeComponent();
            Data = data;
            News = news;
            UcNews = this.Content;

            LatestArticle.ItemsSource = new List<News> {News.Last()};
      
            OlderArticles.ItemsSource = News.OrderByDescending(x => x.NewsID).Skip(1);
        }

        private void Article_Click(object sender, MouseButtonEventArgs e)
        {
            var senderPanel = sender as Grid;
            var panelLabel = senderPanel.Children.OfType<Label>().First();
            int newsID = (int)panelLabel.Content;

            
            var newsDetails = News.Find(x => x.NewsID.Equals(newsID));


            var recipeDetails = Data.RecipesAllList.FindAll(x => x.RecipeID.Equals(newsDetails.RecipeID)); //Get information on a specifed recipe

            if (newsDetails.IsRecipe == true) //If the article includes a recipe attachment, show the recipe details
            {

                int recipeID = newsDetails.RecipeID;
                DbConnect db = new DbConnect();
                var images = db.GetDetailImages(recipeID);
                var ingredientsQuantity = db.GetDetailQuantityIngredient(recipeID);
                var ingredientsName = db.GetSelectedIngredientsName(recipeID);

                UserControlRecipeDetails uscRecipeDet = new UserControlRecipeDetails(recipeDetails, images, ingredientsQuantity, ingredientsName, Data);

                this.Content = uscRecipeDet;
            }
            else
            {
                var NewsDetails = News.FindAll(x => x.NewsID.Equals(newsID));

                UserControlNewsDetails uscNewsDet = new UserControlNewsDetails(UcNews, NewsDetails);

                this.Content = uscNewsDet;

            }
           
        }
    }
}
