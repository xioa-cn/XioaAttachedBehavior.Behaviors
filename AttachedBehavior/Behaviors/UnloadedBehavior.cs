using System.Windows;
using System.Windows.Input;

namespace XioaAttachedBehavior.Behaviors
{
    public static class UnloadedBehavior
    {
        public static readonly DependencyProperty CommandProperty =
            DependencyProperty.RegisterAttached(
                "Command",
                typeof(ICommand),
                typeof(UnloadedBehavior),
                new PropertyMetadata(null, OnCommandChanged));

        public static ICommand GetCommand(DependencyObject obj)
            => (ICommand)obj.GetValue(CommandProperty);

        public static void SetCommand(DependencyObject obj, ICommand value)
            => obj.SetValue(CommandProperty, value);

        public static readonly DependencyProperty CommandParameterProperty =
            DependencyProperty.RegisterAttached(
                "CommandParameter",
                typeof(object),
                typeof(UnloadedBehavior),
                new PropertyMetadata(null));

        public static object GetCommandParameter(DependencyObject obj)
            => obj.GetValue(CommandParameterProperty);

        public static void SetCommandParameter(DependencyObject obj, object value)
            => obj.SetValue(CommandParameterProperty, value);

        private static void OnCommandChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is not FrameworkElement element) return;

            if (e.OldValue != null)
            {
                element.Unloaded -= OnUnloaded;
            }

            if (e.NewValue != null)
            {
                element.Unloaded += OnUnloaded;
            }
        }

        private static void OnUnloaded(object sender, RoutedEventArgs e)
        {
            if (sender is not FrameworkElement element) return;

            var command = GetCommand(element);
            var parameter = GetCommandParameter(element);

            if (command?.CanExecute(parameter) == true)
            {
                command.Execute(parameter);
            }

            // 注销事件，防止内存泄漏
            element.Unloaded -= OnUnloaded;
        }
    }
} 