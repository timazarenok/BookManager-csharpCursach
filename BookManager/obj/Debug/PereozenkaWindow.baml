   M S B A M L   `   `   `  ����F  BBookManager, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:BookManagerBookManager  5   	   "    BookManager.PereozenkaWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   +localclr-namespace:BookManager     5   	     � Title$  PereozenkaWindow��5   	   $	��650��6"   $	��800��6/   .��5	      � .+�5
   
   �� $��
100,30,0,0q�6   $	��Top=�6$   $
��Left=�6<   $��30��6W   $��30��6b   .��6n   ID6r   �� 5   
   -   5         ��Name$ ID��5      $��30��6   $��30��6)   $	��Top=�65   $
��Left=�6M   $��
100,70,0,0q�6h   �� 5   
   $��
200,30,0,0q�6   $	��Top=�6$   $
��Left=�6<   $	��160��6W   $��30��6c   .��6o   Новая цена6{   �� 5   
   -   5        $ NewPrice��5      $	��120��6$   $��30��60   $	��Top=�6<   $
��Left=�6T   $��
200,70,0,0q�6o   6�   �� 5   
   -   5         ��Name$ Confirm��5      $��Отправить��6"   $	��120��66   $��30��6B   $	��Top=�6N   $��Center=�6f   $��	20,70,0,0q�6�   6�   &   System.Windows.Controls.DataGrid  5   
   -   5          Name$ Table��5         AutoGenerateColumns .  $	��700��6>   $��0, 200, 0, 50q�6J      Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$ Название��6%      Width5  /System.Windows.Controls.DataGridLengthConverter$	 100 67   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6C   �� 	 �� Path$
	 Name�  5      $ Категория��6%   $	 100 68    6D   �� $	 Category�  5      $ 
Автор��6%   $	 100 64    6@   �� $	 Author�  5      $ Описание��6%   
   FontSize$
 12Q�67   $	 270 6E    6Q   �� $	 Description�   ElementStyle 5      �� 5       �� 
TargetType" ��6    .�5      ��  �� Property � �� �� Value$
 Wrap=�6G   5      5        5      $ Цена��6%   $ 50 6R    63   �� $	 Price�6R     5      $ Кол-во��6%    65   �� $	 Amount�5      5      5       