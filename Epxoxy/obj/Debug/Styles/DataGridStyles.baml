   M S B A M L   `   `   `  ����A  =Epxoxy, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:Epxoxy.ConvertersEpxoxy.Converters  5      b ^PresentationFramework.Aero2, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35hKclr-namespace:Microsoft.Windows.Themes;assembly=PresentationFramework.Aero2Microsoft.Windows.Themes 5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0893    -Epxoxy.Converters.DataGridVisibilityConverter   datagridConverter  ColumnHeaderGripperStyle  �� 
TargetType �� Property �� Value �� 
TargetType �� Padding  DataGridColumnHeaderStyle1=  7System.Windows.Controls.Primitives.DataGridColumnHeader3  -Microsoft.Windows.Themes.DataGridHeaderBorder   CanUserSort   IsClickable   	IsPressed J� IsMouseOver	   	IsHovered
   SortDirection   SortDirection   SeparatorBrush   SeparatorBrush   SeparatorVisibility   SeparatorVisibility J� SnapsToDevicePixels n�Name  DataGridRowHeaderStyle:  4System.Windows.Controls.Primitives.DataGridRowHeader  DataGridCellStyle*  $System.Windows.Controls.DataGridCell �� TextAlignment �� Triggers P� Property P� Value  DataGridRowStyle)  #System.Windows.Controls.DataGridRow  LightDataGridStyle&   System.Windows.Controls.DataGrid ��Name � ColumnDefinitions � RowDefinitions �� 
ResourceId �� TypeInTargetAssembly �� RelativeSource �� AncestorLevel �� AncestorType �� Path �� 	Converter  �� ConverterParameter7  1System.Windows.Controls.DataGridHeadersVisibilityG  ASystem.Windows.Controls.Primitives.DataGridColumnHeadersPresenter!  Name" ��Name# �� CanContentScroll$ �� CanHorizontallyScroll% �� CanVerticallyScroll& �� ContentStringFormat' �� ContentStringFormat( ��Name) �� ScrollableHeight* � Maximum+ �� Orientation*, �� #ComputedVerticalScrollBarVisibility- �� ViewportHeight. �� ViewportSize/ � Value0 �� Mode1 �� ScrollableWidth,2 �� %ComputedHorizontalScrollBarVisibility3 �� ViewportWidth4 ]� 
Conditions5 �� Property6 �� Value�� 5      D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    :x,http://schemas.microsoft.com/winfx/2006/xaml     5      .
convertersclr-namespace:Epxoxy.Converters  5      VThemesKclr-namespace:Microsoft.Windows.Themes;assembly=PresentationFramework.Aero2  5      %I  &	        &	      &	 %    7  7  &	 �    &	 �    &	 7    &	 k    7  7  7  7  7   7      5      6-   �� 5	      "  n�6-   .�5
   
   ��  � ��$ 8��6"   �� 5   
    � ��
 ���� 6'   �� 5   
    �@�Cursor$ SizeWE��6#   �� 5   
    � �� 5      �� 5      " n�6"   .R�5      �� #��z��6   # z��6G   5      5      5      �� 5      "   6/   .�5   
   ��  � ��
 �����6(   �� 5   
    � ��$ 0,0,1,1q�6,   �� 5   
    � �� �	#FFF3E7C46'   �� 5   
    � ��$ 28��6&   �� 5   
    � �� 5      �� 5      "  6"   .R�5      � .+�5        #��z��66   #��z��6b   #��z��6�   # z 6�   # z��6�   #	 z 6  # z��6>  # z
 6b  # z 6�  # z 6�  ._�5       �� $��Center=�6/   ��. # z 6g   #��z��6�   5!      n� 5"      $ PART_LeftHeaderGripper��6    $
��Left=�6@   8��  6[   n� 5#      $ PART_RightHeaderGripper��6    $��Right=�6A   8�� 6]   5$      5%      5'      5(      �� 5*      "   6+   .�5+   
   ��  � ��$ 25��6%   �� 5,   
    � ��
 ���� 6'   �� 5-   
    � ��
 �����6(   �� 5.   
    � ��$ 0,0,1,1q�6,   �� 5/   
    � ��$ 7q�6$   50      �� 56      "   6&   .�57   
   ��  � �� 58      �� 59      "  6"   .R�5:      � .+�5;      �� #��z��6$   �� 5<      $ Center=�6$   $��Center=�6;   �� 5=      .F�5>      �� 5?      5@      5A      5B      5D       5E   
   P� 5F       �@ 
IsSelected . . �5G      ��  � ��
 �   �60   �� 5H       � �� �	#FFFFCE656/   5I      5K      �� 5M      "   6%   .�5N   
   ��  � �� �#F2F2F26'   �� 5O   
    � ��$ 25��6&   �� 5P   
    � ��
 �   �6'    5Q   
   P� 5R       �@ AlternationIndex$ 0��62   . �5S      ��  � �� �#e7e7e76/   5T      P� 5U       �@ AlternationIndex$ 1��62   . �5V      ��  � �� �#f2f2f26/   5W      P� 5Y       �@J�IsMouseOver . . �5Z      ��  � ��
 �����6/   5[      P� 5\       �@ 
IsSelected . . �5]      ��  � �� �	#FFFBD0736/   5^      5`      �� 5d      "   6'   .�5e   
   ��  �@ 	CellStyle8   6&   �� 5f   
    �@ RowStyle8  6%   �� 5g   
    �@ ColumnHeaderStyle8  6.   �� 5h   
    �@ RowHeaderStyle8  6+   �� 5i   
    �@ VerticalGridLinesBrush �#d6c79b63   �� 5j   
     �@ HorizontalGridLinesBrush �#d6c79b65   �� 5k   
    �@��AlternationCount$ 2��6-   �� 5l   
    �@ CanUserAddRows . 6+   �� 5m   
    �@ AutoGenerateColumns . 60   �� 5n   
    � ��$ 0q�6,   �� 5o   
    � ��# � ��6'   �� 5p   
    � ��# � ��6'   �� 5q   
    � �� �	#FF688CAF6(   �� 5r   
     �@ RowDetailsVisibilityMode$ VisibleWhenSelected=�65   �� 5s   
    � �� . 6:   �� 5t   
    �@��PanningMode$
 Both=�65   �� 5u   
    �@��IsFlicksEnabled .  63   �� 5v   
    � �� 5w      �� 5x      "  6"   .R�5y      �� #��z��6   #��z��6I   #��z��6}   # z��6�    . ._�5z      �� $ DG_ScrollViewer��6'   |�.  ��5{      �� 5|   "   " ��62   .R�5}   &   �  5~   *   �� 5   .   $
