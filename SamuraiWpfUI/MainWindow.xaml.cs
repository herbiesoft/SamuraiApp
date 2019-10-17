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
using SamuraiApp.Data;
using SamuraiApp.Domain;

namespace SamuraiWpfUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly ConnectedData _data = new ConnectedData();
        private Samurai _currentSamurai;
        private bool _isListChanging;
        private bool _isLoading;
        private ObjectDataProvider _samuraiViewSource;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            _isLoading = true;
            //samuraiListBox.ItemSource = _data.SamuraisListInMemory();

            //System.Windows.Data.CollectionViewSource samuraiViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("samuraiViewSource")));
            // Load data by setting the CollectionViewSource.Source property:
            // samuraiViewSource.Source = [generic data source]
        }
    }
}
