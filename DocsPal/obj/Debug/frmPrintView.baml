   M S B A M L   `   `   `  ����  	DocWriter    DocWriter.frmPrintView   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$  
Print View��5   	   $	��400��6   $	��500��6)    � WindowStartupLocation$ CenterOwner=�65   .��5      � .+�5   
   �� -   5         ��Name$ rTextBox��5      $��10,44,10,50q�6(   $��Courier New)�6=    �� 
IsReadOnly . $ �� HorizontalScrollBarVisibility$
 Auto=�5      " �� VerticalScrollBarVisibility$
 Auto=�5      . �5      /�  /� 	PageWidth$
 2048��6   .T�5      J�  �� 
LineHeight$ 1��6   .*�6,   .*�5      �� $� ��6   5      5      5      �� 5   
   $��
From Date:��6   $
��Left=�6%   $��29��6@   $��10,9,0,0q�6L   $	��Top=�6^   $��69��6v   (  "System.Windows.Controls.DatePicker  5   
   -   5          Name$ dtpFromDate��5      $
��Left=�6*   $��29��6E   $��	79,12,0,0q�6Q   $	��Top=�6d   $	��115��6|   	 J� RenderTransformOrigin$	 0.102,0.345&�6�   �� 5   
   $��To Date:��6   $
��Left=�6#   $��29��6>   $��
205,11,0,0q�6J   $	��Top=�6^   $��55��6v     5   
   -   5        $ 	dtpToDate��5      $
��Left=�6(   $��29��6C   $��
260,12,0,0q�6O   $	��Top=�6c   $	��115��6{   �� 5   
   -   5        
 ��Name$
 btnVewReport��5      $��
Vew Report��6'   $��29��6<   $��	0,10,10,0q�6H   $	��Top=�6[   $��Right=�6s   $��74��6�   6�   �� 5   
   -   5        $
 	btnCancel��5      $��Cancel��6$   $��Right=�65   $��35��6Q   $��	0,0,10,10q�6]   $��Bottom=�6p   $��46��6�   $	 0.311,0.429&�6�   6�   �� 5   
   -   5        $
 btnSave2Pdf��5      $��Save to PDF��6&   $��Right=�6<   $��35��6X   $��	0,0,60,10q�6d   $��Bottom=�6w   $��74��6�   {�. 6�   �� 5   
   -   5        $
 btnSavetoCSV��5      $��Save to CSV��6'   $��35��6=   $��
0,0,236,10q�6I   $��Bottom=�6]   $��Right=�6x   $��74��6�   {�. 6�   �� 5   
   -   5        $
 btnCopy2Clipboard��5      $��Copy2 Clipboard��6,   $��35��6F   $��
0,0,136,10q�6R   $��Bottom=�6f   $��Right=�6�   $��97��6�   {�. 6�   �� 5   
   -	   5        $
 btnPrint��5      {�. $��Print��65   $��Right=�6E   $��35��6a   $��
0,0,312,10q�6m   $��Bottom=�6�   $��64��6�   6�   �� 5   
   -
   5         ��Name$ 
lblMessage��5      $��.��6$   $��10,0,381,10q�60   $��35��6t   $��Bottom=�6�   5      5      