��Auto �6?   �� 5�   .   $��* �6?   �� 5�   .   $
��Auto �6?    5�   *   �� 5�   .   $
��Auto �6<   �� 5�   .   $��* �6<   �� 5�   .   $
��Auto �6<   .+�5�   *   �� $#��ApplicationCommands.SelectAll��61   |�.  ��6k   C� *�� $" DataGridSelectAllButtonStyle��"  +��6�   �� *CellsPanelHorizontalOffset+ �� *FindAncestor+$ 1��"  y�5�   .   �� 5�   2   $ HeadersVisibility�6:   8   6S   �� *FindAncestor+$ 1��"    5�   6    5�   :   All=�6Y   5�   3   5�   +     5�   *   $!! PART_ColumnHeadersPresenter��6I   $��1��6n   y�5�   .   �� 5�   2   $ HeadersVisibility�6:   8   6S   �� *FindAncestor+$ 1��"    5�   6    5�   :   
Column=�6\   5�   3   5�   +   �� 5�   *   $!" PART_ScrollContentPresenter��6A   ## z��6f   $ .  $��2��6�   % .  #��z��6�   #��z��6  #' z& 6B  $��1��6~  �� 5�   *   $( PART_VerticalScrollBar��64   $��2��6T   #* z) 6d   $+ Vertical=�6�   $��1��6�   #y�z, 6�   #. z- 6W  / 6�   �� *VerticalOffset+$0 OneWay=� �� *TemplatedParent+6W  � 5�   *   $��1��6/   $��2��6?    5�   .   �� 5�   2   ��6C   �� **(NonFrozenColumnsViewportHorizontalOffset+ �� *FindAncestor+$ 1��"  �� 5�   2   $��* �6C   .+�5�   .   �� $( PART_HorizontalScrollBar��68   $��1��6Z   #* z1 6j   $+ 
Horizontal=�6�   #y�z2 6�   #. z3 6U  / 6�   �� *HorizontalOffset+$0 OneWay=� �� *TemplatedParent+6U  5�   +   5�   '   5�   #   .��5�      �� # z 6-   5�      5�      5�      5�       5�   
   ]� 5�      4 5�      �� 5�      5 �@��
IsGrouping6 . 66   �� 5�      "5 �@%�IsVirtualizingWhenGrouping6 .  6X   ./�5�      ��  � �� .  6B   5�      5�      5�      