using AggregionCatalogApi;
using CatalogDemo.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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
using ViewModel;

namespace CatalogDemo
{
    
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        public void RaisePropertyChanged([CallerMemberName] string propertyName = null) =>
           PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        #endregion

        private CatalogsListViewModel _viewModel;
        public CatalogsListViewModel ViewModel
        {
            get { return _viewModel; }
            set
            {
                _viewModel = value;
                RaisePropertyChanged();
            }
        }

        public MainWindow()
        {
            InitializeComponent();
            this.Loaded += (s, a) => {
                ViewModel = new CatalogsListViewModel(new CatalogProvider(), new NotificationService());
            };
        }

    }
}
