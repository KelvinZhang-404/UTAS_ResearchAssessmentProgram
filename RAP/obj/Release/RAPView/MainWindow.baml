   M S B A M L   `   `   `  ���� >  :RAP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;"clr-namespace:RAPWPF.RAPControllerRAPWPF.RAPController  1clr-namespace:RAPWPF.RAPModelRAPWPF.RAPModel  O Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem     RAPWPF.MainWindow   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     3ctrl"clr-namespace:RAPWPF.RAPController     /modelclr-namespace:RAPWPF.RAPModel     9System&clr-namespace:System;assembly=mscorlib       � Title$  
MainWindow��$	��600��$	��800�� � 	Resources 3  -RAPWPF.RAPController.ResearcherListController   researcherListContrller  AllResearcherList S� ObjectInstance S� 
MethodName  ResearcherTemplate � ColumnDefinitions � RowDefinitions �� Path��%�   &	        &	      7   &	 ,       S� 8   $ GetViewableResearchers���� .R��  �� ��  �� .+��� $��0��$��0������ $
 Name��� $��0��$��1������ $ ID�.��� .+��� .@��� $��
Researcher��.��� ���	#FFE5E5E5.+�� $��
10,10,0,10q�$
��Left=�.+��� $��Enter name to search:��$
��Left=�$	��Top=�$��27��$	��150���� -    ��Name$ NameTextBox��$
��Left=�$��20�� �� TextWrapping$
 Wrap=�$	��Top=�$	��150��$��0,27,0,0q��� $��Filter��$
��Left=�$��0,52,0,0q�$	��Top=�$	��150���� -   	 ��Name$	 LevelComboBox��$
��Left=�$��0,79,0,0q�$	��Top=�$	��150��$��21��.@���All����A����B����C����D����E����Students���� -   
 ��Name$
 ResearcherListBox��$
��Left=�$��	0,105,0,0q�$	��150������  �� Source# [ �� $��165,10,10,0q� �� Padding$ 5q�
�������$��3,5,3,5q� �� CornerRadius$ 3��$	��Top=�._��� -    ��Name$ PrimaryDetailsPanel��$y�Hidden=�$��5,5,5,5q� �� Orientation$ 
Horizontal=�$	��130��$	��Top=� � DataContext �� *
Researcher+.+��� $	��120��$	��130������ *Photo+�� $ Vertical=�.+��� $ 
Horizontal=�.+��� $��Name:��$��60��$��10,0,0,0q�$
��Bold"��� ���� *Name+�� $ 
Horizontal=�.+��� $��
Job Title:��$��60��$��10,0,0,0q�$
��Bold"��� ���� *
JobTitle+�� $ 
Horizontal=�.+��� $��Unit:��$��60��$��10,0,0,0q�$
��Bold"��� ���� *Unit+�� $ 
Horizontal=�.+��� $��Campus:��$��60��$��10,0,0,0q�$
��Bold"��� ���� *Campus+�� $ 
Horizontal=�.+��� $��Email:��$��60��$��10,0,0,0q�$
��Bold"��� ���� *Email+�� -    ��Name$ PersonalDetailsTabCtrl��$��165,175,10,10q�$y�Hidden=�.@��� -   $��	DetailTab��$��Detail��.���� $ 5q�
�������$��3,5,3,5q�$ 3��._��� -   $ PersonalDetailsPanel�� �� *
Researcher+.+��� -   $ StudentListPanel��$y�	Collapsed=�.+��� -	   $��Go Back��&   System.Windows.Controls.DataGrid  -
     Name$ tblStudents��   AutoGenerateColumns .     
IsReadOnly .    Columns 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn   Header$ ID��   Width5  /System.Windows.Controls.DataGridLengthConverter$ 50 1  +System.Windows.Controls.DataGridBoundColumn   Binding �� *ID+  $
 Name��$ *  �� *Name+�� -   $ CumulativeCountsPanel��$y�	Collapsed=�.+��� -   $��Go Back��  -   $ tblCumulativeCounts�� .   .    $
 Year��$ 50  �� *Item1+  $ Number of Publications��$ *  �� *Item2+�� -   $ OtherDetailsPanel��$y�Hidden=�.+��� $ 
Horizontal=�.+��� $	��200��$��Current Job Title:��$
��Bold"��� $	��120������ *
JobTitle+�� $ 
Horizontal=�.+��� $	��200��$ ��Commenced with insitution:��$
��Bold"��� $	��120������ *CommencedIns+�� $ 
Horizontal=�.+��� $	��200��$!��Commenced current position:��$
��Bold"���  �� ContentStringFormat$ {0:dd/MM/yyyy}��$	��120������ *CommencedPos+�� $ 
Horizontal=�.+��� $	��200��$��Tenue:��$
��Bold"��� $ 	{0} Years��$	��120������ *Tenure+�� $ 
Horizontal=�.+��� $	��200��$��Publications:��$
��Bold"��� $	��120������ *PublicationCount+�� -    ��Name$ CumulativeBtn��$!��See cumulative publications��$	��160���� $ 
Horizontal=�.+��� $	��200��$��3-year Average:��$
��Bold"��� $
 {0}%��$	��120������ *ThreeYearAvg+�� $ 
Horizontal=�.+��� $	��200��$��Performance:��$
��Bold"��� $	��120������ *Performance+�� $ 
Horizontal=�.+��� $	��200��$��Supervision:��$
��Bold"��� $	��120������ *Supervisions+�� -   $ SupervisonBtn��$��See all students' names��$	��160��$��0q��� $ 
Horizontal=�.+��� $	��200��$��Degree:��$
��Bold"��� $	��120������ *Degree+�� $ 
Horizontal=�.+��� $	��200��$��Supervisor:��$
��Bold"��� $	��120������ *
Supervisor+�� $��Publication��.���� $ 5q�
�������$��3,5,3,5q�$ 3��._��� -   $ PersonalPublicationPanel��$ Vertical=�$��Center=�$	��2,0q�$	��585�� �� *
Researcher+.+��� -   $ PubListPanel��$y�Visible=�.+��� $ 
Horizontal=�.+��� $��Total Publication:��$
��Bold"��� $��50������ *PublicationCount+�� $	��120��$��Specify a year range:��$
��Bold"��� -   $	 FromComboBox��$��70���� $��to���� -   $	 
ToComboBox��$��70���� -   $ SearchRangeBtn��$��Search��$��75��$��10,0,0,0q��� $	��284��$
��Auto=�.��  -   $ tblPublications�� .   .    $ Publication Year��$	 100  �� *Year+  $ Title��$ *  �� *Title+�� -   $ PubDetailsPanel��$y�	Collapsed=�$	��550�� �� *Publication+.+��� -   $��Go Back���� $ 
Horizontal=�.+��� $
��DOI:��$	��100���� ���� *DOI+�� $ 
Horizontal=�.+��� $��Title:��$	��100���� $
 Wrap=�$	��450�� �� 
IsReadOnly . ���� *Title+�� $ 
Horizontal=�.+��� $��Authors:��$	��100���� ���� *	Authors+�� $ 
Horizontal=�.+��� $��Publication year:��$	��100���� ���� *Year+�� $ 
Horizontal=�.+��� $��Type:��$	��100���� ���� *Mode+�� $ 
Horizontal=�.+��� $��Cite as:��$	��100���� $
 Wrap=�$	��450�� . ���� *CiteAs+�� $ 
Horizontal=�.+��� $��Available date:��$	��100���� ���� *	Certified+�� $ 
Horizontal=�.+��� $
��Age:��$	��100���� $ {0} Days������ *Age+�� -    ��Name$ PositionTab��$��Position History��.���� $ 5q�
�������$��3,5,3,5q�$ 3��._��� -   $ PositionHistoryPanel�� �� *
Researcher+.+��� $	��284��$
��Auto=�.��  -   $ tblPositions�� .   .    $ Position��$	 100  �� *Level+  $ 
Start Date��$ *  �� *	StartDate+ �� StringFormat$ {0:MM/dd/yyyy}��  $ End Date��$ *  �� *	EndDate+$ {0:MM/dd/yyyy}���� $��Report��.��� ���	#FFE5E5E5