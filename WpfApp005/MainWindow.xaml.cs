using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
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
using WpfApp005.Models;

namespace WpfApp005
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<string> list { get; set; } = new List<string>() { "Authors", "Themes", "Categories" };


        string? SelectedItem1;

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
        }




        public DbContextOptions<LibraryContext> GetOptions(string configname)
        {
            var builder = new ConfigurationBuilder();
            builder.SetBasePath(Directory.GetCurrentDirectory());
            builder.AddJsonFile("AppConfig.json");
            var config = builder.Build();
            string ConnectionString = config.GetConnectionString(configname)!;
            DbContextOptionsBuilder<LibraryContext> optionsbuilder = new DbContextOptionsBuilder<LibraryContext>();
            optionsbuilder.UseLazyLoadingProxies().UseSqlServer(ConnectionString);
            return optionsbuilder.Options;
        }


        private void combobox1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            listvv.ItemsSource = null;
            var combo = sender as ComboBox;
            if (combo is not null)
            {
                SelectedItem1 = combo.SelectedItem as string;
                using (var db = new LibraryContext(GetOptions("Connection1")))
                {
                    if (SelectedItem1 == "Authors")
                    {
                        combobox2.ItemsSource = db.Authors.ToList();
                    }
                    else if (SelectedItem1 == "Categories")
                    {
                        combobox2.ItemsSource = db.Categories.ToList();
                    }
                    else if (SelectedItem1 == "Themes")
                    {
                        combobox2.ItemsSource = db.Themes.ToList();
    
                    }
                }
            }

        }

        private void combobox2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var combo = sender as ComboBox;
            if (combo is not null && combo.SelectedItem is not null)
            {
                using (var db = new LibraryContext(GetOptions("Connection1")))
                {
                    if (SelectedItem1 == "Authors")
                    {
                        var author = db.Authors.ToList().FirstOrDefault(a => a.ToString() == combo.SelectedItem.ToString());
                        listvv.ItemsSource = author!.Books;
                    }
                    else if (SelectedItem1 == "Themes")
                    {
                        var theme = db.Themes.ToList().FirstOrDefault(t => t.ToString() == combo.SelectedItem.ToString());
                        listvv.ItemsSource=theme!.Books;
                    }
                    else if (SelectedItem1 == "Categories")
                    {
                        var category=db.Categories.ToList().First(c=>c.ToString() == combo.SelectedItem.ToString());
                        listvv.ItemsSource = category.Books;
                    }
                }
            }
        }
    }
}
