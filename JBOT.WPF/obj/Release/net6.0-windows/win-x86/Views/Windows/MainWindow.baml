   M S B A M L   `   `   `  ���� B  >ToolBox, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null?$clr-namespace:JBOT.WPF.Views.WindowsJBOT.WPF.Views.Windows  ;"clr-namespace:JBOT.WPF.Views.PagesJBOT.WPF.Views.Pages  '    !JBOT.WPF.Views.Windows.MainWindow   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     6local$clr-namespace:JBOT.WPF.Views.Windows     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     4pages"clr-namespace:JBOT.WPF.Views.Pages     A =Wpf.Ui, Version=2.0.3.0, Culture=neutral, PublicKeyToken=null/ui&http://schemas.lepo.co/wpfui/2022/xaml  $
��1100��$	��650��   TextFillColorPrimaryBrush#���     Wpf.Ui.Controls.UiWindow!    ExtendsContentIntoTitleBar$
  True��   WindowBackdropType$
 Mica=�   WindowCornerPreference$ Round=� � WindowStartupLocation$ CenterScreen=� � Title �� *ViewModel.ApplicationTitle+ �� Mode$ OneWay=�.���  � RowDefinitions �� $
��Auto ��� $��* �.+�� -    �Name$ RootMainGrid��$��1��$��0q� � ColumnDefinitions �� $
��Auto ��� $��* �.+�� $��0��$��
18,0,18,18q� �� $��67 ��� $
��Auto ��� $��* �.+��� $��0��$
��0,12q�.+��� $��16(�$��Toolbox���� $��12(�  TextFillColorTertiaryBrush#���  $��Italic$�$��For Smart Coding��&   Wpf.Ui.Controls.NavigationFluent  -   	  Name$	 RootNavigation��$��2��/  )Wpf.Ui.Controls.Navigation.NavigationBase
   Footer
 �� *ViewModel.NavigationFooter+$ TwoWay=�   Frame ��  �� ElementName$ 	RootFrame��$ OneWay=�   Items �� *ViewModel.NavigationItems+$ TwoWay=��� $��1�� �� CornerRadius$ 8,0,0,0��._��  �� $
��Auto ��� $��* �.+� � -     �Name$ 	RootFrame��$��1��$��0q�   Wpf.Ui.Controls.Breadcrumb  $��0��$��0,0,0,12q�$
��Left=�$	��Top=�$��24(�   
Navigation �� $ RootNavigation��$ OneWay=�  Wpf.Ui.Controls.TitleBar  $��0��   Icon$; 5pack://application:,,,/Assets/applicationIcon-256.png��   UseSnapLayout$
 True��   Title �� *ViewModel.ApplicationTitle+$ OneWay=�   Tray    Wpf.Ui.Controls.NotifyIcon     FocusOnLeftClick$
 True��   Icon$; 5pack://application:,,,/Assets/applicationIcon-256.png��   MenuOnRightClick$
 True��   TooltipText$ JBOT.WPF��   Menu �� .@�m� $
��Home��
 � Tag$
 home��