   M S B A M L   `   `   `  ����U  QLiveCharts.Wpf, Version=0.9.7.0, Culture=neutral, PublicKeyToken=0bc1f845d1ebb8dfG4clr-namespace:LiveCharts.Wpf;assembly=LiveCharts.WpfLiveCharts.Wpf  5       VeterinaryClinic%   VeterinaryClinic.StatisticsPage   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Dlvc4clr-namespace:LiveCharts.Wpf;assembly=LiveCharts.Wpf     5        N� Title$  StatisticsPage��5	       � 	Resources 5
         
CustomFont��%U   &	        *�5   
   73pack://application:,,,/Fonts/#Montserrat Alternates)�6]   5      .��5      � $��10q�6   .+�5   
   �� $��Статистика��6   #��[  6&   $��24(�6O   $
��Bold"�6]   $��Center=�6o   $	��Top=�6�   $��	0,10,0,20q�6�   � 5   
   $��	0,50,0,65q�6    � ColumnDefinitions 5      �� 5      $
��387* �6#   �� 5      $
��493* �6#   .+�5      �� $��0��6   $��10,10,10,10q�6)   .+�5      �� $'��!Популярные услуги��6   #��[  65   $��18(�6^   $
��Bold"�6l   $��0,0,0,10q�6~      LiveCharts.Wpf.PieChart  5      -   5          Name$ ServicesChart��5      $��Stretch=�66   $	��259��6s   &    LiveCharts.Wpf.Charts.Base.Chart   Series 6T   �� *ChartSeries+6s   5      � 5       $��1��6   $��10q�6#   .+�5!      �� $
��Auto=�6   $
��Auto=�6B   .��5"      &   System.Windows.Controls.DataGrid  -   5          Name$ ServicesDataGrid��5"   H   #��[  6   $y�Visible=�6b   ���#FFFFFF
���   ����	#FFAB648C   AutoGenerateColumns .     
IsReadOnly . $��Stretch=�5#   M   $��Stretch=�6i   $��0q�6�      Columns 5$      0  *System.Windows.Controls.DataGridTextColumn  5%      ,  &System.Windows.Controls.DataGridColumn	   Header$#	 Название услуги��61   
   
FontFamily#
 [  6J      FontSize$ 12Q�6�      Width5  /System.Windows.Controls.DataGridLengthConverter$	 250 6�   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6s   �� *name+6�     5&      $5	 /Количество использований��61   #
 [  6S   $ 12Q�6�   $	 210 6�    6|   �� *usage_count+6�     5'      $#	 Общая стоимость��61   #
 [  6J   $	 150 6�   $ 12Q�6�    6s   �� *price+6�   5)      5*      5+      5,      �� 5/   
    �� Orientation$ 
Horizontal=�6   $��Right=�6.   $��Bottom=�6J   $��10q�6e   .+�50      �� -   5        $*��$Экспорт базы данных��50      #��[  63   $	��220��6\   ���	#FFC28DAB���	#FFAB648C$��40��6�   $��5q�6�   6�   �� 51      -   5        $&�� Экспорт услуг в CSV��51      #��[  63   $	��200��6\   ���	#FFC28DAB���	#FFAB648C$��40��6�   $��5q�6�   6�   �� 52      -   5        $&�� Экспорт услуг в SQL��52      #��[  63   $	��250��6\   ���	#FFC28DAB���	#FFAB648C$��40��6�   $��5q�6�   6�   53      54      55      