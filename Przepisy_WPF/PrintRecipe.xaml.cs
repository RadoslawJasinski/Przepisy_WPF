using Przepisy_WPF.Models;
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
using System.Windows.Shapes;

namespace Przepisy_WPF
{

    public partial class PrintRecipe : Window
    {
        public PrintRecipe(List<Recipe> recipes, List<Ingredient> quantity, List<Ingredient> ingredientsName)
        {

            InitializeComponent();

            DescriptionBind.ItemsSource = recipes;
            RecipeName.ItemsSource = recipes;
            IngredientNameBind.ItemsSource = ingredientsName;
            IngredientQuantityBind.ItemsSource = quantity;
            SpicesBind.ItemsSource = recipes;
            Print();
            this.Close();
        }

        private void Print()
        {
            PrintDialog print = new PrintDialog
            {
                PageRangeSelection = PageRangeSelection.AllPages,
                UserPageRangeEnabled = true
            };

            print.PrintVisual(this.Content as Visual, "Drukowanie przepisu z aplikacji");
        }
    }
}