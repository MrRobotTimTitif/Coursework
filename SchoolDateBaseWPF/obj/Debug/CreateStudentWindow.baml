   M S B A M L   `   `   `  ����L  HSchoolDateBaseWPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:SchoolDateBaseWPFSchoolDateBaseWPF  5   	   +    %SchoolDateBaseWPF.CreateStudentWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   1localclr-namespace:SchoolDateBaseWPF     5   	     � Title$  Create Student Window��5   	   $	��450��6(   $	��800��65    � WindowStartupLocation$ CenterScreen=�6B   .��5	      � .+�5
   
   �� -   5         ��Name$ cancelButton��5
      $��Cancel��6'   $
��Left=�68   $��
10,349,0,0q�6S   $	��Top=�6g   $��60��6�   $	��215��6�   $
��Bold"�6�   $��16(�6�   �� 5   
   -   5         ��Name$ labelcreatetype��5      $��Create Student��6)   $��Center=�6E   $��53��6b   $��198,10,176.6,0q�6n   $	��Top=�6�   $	��419��6�   $��36(�6�   $��RightToLeft=�6�   $��Center=�6�   $
��Bold"�6�   �� 5   
   $��
First Name��6   $
��Left=�6%   $��45��6@   $��	61,80,0,0q�6L   $	��Top=�6_   $	��100��6w   $��16(�6�   $
��Bold"�6�   �� 5   
   $��	Last Name��6   $
��Left=�6$   $��45��6?   $��
61,130,0,0q�6K   $	��Top=�6_   $	��100��6w   $��16(�6�   $
��Bold"�6�   �� 5   
   $��Class��6   $
��Left=�6    $��45��6;   $��
61,222,0,0q�6G   $	��Top=�6[   $	��100��6s   $��16(�6   $
��Bold"�6�   �� 5   
   -   5         ��Name$+ %textBoxFirstNameInCreateStudentWindow��5      $
��Left=�6A   $��45��6\   $��
198,80,0,0q�6h    �� TextWrapping$
 Wrap=�6|   $�� ��6�   $	��Top=�6�   $	��210��6�   $��Center=�6�   6�   �� 5   
   -   5        $* $textBoxLastNameInCreateStudentWindow��5      $
��Left=�6A   $��45��6\   $��198,130,0,0q�6h   $
 Wrap=�6}   $�� ��6�   $	��Top=�6�   $	��210��6�   $��Center=�6�   $��16(�6�   6�   �� 5   
   -   5        $ buttonCreateStudent��5      $��Create Student��6.   $
��Left=�6G   $��60��6b   $��469,349,0,0q�6n   $	��Top=�6�   $	��315��6�   $
��Bold"�6�   $��16(�6�   �� 5   
   -   5        $ labelCreateError��5      $��Italic$�6*   $y�Hidden=�6=   $�� ��6Q   $
��Left=�6]   $��45��6x   $��469,304,0,0q�6�   $	��Top=�6�   $	��315��6�   �� 5   
   -   5         ��Name$" comboBoxClassOnCreateStudent��5       �� SelectedIndex$ 0��6:   $��Center=�6M   $��Center=�6o   $
��Left=�6�   $��198,222,0,0q�6�   $	��Top=�6�   $��45��6�   $��61��6�   $
��Bold"�6�   $��18(�6  .@�5      �� A6   �� 5      B6   �� 5      C6   �� 5      D6   �� 5      F6   5      5      5       