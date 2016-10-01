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

namespace CatalogDemo.Controls
{
    /// <summary>
    /// Interaction logic for FallbackImage.xaml
    /// </summary>
    public partial class FallbackImage : UserControl
    {
        #region Dependency property

        public static readonly DependencyProperty SourceProperty =
             DependencyProperty.Register("Source", typeof(string),
             typeof(FallbackImage), new PropertyMetadata(null, (s,a) =>
             {
                 FallbackImage im = s as FallbackImage;
                 if (im == null) return;

                 im.CurrentIm.Visibility = Visibility.Visible;
                 im.DefaultIm.Visibility = Visibility.Collapsed;

             }));


        public string Source
        {
            get { return (string)GetValue(SourceProperty); }
            set { SetValue(SourceProperty, value); }
        }

        public static readonly DependencyProperty FallbackSourceProperty =
             DependencyProperty.Register("FallbackSource", typeof(string),
             typeof(FallbackImage), new PropertyMetadata(null));


        public string FallbackSource
        {
            get { return (string)GetValue(SourceProperty); }
            set { SetValue(SourceProperty, value); }
        }


        #endregion

        public FallbackImage()
        {
            InitializeComponent();
        }


        private void CurrentIm_ImageFailed(object sender, ExceptionRoutedEventArgs e)
        {
            DefaultIm.Visibility = Visibility.Visible;
            CurrentIm.Visibility = Visibility.Collapsed;
        }
    }
}
