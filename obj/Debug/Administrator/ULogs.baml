   M S B A M L   `   `   `  ����A  =Castle, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;"clr-namespace:Castle.AdministratorCastle.Administrator  5           Castle.Administrator.ULogs   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      4local"clr-namespace:Castle.Administrator     5      _ [MaterialDesignThemes.Wpf, Version=4.5.0.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5        N� Title$  ULogs��5
      .��5      � ���#CAF0F8 � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $��* �6   �� 5      $
��Auto �6   .+�5   
   �� $%��Журнал изменений��6   $1��+pack://application:,,,/Fonts/#Roboto Italic)�5      $��30(�5      $��Center=�5      $��	0,20,0,20q�5      &   System.Windows.Controls.DataGrid  5   
   -   5          Name$ DGLogs��5      $��1��5         AutoGenerateColumns .  $��20q�5         RowBackground �#E0F7FF   AlternatingRowBackground �#FFFFFF   CanUserAddRows .     CanUserDeleteRows .     Columns 5#      0  *System.Windows.Controls.DataGridTextColumn  5$      ,  &System.Windows.Controls.DataGridColumn	   Header$	 ID��6%   
   Width5  /System.Windows.Controls.DataGridLengthConverter$	
 100 6H      
IsReadOnly . 1  +System.Windows.Controls.DataGridBoundColumn   Binding 61   �� *Id+6T     5%      $	 Пользователь��6%   $	
 150 6Z    .  6;   �� *
User.Login+6f     5&      $	 Действие��6%   $
 * 6R    .  67   �� *Action+6\     5'      $	 Подробности��6%   $
 * 6V    .   6:   �� *	Details+6`     5(      $	 Дата��6%   $	
 150 6t    .  63   �� *Date+ �� StringFormat$ {0:dd.MM.yyyy HH:mm:ss}��6�   5*      #  MaterialDesignThemes.Wpf.Card  5-   
   $��2��6   $��20,10,20,20q�6+   $��15q�6@   .��5.      ��  �� Orientation$ 
Horizontal=�6   $��Right=�62   .+�5/      �� -   5           MaterialDesignRaisedButton#��[  5/   3   $��0,0,10,0q�6g   ��50      �� 51      $ 
Horizontal=�6%   .+�52      '  !MaterialDesignThemes.Wpf.PackIcon     Kind$ ContentSave=�66   $��0,0,8,0q�6I   �� 53      $+��%Сохранить изменения��6(   $1��+pack://application:,,,/Fonts/#Roboto Italic)�6C   $��16(�6|   54      56      �� 57      -   5        #��[  57   /   ��58      �� 59      $ 
Horizontal=�6%   .+�5:        $ Refresh=�66   $��0,0,8,0q�6E   �� 5;      $��Обновить��6(   $1��+pack://application:,,,/Fonts/#Roboto Italic)�68   $��16(�6q   5<      5>      5?      5@      5A      5B      