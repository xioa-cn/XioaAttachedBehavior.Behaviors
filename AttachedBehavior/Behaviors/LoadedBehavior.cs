using System.Windows;
using System.Windows.Input;

namespace XioaAttachedBehavior.Behaviors
{
    public static class LoadedBehavior
    {
        public static readonly DependencyProperty CommandProperty =
            DependencyProperty.RegisterAttached(
                "Command",
                typeof(ICommand),
                typeof(LoadedBehavior),
                new PropertyMetadata(null, OnCommandChanged));

        public static ICommand GetCommand(DependencyObject obj)
            => (ICommand)obj.GetValue(CommandProperty);

        public static void SetCommand(DependencyObject obj, ICommand value)
            => obj.SetValue(CommandProperty, value);

        public static readonly DependencyProperty CommandParameterProperty =
            DependencyProperty.RegisterAttached(
                "CommandParameter",
                typeof(object),
                typeof(LoadedBehavior),
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
                element.Loaded -= OnLoaded;
            }

            if (e.NewValue != null)
            {
                // 如果元素已经加载完成，直接执行命令
                if (element.IsLoaded)
                {
                    ExecuteCommand(element);
                }
                // 否则等待Loaded事件
                else
                {
                    element.Loaded += OnLoaded;
                }
            }
        }

        private static void OnLoaded(object sender, RoutedEventArgs e)
        {
            if (sender is not FrameworkElement element) return;

            ExecuteCommand(element);
            
            // 注销事件，因为Loaded只会触发一次
            element.Loaded -= OnLoaded;
        }

        private static void ExecuteCommand(FrameworkElement element)
        {
            var command = GetCommand(element);
            var parameter = GetCommandParameter(element);

            if (command?.CanExecute(parameter) == true)
            {
                command.Execute(parameter);
            }
        }
    }
} 