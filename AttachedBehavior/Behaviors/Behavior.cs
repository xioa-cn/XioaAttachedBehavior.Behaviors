using System;
using System.Windows;

namespace XioaAttachedBehavior.Behaviors
{
    /// <summary>
    /// 行为基类
    /// </summary>
    public abstract class Behavior
    {
        /// <summary>
        /// 注册一个附加行为
        /// </summary>
        protected static DependencyProperty RegisterProperty<TProperty>(
            string propertyName,
            Type ownerType,
            TProperty defaultValue = default,
            PropertyChangedCallback propertyChanged = null)
        {
            return DependencyProperty.RegisterAttached(
                propertyName,
                typeof(TProperty),
                ownerType,
                new PropertyMetadata(defaultValue, propertyChanged));
        }

        /// <summary>
        /// 获取附加属性值
        /// </summary>
        protected static TProperty GetValue<TProperty>(DependencyObject obj, DependencyProperty property)
        {
            return (TProperty)obj.GetValue(property);
        }

        /// <summary>
        /// 设置附加属性值
        /// </summary>
        protected static void SetValue<TProperty>(DependencyObject obj, DependencyProperty property, TProperty value)
        {
            obj.SetValue(property, value);
        }
    }
} 