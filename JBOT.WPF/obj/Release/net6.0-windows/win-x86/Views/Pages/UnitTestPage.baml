   M S B A M L   `   `   `  ���� B  >ToolBox, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;"clr-namespace:JBOT.WPF.Views.PagesJBOT.WPF.Views.Pages  F BJBOT.Domain, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null\=clr-namespace:JBOT.Domain.Entities.Enums;assembly=JBOT.DomainJBOT.Domain.Entities.Enums '    !JBOT.WPF.Views.Pages.UnitTestPage   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     4local"clr-namespace:JBOT.WPF.Views.Pages     A =Wpf.Ui, Version=2.0.3.0, Culture=neutral, PublicKeyToken=null/ui&http://schemas.lepo.co/wpfui/2022/xaml  Henum=clr-namespace:JBOT.Domain.Entities.Enums;assembly=JBOT.Domain    N� Title$  UnitTestPage��   TextFillColorPrimaryBrush N� 
Foreground# �   .���  � RowDefinitions �� $
��Auto ��� .+���  �� Orientation$ 
Horizontal=�$
��Left=�.+��� $��	0,0,10,10q�$	��Top=�$	��200������ *ViewModel.Servers+ �� Mode$ OneWay=� �� SelectedItem �� *ViewModel.CurrentServer+$ TwoWay=��� $��	0,0,10,10q� �� DisplayMemberPath$
 Name�� �� SelectedValuePath$ Id��$	��Top=�$	��200������ *ViewModel.Databases+$ OneWay=� �� *ViewModel.CurrentDatabase+$ TwoWay=��� $ 
Horizontal=�$��Right=�.+�  Wpf.Ui.Controls.Button     
Appearance$ Caution=�$��16(�$��55��$��30��	   Icon$	 ArrowClockwise48=�$��Center=�$��Right=�$��	0,0,10,10q����� *ViewModel.LoadGridCommand+���� *ViewModel.CurrentConnections+  
��� � �$ Transparent=�$��16(�$��55��$��30��$	 Play48=�
   IconForeground

 � � �$��Center=�$��Right=�$��	0,0,10,10q����� *ViewModel.RunTestCommand+  $ Primary=�$	 Add28=�$��16(�$��55��$��30��$��Center=�$��Right=�$��	0,0,10,10q����� *(&ViewModel.ShowAddUnitTestDialogCommand+  Wpf.Ui.Controls.DataGrid  $��1��&   System.Windows.Controls.DataGrid   AutoGenerateColumns$ False��   CanUserAddRows$ False��   CanUserDeleteRows$ False��   CanUserReorderColumns$ False��   SelectionUnit$ FullRow=�$��Center=�   FrozenColumnCount$ 2������ *ViewModel.UnitTests+ � 	Resources   EditTestCommand  RemoveTestCommand  BadgeTemplate  Wpf.Ui.Controls.Badge �� 
TargetType �� BasedOn �� Triggers,  &JBOT.Domain.Entities.Enums.StatusEnums   Failed �� Value �� Binding �� Property �� Value   Success��%�  &	       &	 q     &	 �     7 �� .R�  $��1q�$	 BeakerEdit24=�$ Transparent=�$��Stretch=�$�� ������ *EditCommand+���� �� .R�  $��1q�$	 Delete48=�$ Transparent=�$��Stretch=�$�� ������ *RemoveCommand+���� �� .R�  ���� "  8    �� # Z  �� *Status+.a���  �@ 
Appearance$ Danger=���  � ��$ Failed���� # Z  �� *Status+.a���  �@ 
Appearance$ Success=���  � ��$ Success��   Columns 4  .System.Windows.Controls.DataGridTemplateColumn  -   Edit��   CellTemplate# [ ,  &System.Windows.Controls.DataGridColumn   Header$
 Edit��   MinWidth$ 50Q�  -   
Remove��# [ $ Remove��$ 50Q�  $ Status��# [ 0  *System.Windows.Controls.DataGridTextColumn  $
 Name��   Width5	  /System.Windows.Controls.DataGridLengthConverter$
 Auto	     
IsReadOnly$
  True��1
  +System.Windows.Controls.DataGridBoundColumn! 
  Binding! �� *
TestName+  $ 
ObjectName��$
 Auto	 $
  True��! �� *
ObjectName+  $ 
ObjectType��$
 Auto	 $
  True��! �� *
ObjectType+  $ 
Parameters��$
 Auto	 $
  True��! �� *
Parameters+