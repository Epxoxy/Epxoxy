   M S B A M L   `   `   `  ���� A  =Epxoxy, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:Epxoxy.ControlsEpxoxy.Controls  3clr-namespace:Epxoxy.BehaviorsEpxoxy.Behaviors  \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089   ScrollBar.Static.Border  �� Color  ScrollBar.Pressed.Glyph  ScrollBar.MouseOver.Glyph  ScrollBar.Disabled.Glyph  ScrollBar.Static.Glyph  ScrollBar.MouseOver.Border  ScrollBar.Pressed.Border  ScrollBar.Disabled.Border  ScrollBarButton �� 
TargetType �� Property �� Value �� 
TargetType ��Name J� SnapsToDevicePixels ��Name �� Triggers	 P� Property
 P� Value �� 
TargetName 	 RepeatButtonTransparent 
 RepeatOnlyScrollBarStyle �Name � RowDefinitions ��Name �� LineUpCommand J� IsMouseOver F�Name �� Stretch �� LineDownCommand ^� 
Conditions �� Binding �� ElementName �� Value �� Triggers � ColumnDefinitions �� LineLeftCommand �� LineRightCommand # RepeatVerticalScrollViewerStyle ��Name4   .System.Windows.Automation.AutomationProperties    AutomationId � Cursor �� ScrollableHeight  � Maximum! � Minimum*" �� #ComputedVerticalScrollBarVisibility# �� ViewportHeight$ �� ViewportSize% � Value& �� Mode' �� RelativeSource( ��Name) �� CanContentScroll* �� CanHorizontallyScroll+ �� CanVerticallyScroll % !RepeatHorizontalScrollViewerStyle, �� ScrollableWidth- �� Orientation,. �� %ComputedHorizontalScrollBarVisibility/ �� ViewportWidth  Pipe.Selected.Fill  Item.Normal.Foreground  Item.Selected.Fill  LightTabItemStyle0 ��Name  LightTabItemHorizontalStyle1 �� Padding   TransitioningTabControlStyle&    Epxoxy.Controls.TabControlHelper-  'System.Windows.Input.KeyboardNavigation2   TabNavigation3 ��Name4 ��Name5 ��Name6 ��Name7 K� ZIndex8 K� IsItemsHost9   TabIndex1   +Epxoxy.Controls.TransitioningContentControl:  Name%   Epxoxy.Behaviors.ReloadBehavior;   OnSelectedTabChanged'<    RestartTransitionOnContentChange=   
Transition>   
Transition? �� SelectedContent"@ �� SelectedContentStringFormatA �� ContentStringFormatB �� SelectedContentTemplate   VerticalScrollViewerTemplate * &TransitioningTabControlWithShadowStyleC � 	Resources  KeyD �� AE �� RF �� GG �� B  gs1H � ColorI � Offset  gs0J �� 
StartPointK �� EndPoint  ShadowGSC01  VerticalShadowToBottomL J� IsHitTestVisible  VerticalShadowToUp  HorizontalShadowToRight  HorizontalShadowToLeft  SimpleTabControlStyle # SimpleTabControlWithShadowStyle�� D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    :x,http://schemas.microsoft.com/winfx/2006/xaml     'localclr-namespace:Epxoxy.Controls  ,	behaviorsclr-namespace:Epxoxy.Behaviors  %^?  &	        &	      &	 (     &	 ;     &	 N     &	 a     &	 t     &	 �     &	 �     &		 %    &	
 �    7   7  7  7  7  7  7  7  7  7  7  7  7  7  7  7  7  7  7  7  7  &	 �    7 
 &	 Z    7 
 &	 2    &	 E    &	 X    &	 i    7  7  7  7  &	 �    7  7  7  7  &	 �    7  7  &	 2    7  7  7  7  7  7  7  7  7  7  7  7  &	 �*    7  7  &	 -2    7  7  7  7  7  7  7  7  7  7  7  7  �� $  #F0F0F0���� $  	#FF909090���� $  #000000���� $  #BFBFBF���� $  #606060���� $  #DADADA���� $  #606060���� $  #F0F0F0���� " ��.���  � ��$ 0q���  � ��$ Center=���  � ��$ Center=���  � ��
 ���� ��  � ��$ 1q���  � |� .  ��  � �� .  ��  � �� �� " ��.R��� $ border��#��z��#��z��#��z�� . ._��� $ contentPresenter��|�.  #��z��#��z��# z #��z�� P� 	 � {�
 .  . ��� $ contentPresenter�� �@J�Opacity$
 0.56Q��� " ��.���  �@�OverridesDefaultStyle . ��  � ��
 ���� ��  � |� .  ��  � �� .  ��  � �� �� " ��.R��� #��z��#��z��#��z���� " ��.���  �@��IsPressAndHoldEnabled .  ��  �@��IsFlicksEnabled .  ��  � ��
 ���� ��  � ��8   ��  � ��# � ����  � ��$	 0,0q���  � ��# � r���  � ��# � r���  � �� �� " ��.R�� $ Bg�� .  �� #��� q��� $��0.00001* ��� #��� q�.+��� #��z��#��z��#��z��$��1���� $ PART_LineUpButton��#��Z #{�z 8�� .��F� $ ArrowTop��"���Ѐ� @T� � z ��- р�� @T� 	8�� 
������ $ Uniform=��� $ PART_LineDownButton��#��Z #{�z $��2��8�� .��F� $ ArrowBottom��"���Ѐ� ��� � z  $� р�� ��� 	8�� $ Uniform=� ^�  ��  �� *IsMouseOver+$ PART_LineDownButton�� true��  �� *	IsPressed+$ PART_LineDownButton�� true.0��� $ ArrowBottom��8   � ��^�  ��  �� *IsMouseOver+$ PART_LineUpButton�� true��  �� *	IsPressed+$ PART_LineUpButton�� true.0��� $ ArrowTop��8   � ��^�  ��  �� *IsMouseOver+$ PART_LineDownButton�� true��  �� *	IsPressed+$ PART_LineDownButton��	 false.0��� $ ArrowBottom��8   � ��^�  ��  �� *IsMouseOver+$ PART_LineUpButton�� true��  �� *	IsPressed+$ PART_LineUpButton��	 false.0��� $ ArrowTop��8   � ��P� 	 � {�
 .  . ��� $ ArrowTop��8 	  � ���� $ ArrowBottom��8 
  � �� P� 	 �@��Orientation$
 
Horizontal=�. ���  � ��$
 Auto����  � ��$ 0����  � ��# � ����  � ��# � ����  � ��$	 0,0q���  � �� �� " ��.R�� $ Bg�� .  �� #��� ���� $��0.00001* ��� #��� ��.+��� #��z��#��z��#��z��$��1���� $ PART_LineLeftButton��#��Z #{�z 8�� .��F� $ 	ArrowLeft��"���� z ��� р� @T� � z ��- 	8�� $ Uniform=��� $ PART_LineRightButton��$��2��#��Z #{�z 8�� .��F� $ 
ArrowRight��"����@T� ��� ���� @T� �@T� ��- 	8�� $ Uniform=� ^�  ��  �� *IsMouseOver+$ PART_LineRightButton�� true��  �� *	IsPressed+$ PART_LineRightButton�� true.0��� $ 
ArrowRight��8   � ��^�  ��  �� *IsMouseOver+$ PART_LineLeftButton�� true��  �� *	IsPressed+$ PART_LineLeftButton�� true.0��� $ 	ArrowLeft��8   � ��^�  ��  �� *IsMouseOver+$ PART_LineRightButton�� true��  �� *	IsPressed+$ PART_LineRightButton��	 false.0��� $ 
ArrowRight��8   � ��^�  ��  �� *IsMouseOver+$ PART_LineLeftButton�� true��  �� *	IsPressed+$ PART_LineLeftButton��	 false.0��� $ 	ArrowLeft��8   � ��P� 	 � {�
 .  . ��� $ 	ArrowLeft��8   � ���� $ 
ArrowRight��8   � ���� " ��.���  � �� Z� ��  � ��$ Disabled=���  � ��$
 Auto=���  � �� �� " ��.R��� #��z��#��z��#��z��._�� $
 Grid��.+��� $ PART_VerticalScrollBar��$��14��$ VerticalScrollBar��$ Arrow��#  z $! 0Q�#y�z" #$ z# 8��  % �� *VerticalOffset+$& OneWay=�' �� *TemplatedParent+�� $!( PART_ScrollContentPresenter��$
��0,14q�#) z��* .  + .  #��z��#��z���� " ��.���  � �� Z� ��  � ��$
 Auto=���  � ��$ Disabled=���  � �� �� " ��.R��� #��z��#��z��#��z��._�� $
 Grid��.+��� $ PART_HorizontalScrollBar��$��14��$ HorizontalScrollBar��$ Arrow��#  z, $! 0Q�$- 
Horizontal=�#y�z. #$ z/ 8��  % �� *HorizontalOffset+$& OneWay=�' �� *TemplatedParent+�� $!( PART_ScrollContentPresenter��$
��14,0q�#) z��* .  + .  #��z��#��z��$��0��$��0���� $  #4285F4���� $  #75758b���� $  Black���� " ��.���  � ��8   ��  � ��
 ���� ��  � ��
 ���� ��  � ��$ 0q���  � ��$ 0q���  � ��$ 30����  � ��$
 12,0q���  � ��$ 80����  �@J�SnapsToDevicePixels . ��  � ��$ Stretch=���  � ��$ Stretch=���  � �� �� " ��.R��� $ 
mainBorder��#��z��#��z��#��z�� . ._�� $ templateRoot�� .  �� �� $��3 �.+��� $0 
SelectPipe��$��1��$y�Hidden=�$��2��8�� $��Bottom=����� *ActualWidth+$ contentPresenter���� $ contentPresenter��$��Header��|�.  $��0��$��Center=�# z $��Center=�#��z�� P� 	 �@J�IsMouseOver
 . . ��� 8   � ��P� 	 �@��
IsSelected
 . . ��� $ 
SelectPipe�� � y�$ Visible=��� 8   � ��P� 	 � {�
 .  . ��� $ 
mainBorder��# � �� � ���� " ��.���  � ��8   ��  � �� �#f4f4f4��  � ��$ 0q���  � ��$ 0q���  � ��$ 40����  � ��$ 0,7,12,7q���  � ��$ Stretch=���  � ��$ Stretch=���  � �� �� " ��.R��� $ 
mainBorder��#��z��#��z��#��z��#1 z�� . ._�� $ templateRoot�� .  �� $��5 ��� .+��� $0 
SelectPipe��$y�	Collapsed=�$
��Left=�$��2��8�� $	��0,2q��� $ contentPresenter��$��1��$��Header��|�.  #��z��# z $��Center=� P� 	 �@J�IsMouseOver
 . . ��� 8   � ��P� 	 �@��
IsSelected
 . . ��� $ 
SelectPipe�� � y�$ Visible=���  � ��
 ������� 8   � ��P� 	 � {�
 .  . ��� $ 
mainBorder��# � �� � ���� " ��.���  �@ 
Transition$
 Left=���  � �� Z� ��  � ��8   ��  � ��# � ����  � �� �� " ��.R�� $2 Local=� �� $3 ColumnDefinition0���� $3 ColumnDefinition1��$��0 � �� $4 RowDefinition0��$
��Auto ��� $4 RowDefinition1��$��* �.+��� $5 HeaderPanelScroll��$��0��$��0��$
��Auto=�8�� #��z��$��Disabled=�#��z��#��z��.���� $6 HeaderPanel��$7 1��8 . $9 1��  $: ContentPanel��$��1��$��0��; . < . #> z= .���� $ PART_SelectedContentHost��#��z��#��z? $��SelectedContent��#A z@ #��zB # z  P� 	 �@��TabStripPlacement$
 Bottom=�. ��� $ ContentPanel�� � ��$ 0���� $ HeaderPanelScroll�� � ��$ 1���� $ RowDefinition0�� � ��$ * ��� $ RowDefinition1�� � ��$
 Auto �P� 	 �@��TabStripPlacement$

 Left=�. ���  �@ 
Transition$ Right=��� $ ColumnDefinition0�� � ��$
 Auto ��� $ ColumnDefinition1�� � ��$ * ��� $ ContentPanel�� � ��$ 1���� $ ContentPanel�� � ��$ 0���� $ HeaderPanelScroll�� � ��$ 0���� $ HeaderPanelScroll�� � ��$ 0���� $ HeaderPanelScroll�� � ��$ Disabled=��� $ HeaderPanelScroll��# �   � ���� $ HeaderPanelScroll�� � ��$
 Auto=��� $ RowDefinition0�� � ��$ * ��� $ RowDefinition1�� � ��$ 0 �P� 	 �@��TabStripPlacement$
 Right=�. ��� $ ColumnDefinition0�� � ��$ * ��� $ ColumnDefinition1�� � ��$
 Auto ��� $ ContentPanel�� � ��$ 0���� $ ContentPanel�� � ��$ 0���� $ HeaderPanelScroll�� � ��$ 1���� $ HeaderPanelScroll�� � ��$ 0���� $ HeaderPanelScroll�� � ��$ Disabled=��� $ HeaderPanelScroll��# �   � ���� $ HeaderPanelScroll�� � ��$
 Auto=��� $ RowDefinition0�� � ��$ * ��� $ RowDefinition1�� � ��$ 0 ��� " ��.���  �@ 
Transition$
 Left=���  � ��
 �������  � ��$ 0q���  � ��8   ��  � ��# � ����  � �� �� " ��C ���� gs0 $	D 115��$	E 120��$	F 120��$	G 120���� gs1 $D 0��$	E 120��$	F 120��$	G 120��� ShadowGSC01 � 8H  $I 0Q�� 8H  $I 1Q��� HorizontalShadowToLeft $	J 0,0&�$	K 1,0&�8�� �� HorizontalShadowToRight $	J 1,0&�$	K 0,0&�8�� �� VerticalShadowToBottom $	J 0,1&�$	K 0,0&�8�� �� VerticalShadowToUp $	J 0,0&�$	K 0,1&�8�� .R�� $2 Local=� �� $3 ColumnDefinition0���� $3 ColumnDefinition1��$��0 � �� $4 RowDefinition0��$
��Auto ��� $4 RowDefinition1��$��* �.+��� $5 HeaderPanelScroll��$��0��$��0��$
��Auto=�8�� #��z��$��Disabled=�#��z��#��z��.���� $6 HeaderPanel��$7 1��8 . $9 1��  $: ContentPanel��$��1��$��0��; . < . #> z= .���� $ PART_SelectedContentHost��#��z��#��z? $��SelectedContent��#A z@ #��zB # z �� $0 Shadow��8�� $��4��$��4��$��Stretch=�$	��Top=�$��1��$��0�� . L .   P� 	 �@��TabStripPlacement$
 Bottom=�. ��� $ ContentPanel�� � ��$ 0���� $ HeaderPanelScroll�� � ��$ 1���� $ RowDefinition0�� � ��$ * ��� $ RowDefinition1�� � ��$
 Auto ��� $ Shadow�� � ��$ Stretch=��� $ Shadow�� � ��$ Bottom=��� $ Shadow�� � ��$ 0���� $ Shadow�� � ��$ 0���� $ Shadow��8 	  � ��P� 	 �@��TabStripPlacement$

 Left=�. ���  �@ 
Transition$ Right=��� $ ColumnDefinition0�� � ��$
 Auto ��� $ ColumnDefinition1�� � ��$ * ��� $ ContentPanel�� � ��$ 1���� $ ContentPanel�� � ��$ 0���� $ HeaderPanelScroll�� � ��$ 0���� $ HeaderPanelScroll�� � ��$ 0���� $ HeaderPanelScroll�� � ��$ Disabled=��� $ HeaderPanelScroll��# �   � ���� $ HeaderPanelScroll�� � ��$
 Auto=��� $ RowDefinition0�� � ��$ * ��� $ RowDefinition1�� � ��$ 0 ��� $ Shadow�� � ��$
 Left=��� $ Shadow�� � ��$ Stretch=��� $ Shadow�� � ��$ 0���� $ Shadow�� � ��$ 1���� $ Shadow��8 
  � ��P� 	 �@��TabStripPlacement$
 Right=�. ��� $ ColumnDefinition0�� � ��$ * ��� $ ColumnDefinition1�� � ��$
 Auto ��� $ ContentPanel�� � ��$ 0���� $ ContentPanel�� � ��$ 0���� $ HeaderPanelScroll�� � ��$ 1���� $ HeaderPanelScroll�� � ��$ 0���� $ HeaderPanelScroll�� � ��$ Disabled=��� $ HeaderPanelScroll��# �   � ���� $ HeaderPanelScroll�� � ��$
 Auto=��� $ RowDefinition0�� � ��$ * ��� $ RowDefinition1�� � ��$ 0 ��� $ Shadow�� � ��$ Right=��� $ Shadow�� � ��$ Stretch=��� $ Shadow�� � ��$ 0���� $ Shadow�� � ��$ 0���� $ Shadow��8   � ���� " ��.���  � �� Z� ��  � ��8   ��  � ��# � ����  � �� �� " ��.R�� $2 Local=� �� $3 ColumnDefinition0���� $3 ColumnDefinition1��$��0 � �� $4 RowDefinition0��$
��Auto ��� $4 RowDefinition1��$��* �.+��� $5 HeaderPanelScroll��$��0��$��0��$
��Auto=�8�� #��z��$��Disabled=�#��z��#��z��.���� $6 HeaderPanel��$7 1��8 . $9 1���� $ PART_SelectedContentHost��$��1��$��0��#��z��#��z? $��SelectedContent��#A z@ #��zB # z  P� 	 �@��TabStripPlacement$
 Bottom=�. ��� $ PART_SelectedContentHost�� � ��$ 0���� $ HeaderPanelScroll�� � ��$ 1���� $ RowDefinition0�� � ��$ * ��� $ RowDefinition1�� � ��$
 Auto �P� 	 �@��TabStripPlacement$

 Left=�. ��� $ ColumnDefinition0�� � ��$
 Auto ��� $ ColumnDefinition1�� � ��$ * ��� $ PART_SelectedContentHost�� � ��$ 1���� $ PART_SelectedContentHost�� � ��$ 0���� $ HeaderPanelScroll�� � ��$ 0���� $ HeaderPanelScroll�� � ��$ 0���� $ HeaderPanelScroll�� � ��$ Disabled=��� $ HeaderPanelScroll��# �   � ���� $ HeaderPanelScroll�� � ��$
 Auto=��� $ RowDefinition0�� � ��$ * ��� $ RowDefinition1�� � ��$ 0 �P� 	 �@��TabStripPlacement$
 Right=�. ��� $ ColumnDefinition0�� � ��$ * ��� $ ColumnDefinition1�� � ��$
 Auto ��� $ PART_SelectedContentHost�� � ��$ 0���� $ PART_SelectedContentHost�� � ��$ 0���� $ HeaderPanelScroll�� � ��$ 1���� $ HeaderPanelScroll�� � ��$ 0���� $ HeaderPanelScroll�� � ��$ Disabled=��� $ HeaderPanelScroll��# �   � ���� $ HeaderPanelScroll�� � ��$
 Auto=��� $ RowDefinition0�� � ��$ * ��� $ RowDefinition1�� � ��$ 0 ��� " ��.���  � ��
 �������  � ��$ 0q���  � ��8   ��  � ��# � ����  � �� �� " ��C ���� gs0 $	D 115��$	E 120��$	F 120��$	G 120���� gs1 $D 0��$	E 120��$	F 120��$	G 120��� ShadowGSC01 � 8H  $I 0Q�� 8H  $I 1Q��� HorizontalShadowToLeft $	J 0,0&�$	K 1,0&�8�� �� HorizontalShadowToRight $	J 1,0&�$	K 0,0&�8�� �� VerticalShadowToBottom $	J 0,1&�$	K 0,0&�8�� �� VerticalShadowToUp $	J 0,0&�$	K 0,1&�8�� .R�� $2 Local=� �� $3 ColumnDefinition0���� $3 ColumnDefinition1��$��0 � �� $4 RowDefinition0��$
��Auto ��� $4 RowDefinition1��$��* �.+��� $5 HeaderPanelScroll��$��0��$��0��$
��Auto=�8�� #��z��$��Disabled=�#��z��#��z��.���� $6 HeaderPanel��$7 1��8 . $9 1���� $ PART_SelectedContentHost��$��1��$��0��#��z��#��z? $��SelectedContent��#A z@ #��zB # z �� $0 Shadow��8�� $��4��$��4��$��Stretch=�$	��Top=�$��1��$��0�� . L .   P� 	 �@��TabStripPlacement$
 Bottom=�. ��� $ PART_SelectedContentHost�� � ��$ 0���� $ HeaderPanelScroll�� � ��$ 1���� $ RowDefinition0�� � ��$ * ��� $ RowDefinition1�� � ��$
 Auto ��� $ Shadow�� � ��$ Stretch=��� $ Shadow�� � ��$ Bottom=��� $ Shadow�� � ��$ 0���� $ Shadow�� � ��$ 0���� $ Shadow��8 	  � ��P� 	 �@��TabStripPlacement$

 Left=�. ��� $ ColumnDefinition0�� � ��$
 Auto ��� $ ColumnDefinition1�� � ��$ * ��� $ PART_SelectedContentHost�� � ��$ 1���� $ PART_SelectedContentHost�� � ��$ 0���� $ HeaderPanelScroll�� � ��$ 0���� $ HeaderPanelScroll�� � ��$ 0���� $ HeaderPanelScroll�� � ��$ Disabled=��� $ HeaderPanelScroll��# �   � ���� $ HeaderPanelScroll�� � ��$
 Auto=��� $ RowDefinition0�� � ��$ * ��� $ RowDefinition1�� � ��$ 0 ��� $ Shadow�� � ��$
 Left=��� $ Shadow�� � ��$ Stretch=��� $ Shadow�� � ��$ 0���� $ Shadow�� � ��$ 1���� $ Shadow��8 
  � ��P� 	 �@��TabStripPlacement$
 Right=�. ��� $ ColumnDefinition0�� � ��$ * ��� $ ColumnDefinition1�� � ��$
 Auto ��� $ PART_SelectedContentHost�� � ��$ 0���� $ PART_SelectedContentHost�� � ��$ 0���� $ HeaderPanelScroll�� � ��$ 1���� $ HeaderPanelScroll�� � ��$ 0���� $ HeaderPanelScroll�� � ��$ Disabled=��� $ HeaderPanelScroll��# �   � ���� $ HeaderPanelScroll�� � ��$
 Auto=��� $ RowDefinition0�� � ��$ * ��� $ RowDefinition1�� � ��$ 0 ��� $ Shadow�� � ��$ Right=��� $ Shadow�� � ��$ Stretch=��� $ Shadow�� � ��$ 0���� $ Shadow�� � ��$ 0���� $ Shadow��8   � ��