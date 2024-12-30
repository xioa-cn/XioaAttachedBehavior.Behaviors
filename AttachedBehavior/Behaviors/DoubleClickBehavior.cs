using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace XioaAttachedBehavior.Behaviors
{
    public static class DoubleClickBehavior
    {
        private static readonly Dictionary<UIElement, DispatcherTimer> _timers 
            = new Dictionary<UIElement, DispatcherTimer>();
        
        private static readonly Dictionary<UIElement, int> _clickCounts 
            = new Dictionary<UIElement, int>();

        #region Command
        public static readonly DependencyProperty CommandProperty =
            DependencyProperty.RegisterAttached(
                "Command",
                typeof(ICommand),
                typeof(DoubleClickBehavior),
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
                typeof(DoubleClickBehavior),
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
                element.PreviewMouseLeftButtonDown -= Element_PreviewMouseLeftButtonDown;
                CleanupElement(element);
            }

            if (e.NewValue != null)
            {
                element.PreviewMouseLeftButtonDown += Element_PreviewMouseLeftButtonDown;
            }
        }

        private static void Element_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (sender is not UIElement element) return;

            // 增加点击计数
            if (!_clickCounts.ContainsKey(element))
            {
                _clickCounts[element] = 0;
            }
            _clickCounts[element]++;

            if (_clickCounts[element] == 1)
            {
                // 第一次点击，启动计时器
                StartTimer(element);
            }
            else if (_clickCounts[element] == 2)
            {
                // 第二次点击，执行命令
                ExecuteCommand(element, e);
                CleanupElement(element);
            }
        }

        private static void StartTimer(UIElement element)
        {
            // 清理现有计时器
            if (_timers.ContainsKey(element))
            {
                _timers[element].Stop();
                _timers.Remove(element);
            }

            // 创建新计时器
            var timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromMilliseconds(300) // 双击间隔时间
            };

            timer.Tick += (s, e) =>
            {
                timer.Stop();
                CleanupElement(element);
            };

            _timers[element] = timer;
            timer.Start();
        }

        private static void ExecuteCommand(UIElement element, MouseButtonEventArgs e)
        {
            var command = GetCommand(element);
            var parameter = GetCommandParameter(element) ?? e;

            if (command?.CanExecute(parameter) == true)
            {
                command.Execute(parameter);
                e.Handled = true;
            }
        }

        private static void CleanupElement(UIElement element)
        {
            // 清理计时器
            if (_timers.ContainsKey(element))
            {
                _timers[element].Stop();
                _timers.Remove(element);
            }

            // 重置点击计数
            _clickCounts.Remove(element);
        }
    }
} 