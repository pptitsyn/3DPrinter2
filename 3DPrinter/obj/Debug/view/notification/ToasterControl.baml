   M S B A M L   `   `   `  ����D  @3DPrinter, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullK*clr-namespace:_3DPrinter.view.notification_3DPrinter.view.notification  5      1    +_3DPrinter.view.notification.ToasterControl   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Cnotification*clr-namespace:_3DPrinter.view.notification     5      -   5        
������   � Triggers  5	      :� 5
   
    :� RoutedEvent$ UserControl.Unloaded��6   .[�5      �� .��5      �� .��5      U�  g� Duration$ 0:0:1D�64    �� TargetProperty$, &RenderTransform.(TranslateTransform.X)�6F   .\�5      9  3System.Windows.Media.Animation.EasingDoubleKeyFrame   O� KeyTime$ 0%��6/    O� Value$ 0Q�6<     5      $	 99%��6/   $	 360Q�6=   5      5      5      5       � 	Resources 5         FilledColor �� A �� B	 �� R
 �� G  UnfilledColor  BusyAnimationStyle �� 
TargetType �� Property �� Value �� 
TargetType � 	Resources  Key g� 	BeginTime g� RepeatBehavior �� 
TargetName �� KeyTime �� Value �� Triggers P� Property P� Value N� EnterActions ��Name  
Animation0  
Animation1  
Animation2  
Animation3  
Animation4 	 
Animation5 
 
Animation6  
Animation7 N� ExitActions �� BeginStoryboardName A�Name �� Left
 �� Top �� Path��%�  &	        &	 J     &	 �     7   7  7   7  7   7  7   7  7   7  7   7  7   7  7   7  7  7  7  7  7  7 	 7 
 7  �� 5   
   $	 255��6$   $	 155��6,   $		 155��64   $	
 155��6<   �� 5   
   $ 0��6&   $	 155��6,   $		 155��64   $	
 155��6<   �� 5   
   " ��6+   .�5      ��  � �� �	#7F0000006+   �� 5       � �� 5      �� 5       " ��6&    5!      ���� 5"      
Animation0 $ 
00:00:00.0X�6<   $ Forever��6S   .��5#   "   �� $ ellipse0��6?   $* $(Shape.Fill).(SolidColorBrush.Color)�6`   .g�5$   &   �� $ 
00:00:00.0��6:   8   6O   �� 5%   &   $ 
00:00:01.6��6:   8  6O   5&   #   5'      �� 5)      
Animation1 $ 
00:00:00.2X�6<   $ Forever��6S   .��5*   "   �� $ ellipse1��6?   $* $(Shape.Fill).(SolidColorBrush.Color)�6`   .g�5+   &   �� $ 
00:00:00.0��6:   8  6O   �� 5,   &   $ 
00:00:01.6��6:   8  6O   5-   #   5.      �� 50      
Animation2 $ 
00:00:00.4X�6<   $ Forever��6S   .��51   "   �� $ ellipse2��6?   $* $(Shape.Fill).(SolidColorBrush.Color)�6`   .g�52   &   �� $ 
00:00:00.0��6:   8  6O   �� 53   &   $ 
00:00:01.6��6:   8  6O   54   #   55      �� 57      
Animation3 $ 
00:00:00.6X�6<   $ Forever��6S   .��58   "   �� $ ellipse3��6?   $* $(Shape.Fill).(SolidColorBrush.Color)�6`   .g�59   &   �� $ 
00:00:00.0��6:   8  6O   �� 5:   &   $ 
00:00:01.6��6:   8  6O   5;   #   5<      �� 5>      
Animation4 $ 
00:00:00.8X�6<   $ Forever��6S   .��5?   "   �� $ ellipse4��6?   $* $(Shape.Fill).(SolidColorBrush.Color)�6`   .g�5@   &   �� $ 
00:00:00.0��6:   8  6O   �� 5A   &   $ 
00:00:01.6��6:   8 	 6O   5B   #   5C      �� 5E      
Animation5 $ 
00:00:01.0X�6<   $ Forever��6S   .��5F   "   �� $ ellipse5��6?   $* $(Shape.Fill).(SolidColorBrush.Color)�6`   .g�5G   &   �� $ 
00:00:00.0��6:   8 
 6O   �� 5H   &   $ 
