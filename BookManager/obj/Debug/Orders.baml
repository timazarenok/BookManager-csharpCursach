   M S B A M L   `   `   `  ����F  BBookManager, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:BookManagerBookManager  5   	       BookManager.Orders   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   +localclr-namespace:BookManager     5   	     � Title$  Orders��5   	   $	��450��6   $	��800��6%   .��5	      � .+�5
   
   �� $��Center=�6   $	��Top=�6-   .��6E   Заказы6M   (  "System.Windows.Controls.DatePicker  5   
   -   5          Name$ SearchByDate��5      $	��200��6+   $��25��6n   $	��Top=�6z   $��Center=�6�   $��
500,40,0,0q�6�   �� 5   
   -   5         ��Name$ ShowAll��5      $	��120��6"   $��25��6.   $��Center=�6:   $	��Top=�6W   $��0,40,0,0q�6o   $��Показать все��6�   6�   �� 5   
   -   5         ��Name$ 	OrdersAll��5      $��10,50,10,10q�6%   $��Center=�6e   $
��Left=�6�   $	��300��6�   $	��300��6�   ��5      �� 5      .R�5      �  � ColumnDefinitions 5      �� 5      �� 5       � RowDefinitions 5      �� 5      �� 5      .+�5      �� $��1��6:   $
��Bold"�6G   $��0,0,0,10q�6Y   ��6$   �� *Date+6Y   �� 5      $��
Итог: ��6$   $��0��62   $��	10,0,0,10q�6B   �� 5      $��1��6<   $
��Bold"�6L   $��0,0,0,10q�6^   ��6$   �� *Result+6^   5      5      5      &   System.Windows.Controls.DataGrid  5    
   -   5          Name$ OrderProducts��5          AutoGenerateColumns .  $��320,50,10,10q�6F   $��Center=�6\   $��Center=�6w   $	��300��6�   $	��450��6�      Columns 5!      0  *System.Windows.Controls.DataGridTextColumn  5"      ,  &System.Windows.Controls.DataGridColumn	   Header$	 Название��6%   1  +System.Windows.Controls.DataGridBoundColumn
   Binding
 67   ��  �� Path$
 Name�  5#      $	 Цена��6%   
 63   �� $ Price�5%      �� 5&   
   -   5        $ Excel��5&      $	��200��6    $��30��6,   $	��Top=�68   $��Center=�6P   $��
0,30,500,0q�6m   .��6�   Экспорт в EXCEL6�   5'      5(      