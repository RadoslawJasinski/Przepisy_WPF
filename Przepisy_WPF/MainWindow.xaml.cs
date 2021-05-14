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
    public partial class MainWindow : Window
    {
        
        private readonly DbConnect _data = new DbConnect();

        public MainWindow()
        {
            InitializeComponent();
            
            _data.GetData();
            _data.GetIngredient();

            MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
            UserControlHome UscHome = new UserControlHome(_data);

            ContentPanel.Children.Add(UscHome);
        }

        private void DragWindow(object sender, RoutedEventArgs e)
        {
            DragMove();
        }
        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }
        private void BtnMax_Click(object sender, RoutedEventArgs e)
        {
            if(this.WindowState == WindowState.Normal)
            {
                this.WindowState = WindowState.Maximized;
            }
            else
            {
                this.WindowState = WindowState.Normal;
            }
        }
        private void BtnMin_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        public Brush GradientButtonsMenu()
        {
            LinearGradientBrush myGradient = new LinearGradientBrush
            {
                StartPoint = new Point(0, 0),

                EndPoint = new Point(0, 1)
            };

            GradientStop fir = new GradientStop
            {
                Color = Color.FromRgb(248, 78, 111),

                Offset = 0.1
            };
            myGradient.GradientStops.Add(fir);


            GradientStop sec = new GradientStop
            {
                Color = Color.FromRgb(249, 128, 71),

                Offset = 1
            };
            
            myGradient.GradientStops.Add(sec);
            
            return myGradient;
        }

        private Brush GradientButtonsSubMenu()
        {
            LinearGradientBrush myGradient = new LinearGradientBrush
            {
                StartPoint = new Point(0, 0),

                EndPoint = new Point(0, 1)
            };

            GradientStop fir = new GradientStop
            {
                Color = Color.FromRgb(255, 255, 255),

                Offset = 0.1
            };
            myGradient.GradientStops.Add(fir);

            myGradient.Opacity = 0.1;

            return myGradient;
        }

        private void BtnMenu_Click(object sender, RoutedEventArgs e)
        {
            btn_Home.Background = Brushes.Transparent;
            btn_Home.Foreground = new SolidColorBrush(Color.FromRgb(151, 152, 155));

            btn_Category.Background = Brushes.Transparent;
            btn_Category.Foreground = new SolidColorBrush(Color.FromRgb(151, 152, 155));

            btn_CategoryClose.Background = Brushes.Transparent;
            btn_CategoryClose.Foreground = new SolidColorBrush(Color.FromRgb(151, 152, 155));

            btn_News.Background = Brushes.Transparent;
            btn_News.Foreground = new SolidColorBrush(Color.FromRgb(151, 152, 155));

            btn_Info.Background = Brushes.Transparent;
            btn_Info.Foreground = new SolidColorBrush(Color.FromRgb(151, 152, 155));

            Button clickedButton = (Button)sender;
            clickedButton.Background = GradientButtonsMenu();
            clickedButton.Foreground = Brushes.White;

            if(clickedButton == btn_Home)
            {
                UserControlHome UscHome = new UserControlHome(_data);
                ContentPanel.Children.Add(UscHome);
            }


            if(clickedButton == btn_Category)
            {
                btn_CategoryClose.Visibility = Visibility.Visible;

                SubMenu.Visibility = Visibility.Visible;
                btn_Category.Visibility = Visibility.Collapsed;
                btn_CategoryClose.Background = GradientButtonsMenu();
                btn_CategoryClose.Foreground = Brushes.White;
            }
            else if (clickedButton == btn_CategoryClose)
            {
                btn_CategoryClose.Visibility = Visibility.Collapsed;
                btn_Category.Visibility = Visibility.Visible;
                btn_Category.Background = GradientButtonsMenu();
                btn_Category.Foreground = Brushes.White;
            }
            else
            {
                SubMenu.Visibility = Visibility.Collapsed;
                btn_CategoryClose.Visibility = Visibility.Collapsed;
                btn_Category.Visibility = Visibility.Visible;
                ClearSubMenuButtons();
            }

            if(clickedButton == btn_News)
            {
                var usc = new UserControlNews();
                ContentPanel.Children.Add(usc);
            }

            if (clickedButton == btn_Info)
            {
                var usc = new UserControlAbout();
                ContentPanel.Children.Add(usc);
            }
        }

        private void BtnSubMenu_Click(object sender, RoutedEventArgs e)
        {
            ClearSubMenuButtons();

            Button clickedButton = (Button)sender;

            clickedButton.Background = GradientButtonsSubMenu();
            clickedButton.Foreground = Brushes.White;

            if(clickedButton == btn_Breakfast)
            {
                ContentPanel.Children.Clear();
                UserControlHome UscHome = new UserControlHome(_data);

                UscHome.lb_Category.Content = "Śniadania";
                
                UscHome.ItemsList.ItemsSource = _data.BreakfastList;
                btn_CategoryClose.Visibility = Visibility.Collapsed;
                btn_Category.Visibility = Visibility.Visible;
                ContentPanel.Children.Add(UscHome);

            }
            else if (clickedButton == btn_Dinner)
            {
                ContentPanel.Children.Clear();
                UserControlHome UscHome = new UserControlHome(_data);

                UscHome.lb_Category.Content = "Obiady";

                UscHome.ItemsList.ItemsSource = _data.DinnerList;
                btn_CategoryClose.Visibility = Visibility.Collapsed;
                btn_Category.Visibility = Visibility.Visible;
                ContentPanel.Children.Add(UscHome);
            }
            else if (clickedButton == btn_Snack)
            {
                ContentPanel.Children.Clear();
                UserControlHome UscHome = new UserControlHome(_data);

                UscHome.lb_Category.Content = "Przekąski";

                UscHome.ItemsList.ItemsSource = _data.SnackList;
                btn_CategoryClose.Visibility = Visibility.Collapsed;
                btn_Category.Visibility = Visibility.Visible;
                ContentPanel.Children.Add(UscHome);
            }
            else if (clickedButton == btn_Dessert)
            {
                ContentPanel.Children.Clear();
                UserControlHome UscHome = new UserControlHome(_data);

                UscHome.lb_Category.Content = "Desery";

                UscHome.ItemsList.ItemsSource = _data.DessertList;
                btn_CategoryClose.Visibility = Visibility.Collapsed;
                btn_Category.Visibility = Visibility.Visible;
                ContentPanel.Children.Add(UscHome);
            }

        }

        public void ClearSubMenuButtons()
        {
            btn_Breakfast.Background = Brushes.Transparent;
            btn_Breakfast.Foreground = new SolidColorBrush(Color.FromRgb(251, 180, 166));

            btn_Dinner.Background = Brushes.Transparent;
            btn_Dinner.Foreground = new SolidColorBrush(Color.FromRgb(251, 180, 166));

            btn_Snack.Background = Brushes.Transparent;
            btn_Snack.Foreground = new SolidColorBrush(Color.FromRgb(251, 180, 166));

            btn_Dessert.Background = Brushes.Transparent;
            btn_Dessert.Foreground = new SolidColorBrush(Color.FromRgb(251, 180, 166));
        }

    }
}
