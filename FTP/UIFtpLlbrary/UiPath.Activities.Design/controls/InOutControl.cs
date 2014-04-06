using Microsoft.CSharp.RuntimeBinder;
using System;
using System.Activities;
using System.Activities.Presentation.Model;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Markup;
namespace FtpActivities.Design
{
	public partial class InOutControl : UserControl, IComponentConnector
	{
        public interface IContainerActivity
        {
            Activity Body
            {
                get;
                set;
            }
        }
        public interface IScreenshotActivity
	    {
		    InArgument<Image> Image
		    {
			    get;
			    set;
		    }
	    }

		[CompilerGenerated]
		private static class OutputIsOfTypeStatic
		{
			public static CallSite<Func<CallSite, object, bool>> p__Site1;
			public static CallSite<Func<CallSite, object, object, object>> p__Site2;
			public static CallSite<Func<CallSite, object, bool>> p__Site3;
			public static CallSite<Func<CallSite, object, object, object>> p__Site4;
			public static CallSite<Func<CallSite, object, Type, object>> p__Site5;
			public static CallSite<Func<CallSite, object, object>> p__Site6;
		}
		[CompilerGenerated]
		private static class FromContainer_ClickStatic
		{
			public static CallSite<Func<CallSite, object, bool>> p__Site8;
			public static CallSite<Func<CallSite, object, Type, object>> p__Site9;
			public static CallSite<Func<CallSite, object, object>> p__Sitea;
			public static CallSite<Func<CallSite, object, bool>> p__Siteb;
			public static CallSite<Func<CallSite, object, object, object>> p__Sitec;
			public static CallSite<Func<CallSite, object, object>> p__Sited;
			public static CallSite<Action<CallSite, Type, ModelProperty, object>> p__Sitee;
			public static CallSite<Func<CallSite, object, object>> p__Sitef;
		}
		private const string NoMatchingVariable = "No matching variable";
		public static readonly DependencyProperty ModelItemProperty = DependencyProperty.Register("ModelItem", typeof(ModelItem), typeof(InOutControl), new FrameworkPropertyMetadata(new PropertyChangedCallback(InOutControl.OnModelItemPropertyChanged)));
		public static readonly DependencyProperty FromLinkButtonProperty = DependencyProperty.Register("FromLinkButton", typeof(bool), typeof(InOutControl));
		public static readonly DependencyProperty VariableTypeProperty = DependencyProperty.Register("VariableType", typeof(Type), typeof(InOutControl));
		public static readonly DependencyProperty PropertyNameProperty = DependencyProperty.Register("PropertyName", typeof(string), typeof(InOutControl));
		public static readonly DependencyProperty IsContainerProperty = DependencyProperty.Register("IsContainer", typeof(bool), typeof(InOutControl));
		public static readonly DependencyProperty LinkContentProperty = DependencyProperty.Register("LinkContent", typeof(string), typeof(InOutControl), new FrameworkPropertyMetadata(new PropertyChangedCallback(InOutControl.OnLinkContentPropertyChanged)));
		public static readonly DependencyProperty OtherMenusProperty = DependencyProperty.Register("OtherMenus", typeof(ControlTemplate), typeof(InOutControl), new PropertyMetadata(null, new PropertyChangedCallback(InOutControl.OnOtherMenusPropertyChanged)));
		public static readonly RoutedEvent SelectEvent = EventManager.RegisterRoutedEvent("Select", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(InOutControl));
		public static readonly RoutedEvent OptionsEvent = EventManager.RegisterRoutedEvent("Options", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(InOutControl));
		private string menuItemLabel = "Use a value stored in a variable";
		public bool HasActivities;
        //internal InOutControl InOutCtrl;
        //internal Grid GridInOut;
        //internal Button SelectActivityButton;
        //internal ContextMenu ContextMenuActivities;
        //internal MenuItem ActivitiesMenu;
        //internal CollectionContainer MatchingVariablesContainer;
        //internal Separator VariableSeparator;
        //internal CollectionContainer MatchingObjectVariablesContainer;
        //internal Separator ReserImageSeparator;
        //internal MenuItem OptionsMenu;
        //private bool _contentLoaded;
		public event RoutedEventHandler Select
		{
			add
			{
				base.AddHandler(InOutControl.SelectEvent, value);
			}
			remove
			{
				base.RemoveHandler(InOutControl.SelectEvent, value);
			}
		}
		public event RoutedEventHandler Options
		{
			add
			{
				base.AddHandler(InOutControl.OptionsEvent, value);
			}
			remove
			{
				base.RemoveHandler(InOutControl.OptionsEvent, value);
			}
		}
		public ModelItem ModelItem
		{
			get
			{
				return base.GetValue(InOutControl.ModelItemProperty) as ModelItem;
			}
			set
			{
				base.SetValue(InOutControl.ModelItemProperty, value);
			}
		}
		public bool FromLinkButton
		{
			get
			{
				return bool.Parse(base.GetValue(InOutControl.FromLinkButtonProperty).ToString());
			}
			set
			{
				base.SetValue(InOutControl.FromLinkButtonProperty, value);
			}
		}
		public Type VariableType
		{
			get
			{
				return base.GetValue(InOutControl.VariableTypeProperty) as Type;
			}
			set
			{
				base.SetValue(InOutControl.VariableTypeProperty, value);
			}
		}
		public string PropertyName
		{
			get
			{
				return base.GetValue(InOutControl.PropertyNameProperty) as string;
			}
			set
			{
				base.SetValue(InOutControl.PropertyNameProperty, value);
			}
		}
		public bool IsContainer
		{
			get
			{
				return bool.Parse(base.GetValue(InOutControl.IsContainerProperty).ToString());
			}
			set
			{
				base.SetValue(InOutControl.IsContainerProperty, value);
			}
		}
		public string LinkContent
		{
			get
			{
				return base.GetValue(InOutControl.LinkContentProperty) as string;
			}
			set
			{
				base.SetValue(InOutControl.LinkContentProperty, value);
			}
		}
		public ControlTemplate OtherMenus
		{
			get
			{
				return base.GetValue(InOutControl.OtherMenusProperty) as ControlTemplate;
			}
			set
			{
				base.SetValue(InOutControl.OtherMenusProperty, value);
			}
		}
		public string MenuItemLabel
		{
			get
			{
				return this.menuItemLabel;
			}
			set
			{
				this.menuItemLabel = value;
				this.ActivitiesMenu.Header = value;
			}
		}
		private static void OnModelItemPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			InOutControl inOutControl = (InOutControl)d;
			inOutControl.IsContainer = typeof(IContainerActivity).IsAssignableFrom((e.NewValue as ModelItem).ItemType);
		}
		private static void OnLinkContentPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			InOutControl inOutControl = (InOutControl)d;
			inOutControl.SelectActivityButton.Content = e.NewValue;
		}
		private static void OnOtherMenusPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			if (e.NewValue == DependencyProperty.UnsetValue)
			{
				return;
			}
			InOutControl inOutControl = (InOutControl)d;
			inOutControl.OptionsMenu.Template = (ControlTemplate)e.NewValue;
			if (e.NewValue == null)
			{
				inOutControl.OptionsMenu.Visibility = Visibility.Hidden;
				return;
			}
			inOutControl.OptionsMenu.Visibility = Visibility.Visible;
		}
		private void RaiseSelectEvent(MenuItem item)
		{
			base.RaiseEvent(new RoutedEventArgs(InOutControl.SelectEvent)
			{
				Source = item
			});
		}
		private bool RaiseOptionsEvent(object originalSource)
		{
			RoutedEventArgs routedEventArgs = new RoutedEventArgs(InOutControl.OptionsEvent);
			routedEventArgs.Source = originalSource;
			base.RaiseEvent(routedEventArgs);
			return routedEventArgs.Handled;
		}
		public InOutControl()
		{
			this.InitializeComponent();
			this.ActivitiesMenu.Header = this.MenuItemLabel;
		}
		private void Button_Click(object sender, RoutedEventArgs e)
		{
			if (this.ModelItem == null)
			{
				return;
			}
			this.HasActivities = false;
			this.VariableSeparator.Visibility = Visibility.Collapsed;
			if (typeof(IContainerActivity).IsAssignableFrom(this.ModelItem.ItemType) || typeof(IScreenshotActivity).IsAssignableFrom(this.ModelItem.ItemType) || this.FromLinkButton)
			{
				this.ActivitiesMenu.Visibility = Visibility.Visible;
				List<VariableActivityPair> list = new List<VariableActivityPair>();
				List<VariableActivityPair> list2 = new List<VariableActivityPair>();
				Dictionary<string, string> variables = TargetHelper.GetVariables(this.ModelItem, this.VariableType);
				if (variables != null && variables.Count != 0)
				{
					this.HasActivities = true;
					foreach (KeyValuePair<string, string> current in variables)
					{
						if (current.Key.EndsWith(".ToString()"))
						{
							list2.Add(new VariableActivityPair(current.Key, current.Value));
							this.VariableSeparator.Visibility = Visibility.Visible;
						}
						else
						{
							list.Add(new VariableActivityPair(current.Key, current.Value));
						}
					}
				}
				if (!this.HasActivities)
				{
					list.Add(new VariableActivityPair("No matching variable", string.Empty));
				}
				this.MatchingVariablesContainer.Collection = list;
				this.MatchingObjectVariablesContainer.Collection = list2;
			}
			object originalSource = this;
			if (e.Source != e.OriginalSource)
			{
				originalSource = e.OriginalSource;
			}
			if (!this.RaiseOptionsEvent(originalSource))
			{
				this.ContextMenuActivities.Placement = PlacementMode.Bottom;
				TargetHelper.OpenContextMenu(this.ContextMenuActivities, e.OriginalSource as Button);
			}
		}
		private bool OutputIsOfType(Type variableType)
		{
			ModelItem parent = this.ModelItem.Parent.Parent.Parent;
			ModelPropertyCollection properties = parent.Properties;
			foreach (ModelProperty current in properties)
			{
				if (current.PropertyType.BaseType.Equals(typeof(OutArgument)))
				{
					object computedValue = current.ComputedValue;
					if (InOutControl.OutputIsOfTypeStatic.p__Site1 == null)
					{
						InOutControl.OutputIsOfTypeStatic.p__Site1 = CallSite<Func<CallSite, object, bool>>.Create(Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.IsTrue, typeof(InOutControl), new CSharpArgumentInfo[]
						{
							CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
						}));
					}
					Func<CallSite, object, bool> arg_25A_0 = InOutControl.OutputIsOfTypeStatic.p__Site1.Target;
					CallSite arg_25A_1 = InOutControl.OutputIsOfTypeStatic.p__Site1;
					if (InOutControl.OutputIsOfTypeStatic.p__Site2 == null)
					{
						InOutControl.OutputIsOfTypeStatic.p__Site2 = CallSite<Func<CallSite, object, object, object>>.Create(Binder.BinaryOperation(CSharpBinderFlags.None, ExpressionType.NotEqual, typeof(InOutControl), new CSharpArgumentInfo[]
						{
							CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
							CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.Constant, null)
						}));
					}
					object obj = InOutControl.OutputIsOfTypeStatic.p__Site2.Target(InOutControl.OutputIsOfTypeStatic.p__Site2, computedValue, null);
					if (InOutControl.OutputIsOfTypeStatic.p__Site3 == null)
					{
						InOutControl.OutputIsOfTypeStatic.p__Site3 = CallSite<Func<CallSite, object, bool>>.Create(Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.IsFalse, typeof(InOutControl), new CSharpArgumentInfo[]
						{
							CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
						}));
					}
					object arg_25A_2;
					if (!InOutControl.OutputIsOfTypeStatic.p__Site3.Target(InOutControl.OutputIsOfTypeStatic.p__Site3, obj))
					{
						if (InOutControl.OutputIsOfTypeStatic.p__Site4 == null)
						{
							InOutControl.OutputIsOfTypeStatic.p__Site4 = CallSite<Func<CallSite, object, object, object>>.Create(Binder.BinaryOperation(CSharpBinderFlags.BinaryOperationLogical, ExpressionType.And, typeof(InOutControl), new CSharpArgumentInfo[]
							{
								CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
								CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
							}));
						}
						Func<CallSite, object, object, object> arg_251_0 = InOutControl.OutputIsOfTypeStatic.p__Site4.Target;
						CallSite arg_251_1 = InOutControl.OutputIsOfTypeStatic.p__Site4;
						object arg_251_2 = obj;
						if (InOutControl.OutputIsOfTypeStatic.p__Site5 == null)
						{
							InOutControl.OutputIsOfTypeStatic.p__Site5 = CallSite<Func<CallSite, object, Type, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.None, "Equals", null, typeof(InOutControl), new CSharpArgumentInfo[]
							{
								CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
								CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null)
							}));
						}
						Func<CallSite, object, Type, object> arg_24C_0 = InOutControl.OutputIsOfTypeStatic.p__Site5.Target;
						CallSite arg_24C_1 = InOutControl.OutputIsOfTypeStatic.p__Site5;
						if (InOutControl.OutputIsOfTypeStatic.p__Site6 == null)
						{
							InOutControl.OutputIsOfTypeStatic.p__Site6 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "ArgumentType", typeof(InOutControl), new CSharpArgumentInfo[]
							{
								CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
							}));
						}
						arg_25A_2 = arg_251_0(arg_251_1, arg_251_2, arg_24C_0(arg_24C_1, InOutControl.OutputIsOfTypeStatic.p__Site6.Target(InOutControl.OutputIsOfTypeStatic.p__Site6, computedValue), this.VariableType));
					}
					else
					{
						arg_25A_2 = obj;
					}
					if (arg_25A_0(arg_25A_1, arg_25A_2))
					{
						return true;
					}
				}
			}
			return false;
		}
		private void ActivitiesMenu_Click(object sender, RoutedEventArgs e)
		{
			VariableActivityPair variableActivityPair = (e.OriginalSource as MenuItem).Header as VariableActivityPair;
			if (variableActivityPair == null || string.IsNullOrWhiteSpace(variableActivityPair.VariableName) || variableActivityPair.VariableName.Equals("No matching variable"))
			{
				return;
			}
			if (this.ModelItem.Properties[this.PropertyName] != null)
			{
				TargetHelper.SetInArgumentValue(this.ModelItem.Properties[this.PropertyName], variableActivityPair.VariableName);
			}
			this.RaiseSelectEvent(new MenuItem
			{
				Header = variableActivityPair.ToString()
			});
		}
		private void FromContainer_Click(object sender, RoutedEventArgs e)
		{
			ModelItem parent = this.ModelItem.Parent.Parent.Parent;
			ModelPropertyCollection properties = parent.Properties;
			object arg = null;
			foreach (ModelProperty current in properties)
			{
				if (current.PropertyType.BaseType.Equals(typeof(OutArgument)))
				{
					object computedValue = current.ComputedValue;
					if (InOutControl.FromContainer_ClickStatic.p__Site8 == null)
					{
						InOutControl.FromContainer_ClickStatic.p__Site8 = CallSite<Func<CallSite, object, bool>>.Create(Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.IsTrue, typeof(InOutControl), new CSharpArgumentInfo[]
						{
							CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
						}));
					}
					Func<CallSite, object, bool> arg_154_0 = InOutControl.FromContainer_ClickStatic.p__Site8.Target;
					CallSite arg_154_1 = InOutControl.FromContainer_ClickStatic.p__Site8;
					if (InOutControl.FromContainer_ClickStatic.p__Site9 == null)
					{
						InOutControl.FromContainer_ClickStatic.p__Site9 = CallSite<Func<CallSite, object, Type, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.None, "Equals", null, typeof(InOutControl), new CSharpArgumentInfo[]
						{
							CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
							CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null)
						}));
					}
					Func<CallSite, object, Type, object> arg_14F_0 = InOutControl.FromContainer_ClickStatic.p__Site9.Target;
					CallSite arg_14F_1 = InOutControl.FromContainer_ClickStatic.p__Site9;
					if (InOutControl.FromContainer_ClickStatic.p__Sitea == null)
					{
						InOutControl.FromContainer_ClickStatic.p__Sitea = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "ArgumentType", typeof(InOutControl), new CSharpArgumentInfo[]
						{
							CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
						}));
					}
					if (arg_154_0(arg_154_1, arg_14F_0(arg_14F_1, InOutControl.FromContainer_ClickStatic.p__Sitea.Target(InOutControl.FromContainer_ClickStatic.p__Sitea, computedValue), this.VariableType)))
					{
						arg = current.ComputedValue;
					}
				}
			}
			if (InOutControl.FromContainer_ClickStatic.p__Siteb == null)
			{
				InOutControl.FromContainer_ClickStatic.p__Siteb = CallSite<Func<CallSite, object, bool>>.Create(Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.IsTrue, typeof(InOutControl), new CSharpArgumentInfo[]
				{
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
				}));
			}
			Func<CallSite, object, bool> arg_21C_0 = InOutControl.FromContainer_ClickStatic.p__Siteb.Target;
			CallSite arg_21C_1 = InOutControl.FromContainer_ClickStatic.p__Siteb;
			if (InOutControl.FromContainer_ClickStatic.p__Sitec == null)
			{
				InOutControl.FromContainer_ClickStatic.p__Sitec = CallSite<Func<CallSite, object, object, object>>.Create(Binder.BinaryOperation(CSharpBinderFlags.None, ExpressionType.NotEqual, typeof(InOutControl), new CSharpArgumentInfo[]
				{
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.Constant, null)
				}));
			}
			if (arg_21C_0(arg_21C_1, InOutControl.FromContainer_ClickStatic.p__Sitec.Target(InOutControl.FromContainer_ClickStatic.p__Sitec, arg, null)))
			{
				object arg2 = Activator.CreateInstance(this.VariableType);
				if (InOutControl.FromContainer_ClickStatic.p__Sited == null)
				{
					InOutControl.FromContainer_ClickStatic.p__Sited = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "Expression", typeof(InOutControl), new CSharpArgumentInfo[]
					{
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
					}));
				}
				arg2 = InOutControl.FromContainer_ClickStatic.p__Sited.Target(InOutControl.FromContainer_ClickStatic.p__Sited, arg);
				if (InOutControl.FromContainer_ClickStatic.p__Sitee == null)
				{
					InOutControl.FromContainer_ClickStatic.p__Sitee = CallSite<Action<CallSite, Type, ModelProperty, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "SetInArgumentValue", null, typeof(InOutControl), new CSharpArgumentInfo[]
					{
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.IsStaticType, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
					}));
				}
				Action<CallSite, Type, ModelProperty, object> arg_35C_0 = InOutControl.FromContainer_ClickStatic.p__Sitee.Target;
				CallSite arg_35C_1 = InOutControl.FromContainer_ClickStatic.p__Sitee;
				Type arg_35C_2 = typeof(TargetHelper);
				ModelProperty arg_35C_3 = this.ModelItem.Properties[this.PropertyName];
				if (InOutControl.FromContainer_ClickStatic.p__Sitef == null)
				{
					InOutControl.FromContainer_ClickStatic.p__Sitef = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "ExpressionText", typeof(InOutControl), new CSharpArgumentInfo[]
					{
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
					}));
				}
				arg_35C_0(arg_35C_1, arg_35C_2, arg_35C_3, InOutControl.FromContainer_ClickStatic.p__Sitef.Target(InOutControl.FromContainer_ClickStatic.p__Sitef, arg2));
			}
		}
        //[GeneratedCode("PresentationBuildTasks", "4.0.0.0"), DebuggerNonUserCode]
        //public void InitializeComponent()
        //{
        //    if (this._contentLoaded)
        //    {
        //        return;
        //    }
        //    this._contentLoaded = true;
        //    Uri resourceLocator = new Uri("/FtpActivities.Design;component/controls/inoutcontrol.xaml", UriKind.Relative);
        //    Application.LoadComponent(this, resourceLocator);
        //}
        //[GeneratedCode("PresentationBuildTasks", "4.0.0.0"), EditorBrowsable(EditorBrowsableState.Never), DebuggerNonUserCode]
        //void IComponentConnector.Connect(int connectionId, object target)
        //{
        //    switch (connectionId)
        //    {
        //    case 1:
        //        this.InOutCtrl = (InOutControl)target;
        //        return;
        //    case 2:
        //        this.GridInOut = (Grid)target;
        //        return;
        //    case 3:
        //        this.SelectActivityButton = (Button)target;
        //        this.SelectActivityButton.Click += new RoutedEventHandler(this.Button_Click);
        //        return;
        //    case 4:
        //        this.ContextMenuActivities = (ContextMenu)target;
        //        return;
        //    case 5:
        //        this.ActivitiesMenu = (MenuItem)target;
        //        this.ActivitiesMenu.Click += new RoutedEventHandler(this.ActivitiesMenu_Click);
        //        return;
        //    case 6:
        //        this.MatchingVariablesContainer = (CollectionContainer)target;
        //        return;
        //    case 7:
        //        this.VariableSeparator = (Separator)target;
        //        return;
        //    case 8:
        //        this.MatchingObjectVariablesContainer = (CollectionContainer)target;
        //        return;
        //    case 9:
        //        this.ReserImageSeparator = (Separator)target;
        //        return;
        //    case 10:
        //        this.OptionsMenu = (MenuItem)target;
        //        return;
        //    default:
        //        this._contentLoaded = true;
        //        return;
        //    }
        //}
	}
}
