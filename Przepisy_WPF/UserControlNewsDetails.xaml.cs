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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Przepisy_WPF
{
    /// <summary>
    /// Logika interakcji dla klasy UserControlNewsDetails.xaml
    /// </summary>
    public partial class UserControlNewsDetails : UserControl
    {
        public object UcNews { get; set; }
        public List<News> NewsDetails { get; set; }
        public UserControlNewsDetails(object uc_news, List<News> newsDetails)
        {
            InitializeComponent();
            UcNews = uc_news;
            NewsDetails = newsDetails;

            HeaderIC.ItemsSource = NewsDetails;
            DetailsIC.ItemsSource = newsDetails;
        }

        private void BtnCloseNews_Click(object sender, RoutedEventArgs e)
        {
            this.Content = UcNews;
        }
    }
}
