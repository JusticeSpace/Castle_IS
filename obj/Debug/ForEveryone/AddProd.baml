   M S B A M L   `   `   `  ����
  Castle     Castle.ForEveryone.AddProd   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   _ [MaterialDesignThemes.Wpf, Version=4.5.0.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5   	     � Title$!  Добавить товар��5   	   $	��600��6    $	��500��6-   .��5	      �� $
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
��Auto �6    .+�5      �� $��0��6   )  #MaterialDesignThemes.Wpf.HintAssist   Hint$# Название товара��6#   $
��0,10q�5      $��16(�6$   ��5      �� *ProductName+ �� UpdateSourceTrigger$ PropertyChanged=�5   $   � 5      $��1��6   $
��0,10q�6    .+�5      �� -   5         ��Name$ PriceTextBox��5      $�� ��5      $��16(�5      5"      �� 5#      -   5         ��Name$ 	PriceHint��5#      $��Цена��5$      $��16(�5%      
������� J� IsHitTestVisible .  $��5,0,0,0q�5(      $y�Visible=�5)      5*      � 5-      $��2��6   $
��0,10q�6    .+�5.      �� -   5        $ QuantityTextBox��5.      $�� ��5/      $��16(�50      53      �� 54      -   5        $ QuantityHint��54      $��Количество��55      $��16(�56      
������� .  $��5,0,0,0q�59      $y�Visible=�5:      5;      �� 5>      -   5         ��Name$ CategoryComboBox��5?      $��3��5>      $ Категория��6$   $
��0,10q�5?   1   $��16(�6?    �� DisplayMemberPath$ CategoryName��5@      	 �� SelectedValuePath$	 
CategoryID��68   
 �� SelectedValue
 5A      �� *
CategoryID+$ PropertyChanged=��� 5D      -   5        $ SupplierComboBox��5E      $��4��5D      $ Поставщик��6$   $��	0,10,0,20q�5E   1   $��16(�6D   $ SupplierName��5F      $	 
SupplierID��68   
 5G      �� *
SupplierID+$ PropertyChanged=��� 5J      $��5��6   $��	0,20,0,10q�6&   .+�5K      �� $��Комментарий��6   $��14(�6/   $
��Bold"�6=   �� 5L      $" (необязательно)��6   $��16(�5N       �� TextWrapping$
 Wrap=�6(    �� AcceptsReturn . $��80��6Q   ��5M      �� *	Comment+$ PropertyChanged=�5N   Q   5O      � 5R      $��6��6   $
��0,10q�6     � ColumnDefinitions 5S      �� 5T      $��* �6'   �� 5U      $
��Auto �6'   �� 5V      $��* �6'   .+�5X      �� $��1��6    �� Orientation$ Vertical=�6-   $��Center=�6D   .+�5Y      �� -   5         ��Name$ LoadImageButton��5Z   O   $!��Загрузить фото��5Y         MaterialDesignFlatButton#��[  5Z      6O   �� 5[      -   5        $ ImageStatusText��5[   7   $"��Фото не выбрано��6    $��Center=�6P   $��0,5,0,0q�6m   
�������$��Italic$�5\   2   �� 5]      
�������$��1q�65    �� CornerRadius$ 5��6I   $��0,5,0,0q�6Z   ._�5^      �� -	   5         ��Name$ ProductImage��5^       $	��100��66   $	��100��6B   $��Uniform=�6O   $y�	Collapsed=�6a   5_      5`      5a      �� 5d      -
   5        $��7��5d      $��Сохранить��6"     MaterialDesignRaisedButton#��[ 5e      $��0,20,0,0q�6I   $	��150��5f      $��40��6!   $��Center=�6-   5g      5h      5i      