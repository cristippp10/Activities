using System;
using System.Activities.Presentation.Model;
using System.Activities.Presentation.View;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Shapes;
namespace FtpActivities.Design
{
	public partial class ComboboxControl : UserControl, IComponentConnector
	{
		public static readonly DependencyProperty ModelItemProperty = DependencyProperty.Register("ModelItem", typeof(ModelItem), typeof(ComboboxControl));
		public static readonly DependencyProperty SelectedItemProperty = DependencyProperty.Register("SelectedItem", typeof(ModelItem), typeof(ComboboxControl));
		public static readonly DependencyProperty PropertyNameProperty = DependencyProperty.Register("PropertyName", typeof(string), typeof(ComboboxControl));
		public static readonly DependencyProperty ItemsSourceProperty = DependencyProperty.Register("ItemsSource", typeof(List<string>), typeof(ComboboxControl));
		public static readonly DependencyProperty HintTextProperty = DependencyProperty.Register("HintText", typeof(string), typeof(ComboboxControl), new PropertyMetadata("Text must be qouted"));
		public static readonly RoutedEvent SelectionChangedEvent = EventManager.RegisterRoutedEvent("SelectionChanged", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(ComboboxControl));
        //internal ComboboxControl Combobox;
        //internal LinkPropertyControl TextSetter;
        //internal Path Arrow;
        //internal ComboBox PropertiesComboBox;
        //internal ExpressionTextBox ComboboxInputBox;
        //private bool _contentLoaded;
		public event RoutedEventHandler SelectionChanged
		{
			add
			{
				base.AddHandler(ComboboxControl.SelectionChangedEvent, value);
			}
			remove
			{
				base.RemoveHandler(ComboboxControl.SelectionChangedEvent, value);
			}
		}
		public ModelItem ModelItem
		{
			get
			{
				return base.GetValue(ComboboxControl.ModelItemProperty) as ModelItem;
			}
			set
			{
				base.SetValue(ComboboxControl.ModelItemProperty, value);
			}
		}
		public ModelItem SelectedItem
		{
			get
			{
				return base.GetValue(ComboboxControl.SelectedItemProperty) as ModelItem;
			}
			set
			{
				base.SetValue(ComboboxControl.SelectedItemProperty, value);
			}
		}
		public string PropertyName
		{
			get
			{
				return base.GetValue(ComboboxControl.PropertyNameProperty) as string;
			}
			set
			{
				base.SetValue(ComboboxControl.PropertyNameProperty, value);
			}
		}
		public List<string> ItemsSource
		{
			get
			{
				return base.GetValue(ComboboxControl.ItemsSourceProperty) as List<string>;
			}
			set
			{
				base.SetValue(ComboboxControl.ItemsSourceProperty, value);
			}
		}
		public string HintText
		{
			get
			{
				return base.GetValue(ComboboxControl.HintTextProperty) as string;
			}
			set
			{
				base.SetValue(ComboboxControl.HintTextProperty, value);
			}
		}
		public ComboboxControl()
		{
			base.Loaded += delegate(object s, RoutedEventArgs e)
			{
				this.ComboboxInputBox.HintText = this.HintText;
				this.TextSetter.PropertyName = this.PropertyName;
			};
			this.InitializeComponent();
		}
		private void PropertiesComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (e.AddedItems.Count != 0)
			{
				base.RaiseEvent(new RoutedEventArgs(ComboboxControl.SelectionChangedEvent)
				{
					Source = this.PropertiesComboBox
				});
				this.PropertiesComboBox.SelectedIndex = -1;
			}
		}
		private void Button_Click(object sender, RoutedEventArgs e)
		{
			this.PropertiesComboBox.IsDropDownOpen = true;
		}
        //[GeneratedCode("PresentationBuildTasks", "4.0.0.0"), DebuggerNonUserCode]
        //public void InitializeComponent()
        //{
        //    if (this._contentLoaded)
        //    {
        //        return;
        //    }
        //    this._contentLoaded = true;
        //    Uri resourceLocator = new Uri("/FtpActivities.Design;component/controls/comboboxcontrol.xaml", UriKind.Relative);
        //    Application.LoadComponent(this, resourceLocator);
        //}
        //[GeneratedCode("PresentationBuildTasks", "4.0.0.0"), DebuggerNonUserCode]
        //internal Delegate _CreateDelegate(Type delegateType, string handler)
        //{
        //    return Delegate.CreateDelegate(delegateType, this, handler);
        //}
        //[GeneratedCode("PresentationBuildTasks", "4.0.0.0"), EditorBrowsable(EditorBrowsableState.Never), DebuggerNonUserCode]
        //void IComponentConnector.Connect(int connectionId, object target)
        //{
        //    switch (connectionId)
        //    {
        //    case 1:
        //        this.Combobox = (ComboboxControl)target;
        //        return;
        //    case 2:
        //        this.TextSetter = (LinkPropertyControl)target;
        //        return;
        //    case 3:
        //        ((Button)target).Click += new RoutedEventHandler(this.Button_Click);
        //        return;
        //    case 4:
        //        this.Arrow = (Path)target;
        //        return;
        //    case 5:
        //        this.PropertiesComboBox = (ComboBox)target;
        //        this.PropertiesComboBox.SelectionChanged += new SelectionChangedEventHandler(this.PropertiesComboBox_SelectionChanged);
        //        return;
        //    case 6:
        //        this.ComboboxInputBox = (ExpressionTextBox)target;
        //        return;
        //    default:
        //        this._contentLoaded = true;
        //        return;
        //    }
        //}
	}
}
