   M S B A M L   `   `   `  ����
  Castle    Castle.Manager.AddWorker   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   _ [MaterialDesignThemes.Wpf, Version=4.5.0.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5   	     � Title$)  #Добавить работника��5   	   $	��600��6$   $	��500��61   .��5	      �� $
��Auto=�6   .��5
   
   � $��20q�6    � RowDefinitions 5      �� 5      $
��Auto �6    �� 5      $
��Auto �6    �� 5      $
��Auto �6    �� 5      $
��Auto �6    �� 5      $
��Auto �6    �� 5      $
��Auto �6    �� 5      $
��Auto �6    �� 5      $
��Auto �6    .+�5      �� $��0��6   )  #MaterialDesignThemes.Wpf.HintAssist   Hint$ 
Логин��6#   $
��0,10q�5      $��16(�6$   ��5      �� *Login+ �� UpdateSourceTrigger$ PropertyChanged=�5   $   �� 5      $��1��6   $ Пароль��6#   $
��0,10q�5      $��16(�6$   ��5      �� *
Password+$ PropertyChanged=�5   $   �� 5!      $��2��6   $ Фамилия��6#   $
��0,10q�5#      $��16(�6$   ��5"      �� *	Surname+$ PropertyChanged=�5#   $   �� 5&      $��3��6   $ Имя��6#   $
��0,10q�5(      $��16(�6$   ��5'      �� *
UserName+$ PropertyChanged=�5(   $   �� 5+      $��4��6   $3 -Отчество (необязательно)��6#   $
��0,10q�5-      $��16(�6$   ��5,      �� *
Patronymic+$ PropertyChanged=�5-   $   �� 50      $��5��6   $D >Электронная почта (необязательно)��6#   $
��0,10q�52      $��16(�6$   ��51      �� *Email+$ PropertyChanged=�52   $   �� 55      -   5         ��Name$ RoleComboBox��56      $��6��55      $ Роль��6$   $��	0,10,0,20q�56   -   $��16(�6@    �� DisplayMemberPath$ RoleName��57       �� SelectedValuePath$ RoleID��64    �� SelectedValue 58      �� *RoleID+$ PropertyChanged=�� 5;      $��7��6   $
��0,10q�6     � ColumnDefinitions 5<      �� 5=      $��* �6'   �� 5>      $
��Auto �6'   �� 5?      $��* �6'   .+�5A      �� $��1��6   	 �� Orientation$	 Vertical=�6-   $��Center=�6D   .+�5B      �� -   5        
 ��Name$
 LoadImageButton��5C   O   $!��Загрузить фото��5B         MaterialDesignFlatButton#��[  5C      6O   �� 5D      -   5         ��Name$ ImageStatusText��5D   7   $"��Фото не выбрано��6    $��Center=�6P   $��0,5,0,0q�6m   
�������$��Italic$�5E   2   �� 5F      
�������$��1q�65    �� CornerRadius$ 5��6I   $��0,5,0,0q�6Z   ._�5G      �� -   5         ��Name$ WorkerImage��5G       $	��100��65   $	��100��6A   $��Uniform=�6N   $y�	Collapsed=�6`   5H      5I      5J      �� 5M      -   5        $��8��5M      $��Сохранить��6"     MaterialDesignRaisedButton#��[ 5N      $��	0,150,0,0q�6I   $	��150��5O      $��40��6!   $��Center=�6-   5P      5Q      5R      