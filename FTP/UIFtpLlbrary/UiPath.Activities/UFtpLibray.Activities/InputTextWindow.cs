using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
namespace FtpActivities
{
	public partial class InputTextWindow : Window, INotifyPropertyChanged, IComponentConnector, IStyleConnector
	{
		private string selectedValue;
        //internal InputTextWindow InputTextWnd;
        //internal TextBox InputTextBox;
        //internal ItemsControl OptionsRadioButtons;
        //internal ComboBox OptionsCombobox;
        //private bool _contentLoaded;
		public event PropertyChangedEventHandler PropertyChanged;
		public string LabelText
		{
			get;
			set;
		}
		public string[] Options
		{
			get;
			set;
		}
		public string SelectedValue
		{
			get
			{
				return this.selectedValue;
			}
			set
			{
				this.selectedValue = value;
				this.OnNotifyPropertyChanged("SelectedValue");
			}
		}
		public string InputValue
		{
			get;
			internal set;
		}
		public string InputPanel
		{
			get;
			internal set;
		}
		private void OnNotifyPropertyChanged(string propertyName)
		{
			if (this.PropertyChanged != null)
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
		public InputTextWindow(string title, string labelText, string[] options)
		{
			base.Title = (title ?? string.Empty);
			this.LabelText = (labelText ?? string.Empty);
			this.Options = options;
			this.InputPanel = "InputText";
			if (this.Options != null)
			{
				int num = this.Options.Length;
				if (num == 1)
				{
					this.SelectedValue = this.Options[0];
				}
				else
				{
					if (num == 2 || num == 3)
					{
						this.InputPanel = "RadioButtons";
					}
					else
					{
						if (num > 3)
						{
							this.SelectedValue = this.Options[0];
							this.InputPanel = "ComboBox";
						}
					}
				}
			}
			this.InitializeComponent();
		}
		private void RadioButton_Checked(object sender, RoutedEventArgs e)
		{
			RadioButton radioButton = sender as RadioButton;
			if (radioButton != null)
			{
				this.SelectedValue = radioButton.Content.ToString();
			}
		}
		private void OKButton_Click(object sender, RoutedEventArgs e)
		{
			this.InputValue = this.SelectedValue;
			base.Close();
		}
		private void CancelButton_Click(object sender, RoutedEventArgs e)
		{
			base.Close();
		}
		private void InputTextWnd_Loaded(object sender, RoutedEventArgs e)
		{
			base.WindowState = WindowState.Normal;
			base.Activate();
		}
        //[GeneratedCode("PresentationBuildTasks", "4.0.0.0"), System.Diagnostics.DebuggerNonUserCode]
        //public void InitializeComponent()
        //{
        //    if (this._contentLoaded)
        //    {
        //        return;
        //    }
        //    this._contentLoaded = true;
        //    Uri resourceLocator = new Uri("/UiPath.Activities;component/dialog/inputtextwindow.xaml", UriKind.Relative);
        //    Application.LoadComponent(this, resourceLocator);
        //}
        //[GeneratedCode("PresentationBuildTasks", "4.0.0.0"), EditorBrowsable(EditorBrowsableState.Never), System.Diagnostics.DebuggerNonUserCode]
        //void IComponentConnector.Connect(int connectionId, object target)
        //{
        //    switch (connectionId)
        //    {
        //    case 1:
        //        this.InputTextWnd = (InputTextWindow)target;
        //        this.InputTextWnd.Loaded += new RoutedEventHandler(this.InputTextWnd_Loaded);
        //        return;
        //    case 2:
        //        ((Button)target).Click += new RoutedEventHandler(this.OKButton_Click);
        //        return;
        //    case 3:
        //        this.InputTextBox = (TextBox)target;
        //        return;
        //    case 4:
        //        this.OptionsRadioButtons = (ItemsControl)target;
        //        return;
        //    case 6:
        //        this.OptionsCombobox = (ComboBox)target;
        //        return;
        //    }
        //    this._contentLoaded = true;
        //}
        //[GeneratedCode("PresentationBuildTasks", "4.0.0.0"), EditorBrowsable(EditorBrowsableState.Never), System.Diagnostics.DebuggerNonUserCode]
        void IStyleConnector.Connect(int connectionId, object target)
        {
            if (connectionId != 5)
            {
                return;
            }
            ((RadioButton)target).Checked += new RoutedEventHandler(this.RadioButton_Checked);
        }
	}
}
