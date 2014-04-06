using System;
using System.Activities.Presentation.Model;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Markup;
namespace FtpActivities.Design
{
	public partial class LinkPropertyControl : UserControl, IComponentConnector
	{
		public static readonly DependencyProperty ModelItemProperty = DependencyProperty.Register("ModelItem", typeof(ModelItem), typeof(LinkPropertyControl));
		public static readonly DependencyProperty PropertyNameProperty = DependencyProperty.Register("PropertyName", typeof(string), typeof(LinkPropertyControl));
		private string menuItemLabel = "Use a value stored in a variable";
        //internal InOutControl PropertySetter;
        //private bool _contentLoaded;
		public ModelItem ModelItem
		{
			get
			{
				return base.GetValue(LinkPropertyControl.ModelItemProperty) as ModelItem;
			}
			set
			{
				base.SetValue(LinkPropertyControl.ModelItemProperty, value);
			}
		}
		public string PropertyName
		{
			get
			{
				return base.GetValue(LinkPropertyControl.PropertyNameProperty) as string;
			}
			set
			{
				base.SetValue(LinkPropertyControl.PropertyNameProperty, value);
			}
		}
		public Type VariableType
		{
			get;
			set;
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
				//modif
                //this.PropertySetter.ActivitiesMenu.Header = value;
			}
		}
		public LinkPropertyControl()
		{
			this.InitializeComponent();
			base.Loaded += new RoutedEventHandler(this.LinkPropertyControl_Loaded);
		}
		private void LinkPropertyControl_Loaded(object sender, RoutedEventArgs e)
		{
			//modif
            //this.PropertySetter.MenuItemLabel = this.MenuItemLabel;
            //this.PropertySetter.VariableType = this.VariableType;
            //this.PropertySetter.PropertyName = this.PropertyName;
		}
		public void PropertySetterButton_Click(object sender, RoutedEventArgs e)
		{
            try
            {
                if (this.ModelItem == null)
                {
                    return;
                }
                RoutedEventArgs routedEventArgs = new RoutedEventArgs(ButtonBase.ClickEvent);
                routedEventArgs.Source = sender;
                //modif
                //this.PropertySetter.SelectActivityButton.RaiseEvent(e);
            }
            catch (Exception ex)
            {
                string exs = "" ;
                exs = ex.Message;
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
        //    Uri resourceLocator = new Uri("/FtpActivities.Design;component/controls/linkpropertycontrol.xaml", UriKind.Relative);
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
        //        case 1:
        //            ((Button)target).Click += new RoutedEventHandler(this.PropertySetterButton_Click);
        //            return;
        //        case 2:
        //            this.PropertySetter = (InOutControl)target;
        //            return;
        //        default:
        //            this._contentLoaded = true;
        //            return;
        //    }
        //}
	}
}
