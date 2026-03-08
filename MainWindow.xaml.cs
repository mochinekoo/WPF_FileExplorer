using System.Drawing.Configuration;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPF_FileExplorer
{

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainViewModel();
        }

        private void onFilePathKeyDown(object sender, KeyEventArgs e) {
            MainViewModel mainViewModel = (MainViewModel) DataContext;
            string path = mainViewModel.FilePath;

            if (e.Key == Key.Enter) {
                FileWrapPanel.Children.Clear();
                if (path == null) return;

                string[] files;
                try {
                    files = Directory.GetFiles(path);
                } catch (System.IO.DirectoryNotFoundException) {
                    MessageBox.Show("指定したファイルは見つかりませんでした", "エラー", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                
               if (files == null) return;
               if (files.Length == 0) return;
                foreach (string filePath in files) {
                    FileInfo fileInfo = new FileInfo(filePath);

                    // ファイルを表示する
                    TextBlock textBlock = new TextBlock();
                    textBlock.Text = fileInfo.Name;
                    textBlock.HorizontalAlignment = HorizontalAlignment.Center;

                    Image image = new Image();
                    System.Drawing.Icon icon = System.Drawing.Icon.ExtractAssociatedIcon(filePath);
                    image.Source = Imaging.CreateBitmapSourceFromHIcon(
                        icon.Handle,
                        Int32Rect.Empty,
                        BitmapSizeOptions.FromEmptyOptions());
                    image.Width = 32;
                    image.Height = 32;

                    StackPanel stackPanel = new StackPanel();
                    stackPanel.Orientation = Orientation.Vertical;
                    stackPanel.HorizontalAlignment = HorizontalAlignment.Center;
                    stackPanel.Children.Add(image);
                    stackPanel.Children.Add(textBlock);

                    Button button = new Button();
                    button.Background = Brushes.Transparent;
                    button.Margin = new Thickness(0, 0, 5, 0);
                    button.Content = stackPanel;
                    FileWrapPanel.Children.Add(button);
                }

            }
        }
    }
}
