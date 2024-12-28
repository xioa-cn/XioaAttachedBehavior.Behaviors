using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;

namespace XioaAttachedBehavior.Behaviors
{
    public static class PreviewKeyUpBehavior
    {
        public static readonly DependencyProperty CommandProperty =
            DependencyProperty.RegisterAttached(
                "Command",
                typeof(ICommand),
                typeof(PreviewKeyUpBehavior),
                new PropertyMetadata(null, OnCommandChanged));

        public static ICommand GetCommand(DependencyObject obj)
            => (ICommand)obj.GetValue(CommandProperty);

        public static void SetCommand(DependencyObject obj, ICommand value)
            => obj.SetValue(CommandProperty, value);

        public static readonly DependencyProperty CommandParameterProperty =
            DependencyProperty.RegisterAttached(
                "CommandParameter",
                typeof(object),
                typeof(PreviewKeyUpBehavior),
                new PropertyMetadata(null));

        public static object GetCommandParameter(DependencyObject obj)
            => obj.GetValue(CommandParameterProperty);

        public static void SetCommandParameter(DependencyObject obj, object value)
            => obj.SetValue(CommandParameterProperty, value);

        public static readonly DependencyProperty KeyProperty =
            DependencyProperty.RegisterAttached(
                "Key",
                typeof(Key),
                typeof(PreviewKeyUpBehavior),
                new PropertyMetadata(Key.None));

        public static Key GetKey(DependencyObject obj)
            => (Key)obj.GetValue(KeyProperty);

        public static void SetKey(DependencyObject obj, Key value)
            => obj.SetValue(KeyProperty, value);

        private static void OnCommandChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is not UIElement element) return;

            if (e.OldValue != null)
            {
                element.PreviewKeyUp -= OnPreviewKeyUp;
            }

            if (e.NewValue != null)
            {
                element.PreviewKeyUp += OnPreviewKeyUp;
            }
        }

        private static void OnPreviewKeyUp(object sender, KeyEventArgs e)
        {
            if (sender is not DependencyObject d) return;

            var command = GetCommand(d);
            var parameter = GetCommandParameter(d);
            var key = GetKey(d);

            // 如果指定了Key且不匹配，则不执行命令
            if (key != Key.None && e.Key != key) return;

            // 如果没有指定参数，则使用按键信息作为参数
            if (parameter == null)
            {
                parameter = e;
            }

            if (command?.CanExecute(parameter) == true)
            {
                command.Execute(parameter);
                
                // 只有在不是文本输入控件时，才标记事件为已处理
                if (!(sender is TextBox || sender is PasswordBox || sender is RichTextBox))
                {
                    e.Handled = true;
                }
            }
        }
    }
}