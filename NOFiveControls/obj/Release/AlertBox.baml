   M S B A M L   `   `   `  ���� I  ENOFiveControls, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:NOFiveControlsNOFiveControls      NOFiveControls.AlertBox   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     .localclr-namespace:NOFiveControls       � SizeToContent$  WidthAndHeight=� � WindowStyle$
 None=�$	��250�� � AllowsTransparency .  � ShowInTaskbar .   � 
ResizeMode$ NoResize=�$��0q���Z�  � 	Resources    	ShadowGSC � Color � Offset  FocusVisual �� Property	 �� Value
 J� SnapsToDevicePixels �� StrokeDashArray  Item.MouseOver.Background �� Color  Item.Selected.Background  ListBoxItemStyle1 �� 
TargetType �� 
TargetType ��Name �� Padding �� Triggers P� Property P� Value �� 
TargetName  	Blue.Fill �� A �� R �� G �� B  LowBlue.Fill  Red.Fill  LowRed.Fill 	 
Blue.Brush 
 	Red.Brush  
TitleStyle  CircleButtonStyle01 �� CornerRadius'  !System.Windows.VisualStateManager   VisualStateGroups%  System.Windows.VisualStateGroup   States   System.Windows.VisualState   
Storyboard �� 
TargetName �� TargetProperty9  3System.Windows.Media.Animation.EasingDoubleKeyFrame O� KeyTime  O� Value! J� RenderTransformOrigin��%�  &	        &	 X     &	 �     &	 �     &	 �     7  7  7  &	 �    &	 �    &	     &	 H    &		 t    7  &	
 ~    7  '�� �    &	 �    &	 �    � � $ #e2e2e2��$ 0Q�� $ #dfdfdf��$	 0.3Q�� $ #bfbfbf��$ 1Q��� .���  � ��	 �� .R��� $��2q�
 . #��� ��$��1��$	 1 2R��� $ #F3BE3F���� $ #F3D13F���� " ��.���  �@J�SnapsToDevicePixels	 . ��  � ��
	 �������  � ��$	 0,0,0,0q���  � ��$	 30����  � ��$	 1,1,1,0q���  � ��
	 �������  �@�FocusVisualStyle8	   ��  � ��	 �� " ��.R��� $ Bd��#��z��#��z��#��z��# z��
 . ._��� $��Center=�#
 z
 #��z�� P�  �@��
IsSelected . . ��� $ Bd��8	   � ��P�  �@J�IsMouseOver . . ��� $ Bd��8	   � ��P�  � {� .  . ��� $ Bd��#	 � �� � ���� $	 255��$ 53��$	 127��$	 217���� $	 255��$ 77��$	 142��$	 221���� $	 255��$	 205��$ 63��$ 80���� $	 255��$	 211��$ 86��$	 101���� 8   �� 8   �� " ��.���  � ��$
	 Wrap=��� " ��.���  � ��$	 CharacterEllipsis=��� " ��.���  � ��
	 ���� ��  � ��$	 0q���  � ��$	 0q���  � ��	 �� " ��.R��� $ 20��#��z��   CommonStates��.   
Normal��. �� .��U� $ ScaleTransform��$ ScaleX�.\�  $ 0:0:0.1��$  1Q�U� $ ScaleTransform��$ ScaleY�.\�  $ 0:0:0.1��$  1Q�  	MouseOver��. �� .��U� $ ScaleTransform��$ ScaleX�.\�  $ 0:0:0.1��$	  1.2Q�U� $ ScaleTransform��$ ScaleY�.\�  $ 0:0:0.1��$	  1.2Q�  Pressed��  Disabled��._��� $! .5,.5&�z��� ScaleTransform������    NOFiveControls.AlertBox"  .R�� " �Name$" 	PART_ROOT��$	��356��$! 0.5,0.5&�z���    .   Loading��. �� # g� DesiredFrameRate$# 60X�.��W� $ PART_Content��$ Opacity�	$ W� To$$ 0X�% g� Duration$% 0D�U� $ 	PART_ROOT��$ Height�.\�n� $ 0��$	  160Q�U� $ 	PART_ROOT��$9 3(UIElement.RenderTransform).(ScaleTransform.ScaleX)�.\�n� $ 0��$	  0.4Q�  $ 0:0:0.2��$  1Q�U� & g� 	BeginTime$& 0:0:0.25X�$ 	PART_ROOT��$ Height�.\��� $ 0:0:0.2��$	  250Q�W� $& 0:0:0.45X�$ PART_Content��$ Opacity�$$ 1X�$% 0:0:0.2D�  	UnLoading��. �� .��U� $ 	PART_ROOT��$ Height�.\��� $ 0:0:0.2��$	  160Q�U� ' U�Name$' UnLoadingStoryboard��$& 0:0:0X�$ 	PART_ROOT��$ Opacity�.\�  $ 0:0:0.3��$  0Q�.+��� $��3q����#e2e2e2( J� Effect( 3  -System.Windows.Media.Effects.DropShadowEffect  )   ShadowDepth$) 0Q�*   
BlurRadius$* 6Q�+   RenderingBias$+ Performance=�,   	Direction$	, 270Q�-   Opacity$
- 0.55Q�.   Color$. 	LightGray��� $��3q�/ � RowDefinitions/ �� $��68 ��� �� $��68��$
��Auto �.+�� $" PART_TitleArea��
�������$��68��.+��� 0 ��Name$0 
PART_Title��$��20(�
���iii�$��DemiBold"�$��Center=�$��Center=�#��[ 1   
AlertTitle#��z1 �� $��8��	2 �� X1$2 0��	3 �� X2$	3 356��	4 �� Y1$4 1��	5 �� Y2$5 1��$��Bottom=����� 6 �� 
StartPoint$	6 0,1&�7 �� EndPoint$	7 0,0&�#��[  �� 8 ��Name$8 PART_ContentLayer��$	��356��$��1��$��98��$��Disabled=�$
��Auto=�9 %� IsVirtualizing9 . .���� : ��Name$: PART_Content��$��16(�
���iii�$��DemiBold"�$��Center=�$��Center=�;   AlertContent#��z; �� < ��Name$< PART_BottomCanvas��
�������$	��356��$��78��$��2��= J� 	CacheMode= &   System.Windows.Media.BitmapCache  >   EnableClearType> . ?   RenderAtScale$	? 1.0Q�.+�� @ �Name$@ PART_OKMenuPopup��A � 	StaysOpenA .  B � AllowsTransparencyB . $	��Top=�C � HorizontalOffset$	C -40��$
��Fade=�D � PlacementTargetD �� E �� ElementName$E 
PART_OKBtn��.��� / �� �� $��15 �.+�� F � Points7F �   �������, ����� ���@KL @KL $��1�����#eaeaea(   $) 0Q�$* 6Q�$
- 0.75Q�$. 	#FFbfbfbf���� G ��Name$G PART_OkMenusLayer��$��0,0,0,0q�$	��120��$��Disabled=�$��Disabled=�H   OKMenus#��zH $��Bottom=�#���  ��Z� ���� .R��� $��14(�$��SemiBold"�
���iii����� �� I ��Name$I 
PART_OKBtn��$��40��$��40��J �� Left$J 70��K �� Bottom$K 15��L   OkCancelButtonVisibility#y�zL #��[ .��F� #��[	 $��40��$��40����� .��@� M @� Center$M 20,20&�N @� RadiusX$N 19Q�O @� RadiusY$O 19Q�@� $M 20,20&�$N 18Q�$O 18Q�@� $M 20,20&�$N 16Q�$O 16Q�@� $M 20,20&�$N 10Q�$O 10Q�@� $M 20,20&�$N 7Q�$O 7Q��� $I PART_CancelBtn��$��40��$��40��P �� Right$P 70��$K 15��#y�zL #��[ .��F� #��[
 $��40��$��40����� .��@� $M 20,20&�$N 19Q�$O 19Q�@� $M 20,20&�$N 18Q�$O 18Q�@� $M 20,20&�$N 16Q�$O 16Q�B� .��D� E� .��� Q � Points�Q �    � ��� ���  �  -1@f@x} �  ?���� ��^ -1 ?�@x}@x} ?� -1��^���  ?� � @x}@f -1 � ��� �� $��8��$2 0��$	3 356��$4 1��$5 1��$��Bottom=����� $	6 0,0&�$	7 0,1&�#��[   P�   �@ OkCancelButtonVisibility$ 	Collapsed=�. ��� $ PART_BottomCanvas�� � ��$	 68��