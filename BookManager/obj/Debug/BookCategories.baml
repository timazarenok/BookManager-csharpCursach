   M S B A M L   `   `   `  ����F  BBookManager, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:BookManagerBookManager  5   	        BookManager.BookCategories   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   +localclr-namespace:BookManager     5   	     � Title$  BookCategories��5   	   $	��450��6    $	��800��6-   .��5	      � .+�5
   
   �� $	��Top=�6   $��Center=�6(   $��
0,50,450,0q�6E   .��6Y   53Добавьте категорию для книг6v   �� 5   
   -   5         ��Name$
 Name��5      $��Center=�6    $��Center=�6;   $	��150��6X   $��25��6d   $��0,0,550,150q�6p   �� 5   
   -   5         ��Name$	 Add��5      $��Center=�6   $��Center=�69   $��
0,0,550,20q�6V   $	��120��6j   $��25��6v   .��6�   Добавить6�   �� 5   
   -   5        $ Delete��5      $��Center=�6!   $��Center=�6<   $��
0,0,250,20q�6Y   $	��120��6m   $��25��6y   .��6�   Удалить6�   &   System.Windows.Controls.DataGrid  5   
   -   5          Name$ Table��5         AutoGenerateColumns .  $	��300��6>   $��250, 50, 0, 50q�6J      Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$ Название��6%      Width5  /System.Windows.Controls.DataGridLengthConverter$	 290 6U   1  +System.Windows.Controls.DataGridBoundColumn   Binding 67   �� 	 �� Path$
	 Name�6U   5      5      5      