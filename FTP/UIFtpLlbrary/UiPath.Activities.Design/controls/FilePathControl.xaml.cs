using System;
using System.Activities.Presentation.Model;
using System.Activities.Presentation.View;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
namespace FtpActivities.Design
{
	public partial class FilePathControl : UserControl, IComponentConnector
	{
		public static readonly DependencyProperty ModelItemProperty = DependencyProperty.Register("ModelItem", typeof(ModelItem), typeof(FilePathControl));
		public static readonly DependencyProperty ExpressionProperty = DependencyProperty.Register("Expression", typeof(ModelItem), typeof(FilePathControl));
		public static readonly DependencyProperty HintTextProperty = DependencyProperty.Register("HintText", typeof(string), typeof(FilePathControl), new PropertyMetadata("Text must be qouted"));
		public static readonly RoutedEvent OpenEvent = EventManager.RegisterRoutedEvent("Open", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(FilePathControl));
        //internal FilePathControl FilePath;
        //internal Button LoadButton;
        //internal ExpressionTextBox FileNameTextBox;
        //private bool _contentLoaded;
		public event RoutedEventHandler Open
		{
			add
			{
				base.AddHandler(FilePathControl.OpenEvent, value);
			}
			remove
			{
				base.RemoveHandler(FilePathControl.OpenEvent, value);
			}
		}
		public ModelItem ModelItem
		{
			get
			{
				return base.GetValue(FilePathControl.ModelItemProperty) as ModelItem;
			}
			set
			{
				base.SetValue(FilePathControl.ModelItemProperty, value);
			}
		}
		public ModelItem Expression
		{
			get
			{
				return base.GetValue(FilePathControl.ExpressionProperty) as ModelItem;
			}
			set
			{
				base.SetValue(FilePathControl.ExpressionProperty, value);
			}
		}
		public string HintText
		{
			get
			{
				return base.GetValue(FilePathControl.HintTextProperty) as string;
			}
			set
			{
				base.SetValue(FilePathControl.HintTextProperty, value);
			}
		}
		public FilePathControl()
		{
			base.Loaded += delegate(object s, RoutedEventArgs e)
			{
				this.FileNameTextBox.HintText = this.HintText;
			};
			this.InitializeComponent();
		}
		private void LoadButton_Click(object sender, RoutedEventArgs e)
		{
			base.RaiseEvent(new RoutedEventArgs(FilePathControl.OpenEvent)
			{
				Source = this.LoadButton
			});
		}
        //[GeneratedCode("PresentationBuildTasks", "4.0.0.0"), DebuggerNonUserCode]
        //public void InitializeComponent()
        //{
        //    if (this._contentLoaded)
        //    {
        //        return;
        //    }
        //    this._contentLoaded = true;
        //    Uri resourceLocator = new Uri("/FtpActivities.Design;component/controls/filepathcontrol.xaml", UriKind.Relative);
        //    Application.LoadComponent(this, resourceLocator);
        //}
        //[GeneratedCode("PresentationBuildTasks", "4.0.0.0"), EditorBrowsable(EditorBrowsableState.Never), DebuggerNonUserCode]
        //void IComponentConnector.Connect(int connectionId, object target)
        //{
        //    switch (connectionId)
        //    {
        //    case 1:
        //        this.FilePath = (FilePathControl)target;
        //        return;
        //    case 2:
        //        this.LoadButton = (Button)target;
        //        this.LoadButton.Click += new RoutedEventHandler(this.LoadButton_Click);
        //        return;
        //    case 3:
        //        this.FileNameTextBox = (ExpressionTextBox)target;
        //        return;
        //    default:
        //        this._contentLoaded = true;
        //        return;
        //    }
        //}
	}
}
