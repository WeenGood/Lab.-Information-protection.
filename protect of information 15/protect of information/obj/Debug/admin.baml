   M S B A M L   `   `   `  ����Q  Mprotect of information, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null?$clr-namespace:protect_of_informationprotect_of_information  5   	   "    protect_of_information.admin   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   6local$clr-namespace:protect_of_information     5   	   -   5          � Title$+  %Окно администратора��5   	   $��463.18��6%   $��823.513��65   .��5	      � -   5         � ColumnDefinitions 5
   
   �� 5      $
��142* �6   �� 5      $
��129* �6   �� 5      $
��267* �6   .+�5   
   &   System.Windows.Controls.DataGrid  -   5          Name$ 
myDataGrid��5      $
��Left=�6'   $	��399��6B   $��
115,10,0,0q�6O   $	��Top=�6c   $	��467��6{      SelectionUnit$
 Cell=�6�   $��1��6�    J� IsHitTestVisible . $��2��6  �� 5   
   -   5         ��Name$
 newB��5      $��Добавить��6   $
��Left=�62   $��
91,295,0,0q�6M   $	��Top=�6a   $��75��6y   $��2��6�   �� 5   
   -   5        $ 	changePwd��5      $!��Сменить пароль��6$   $
��Left=�6=   $��
31,210,0,0q�6X   $	��Top=�6l   $	��108��6�   6�   �� 5   
   -   5        $��
Выход��5      $
��Left=�6!   $��
10,394,0,0q�6<   $	��Top=�6P   $��75��6h   6s   �� 5   
   -   5         ��Name$ codeEnc��5      $��2��6#   $
��Left=�67   $��23��6R   $��	10,43,0,0q�6^    �� TextWrapping$
 Wrap=�6q   $	��Top=�6�   $	��242��6�   �� 5   
   -   5        $
 encB��5      $��Сохранить��6   $
��Left=�63   $��	68,71,0,0q�6N   $	��Top=�6a   $	��122��6y   $��2��6�   �� 5   
   $-��'Новый код шифрования:��6   $
��Left=�60   $��	10,12,0,0q�6K   $	��Top=�6^   $��2��6v   $	��180��6�   5      5      