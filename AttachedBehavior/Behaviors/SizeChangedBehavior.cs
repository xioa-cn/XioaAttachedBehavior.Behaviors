using System.Windows;
using System.Windows.Input;

namespace XioaAttachedBehavior.Behaviors
{
    public static class SizeChangedBehavior
    {
        public static readonly DependencyProperty CommandProperty =
            DependencyProperty.RegisterAttached(
                "Command",
                typeof(ICommand),
                typeof(SizeChangedBehavior),
                new PropertyMetadata(null, OnCommandChanged));

        public static ICommand GetCommand(DependencyObject obj)
            => (ICommand)obj.GetValue(CommandProperty);

        public static void SetCommand(DependencyObject obj, ICommand value)
            => obj.SetValue(CommandProperty, value);

        public static readonly DependencyProperty CommandParameterProperty =
            DependencyProperty.RegisterAttached(
                "CommandParameter",
                typeof(object),
                typeof(SizeChangedBehavior),
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
                element.SizeChanged -= OnSizeChanged;
            }

            if (e.NewValue != null)
            {
                element.SizeChanged += OnSizeChanged;
            }
        }

        private static void OnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (sender is not FrameworkElement element) return;

            var command = GetCommand(element);
            var parameter = GetCommandParameter(element);

            // 如果没有指定参数，则使用尺寸变化信息作为参数
            if (parameter == null)
            {
                parameter = e;
            }

            if (command?.CanExecute(parameter) == true)
            {
                command.Execute(parameter);
            }
        }
    }
} 