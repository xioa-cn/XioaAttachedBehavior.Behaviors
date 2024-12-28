using System.Windows;
using System.Windows.Input;

namespace XioaAttachedBehavior.Behaviors
{
    public static class MouseRightButtonUpBehavior
    {
        public static readonly DependencyProperty CommandProperty =
            DependencyProperty.RegisterAttached(
                "Command",
                typeof(ICommand),
                typeof(MouseRightButtonUpBehavior),
                new PropertyMetadata(null, OnCommandChanged));

        public static ICommand GetCommand(DependencyObject obj)
            => (ICommand)obj.GetValue(CommandProperty);

        public static void SetCommand(DependencyObject obj, ICommand value)
            => obj.SetValue(CommandProperty, value);

        public static readonly DependencyProperty CommandParameterProperty =
            DependencyProperty.RegisterAttached(
                "CommandParameter",
                typeof(object),
                typeof(MouseRightButtonUpBehavior),
                new PropertyMetadata(null));

        public static object GetCommandParameter(DependencyObject obj)
            => obj.GetValue(CommandParameterProperty);

        public static void SetCommandParameter(DependencyObject obj, object value)
            => obj.SetValue(CommandParameterProperty, value);

        private static void OnCommandChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is not UIElement element) return;

            if (e.OldValue != null)
            {
                element.PreviewMouseRightButtonUp -= OnMouseRightButtonUp;
            }

            if (e.NewValue != null)
            {
                element.PreviewMouseRightButtonUp += OnMouseRightButtonUp;
            }
        }

        private static void OnMouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (sender is not DependencyObject d) return;

            var command = GetCommand(d);
            var parameter = GetCommandParameter(d);

            if (command?.CanExecute(parameter) == true)
            {
                command.Execute(parameter);
                e.Handled = true;
            }
        }
    }
} 