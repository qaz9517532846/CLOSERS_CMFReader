   M S B A M L   `   `   `  ����L  HCLOSERS_CMFReader, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:CLOSERS_CMFReaderCLOSERS_CMFReader  5   	   "    CLOSERS_CMFReader.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   1localclr-namespace:CLOSERS_CMFReader     5   	   -   5          �Name$  CLOSERS_CMFWin��5   	    � Title$ CLOSERS CMF Editor��5   	   $	��750��6$   $
��1000��61   $
��1000��6>   $	��750��6N   $
��1000��6^   $	��750��6n   $y�Visible=�6~    � Icon$ 	/icon.PNG��6�   .��5	      � -   5         �Name$ CMFDataGrid��5	       � ColumnDefinitions 5
   
   �� 5      $��8* �6   �� 5      $	��13* �6   �� 5      $	��61* �6   �� 5      $	��46* �6   �� 5      $	��27* �6   �� 5      $
��367* �6   �� 5      $
��478* �6   ��5   
   �� 5       �� EndPoint$ 0.5,1&�6"    �� 
StartPoint$ 0.5,0&�63   .��5      �  � Color$ 	#FFFFFFB8��6    � Offset$ 1Q�61   � 5      $ 	#FFFFB8C6��6   $ 0Q�61   � 5      $ 	#FFE7E8EC��6   $ 0Q�61   5      .+�5   
   �� $
��Left=�6   $	��460��6+   $��
10,251,0,0q�68   $	��Top=�6L   $	��536��6d   $��/Background1.PNG��6p   $��6��6�   � 5   
   $��7��6   $
��1000��6#    5      �� 5      $
��435* �6#   �� 5      $	��78* �6#   �� 5      $��0* �6#   �� 5       $
��130* �6#   �� 5!      $	��68* �6#   �� 5"      $
��289* �6#   .+�5$      �� -   5        	 ��Name$	 	LableTerm��5$      $$��CLOSERS CMF Editor (Yuri ver.)��6'   $
��Left=�6P   $��13,7,0,0q�6k   $	��Top=�6}   $��20(�6�   $��Times New Roman)�6�   �� 5%      -   5        $	 ResultLable��5%      $��	Result：��6)   $
��Left=�6;   $��	15,46,0,0q�6V   $	��Top=�6i   $��Times New Roman)�6�   $��16(�6�   �� 5&      -   5        $	 UnpackItemLable��5&      $��Unpack Item：��6-   $
��Left=�6D   $��
45,116,0,0q�6_   $	��Top=�6s   $��Times New Roman)�6�   $��16(�6�   $��2��6�   $��4��6�   �� 5'      -   5        
 ��Name$
 ReadCMF_btn��5'      $��Read CMF��6*   $��5��6=   $
��Left=�6M   $��
32,155,0,0q�6h   $	��Top=�6|   $	��100��6�    �� IsCancel$
 True��6�   $��Times New Roman)�6�   $��14(�6�   $��30��6�   6�   �� 5(      -   5        $
 UnpackCMF_btn��5(      $��
Unpack CMF��6,   $��5��6A   $
��Left=�6Q   $��154,154,0,0q�6l   $	��Top=�6�   $	��101��6�   $
 True��6�   $��Times New Roman)�6�   $��14(�6�   $��31��6�   6�   �� 5)      -   5        $
 UnpackCMFRecord_btn��5)      $��Unpack record output��62   $��5��6Q   $
��Left=�6a   $��
31,202,0,0q�6|   $	��Top=�6�   $	��141��6�   $
 True��6�   $��Times New Roman)�6�   $��14(�6�   $��31��6�   6�   �� 5*      -	   5        $
 UnpackClear_btn��5*      $��Clear��6.   $��5��6>   $
��Left=�6N   $��190,203,0,0q�6i   $	��Top=�6~   $��30��6�   $��64��6�   $��Times New Roman)�6�   6�   5+      �� 5,   
   -
   5         ��Name$ CMFListView��5,      $��1��6(   $��6��68   $��12,77,329,24q�6L   ��5-      �� 5.       �� View 50      �� 51      -   5        GridCMFListView��.P�52      �� $
��File��6%    �� Width$	 250��63    �� DisplayMemberBinding 6?   �� *File+�� 53      $
��Name��6%   $	 200��63    6?   �� *Name+�� 54      $
��Size��6%   $	 100��63    6?   �� *Size+�� 55      $
��Type��6%   $	 100��63    6?   �� *Type+56      58      59      5:      