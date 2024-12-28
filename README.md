# WPF 附加行为库

这是一个用于 WPF 的附加行为（Attached Behavior）库，提供了一系列可重用的行为来处理常见的 UI 交互场景。

## 功能特性

### 鼠标事件行为
- `MouseLeftButtonDownBehavior` - 鼠标左键按下
- `PreviewMouseLeftButtonDownBehavior` - 鼠标左键按下（预览）
- `MouseLeftButtonUpBehavior` - 鼠标左键释放
- `PreviewMouseLeftButtonUpBehavior` - 鼠标左键释放（预览）
- `MouseRightButtonDownBehavior` - 鼠标右键按下
- `PreviewMouseRightButtonDownBehavior` - 鼠标右键按下（预览）
- `MouseRightButtonUpBehavior` - 鼠标右键释放
- `PreviewMouseRightButtonUpBehavior` - 鼠标右键释放（预览）
- `MouseEnterBehavior` - 鼠标进入
- `MouseLeaveBehavior` - 鼠标离开
- `MouseMoveBehavior` - 鼠标移动
- `PreviewMouseMoveBehavior` - 鼠标移动（预览）

### 键盘事件行为
- `KeyDownBehavior` - 键盘按下
- `PreviewKeyDownBehavior` - 键盘按下（预览）
- `KeyUpBehavior` - 键盘释放
- `PreviewKeyUpBehavior` - 键盘释放（预览）

### 控件事件行为
- `ClickBehavior` - 点击事件
- `LoadedBehavior` - 控件加载
- `UnloadedBehavior` - 控件卸载
- `SizeChangedBehavior` - 尺寸改变

## 使用方法

1. 在 XAML 中添加命名空间引用：
```xaml
xmlns:behaviors="clr-namespace:XioaAttachedBehavior.Behaviors;assembly=AttachedBehavior"
```

2. 在控件上使用附加行为：
```xaml
<!-- 基本用法 -->
<Button behaviors:MouseLeftButtonDownBehavior.Command="{Binding MyCommand}"
        behaviors:MouseLeftButtonDownBehavior.CommandParameter="参数"/>

<!-- 键盘事件指定按键 -->
<TextBox behaviors:KeyDownBehavior.Command="{Binding MyCommand}"
         behaviors:KeyDownBehavior.Key="Enter"/>

<!-- 多个行为组合使用 -->
<Grid behaviors:LoadedBehavior.Command="{Binding InitCommand}"
      behaviors:UnloadedBehavior.Command="{Binding CleanupCommand}"
      behaviors:SizeChangedBehavior.Command="{Binding ResizeCommand}">
```

## 特性说明

1. 所有行为都支持：
   - Command 绑定
   - CommandParameter 参数传递
   - 自动的事件注册和注销

2. 特殊功能：
   - 键盘事件支持指定具体按键
   - 鼠标移动事件自动传递位置信息
   - 尺寸变化事件包含完整的变化信息
   - 文本输入控件的特殊处理，确保不影响正常输入

## 示例代码

```xaml
<Window x:Class="WPFAttachedBehavior.MainWindow"
        xmlns:behaviors="clr-namespace:WPFAttachedBehavior.Behaviors;assembly=WPFAttachedBehavior"
        Title="WPF 附加行为示例"
        behaviors:LoadedBehavior.Command="{Binding InitCommand}">
    
    <StackPanel>
        <Button Content="点击测试"
                behaviors:MouseLeftButtonDownBehavior.Command="{Binding MyCommand}"
                behaviors:MouseLeftButtonDownBehavior.CommandParameter="Button1"/>
        
        <TextBox behaviors:KeyDownBehavior.Command="{Binding MyCommand}"
                 behaviors:KeyDownBehavior.Key="Enter"
                 behaviors:KeyDownBehavior.CommandParameter="TextBox1"/>
    </StackPanel>
</Window>
```

```csharp
public class MainViewModel : ObservableObject
{
    public MainViewModel()
    {
        MyCommand = new RelayCommand<object>(OnMyCommand);
    }

    public ICommand MyCommand { get; }

    private void OnMyCommand(object parameter)
    {
        MessageBox.Show($"命令执行，参数：{parameter}");
    }
}
```

## 注意事项

1. 事件处理：
   - Preview 事件在隧道阶段触发
   - 普通事件在冒泡阶段触发
   - 某些事件（如 Loaded）只会触发一次

2. 性能考虑：
   - 频繁触发的事件（如 MouseMove、SizeChanged）建议添加防抖处理
   - 不使用的行为及时注销，避免内存泄漏
