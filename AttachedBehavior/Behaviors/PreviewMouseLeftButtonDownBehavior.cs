using System.Windows;
using System.Windows.Input;

namespace XioaAttachedBehavior.Behaviors
{
    public static class PreviewMouseLeftButtonDownBehavior
    {
        #region Command
        public static readonly DependencyProperty CommandProperty =
            DependencyProperty.RegisterAttached(
                "Command",
                typeof(ICommand),
                typeof(PreviewMouseLeftButtonDownBehavior),
                new PropertyMetadata(null, OnCommandChanged));

        public static ICommand GetCommand(DependencyObject obj)
            => (ICommand)obj.GetValue(CommandProperty);

        public static void SetCommand(DependencyObject obj, ICommand value)
            => obj.SetValue(CommandProperty, value);
        #endregion

        #region CommandParameter
        public static readonly DependencyProperty CommandParameterProperty =
            DependencyProperty.RegisterAttached(
                "CommandParameter",
                typeof(object),
                typeof(PreviewMouseLeftButtonDownBehavior),
                new PropertyMetadata(null));

        public static object GetCommandParameter(DependencyObject obj)
            => obj.GetValue(CommandParameterProperty);

        public static void SetCommandParameter(DependencyObject obj, object value)
            => obj.SetValue(CommandParameterProperty, value);
        #endregion

        private static void OnCommandChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is not UIElement element) return;

            if (e.OldValue != null)
            {
                element.PreviewMouseLeftButtonDown -= OnPreviewMouseLeftButtonDown;
            }

            if (e.NewValue != null)
            {
                element.PreviewMouseLeftButtonDown += OnPreviewMouseLeftButtonDown;
            }
        }

        private static void OnPreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (sender is not DependencyObject d) return;

            var command = GetCommand(d);
            var parameter = GetCommandParameter(d);

            if (command?.CanExecute(parameter) == true)
            {
                command.Execute(parameter);
            }
        }
    }
} 