00:00:01.6��6:   8  6O   5I   #   5J      �� 5L      
Animation6 $ 
00:00:01.2X�6<   $ Forever��6S   .��5M   "   �� $ ellipse6��6?   $* $(Shape.Fill).(SolidColorBrush.Color)�6`   .g�5N   &   �� $ 
00:00:00.0��6:   8  6O   �� 5O   &   $ 
00:00:01.6��6:   8  6O   5P   #   5Q      �� 5S      
Animation7 $ 
00:00:01.4X�6<   $ Forever��6S   .��5T   "   �� $ ellipse7��6?   $* $(Shape.Fill).(SolidColorBrush.Color)�6`   .g�5U   &   �� $ 
00:00:00.0��6:   8  6O   �� 5V   &   $ 
00:00:01.6��6:   8  6O   5W   #   5X      5Y       5[      P� 5\       �@J�	IsVisible .  5]   "   �� 5^   &   $ Storyboard0��6_   8�� �� 5_   &   $ Storyboard1��6_   8�� �� 5`   &   $ Storyboard2��6_   8�� �� 5a   &   $ Storyboard3��6_   8�� �� 5b   &   $ Storyboard4��6_   8�� �� 5c   &   $ Storyboard5��6_   8�� �� 5d   &   $ Storyboard6��6_   8�� �� 5e   &   $ Storyboard7��6_   8��  5h   "   �� 5i   &   $ Storyboard0��65   �� 5j   &   $ Storyboard1��65   �� 5k   &   $ Storyboard2��65   �� 5l   &   $ Storyboard3��65   �� 5m   &   $ Storyboard4��65   �� 5n   &   $ Storyboard5��65   �� 5o   &   $ Storyboard6��65   �� 5p   &   $ Storyboard7��65   5r      .R�5u      �� #��z��6!   #��z��6M   #��z��6�   ._�5v      � .+�5w   "   �� $��60��6)   $��60��65    5x   &   ���� 5y   *   'A�       " A�60   .�5z   .   ��  � ��$ 15��6F   �� 5{   .    � ��$ 15��6G   �� 5|   .    � �� �	#009B9B9B6E   5}   +   5~   '   .+�5�   &   A� $ ellipse0��6.   $
 1.75��6@   $ 21��6S   A� 5�   &   $ ellipse1��6.   $ 7��6@   $	 6.5��6O   A� 5�   &   $ ellipse2��6.   $
 20.5��6@   $
 0.75��6S   A� 5�   &   $ ellipse3��6.   $ 34.75��6@   $
 6.75��6T   A� 5�   &   $ ellipse4��6.   $
 40.5��6@   $ 20.75��6S   A� 5�   &   $ ellipse5��6.   $ 34.75��6@   $
 34.5��6T   A� 5�   &   $ ellipse6��6.   $ 20.75��6@   $ 39.75��6T   A� 5�   &   $ ellipse7��6.   $ 34.25��6@   $ 7��6S   A� 5�   &   $
��39.5��6.   $
��39.5��6;   $
 8.75��6I   $ 8��6\   $y�Hidden=�6k   5�   #   �� 5�   "   $��Center=�6F   $��Center=�6c   ��6(   �� $
 Text�6c   5�      5�      5�      5�      5�      5�      .��5�      �� -   5        $��Notification��5�        �� Padding$  0q�6!   ��6-   �� *�� ! �� TypeInTargetAssembly,   &_3DPrinter.view.notification.ThemeKeys"!  " �� 
ResourceId$" 	ToastCard��+._�5�   
   � $��0q�6   # � RowDefinitions# 5�      �� 5�      $
��Auto �6    �� 5�      $��* �6    �� 5�      $
��Auto �6    $ � ColumnDefinitions$ 5�   
   �� 5�      $
��Auto �6   �� 5�      $��* �6   �� 5�      $
��Auto �6   .+�5�      �� $��5q�6   $	��Top=�6   $��0��67   $��2��6D   $��24��6U   $��24��6`   $
��None=�6l   ��6{   �� *Image+% �� RelativeSource% �� & �� AncestorType$!& notification:ToasterControlL�' �� Mode$' FindAncestor=��� 5�      -   5        $��2��5�      $��2��6&   $��Right=�66   $��Отмена��6R   $��10,15q�6c   6�   �� 5�      $��1��6   $��16(�6�   ��6(   �� *Title+% �� $!& notification:ToasterControlL�$' FindAncestor=���6�   �� *�� "!  $" 
ToastTitle��+�� 5�      $��1��6   $��1��6&   $��12(�6�   ��66   �� *	Message+% �� $!& notification:ToasterControlL�$' FindAncestor=���6�   �� *�� "!  $" ToastMessage��+� 5�      -   5        ( �Name$( ProgressBar1��5�      $��2��60   $��3��6=   $��0,5,0,2q�6Q   $��5��6b   $��Bottom=�6m   $y�Hidden=�6�   6�   5�      5�      5�      