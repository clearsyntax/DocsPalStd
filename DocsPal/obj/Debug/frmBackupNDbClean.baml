   M S B A M L   `   `   `  ����  	DocWriter!    DocWriter.frmBackupNDbClean   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$  Database Maintenance��5   	   $	��374��6&   $	��473��63    � WindowStartupLocation$ CenterOwner=�6?   .��5      � .+�5   
   �� $
��Left=�6   $	��273��60   $��	10,10,0,0q�6=   $	��Top=�6P   $	��445��6h   .@�5      �� -   5         ��Name$ tabBackupNClean��5      $��Backup / Cleanup��6/    J� RenderTransformOrigin$ 	28.141,61&�6I   .��5      � ���	#FFE5E5E5.+�5	      �� $��Delete data older than =>��6   $
��Left=�6@   $��32��6[   $��
13,115,0,0q�6g   $	��Top=�6{   $	��151��6�   (  "System.Windows.Controls.DatePicker  5
      -   5          Name$ dtpDelDtaOlderThanDt��5
   !   $
��Left=�6?   $��32��6Z   $��169,115,0,0q�6f   $	��Top=�6{   $	��132��6�   �� 5      -   5         ��Name$ 	btnDelete��5      $��Delete��60   $
��Left=�6A   $��33��6\   $��338,188,0,0q�6h   $	��Top=�6}   $��91��6�   6�   �� 5      -   5         ��Name$ chkIncNrMstDt��5      $(��"Including not required master data��66   $
��Left=�6c   $��24��6~   $��
23,169,0,0q�6�   $	��Top=�6�   $	��308��6�   6�   �� 5      -   5         ��Name$ 
lblMassage��5      $	��...��60   $
��Left=�6>   $��38��6Y   $��
23,197,0,0q�6e   $	��Top=�6y   $	��278��6�   �� 5      -   5        $ 
btnGetData��5      $��Get data��61   $
��Left=�6D   $��31��6_   $��337,116,0,0q�6k   $	��Top=�6�   $��92��6�   6�   �� 5      $��Destination Folder:��6   $
��Left=�6:   $��31��6U   $��	14,10,0,0q�6a   $	��Top=�6t   $	��184��6�   �� 5      -   5         ��Name$ txtBackupLocation��5      $
��Left=�69   $��22��6T   $��	20,35,0,0q�6`   	 �� TextWrapping$
	 Wrap=�6s   $�� ��6�   $	��Top=�6�   $	��376��6�   �� 5      -   5        $ btnFindBackupLocation��5      $	��...��6<   $
��Left=�6J   $��22��6e   $��
401,35,0,0q�6q   $	��Top=�6�   $��26��6�   6�   �� 5      -	   5        $ 	btnBackup��5      $��	Backup DB��60   $
��Left=�6D   $��26��6_   $��	20,62,0,0q�6k   $	��Top=�6~   $	��141��6�   6�   �� 5      -
   5        $ btnAbortDelete��5      $��Abort��65   $
��Left=�6E   $��33��6`   $��340,188,0,0q�6l   $	��Top=�6�   $��87��6�   6�   5      5      5      �� 5   
   -   5        $ 	btnCancel��5      $��Cancel��6$   $
��Left=�65   $��38��6P   $��350,295,0,0q�6\   $	��Top=�6q   $��96��6�   6�   5      5      