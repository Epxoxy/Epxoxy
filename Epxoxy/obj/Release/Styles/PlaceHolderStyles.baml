   M S B A M L   `   `   `  ���� A  =Epxoxy, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:Epxoxy.ControlsEpxoxy.Controls  \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089   TextBox.Static.Border  �� Color  TextBox.MouseOver.Border  TextBox.Focus.Border  FloatingStoryboard �� TargetProperty �� 
TargetName	 t� To g� Duration R� KeyTime R� Value   System.Windows.Visibility  PlaceHolderStoryboard �� 
TargetType �� Property	 �� Value(   "Epxoxy.Controls.PasswordBoxMonitor!   Epxoxy.Controls.PlaceHolder%  System.Windows.SystemParameters
   WindowGlassBrush �� 
TargetType J� SnapsToDevicePixels ��Name   Content ��Name   Epxoxy.Controls.Underline  Name   UnderlineBrush �� Triggers P� Property P� Value �� 
TargetName  FloatingPasswordBoxStyle ��Name K� ZIndex   Epxoxy.Controls.FillPanel ��Name N� EnterActions N� ExitActions  PlaceHolderTextBoxStyle-  'System.Windows.Input.KeyboardNavigation �� Triggers ]� 
Conditions �� Property �� Value # FloatingPlaceHolderTextBoxStyle . *FloatingPlaceHolderNoAnimationTextBoxStyle�� D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    :x,http://schemas.microsoft.com/winfx/2006/xaml     'localclr-namespace:Epxoxy.Controls  %R  &	        &	      &	 (     &	 =     &	 8    'G�     7   7  7  &	 �    7   7  7  7  7  &	 n	    7   7  7  &	 #    7   7  7  7  7  &	 5    7   7  7  �� $  	LightGray���� $  SkyBlue���� $  	#FF569DE5���� .��t� $ Margin�$ PlaceHolder��$ 	0 -20 0 0X�$ 00:00:00.10D�T� $ PlaceHolder��$ 
Visibility�..�i� $ 00:00:00.10��   	Collapsed=�T� $ Floating��$ 
Visibility�..�i� $ 00:00:00.10��   Visible=��� .��T� $ PlaceHolder��$ 
Visibility�..�i� $ 00:00:00��   Visible=�t� $ Margin�$ PlaceHolder��$ 00:00:00.10D�T� $ Floating��$ 
Visibility�..�i� $ 00:00:00��   
Hidden=��� " G�.���  � ��#	 � ����  � ��8	   ��  � ��#	 � ����  � ��$	 1q���  �@ IsMonitoring	 . ��  �@ Content$ 	 Please enter your password����  �@ UnderlineBrush#	 Z
 ��  � ��$
	 Left=���  � ��$	 Center=���  � ��	 �� " G�.R�� .+��� $��Bd��#��z��#��z��#��z�� . ._�� .+��� $ PlaceHolder��#��z #��z��#��z��
�������$y�	Collapsed=�#��z���� $ PART_ContentHost��# z   $ 	Underline��$��2��#��z  P�  � {� .  . ��� $ Bd��#	 � �� � ���� #	 � �� � ��P�  �@J�IsMouseOver . . ��� $ Bd��8	   � ��P�  �@J�IsKeyboardFocused . . ��� $ Bd��8	   � ��P�  �@ PasswordLength$ 0��. ��� $ PlaceHolder�� � y�$	 Visible=�P�  �@J�	IsFocused . . ��� $ 	Underline�� �@ IsActive	 . �� $ PlaceHolder�� � ��
	 ������� " G�.���  � ��#	 � ����  � ��8	   ��  � ��#	 � ����  � ��$	 0,0,0,1q���  �@ IsMonitoring	 . ��  �@ Content$ 	 Please enter your password����  �@ UnderlineBrush#	 Z
 ��  � ��$
	 Left=���  � ��	 �� " G�.R�]� #��z��.+��� $ Floating��
�������$ 2��$��12(�#��z $	��Top=�  .+��� $ Bd��#��z��#��z�� . �� $ PlaceHolder��#��z #��z��#��z��
�������#��z��$y�	Collapsed=��� $ PART_ContentHost��|�.  $��Hidden=�$��Hidden=�  $ 	Underline��$��2��#��z  P�  � {� .  . ��� $ Bd��#	 � �� � ���� #	 � �� � ��P�  �@J�IsMouseOver . . ��� $ Bd��8	   � ��P�  �@J�IsKeyboardFocused . . ��� $ Bd��8	   � ��P�  �@ PasswordLength$ 0�� �� 8��  �� 8�� P�  �@J�	IsFocused . . ��� $ 	Underline�� �@ IsActive	 . �� $ PlaceHolder�� � ��
	 ������� " ��.���  � ��#	 � ����  � ��8	   ��  � ��#	 � ����  � ��$	 1q���  �@ TabNavigation$
	 None=���  � ��$
	 Left=���  � ��$	 Center=���  �@�FocusVisualStyle	 Z� ��  �@J�	AllowDrop	 . ��  �@��PanningMode$	 VerticalFirst=���  �@��IsFlicksEnabled	 .  ��  �@ Content$	 
Enter here����  �@ UnderlineBrush#	 Z
 ��  � ��	 �� " ��.R�  #��z��.+��� $ border��#��z��#��z�� . �� $ PlaceHolder��#��z #��z��#��z��
�������#��z��$y�	Collapsed=��� $ PART_ContentHost��|�.  $��Hidden=�$��Hidden=�  $ 	Underline��$��2��#��z  P�  � ��$  ��. ��� $ PlaceHolder�� � y�$	 Visible=�P�  � {� .  . ��� $ border�� �@J�Opacity$
	 0.56Q�P�  �@J�IsMouseOver . . ��� $ border��8	   � ��P�  �@J�IsKeyboardFocused . . ��� $ border��8	   � ��P�  �@J�	IsFocused . . ��� $ 	Underline�� �@ IsActive	 . �� $ PlaceHolder�� � ��
	 ����� ]�  �� + �@��#IsInactiveSelectionHighlightEnabled . ��  �@��IsSelectionActive .  ./���  �@��SelectionBrush#	 � /��� " ��.���  � ��#	 � ����  � ��8	   ��  � ��#	 � ����  � ��$	 1q���  �@ TabNavigation$
	 None=���  � ��$
	 Left=���  �@�FocusVisualStyle	 Z� ��  �@J�	AllowDrop	 . ��  �@��PanningMode$	 VerticalFirst=���  � ��$	 2q���  �@��IsFlicksEnabled	 .  ��  �@ Content$	 
Enter here����  �@ UnderlineBrush#	 Z
 ��  � ��	 �� " ��.R�]� #��z��.+��� $ Floating��
�������$ 2��$��12(�#��z $	��Top=�#��z��#��z��  .+��� $ border��#��z��#��z�� . �� #��z��#��z��.���� $ PlaceHolder��#��z 
�������#��z���� $ PART_ContentHost��|�.  $��Hidden=�$��Hidden=�  $ 	Underline��$��2��#��z  P�  � ��$  �� �� 8��  �� 8�� P�  � {� .  . ��� $ border�� �@J�Opacity$
	 0.56Q�P�  �@J�IsMouseOver . . ��� $ border��8	   � ��P�  �@J�IsKeyboardFocused . . ��� $ border��8	   � ���� $ Floating�� � ��
	 �   �P�  �@J�	IsFocused . . ��� $ 	Underline�� �@ IsActive	 . �� $ PlaceHolder�� � ��
	 ����� ]�  �� + �@��#IsInactiveSelectionHighlightEnabled . ��  �@��IsSelectionActive .  ./���  �@��SelectionBrush#	 � /��� " ��.���  � ��#	 � ����  � ��8	   ��  � ��#	 � ����  � ��$	 1q���  �@ TabNavigation$
	 None=���  � ��$
	 Left=���  �@�FocusVisualStyle	 Z� ��  �@J�	AllowDrop	 . ��  �@��PanningMode$	 VerticalFirst=���  � ��$	 2q���  �@��IsFlicksEnabled	 .  ��  �@ Content$	 
Enter here����  �@ UnderlineBrush#	 Z
 ��  � ��	 �� " ��.R�]� #��z��.+��� $ Floating��
�������$ 2��$��12(�#��z $	��Top=�#��z��#��z��$y�Visible=�  .+��� $ border��#��z��#��z�� . �� #��z��#��z��.���� $ PlaceHolder��#��z 
�������#��z��$y�	Collapsed=��� $ PART_ContentHost��|�.  $��Hidden=�$��Hidden=�  $ 	Underline��$��2��#��z  P�  � ��$  ��. ��� $ Floating�� � y�$	 Hidden=��� $ PlaceHolder�� � y�$	 Visible=�P�  � {� .  . ��� $ border�� �@J�Opacity$
	 0.56Q�P�  �@J�IsMouseOver . . ��� $ border��8	   � ��P�  �@J�IsKeyboardFocused . . ��� $ border��8	   � ���� $ Floating�� � ��
	 �   �P�  �@J�	IsFocused . . ��� $ 	Underline�� �@ IsActive	 . �� $ PlaceHolder�� � ��
	 ����� ]�  �� + �@��#IsInactiveSelectionHighlightEnabled . ��  �@��IsSelectionActive .  ./���  �@��SelectionBrush#	 � /