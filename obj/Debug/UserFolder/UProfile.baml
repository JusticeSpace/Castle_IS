   M S B A M L   `   `   `  ����A  =Castle, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullE'clr-namespace:Castle.SomeCode.ConverterCastle.SomeCode.Converter  5           Castle.UserFolder.UProfile   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      _ [MaterialDesignThemes.Wpf, Version=4.5.0.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5      3local'clr-namespace:Castle.SomeCode.Converter  5        N� Title$-  'Профиль пользователя��5
       � 	Resources 5      9   3Castle.SomeCode.Converter.ByteArrayToImageConverter   ByteArrayToImageConverter  
LabelStyle �� 
TargetType �� Property �� Value  
InputStyle  StaticTextStyle��%�  &	        &	      &	 �     &	 �      5   
   6*   �� 5   
   " ��6#   .�5      ��  � ��$ 20(�6)   �� 5       � ��$1 +pack://application:,,,/Fonts/#Roboto Italic)�6+   �� 5       � �� �#00B4D86+   �� 5       � ��$ 0,5,10,5q�6'   5      �� 5   
   " ��6#   .�5      ��  � ��$ 20(�6)   �� 5       � ��$2 ,pack://application:,,,/Fonts/#Roboto Regular)�6+   �� 5       � ��$ 0,5,0,5q�6'   5      �� 5   
   " ��6(   .�5      ��  � ��$ 20(�6)   �� 5       � ��$2 ,pack://application:,,,/Fonts/#Roboto Regular)�6+   �� 5       � ��$ 0,5,0,5q�6'   5      5      .��5       � $��10q�6    � RowDefinitions 5!   
   �� 5"      $
��Auto �6   �� 5#      $��* �6   �� 5$      $
��Auto �6   .+�5(   
   �� $-��'Профиль пользователя��6   $��36(�5)      $6��0pack://application:,,,/Fonts/#Roboto Bold Italic)�5*      ���#00B4D8$��Center=�5,      $��0,0,0,10q�5-      � 50   
   $��1��6    � ColumnDefinitions 51      �� 52      $
��Auto �6#   �� 53      $��* �6#   .+�57      #  MaterialDesignThemes.Wpf.Card  $��0��6"   $��0,0,10,0q�62   $��10q�6D   .��58      �� .+�59      ��  �� CornerRadius$ 50��6   $��80��6/   $��80��6:   ���#00B4D8$��2q�6\   ._�5:      � .+�5;      �� -   5         ��Name$ 	UserImage��5;   $   $��UniformToFill=�67   �� 5<      -   5        	 ��Name$	 ImageStatusText��5<   (   $��Нет фото��5=   (   $��12(�5>   (   
�������$��Center=�5@   (   $��Center=�5A   (   $y�Visible=�5B   (   5C      5D      �� 5E      -   5        $!��Загрузить фото��5E        MaterialDesignFlatButton#��[ 5G      $��0,5,0,0q�5H      $��12(�5I      $��Center=�5J      5K      5L        5O      $��1��6"   $��15q�62   .��5P      �� .+�5Q      �� $,��&Информация о профиле��6    $��24(�6<   $6��0pack://application:,,,/Fonts/#Roboto Bold Italic)�6J   ���#00B4D8$��0,0,0,5q�6�   �� 5R      ���#00B4D8$��1��65   $��0,0,0,5q�6@   �� 5S      
 �� Orientation$
 
Horizontal=�6!   .+�5T      �� $!��ID пользователя��6$   #��[ 6;   �� 5U      #��[ 6<   ��6$   �� *IdUser+6<   5V      �� 5W      $
 
Horizontal=�6!   .+�5X      �� $��
Логин��6$   #��[ 61   �� 5Y      #��[ 6;   ��6$   �� *Login+6;   5Z      �� 5[      $
 
Horizontal=�6!   .+�5\      �� $��Роль��6$   #��[ 60   �� 5]      #��[ 6D   ��6$   �� *Roles.RoleName+6D   5^      �� 5_      $
 
Horizontal=�6!   .+�5`      �� $��Имя��6$   #��[ 6/   �� 5a      #��[ 6a   ��6"   �� *
UserName+ �� UpdateSourceTrigger$ PropertyChanged=�6a   5b      �� 5c      $
 
Horizontal=�6!   .+�5d      �� $��Фамилия��6$   #��[ 63   �� 5e      #��[ 6`   ��6"   �� *	Surname+$ PropertyChanged=�6`   5f      �� 5g      $
 
Horizontal=�6!   .+�5h      �� $��Отчество��6$   #��[ 64   �� 5i      #��[ 6c   ��6"   �� *
Patronymic+$ PropertyChanged=�6c   5j      �� 5k      $
 
Horizontal=�6!   .+�5l      �� $'��!Электронная почта��6$   #��[ 6=   �� 5m      #��[ 6^   ��6"   �� *Email+$ PropertyChanged=�6^   5n      5o      5p      5q      �� 5t   
   $��2��6   $
 
Horizontal=�6"   $��Right=�6;   $��0,10,0,0q�6W   .+�5u      �� -   5        $!��Сменить пароль��5u        MaterialDesignRaisedButton#��[ 5w      $	��180��5x      $��40��6!   $��16(�5y      $��0,0,10,0q�5z      �� 5{      -   5        $+��%Сохранить изменения��5{      #��[ 5}      $	��180��5~      $��40��6!   $��16(�5      5�      5�      5�      