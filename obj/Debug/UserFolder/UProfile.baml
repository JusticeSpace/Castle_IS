   M S B A M L   `   `   `  ����A  =Castle, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullE'clr-namespace:Castle.SomeCode.ConverterCastle.SomeCode.Converter  5           Castle.UserFolder.UProfile   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      _ [MaterialDesignThemes.Wpf, Version=4.5.0.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5      3local'clr-namespace:Castle.SomeCode.Converter  5        N� Title$-  'Профиль пользователя��5
       � 	Resources 5      9   3Castle.SomeCode.Converter.ByteArrayToImageConverter   ByteArrayToImageConverter9   3Castle.SomeCode.Converter.NullToVisibilityConverter  NullToVisibilityConverter  
LabelStyle �� 
TargetType �� Property �� Value  
InputStyle  StaticTextStyle  ImageStatusTextStyle��%�  &	        &	      &	 &     &	     &	 �    &	 �      5   
   6*     5   
   6*   �� 5   
   " ��6#   .�5      ��  � ��$ 20(�6)   �� 5       � ��$1 +pack://application:,,,/Fonts/#Roboto Italic)�6+   �� 5       � �� �#00B4D86+   �� 5       � ��$ 0,8,10,8q�6'   5      �� 5   
   " ��6#   .�5      ��  � ��$ 20(�6)   �� 5       � ��$2 ,pack://application:,,,/Fonts/#Roboto Regular)�6+   �� 5       � ��$ 0,8,0,8q�6'   �� 5       � ��$	 250��6&   5      �� 5   
   " ��6(   .�5      ��  � ��$ 20(�6)   �� 5       � ��$2 ,pack://application:,,,/Fonts/#Roboto Regular)�6+   �� 5       � ��$ 0,8,0,8q�6'   5      �� 5   
   " ��6-   .�5       ��  � ��$ 20(�6)   �� 5!       � ��
 �����6+   �� 5"       � ��$ Center=�64   �� 5#       � ��$ Center=�62   �� 5$       � ��
 �����6+   �� 5%       �@J�Opacity$	 0.8Q�6(   5&      5'      .��5(      � ��5*   
   �� 5+       �� 
StartPoint$	 0,0&�6"    �� EndPoint$	 1,1&�63   .��5,      �  � Color$ #CAF0F8��6    � Offset$ 0Q�6/   � 5-      $ #E0F7FF��6   $ 1Q�6/   5.      	 � RowDefinitions	 50   
   �� 51      $
��Auto �6   �� 52      $��* �6   �� 53      $
��Auto �6   .+�56   
   �� $-��'Профиль пользователя��6   $��40(�57      $6��0pack://application:,,,/Fonts/#Roboto Bold Italic)�58      ���#00B4D8$��Center=�5:      $��	0,20,0,20q�5;      $��0��5<      �� 5>   
   $��1��6   $��Center=�6"   $��Center=�6?   $	��900��6Z   .+�5@      #  MaterialDesignThemes.Wpf.Card  $��15q�6"   $��0,0,0,30q�6/   +  %MaterialDesignThemes.Wpf.ShadowAssist
   ShadowDepth$
 Depth2=�5A   "   $��Center=�5B   "   .��5C      �� .+�5D      ��  �� CornerRadius$ 90��6   $	��180��6/   $	��180��6;   
�������}�. ._�5E      � .+�5F      �� -   5         ��Name$ 	UserImage��5F   $   $��Uniform=�67   �� 5G      -   5         ��Name$ ImageStatusText��5G   (   $��Нет фото��5H   (   #��[ 5I   (   y�5J   (   ��  �� ElementName$ 	UserImage�� �� Path$ Source� �� 	Converter# [  �� ConverterParameter$ VisibleWhenNull��5K      5L      �� 5M      -   5        $!��Загрузить фото��5M        MaterialDesignFlatButton#��[ 5O      $��0,15,0,0q�5P      $��16(�5Q      $��Center=�5R      5S      5T        5V      $��30q�6"   $��0,0,0,20q�6/   $
 Depth2=�5W   "   .��5X      �� .+�5Y      �� $,��&Информация о профиле��6    $��28(�5Z       $6��0pack://application:,,,/Fonts/#Roboto Bold Italic)�5[       ���#00B4D8$��0,0,0,20q�5]       $��Center=�5^       � 5_       � ColumnDefinitions 5`      �� 5a      $��* �6/   �� 5b      $��* �6/   .+�5d      �� $��0��6%   $��0,0,20,0q�65   .+�5e      ��  �� Orientation$ 
Horizontal=�6)   .+�5f   "   '  !MaterialDesignThemes.Wpf.PackIcon     Kind$ 
Identifier=�6:   $��0,0,10,0q�6L   ���#00B4D8$��Center=�6s   �� 5g   "   $"��ID пользователя:��6,   #��[ 6D   �� 5h   "   #��[ 6D   ��6,   �� *IdUser+6D   5i      �� 5j      $ 
Horizontal=�6)   .+�5k   "     $ Account=�6:   $��0,0,10,0q�6I   ���#00B4D8$��Center=�6p   �� 5l   "   $��Логин:��6,   #��[ 6:   �� 5m   "   #��[ 6C   ��6,   �� *Login+6C   5n      �� 5o      $ 
Horizontal=�6)   .+�5p   "     $ ShieldAccount=�6:   $��0,0,10,0q�6O   ���#00B4D8$��Center=�6v   �� 5q   "   $��	Роль:��6,   #��[ 69   �� 5r   "   #��[ 6L   ��6,   �� *Roles.RoleName+6L   5s      �� 5t      $ 
Horizontal=�6)   .+�5u   "     $ AccountOutline=�6:   $��0,0,10,0q�6P   ���#00B4D8$��Center=�6w   �� 5v   "   $��Имя:��6,   #��[ 68   �� 5w   "   #��[ 6i   ��6*   �� *
UserName+ �� UpdateSourceTrigger$ PropertyChanged=�6i   5x      5y      �� 5z      $��1��6%   .+�5{      �� $ 
Horizontal=�6)   .+�5|   "     $ AccountOutline=�6:   $��0,0,10,0q�6P   ���#00B4D8$��Center=�6w   �� 5}   "   $��Фамилия:��6,   #��[ 6<   �� 5~   "   #��[ 6h   ��6*   �� *	Surname+$ PropertyChanged=�6h   5      �� 5�      $ 
Horizontal=�6)   .+�5�   "     $ AccountOutline=�6:   $��0,0,10,0q�6P   ���#00B4D8$��Center=�6w   �� 5�   "   $��Отчество:��6,   #��[ 6=   �� 5�   "   #��[ 6k   ��6*   �� *
Patronymic+$ PropertyChanged=�6k   5�      �� 5�      $ 
Horizontal=�6)   .+�5�   "     $ Email=�6:   $��0,0,10,0q�6G   ���#00B4D8$��Center=�6n   �� 5�   "   $��Эл. почта:��6,   #��[ 6>   �� 5�   "   #��[ 6f   ��6*   �� *Email+$ PropertyChanged=�6f   5�      5�      5�      5�      5�      5�      �� 5�   
   $��2��6   $ 
Horizontal=�6"   $��Right=�6;   $��	0,0,30,30q�6W   .+�5�      �� -   5        $!��Сменить пароль��5�        MaterialDesignRaisedButton#��[ 5�      $	��200��5�      $��50��6!   $��18(�5�      $��0,0,15,0q�5�      �� 5�      -   5        $+��%Сохранить изменения��5�      #��[ 5�      $	��200��5�      $��50��6!   $��18(�5�      5�      5�      5�      