   M S B A M L   `   `   `  ����O  KPRN212_Project_Team9, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;"clr-namespace:PRN212_Project_Team9PRN212_Project_Team9  5   	   -    'PRN212_Project_Team9.Supplierforproduct   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   4local"clr-namespace:PRN212_Project_Team9     5   	     � Title$  Supplierforproduct��5   	   $	��450��6$   $	��800��61   .��5	      � .+�5
   
   �� -   5         ��Name$ 
cbSearchBy��5
      $
��Left=�6'   $��	10,10,0,0q�6B   $	��Top=�6U   $	��120��6m   .@�5      �� $��
SupplierID��6   �� 5      $��SupplierName��6   �� 5      $��ContactNumber��6   �� 5      $��Address��6   �� 5      $��Email��6   5      �� 5   
   -   5         ��Name$ 	txtSearch��5      $
��Left=�6%   $��
140,10,0,0q�6@    �� TextWrapping$
 Wrap=�6T   $	��Top=�6h   $	��200��6�   �� 5   
   -   5         ��Name$ 	btnSearch��5      $��Search��6$   $
��Left=�65   $��
350,10,0,0q�6P   $	��Top=�6d   $��60��6|   6�   �� 5   
   -   5         ��Name$ 
lvSupplier��5      $��0,45,252,10q�6'    �� View 5      �� 5      .P�5      �� $��ID��6%    �� Width$ 30��61    �� DisplayMemberBinding 6<   �� 	 �� Path$	 
SupplierID��� 5      $��SupplierName��6%   $	 100��6;    6G   �� $	 SupplierName��� 5      $��ContactNumber��6%   $	 100��6<    6H   �� $	 ContactNumber��� 5      $��Address��6%   $	 140��66    6B   �� $	 Address��� 5      $��Email��6%   $	 160��64    6@   �� $	 Email�5      5      �� 5   
   $��Supplier��6   $
��Left=�6#   $��
623,10,0,0q�6>   $	��Top=�6R   $��36��6j   $	��112��6v   $��20(�6�   �� 5   
   $��
SupplierID��6   $
��Left=�6%   $��
608,55,0,0q�6@   $	��Top=�6T   �� 5    
   $��Supplier Name��6   $
��Left=�6(   $��608,104,0,0q�6C   $	��Top=�6X   �� 5!   
   $��Contact Number��6   $
��Left=�6)   $��608,153,0,0q�6D   $	��Top=�6Y   �� 5"   
   $��Address��6   $
��Left=�6"   $��608,192,0,0q�6=   $	��Top=�6R   
 J� RenderTransformOrigin$
 0.571,-0.384&�6j   �� 5#   
   $��Email��6   $
��Left=�6    $��608,235,0,0q�6;   $	��Top=�6P   �� 5$   
   -   5        $ tbSupplierID��5$      $
��Left=�6(   $��
608,86,0,0q�6C   $
 Wrap=�6W   $	��Top=�6k   $	��120��6�   $
 0.338,0.225&�6�   ${�False��6�   �� 5%   
   -   5        $ tbSupplierName��5%      $
��Left=�6*   $��608,130,0,0q�6E   $
 Wrap=�6Z   $	��Top=�6n   $	��120��6�   �� 5&   
   -   5        $ tbSupplierCN��5&      $
��Left=�6(   $��608,174,0,0q�6C   $
 Wrap=�6X   $	��Top=�6l   $	��120��6�   �� 5'   
   -   5        $ tbSupplierAddress��5'      $
��Left=�6-   $��608,218,0,0q�6H   $
 Wrap=�6]   $	��Top=�6q   $	��120��6�   �� 5(   
   -	   5        $ tbSupplierEmail��5(      $
��Left=�6+   $��608,261,0,0q�6F   $
 Wrap=�6[   $	��Top=�6o   $	��120��6�   �� 5)   
   -
   5        $ Refresh��5)      $��Refresh��6"   $
��Left=�64   $��565,301,0,0q�6O   $	��Top=�6d   6|   �� 5*   
   -   5        $	��Add��5*      $
��Left=�6   $��623,301,0,0q�6:   $	��Top=�6O   $��38��6g   6r   �� 5+   
   -   5        $ Delete��5+      $��Delete��6!   $
��Left=�62   $��728,301,0,0q�6M   $	��Top=�6b   $��60��6z   6�   �� 5,   
   -   5        $
��Edit��5,      $
��Left=�6    $��679,300,0,0q�6;   $	��Top=�6P   $��21��6h   $��38��6t   6   �� 5-   
   -   5        $ btnSupplierProduct��5-      $��Supplier Products��6-   $
��Left=�6I   $��577,348,0,0q�6d   $	��Top=�6y   $	��140��6�   6�   5.      5/      