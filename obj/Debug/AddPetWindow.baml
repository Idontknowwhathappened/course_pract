   M S B A M L   `   `   `  ����  VeterinaryClinic#    VeterinaryClinic.AddPetWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   _ [MaterialDesignThemes.Wpf, Version=5.1.0.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5   	     � Title$%  Добавить питомца��5   	   $	��400��6"   $	��400��6/   ���	#FFFFE2E5 � 	Resources 5         
CustomFont��%U   &	        *�5   
   73pack://application:,,,/Fonts/#Montserrat Alternates)�6]   5      .��5	      �� $��10q�6   .+�5
   
   �� -   5         ��Name$ PetNameTextBox��5
   ;   #��[  6   $	��0,5q�6S   .  (MaterialDesignThemes.Wpf.TextFieldAssist   UnderlineBrush �	#FFAB648C   HasClearButton . $��40��5      )  #MaterialDesignThemes.Wpf.HintAssist   Hint$% Имя пользователя��5      ���	#FF2D2226   
IsFloating .  ���	#FFAB648C �� 
CaretBrush �	#FFAB648C �� SelectionBrush �	#FFAB648C��5      Z� (  "System.Windows.Controls.DatePicker  5   
   -   5        $��BirthDatePicker��5      $��40��6,   $  Дата рождения:��68   ���	#FFAB648C#��[  6�   $	��0,5q�6�   �� 5   
   -   5        	 ��Name$	 KindComboBox��5   T   $��5q�6   $��40��6   #��[  6+   $ Вид��5      $	��0,5q�68   ���#0A1129���	#FFAB648C��6E   Z� 6p   �� 5   
   -   5        $	 SexComboBox��5   T   $��5q�6   $��40��6   #��[  6+   $ Пол��5      $	��0,5q�68   ���#0A1129���	#FFAB648C��6E   Z� 6p   �� 5   
   -   5        $%��Добавить питомца��5      #��[  6,   ���	#FFC28DAB���	#FFAB648C$
��0,10q�6�   $��Center=�6�   5      5      