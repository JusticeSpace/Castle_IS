   M S B A M L   `   `   `  ����
  Castle#    Castle.UserFolder.AddSupplier   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   _ [MaterialDesignThemes.Wpf, Version=4.5.0.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5   	     � Title$+  %Добавить поставщика��5   	   $	��250��6%   $	��400��62   .��5	      � $��20q�6    � RowDefinitions 5
   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   .+�5   
   �� $��0��6   )  #MaterialDesignThemes.Wpf.HintAssist   Hint$+ %Название поставщика��6   $
��0,10q�5      $��16(�6    ��5      �� *SupplierName+ �� UpdateSourceTrigger$ PropertyChanged=�5       �� 5   
   $��1��6   $G AКонтактная информация (телефон, email)��6   $
��0,10q�5      $��16(�6    ��5      �� *ContactInfo+$ PropertyChanged=�5       �� 5   
   -   5        $��2��5      $��Сохранить��6      MaterialDesignRaisedButton#��[  5      $��0,20,0,0q�6E   $	��150��5      $��40��6   $��Center=�6)   5      5      