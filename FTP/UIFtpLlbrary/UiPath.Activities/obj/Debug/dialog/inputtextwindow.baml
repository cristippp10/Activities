   M S B A M L   `   `   `  ����  UFtpLibray.Activities#    FtpActivities.InputTextWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    6�   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     6  -   5        $��InputTextWnd��5   1   $	��350��6E     � SizeToContent$  Height=�6Q    � WindowStartupLocation$ CenterScreen=�6h    � 
ResizeMode$ NoResize=�6�    � 	Resources 5         InputTextStyle �� 
TargetType �� Property �� Value �� Triggers �� Binding	 �� Path
 �� ElementName �� Value  ItemsControlStyle  ComboBoxStyle��%�  &	        &	 �     &	 �    �� 5      " ��6#   .�5      ��  � y�$ 	Collapsed=�6/    5      �� 5   
    6   �� $	 
InputPanel�$
 InputTextWnd�� 	InputText6T   .a�5      ��  � y�$ Visible=�63   5      5
      �� 5      " ��6&   .�5      ��  � y�$ 	Collapsed=�6/    5      �� 5   
    6   �� $	 
InputPanel�$
 InputTextWnd�� RadioButtons6T   .a�5      ��  � y�$ Visible=�63   5      5      �� 5      " ��6"   .�5      ��  � y�$ 	Collapsed=�6/    5      �� 5   
    6   �� $	 
InputPanel�$
 InputTextWnd�� ComboBox6T   .a�5      ��  � y�$ Visible=�63   5      5      5      .��5      ]� ��. $��20q�6#   .+�5      ]� $��Bottom=�6   $��23��6(   .+�5      �� -   5        $��OK��5      $��Center=�6   $��75��69   6D   5      ]� 5       $	��Top=�6   .+�5!      �� $��WrapWithOverflow=�6   $	��Top=�62   $	��Top=�6G   $	��150��6_   ��6o   �� $	 	LabelText�$
 InputTextWnd��� 5"      $��Bottom=�6   $	��Top=�6%   $
��0,20q�6=   .+�5#   
   �� -   5        $��InputTextBox��5#       �� TextWrapping$
 Wrap=�6&   $��25��6:    �� AcceptsReturn . " �� VerticalScrollBarVisibility$
 Auto=�6[   #��[  6~   ��6�   �� $	 SelectedValue�$
 InputTextWnd���� 5$   
   -   5        $��OptionsRadioButtons��5$      #��[ 62   ��6]   �� $	 Options�$
 InputTextWnd����5%      �� 5&      .R�5(      	�  	� 	GroupName$ Options��6   $
��Left=�61   $��0,5,0,0q�6L   ��6]   �� 5)      5+      �� 5,   
   -   5        $��OptionsCombobox��5,      #��[ 6*   ��6Q   �� $	 Options�$
 InputTextWnd�� �� SelectedValue 6�   �� $	 SelectedValue�$
 InputTextWnd��5-   	   5.      5/      50      