   M S B A M L   `   `   `  ����  PRN212_Project_Team9+    %PRN212_Project_Team9.ProductDiscounts   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$  ProductDiscounts��5   	   $	��450��6"   $	��800��6/   .��5      � .+�5   
   �� -   5         ��Name$ 
cbSearchBy��5      $
��Left=�6'   $��	10,10,0,0q�6B   $	��Top=�6U   $	��120��6m   .@�5      �� $��ProductDiscountID��6   �� 5      $��	ProductID��6   �� 5	      $��
DiscountID��6   5
      �� 5   
   -   5         ��Name$ 	txtSearch��5      $
��Left=�6%   $��
140,10,0,0q�6@    �� TextWrapping$
 Wrap=�6T   $	��Top=�6h   $	��200��6�   �� 5   
   -   5         ��Name$ 	btnSearch��5      $��Search��6$   $
��Left=�65   $��
350,10,0,0q�6P   $	��Top=�6d   $��60��6|   6�   �� 5   
   -   5         ��Name$ lvProductDiscount��5      $��
0,45,252,0q�6.    �� View 5      �� 5      .P�5      �� $��ID��6%    �� Width$ 30��61    �� DisplayMemberBinding 6<   �� *ProductDiscountId+�� 5      $��	ProductID��6%   $ 60��68    6C   �� *	ProductId+�� 5      $��Product Name��6%   $	 120��6;    6G   �� *ProductName+�� 5      $��
DiscountID��6%   $ 60��69    6D   �� *
DiscountId+�� 5      $��Discount Name��6%   $	 120��6<    6H   �� *DiscountName+5      5      �� 5   
   $��Product Discount��6   $
��Left=�6+   $��
623,10,0,0q�6F   $	��Top=�6Z   $��36��6r   $	��112��6~   $��20(�6�   �� 5   
   $��ID��6   $
��Left=�6   $��
608,55,0,0q�68   $	��Top=�6L   �� 5   
   -   5        $ tbProductDiscountId��5      $
��Left=�6/   $��
608,86,0,0q�6J   $
 Wrap=�6^   $	��Top=�6r   $	��120��6�   ${�False��6�   �� 5   
   $��Product��6   $
��Left=�6"   $��608,104,0,0q�6=   $	��Top=�6R   �� 5    
   -   5        $ 	cbProduct��5       $
��Left=�6&   $��608,130,0,0q�6A   $	��Top=�6V   $	��120��6n   �� 5"   
   $��Discount��6   $
��Left=�6#   $��608,153,0,0q�6>   $	��Top=�6S   �� 5#   
   -   5        $ 
cbDiscount��5#      $
��Left=�6'   $��608,174,0,0q�6B   $	��Top=�6W   $	��120��6o   �� 5%   
   -   5        $ Refresh��5%      $��Refresh��6"   $
��Left=�64   $��588,301,0,0q�6O   $	��Top=�6d   6|   �� 5&   
   -	   5        $	 Add��5&      $	��Add��6   $
��Left=�6,   $��668,301,0,0q�6G   $	��Top=�6\   $��60��6t   6   �� 5'   
   -
   5        $ Update��5'      $��Update��6!   $
��Left=�62   $��588,341,0,0q�6M   $	��Top=�6b   $��60��6z   6�   �� 5(   
   -   5        $ Delete��5(      $��Delete��6!   $
��Left=�62   $��668,341,0,0q�6M   $	��Top=�6b   $��60��6z   6�   5)      5*      