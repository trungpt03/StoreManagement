   M S B A M L   `   `   `  ����O  KPRN212_Project_Team9, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;"clr-namespace:PRN212_Project_Team9PRN212_Project_Team9  5   	   *    $PRN212_Project_Team9.SupplierProduct   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   4local"clr-namespace:PRN212_Project_Team9     5   	     � Title$  SupplierProduct��5   	   $	��450��6!   $	��800��6.   .��5	      � .+�5   
   �� -   5         ��Name$ 
cbSearchBy��5      $
��Left=�6'   $��	10,10,0,0q�6B   $	��Top=�6U   $	��120��6m   .@�5      �� $��
SupplierID��6   �� 5      $��	ProductID��6   �� 5      $��SupplierName��6   �� 5      $��ProductName��6   5      �� 5   
   -   5         ��Name$ 	txtSearch��5      $
��Left=�6%   $��
140,10,0,0q�6@    �� TextWrapping$
 Wrap=�6T   $	��Top=�6h   $	��200��6�   �� 5   
   -   5         ��Name$ 	btnSearch��5      $��Search��6$   $
��Left=�65   $��
350,10,0,0q�6P   $	��Top=�6d   $��60��6|   6�   �� 5   
   -   5         ��Name$ lvSupplierProduct��5      $��
0,45,252,0q�6.    �� View 5      �� 5      .P�5      �� $��ID��6%    �� Width$ 30��61    �� DisplayMemberBinding 6<   �� *SupplierProductId+�� 5      $��
SupplierID��6%   $ 60��69    6D   �� *
SupplierId+�� 5      $��Supplier Name��6%   $	 120��6<    6H   �� *SupplierName+�� 5      $��	ProductID��6%   $ 60��68    6C   �� *	ProductId+�� 5      $��Product Name��6%   $	 120��6;    6G   �� *ProductName+�� 5      $��Price��6%   $	 100��64    6@   �� *SupplierPrice+5      5       �� 5#   
   $��Supplier Product��6   $
��Left=�6+   $��
623,10,0,0q�6F   $	��Top=�6Z   $��36��6r   $	��112��6~   $��20(�6�   �� 5%   
   $��ID��6   $
��Left=�6   $��
608,55,0,0q�68   $	��Top=�6L   �� 5&   
   -   5        $ tbSupplierProductId��5&      $
��Left=�6/   $��
608,86,0,0q�6J   $
 Wrap=�6^   $	��Top=�6r   $	��120��6�   ${�False��6�   �� 5(   
   $��Supplier��6   $
��Left=�6#   $��608,104,0,0q�6>   $	��Top=�6S   �� 5)   
   -   5        $ 
cbSupplier��5)      $
��Left=�6'   $��608,130,0,0q�6B   $	��Top=�6W   $	��120��6o   �� 5+   
   $��Product��6   $
��Left=�6"   $��608,153,0,0q�6=   $	��Top=�6R   �� 5,   
   -   5        $ 	cbProduct��5,      $
��Left=�6&   $��608,174,0,0q�6A   $	��Top=�6V   $	��120��6n   �� 5.   
   $��Price��6   $
��Left=�6    $��608,192,0,0q�6;   $	��Top=�6P   �� 5/   
   -   5        $ tbSupplierPrice��5/      $
��Left=�6+   $��608,218,0,0q�6F   $
 Wrap=�6[   $	��Top=�6o   $	��120��6�   �� 52   
   -	   5        $ Refresh��52      $��Refresh��6"   $
��Left=�64   $��588,301,0,0q�6O   $	��Top=�6d   6|   �� 53   
   -
   5        $	 Add��53      $	��Add��6   $
��Left=�6,   $��668,301,0,0q�6G   $	��Top=�6\   $��60��6t   6   �� 54   
   -   5        $ Update��54      $��Update��6!   $
��Left=�62   $��588,341,0,0q�6M   $	��Top=�6b   $��60��6z   6�   �� 55   
   -   5        $ Delete��55      $��Delete��6!   $
��Left=�62   $��668,341,0,0q�6M   $	��Top=�6b   $��60��6z   6�   56      57      