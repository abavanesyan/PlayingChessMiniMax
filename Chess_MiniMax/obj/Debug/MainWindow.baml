   M S B A M L   `   `   `  ����  Chess_MiniMax    Chess_MiniMax.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   -   5          � Title$  
MainWindow��5   	   $	��438��6   $	��416��6)    � 
ResizeMode$ CanMinimize=�65   .��5      � -   5        $��myGrid��5      .+�5   
   �� -   5        $��MyBoard��5      $��	board.jpg��6   $
��Fill=�62   �� 5   
   -   5        $��
BlackQueen��5      $��bq.gif��6"   $��	150,0,0,0q�62   $	��Top=�6E   $
��Left=�6]   $��50��6x   $��50��6�   �� 5   
   -   5        $��
MyBnFigure��5      $��bn.gif��6"   $��50,0,0,0q�62   $	��Top=�6D   $
��Left=�6\   $��50��6w   $��50��6�   �� 5	   
   -   5        $��MyBnFigure2��5	      $��bn.gif��6#   $��	300,0,0,0q�63   $	��Top=�6F   $
��Left=�6^   $��50��6y   $��50��6�   �� 5
   
   -   5        $��
MyWbFigure��5
      $��bb.gif��6"   $��	100,0,0,0q�62   $	��Top=�6E   $
��Left=�6]   $��50��6x   $��50��6�    J� RenderTransformOrigin$ -0.063,0.547&�6�   �� 5   
   -   5        $��MyWbFigure2��5      $��bb.gif��6#   $��	250,0,0,0q�63   $	��Top=�6F   $
��Left=�6^   $��50��6y   $��50��6�   $ -0.063,0.547&�6�   �� 5   
   -	   5        $��
BlackPawn1��5      $��bp.gif��6"   $��0,50,0,0q�62   $	��Top=�6D   $
��Left=�6\   $��50��6w   $��50��6�   �� 5   
   -
   5        $��
BlackPawn2��5      $��bp.gif��6"   $��	50,50,0,0q�62   $	��Top=�6E   $
��Left=�6]   $��50��6x   $��50��6�   �� 5   
   -   5        $��
BlackPawn3��5      $��bp.gif��6"   $��
100,50,0,0q�62   $	��Top=�6F   $
��Left=�6^   $��50��6y   $��50��6�   �� 5   
   -   5        $��
BlackPawn4��5      $��bp.gif��6"   $��
150,50,0,0q�62   $	��Top=�6F   $
��Left=�6^   $��50��6y   $��50��6�   �� 5   
   -   5        $��
BlackPawn5��5      $��bp.gif��6"   $��
200,50,0,0q�62   $	��Top=�6F   $
��Left=�6^   $��50��6y   $��50��6�   �� 5   
   -   5        $��
BlackPawn6��5      $��bp.gif��6"   $��
250,50,0,0q�62   $	��Top=�6F   $
��Left=�6^   $��50��6y   $��50��6�   �� 5   
   -   5        $��
BlackPawn7��5      $��bp.gif��6"   $��
300,50,0,0q�62   $	��Top=�6F   $
��Left=�6^   $��50��6y   $��50��6�   �� 5   
   -   5        $��
BlackPawn8��5      $��bp.gif��6"   $��
350,50,0,0q�62   $	��Top=�6F   $
��Left=�6^   $��50��6y   $��50��6�   �� 5   
   -   5        $��	BlackRook��5      $��br.gif��6!   $��0,0,0,0q�61   $	��Top=�6B   $
��Left=�6Z   $��50��6u   $��50��6�   �� 5   
   -   5        $��
BlackRook2��5      $��br.gif��6"   $��	350,0,0,0q�62   $	��Top=�6E   $
��Left=�6]   $��50��6x   $��50��6�   �� 5   
   -   5        $��
WhitePawn1��5      $��wp.gif��6"   $��	0,300,0,0q�62   $	��Top=�6E   $
��Left=�6]   $��50��6x   $��50��6�    � Cursor$
 Hand��6�   �� 5   
   -   5        $��
WhitePawn2��5      $��wp.gif��6"   $��
50,300,0,0q�62   $	��Top=�6F   $
��Left=�6^   $��50��6y   $��50��6�   $
 Hand��6�   �� 5   
   -   5        $��
WhitePawn3��5      $��wp.gif��6"   $��100,300,0,0q�62   $	��Top=�6G   $
��Left=�6_   $��50��6z   $��50��6�   $
 Hand��6�   �� 5   
   -   5        $��
WhitePawn4��5      $��wp.gif��6"   $��150,300,0,0q�62   $	��Top=�6G   $
��Left=�6_   $��50��6z   $��50��6�   $
 Hand��6�   �� 5   
   -   5        $��
WhitePawn5��5      $��wp.gif��6"   $��200,300,0,0q�62   $	��Top=�6G   $
��Left=�6_   $��50��6z   $��50��6�   $
 Hand��6�   �� 5   
   -   5        $��
WhitePawn6��5      $��wp.gif��6"   $��250,300,0,0q�62   $	��Top=�6G   $
��Left=�6_   $��50��6z   $��50��6�   $
 Hand��6�   �� 5   
   -   5        $��
WhitePawn7��5      $��wp.gif��6"   $��300,300,0,0q�62   $	��Top=�6G   $
��Left=�6_   $��50��6z   $��50��6�   $
 Hand��6�   �� 5   
   -   5        $��
WhitePawn8��5      $��wp.gif��6"   $��350,300,0,0q�62   $	��Top=�6G   $
��Left=�6_   $��50��6z   $��50��6�   $
 Hand��6�   �� 5   
   -   5        $��
WhiteQueen��5      $��wq.gif��6"   $��150,350,0,0q�62   $	��Top=�6G   $
��Left=�6_   $��50��6z   $��50��6�   $
 Hand��6�   �� 5   
   -   5        $��
WhiteRook1��5      $��wr.gif��6"   $��	0,350,0,0q�62   $	��Top=�6E   $
��Left=�6]   $��50��6x   $��50��6�   $
 Hand��6�   �� 5    
   -   5        $��
WhiteRook2��5       $��wr.gif��6"   $��350,350,0,0q�62   $	��Top=�6G   $
��Left=�6_   $��50��6z   $��50��6�   $
 Hand��6�   �� 5!   
   -   5        $��WhiteKnight1��5!      $��wn.gif��6$   $��
50,350,0,0q�64   $	��Top=�6H   $
��Left=�6`   $��50��6{   $��50��6�   $
 Hand��6�   �� 5"   
   -   5        $��WhiteKnight2��5"      $��wn.gif��6$   $��300,350,0,0q�64   $	��Top=�6I   $
��Left=�6a   $��50��6|   $��50��6�   $
 Hand��6�   �� 5#   
   -    5        $��WhiteBishop1��5#      $��wb.gif��6$   $��100,350,0,0q�64   $	��Top=�6I   $
��Left=�6a   $��50��6|   $��50��6�   $
 Hand��6�   �� 5$   
   -!   5        $��WhiteBishop2��5$      $��wb.gif��6$   $��250,350,0,0q�64   $	��Top=�6I   $
��Left=�6a   $��50��6|   $��50��6�   $
 Hand��6�   �� 5%   
   -"   5        $��	WhiteKing��5%      $��wk.gif��6!   $��200,350,0,0q�61   $	��Top=�6F   $
��Left=�6^   $��50��6y   $��50��6�   $
 Hand��6�   �� 5&   
   -#   5        $��	BlackKing��5&      $��bk.gif��6!   $��	200,0,0,0q�61   $	��Top=�6D   $
��Left=�6\   $��50��6w   $��50��6�   5'      5(      