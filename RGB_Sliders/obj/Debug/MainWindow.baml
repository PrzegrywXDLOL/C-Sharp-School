   M S B A M L   `   `   `  ����F  BRGB_Sliders, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:RGB_SlidersRGB_Sliders  5   	       RGB_Sliders.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   +localclr-namespace:RGB_Sliders     5   	     � Title$  
MainWindow��5   	   $	��450��6   $	��400��6)   .��5	      �� .+�5
   
    � -   5          �Name$ frame��5
      $	��250��5      $	��250��5      $��25q�5      
���   �$��2q�5      ��5
      ��  �� ElementName$	 Hex�� �� Path$
 Text�5      � 5   
   $	��350��6   .+�5      �� $	��R: ��6   $
��Bold"�6#   �� 5      -   5         ��Name$ 	sliderRed��5       � Minimum$ 0Q�5       � Maximum$	 255Q�5      $	��300��5       �� IsSnapToTickEnabled .  �� TickFrequency$ 1Q�5      5      �� 5      $��20��5      	 �� TextAlignment$	 Center=�5      $��Right=�5      ��5      �� *Value+$ 	sliderRed��5      5      � 5   
   $	��350��6   $��10q�6   .+�5      �� $	��G: ��6   $
��Bold"�6#   �� 5       -   5        $ sliderGreen��5       $ 0Q�5!      $	 255Q�5"      $	��300��5#       . $ 1Q�5%      5&      �� 5'      $��20��5(      $	 Center=�5)      $��Right=�5*      ��5'      �� *Value+$ sliderGreen��5*      5+      � 5,   
   $	��350��6   .+�5-      �� $	��B: ��6   $
��Bold"�6#   �� 5.      -   5        $ 
sliderBlue��5.      $ 0Q�5/      $	 255Q�50      $	��300��51       . $ 1Q�53      54      �� 55      $��20��56      $	 Center=�57      $��Right=�58      ��55      �� *Value+$ 
sliderBlue��58      59      � 5:   
   $	��175��6   $��10q�6   .+�5;      �� $��HEX: ��6   �� 5<      -   5        
 ��Name$	
 Hex��5<      $	��100��5=       �� TextAlignment$ Center=�5>      5?      5A      5B      