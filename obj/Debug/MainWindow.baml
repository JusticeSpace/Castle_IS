   M S B A M L   `   `   `  ����
  Castle    Castle.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   _ [MaterialDesignThemes.Wpf, Version=4.5.0.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5   	   $	��773��5   	   $	��576��6     � WindowStartupLocation$  CenterScreen=�6"    � AllowsTransparency . 
������  � WindowStyle$
 None=�5   <    � 
ResizeMode$ NoResize=�6O   .��5      �� -   5        ���#FFFFFF �� CornerRadius$ 25��5   "   $��3q�6[   ._�5   
   �  � RowDefinitions 5	      �� 5
      $
��Auto �6    �� 5      $��* �6    �� 5      $
��Auto �6    �� 5      $
��Auto �6    �� 5      $
��Auto �6    �� 5      $��* �6    .+�5      �� $��0��6    �� Orientation$ 
Horizontal=�6&   $��Right=�6?   $��	0,10,10,0q�6[   .+�5      �� -   5           MaterialDesignFlatButton#��[  5      .��5      '  !MaterialDesignThemes.Wpf.PackIcon     Kind$ WindowMinimize=�6.   $��24��6D   $��24��6O   
�������6[   5      �� 5      -   5        #��[  5      $��5,0,0,0q�6e   .��5        $ Close=�6.   $��24��6;   $��24��6F   
�������6R   5      5       �� 5#      $��/Images/image.png.jpeg��6   $��2��64   $��90��6A   $��90��6M   $��Center=�6X   $��0,0,0,20q�6u   �� 5&      $��Авторизация��6   $��34(�6+   $��Center=�69   $��3��6V   $��0,0,0,20q�6c     PrimaryHueMidBrush#��[ 5'      �� 5*      $��4��6   $��Center=�6&   $��Center=�6C   .+�5+      �� -   5         ��Name$ Login��5+       % !MaterialDesignFloatingHintTextBox#��[ 6)   )  #MaterialDesignThemes.Wpf.HintAssist	   Hint$	 
Логин��5,      $	��300��6A   $��0,0,0,20q�6M   $��15(�6_   � 5.      $	��300��6   $��0,0,0,20q�6#   
 � ColumnDefinitions
 5/      �� 50      $��* �6+   �� 51      $
��Auto �6+   .+�54      � $��0��6   .+�55      G� -   5         G�Name$ Password��55   &    ) %MaterialDesignFloatingHintPasswordBox#��[ 68   $	 Пароль��56   &   $��15(�6N   $y�Visible=�6\   6q   �� 57      -   5        $ 
VisiblePas��57   "   #��[ 66   $	 Пароль��58   "   $��15(�6J   $y�	Collapsed=�6X   59      �� 5:      -   5        $��1��5:        MaterialDesignIconButton#��[ 6-   $��30��6_   $��30��6j   $��5,0,0,0q�5;   D   .��5<        -   5          Name$ PasswordEyeIcon��5<   2   $	 Eye=�6K   $��20��6V   $��20��6a   5=      5>      �� 5?      -	   5        $��4��5?      $��
Войти��6&   $��	0,20,0,10q�6K     MaterialDesignRaisedButton#��[ 5@      $	��200��6E   $��16(�6Q    �� 	IsDefault . 6_   5A      5B      5C      5D      