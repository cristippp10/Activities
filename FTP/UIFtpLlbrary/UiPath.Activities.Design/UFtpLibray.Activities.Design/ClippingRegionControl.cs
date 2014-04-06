using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
namespace FtpActivities.Design
{
	public partial class ClippingRegionControl : UserControl, INotifyPropertyChanged, IComponentConnector
	{
		public static readonly DependencyProperty TextProperty = DependencyProperty.Register("Text", typeof(string), typeof(ClippingRegionControl), new FrameworkPropertyMetadata(null));
        //internal ClippingRegionControl ClippingRegionCtrl;
        //internal TextBox LeftTextBox;
        //internal TextBox TopTextBox;
        //internal TextBox RightTextBox;
        //internal TextBox BottomTextBox;
        //private bool _contentLoaded;
		public event PropertyChangedEventHandler PropertyChanged;
		public string Text
		{
			get
			{
				return (string)base.GetValue(ClippingRegionControl.TextProperty);
			}
			set
			{
				base.SetValue(ClippingRegionControl.TextProperty, value);
			}
		}
		public string Left
		{
			get;
			set;
		}
		public string Top
		{
			get;
			set;
		}
		public string Right
		{
			get;
			set;
		}
		public string Bottom
		{
			get;
			set;
		}
		private void SetValues()
		{
			if (this.Text == null)
			{
				return;
			}
			string text = this.Text.Replace("(", "").Replace(")", "");
			string[] array = text.Split(new char[]
			{
				','
			});
			if (array.Length == 0)
			{
				return;
			}
			this.Left = array[0];
			this.Top = array[1];
			this.Right = array[2];
			this.Bottom = array[3];
			this.OnNotifyPropertyChanged("Left");
			this.OnNotifyPropertyChanged("Top");
			this.OnNotifyPropertyChanged("Right");
			this.OnNotifyPropertyChanged("Bottom");
		}
		public ClippingRegionControl()
		{
			this.InitializeComponent();
		}
		private void OnNotifyPropertyChanged(string propertyName)
		{
			if (this.PropertyChanged != null)
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
		private void ClippingRegionCtrl_Loaded(object sender, RoutedEventArgs e)
		{
			this.SetValues();
		}
		private void LeftTextBox_TextChanged(object sender, TextChangedEventArgs e)
		{
			if (!this.IsValid(this.LeftTextBox.Text))
			{
				return;
			}
			this.Left = this.LeftTextBox.Text;
			this.Text = this.GetText();
			this.OnNotifyPropertyChanged("Text");
		}
		private string GetText()
		{
			if (string.IsNullOrWhiteSpace(this.Left))
			{
				this.Left = "0";
			}
			if (string.IsNullOrWhiteSpace(this.Top))
			{
				this.Top = "0";
			}
			if (string.IsNullOrWhiteSpace(this.Bottom))
			{
				this.Bottom = "0";
			}
			if (string.IsNullOrWhiteSpace(this.Right))
			{
				this.Right = "0";
			}
			return string.Concat(new string[]
			{
				"(",
				this.Left,
				", ",
				this.Top,
				", ",
				this.Right,
				", ",
				this.Bottom,
				")"
			});
		}
		private bool IsValid(string str)
		{
			int num;
			return int.TryParse(str, out num);
		}
		private void TopTextBox_TextChanged(object sender, TextChangedEventArgs e)
		{
			if (!this.IsValid(this.TopTextBox.Text))
			{
				return;
			}
			this.Top = this.TopTextBox.Text;
			this.Text = this.GetText();
			this.OnNotifyPropertyChanged("Text");
		}
		private void RightTextBox_TextChanged(object sender, TextChangedEventArgs e)
		{
			if (!this.IsValid(this.RightTextBox.Text))
			{
				return;
			}
			this.Right = this.RightTextBox.Text;
			this.Text = this.GetText();
			this.OnNotifyPropertyChanged("Text");
		}
		private void BottomTextBox_TextChanged(object sender, TextChangedEventArgs e)
		{
			if (!this.IsValid(this.BottomTextBox.Text))
			{
				return;
			}
			this.Bottom = this.BottomTextBox.Text;
			this.Text = this.GetText();
			this.OnNotifyPropertyChanged("Text");
		}
        //[GeneratedCode("PresentationBuildTasks", "4.0.0.0"), DebuggerNonUserCode]
        //public void InitializeComponent()
        //{
        //    if (this._contentLoaded)
        //    {
        //        return;
        //    }
        //    this._contentLoaded = true;
        //    Uri resourceLocator = new Uri("/FtpActivities.Design;component/controls/clippingregioncontrol.xaml", UriKind.Relative);
        //    Application.LoadComponent(this, resourceLocator);
        //}
        //[GeneratedCode("PresentationBuildTasks", "4.0.0.0"), EditorBrowsable(EditorBrowsableState.Never), DebuggerNonUserCode]
        //void IComponentConnector.Connect(int connectionId, object target)
        //{
        //    switch (connectionId)
        //    {
        //    case 1:
        //        this.ClippingRegionCtrl = (ClippingRegionControl)target;
        //        this.ClippingRegionCtrl.Loaded += new RoutedEventHandler(this.ClippingRegionCtrl_Loaded);
        //        return;
        //    case 2:
        //        this.LeftTextBox = (TextBox)target;
        //        this.LeftTextBox.TextChanged += new TextChangedEventHandler(this.LeftTextBox_TextChanged);
        //        return;
        //    case 3:
        //        this.TopTextBox = (TextBox)target;
        //        this.TopTextBox.TextChanged += new TextChangedEventHandler(this.TopTextBox_TextChanged);
        //        return;
        //    case 4:
        //        this.RightTextBox = (TextBox)target;
        //        this.RightTextBox.TextChanged += new TextChangedEventHandler(this.RightTextBox_TextChanged);
        //        return;
        //    case 5:
        //        this.BottomTextBox = (TextBox)target;
        //        this.BottomTextBox.TextChanged += new TextChangedEventHandler(this.BottomTextBox_TextChanged);
        //        return;
        //    default:
        //        this._contentLoaded = true;
        //        return;
        //    }
        //}
	}
}
