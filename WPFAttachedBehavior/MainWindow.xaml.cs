using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using WPFAttachedBehavior.ViewModels;
using WPFAttachedBehavior.Views;

namespace WPFAttachedBehavior
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void PreviewMouseLeftButtonDownBehavior_Window(object sender, RoutedEventArgs e)
        {
            PreviewMouseLeftButtonDownBehaviorWindow mouseLeftButtonDownBehaviorWindow = new PreviewMouseLeftButtonDownBehaviorWindow();
            mouseLeftButtonDownBehaviorWindow.ShowDialog();
        }

        private void MouseLeftButtonDownBehavior_Window(object sender, RoutedEventArgs e)
        {
            MouseLeftButtonDownBehaviorWindow mouseLeftButtonDownBehaviorWindowMouse = new MouseLeftButtonDownBehaviorWindow();
            mouseLeftButtonDownBehaviorWindowMouse.ShowDialog();
        }

        private void PreviewMouseLeftButtonUpBehavior_Window(object sender, RoutedEventArgs e)
        {
            PreviewMouseLeftButtonUpBehaviorWIndow previewMouseLeftButtonUpBehaviorWIndow = new PreviewMouseLeftButtonUpBehaviorWIndow();
            previewMouseLeftButtonUpBehaviorWIndow.ShowDialog();
        }

        private void MouseLeftButtonUp_Window(object sender, RoutedEventArgs e)
        {
            MouseLeftButtonUpWindow mouseLeftButtonUpWindow = new MouseLeftButtonUpWindow();
            mouseLeftButtonUpWindow.ShowDialog();
        }

        private void PreviewMouseRightButtonDown_Window(object sender, RoutedEventArgs e)
        {
            PreviewMouseRightButtonDownWindow previewMouseRightButtonDownWindow = new PreviewMouseRightButtonDownWindow()
            {
                DataContext = new MainViewModel()
            };
            previewMouseRightButtonDownWindow.ShowDialog();
        }

        private void MouseRightButtonDownBehavior_Window(object sender, RoutedEventArgs e)
        {
            MouseRightButtonDownBehaviorWindow mouseRightButtonDownBehaviorWindow = new MouseRightButtonDownBehaviorWindow()
            {
                DataContext = new MainViewModel()
            };
            mouseRightButtonDownBehaviorWindow.ShowDialog();
        }

        private void PreviewMouseRightButtonUpBehavior_Window(object sender, RoutedEventArgs e)
        {
            PreviewMouseRightButtonUpBehaviorWindow previewMouseRightButtonUpBehaviorWindow = new PreviewMouseRightButtonUpBehaviorWindow()
            { DataContext = new MainViewModel() };
            previewMouseRightButtonUpBehaviorWindow.ShowDialog();
        }

        private void MouseRightButtonUpBehavior_Window(object sender, RoutedEventArgs e)
        {
            MouseRightButtonUpBehaviorWindow mouseRightButtonUpBehaviorWindow = new MouseRightButtonUpBehaviorWindow()
            { DataContext = new MainViewModel() };
            mouseRightButtonUpBehaviorWindow.ShowDialog();
        }

        private void MouseEnterBehavior_Window(object sender, RoutedEventArgs e)
        {
            MouseEnterBehaviorWindow mouseEnterBehaviorWindow = new MouseEnterBehaviorWindow()
            {
                DataContext = new EnterViewModel()
            };
            mouseEnterBehaviorWindow.ShowDialog();
        }

        private void MouseLeaveBehavior_Window(object sender, RoutedEventArgs e)
        {
            MouseLeaveBehaviorWindow mouseLeaveBehaviorWindow = new MouseLeaveBehaviorWindow()
            {
                DataContext = new EnterViewModel()
            };
            mouseLeaveBehaviorWindow.ShowDialog();

        }

        private void PreviewMouseMoveBehavior_Window(object sender, RoutedEventArgs e)
        {
            PreviewMouseMoveBehaviorWindow previewMouseMoveBehaviorWindow = new PreviewMouseMoveBehaviorWindow()
            {
                DataContext = new EnterViewModel()
            };
            previewMouseMoveBehaviorWindow.ShowDialog();
        }

        private void MouseMoveBehavior_Window(object sender, RoutedEventArgs e)
        {
            MouseMoveBehaviorWindow mouseMoveBehaviorWindow = new MouseMoveBehaviorWindow()
            {
                DataContext = new EnterViewModel()
            };
            mouseMoveBehaviorWindow.ShowDialog();
        }

        private void KeyBehavior_Window(object sender, RoutedEventArgs e)
        {
            KeyBehaviorWindow keyBehaviorWindow = new KeyBehaviorWindow()
            {
                DataContext = new EnterViewModel()
            };
            keyBehaviorWindow.ShowDialog();
        }

        private void ClickBehavior_Window(object sender, RoutedEventArgs e)
        {
            ClickBehaviorWindow clickBehaviorWindow = new ClickBehaviorWindow()
            {
                DataContext = new EnterViewModel()
            };
            clickBehaviorWindow.ShowDialog();
        }
    }
}