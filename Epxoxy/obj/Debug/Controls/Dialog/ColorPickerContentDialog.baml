   M S B A M L   `   `   `  ����A  =Epxoxy, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:Epxoxy.ControlsEpxoxy.Controls  5      .    (Epxoxy.Controls.ColorPickerContentDialog   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      .xoxyclr-namespace:Epxoxy.Controls     5      #   Epxoxy.Controls.ContentDialog    Title$  Select Color��5       � 	Resources 5	       �� MergedDictionaries �� Source�� 5
   
    5      �� 5      $A ;pack://application:,,,/Epxoxy;component/Themes/Generic.xaml?�6%   %    5         
TitleBrush 5      �� 5   
    �� Color 6   �� *SelectedColor+ �� ElementName$ 
pickerPane��.��5      � $	��480��6   .+�5   
   %   Epxoxy.Controls.ColorPickerPane  -   5          Name$ 
pickerPane��5   -   %   Epxoxy.Controls.ColorPickerPane   OnSelectedColorChanged$$ PickerPaneSelectedColorChanged��6�   	   Mode$	 Custom=�6   $��1��6[   
   SelectedColor$
 SkyBlueX�6h   6�   5         BottomToolContent 5      �� 5   
    �� Orientation$ 
Horizontal=�6   .+�5      	� -   5        $��Basic��5       d� 	IsChecked$
 TrueY�6*   $��70��6T   $��Center=�6b   $��Center=�5      
�������$	��4,2q�6U       SelectedFlagRadioButtonStyle#��[  6b   	� 5      -   5        $��Custom��5      $��70��6E   $��Center=�6S   $��Center=�5      
�������$	��4,2q�6U   #��[  6b   �� 5      -   5        $��Clear��5        WinTenBtnStyle#��[ 6%   $��70��6M   $��4q�6[   5      5      5       