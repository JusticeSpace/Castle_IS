   M S B A M L   `   `   `  ����
  Castle,    &Castle.UserFolder.ChangePasswordWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   _ [MaterialDesignThemes.Wpf, Version=4.5.0.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5   	     � Title$!  Сменить пароль��5   	   $	��350��6    $	��450��6-   .��5      � $��20q�6    � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5	      $
��Auto �6   �� 5
      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   .+�5   
   �� $��Старый пароль��6   $��0��6)   $��16(�66   $��0,0,0,5q�6D   � 5   
   $��1��6    � ColumnDefinitions 5      �� 5      $��* �6#   �� 5      $
��Auto �6#   .+�5      G� -   5         G�Name$ OldPassword��5      $��0��6/   $��16(�6?   $��0,0,5,0q�6M   �� 5      -   5        $��1��5         MaterialDesignIconButton#��[  6%   $��30��6W   $��30��6b   $��5,0,0,0q�5   ?   .��5      '  !MaterialDesignThemes.Wpf.PackIcon  -   5          Name$ OldPasswordEyeIcon��5   *      Kind$	 Eye=�6F   $��20��6Q   $��20��6\   5      �� 5      -   5         ��Name$ VisibleOldPassword��5      $��0��62   $��16(�6B   $��0,0,5,0q�6P   $y�	Collapsed=�6a   5      �� 5   
   $��Новый пароль��6   $��2��6(   $��16(�65   $��0,10,0,5q�6C   G� 5   
   -   5        $ NewPassword��5      $��3��6+   $��16(�68   $��0,0,5,0q�6F   �� 5   
   -   5        $��Сменить��5      $��4��6@   $��Right=�6M   $��0,20,0,0q�6i     MaterialDesignRaisedButton#��[ 5      $	��100��6E   $��40��6Q   5      5       