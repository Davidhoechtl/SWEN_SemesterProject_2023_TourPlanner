using System.Windows.Controls;
using System.Windows.Media;
using System.Windows;

namespace TourPlannerFrontEnd.Infrastructure.CustomControls
{
    public class WatermarkTextBox : TextBox
    {
        public string WatermarkText
        {
            get { return (string)GetValue(WatermarkTextProperty); }
            set { SetValue(WatermarkTextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for WatermarkText.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty WatermarkTextProperty =
            DependencyProperty.Register("WatermarkText", typeof(string), typeof(WatermarkTextBox), new PropertyMetadata(null));

        public Brush WatermarkBrush
        {
            get { return (Brush)GetValue(WatermarkBrushProperty); }
            set { SetValue(WatermarkBrushProperty, value); }
        }

        // Using a DependencyProperty as the backing store for WatermarkBrush.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty WatermarkBrushProperty =
            DependencyProperty.Register("WatermarkBrush", typeof(Brush), typeof(WatermarkTextBox), new PropertyMetadata(Brushes.LightGray));

        private Brush ForegroundBrush
        {
            get { return (Brush)GetValue(ForegroundBrushProperty); }
            set { SetValue(ForegroundBrushProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ForegroundBrush.  This enables animation, styling, binding, etc...
        private static readonly DependencyProperty ForegroundBrushProperty =
            DependencyProperty.Register("ForegroundBrush", typeof(Brush), typeof(WatermarkTextBox), new PropertyMetadata(null));

        private bool HasCustomText
        {
            get { return (bool)GetValue(HasCustomTextProperty); }
            set { SetValue(HasCustomTextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for HasCustomText.  This enables animation, styling, binding, etc...
        private static readonly DependencyProperty HasCustomTextProperty =
            DependencyProperty.Register("HasCustomText", typeof(bool), typeof(WatermarkTextBox), new PropertyMetadata(false));

        public bool FocusHandlerRunning
        {
            get { return (bool)GetValue(FocusHandlerRunningProperty); }
            set { SetValue(FocusHandlerRunningProperty, value); }
        }

        // Using a DependencyProperty as the backing store for FocusHandlerRunning.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FocusHandlerRunningProperty =
            DependencyProperty.Register("FocusHandlerRunning", typeof(bool), typeof(WatermarkTextBox), new PropertyMetadata(false));

        static WatermarkTextBox()
        {
            //DefaultStyleKeyProperty.OverrideMetadata(typeof(WatermarkTextBox), new FrameworkPropertyMetadata(typeof(WatermarkTextBox)));
            EventManager.RegisterClassHandler(typeof(WatermarkTextBox), TextBox.GotFocusEvent, new RoutedEventHandler(OnGotFocusClassHandler));
            EventManager.RegisterClassHandler(typeof(WatermarkTextBox), TextBox.LostFocusEvent, new RoutedEventHandler(OnLostFocusClassHandler));
            EventManager.RegisterClassHandler(typeof(WatermarkTextBox), TextBox.TextChangedEvent, new TextChangedEventHandler(OnTextChangedClassHandler));
        }

        private static void OnGotFocusClassHandler(object sender, RoutedEventArgs e)
        {
            var textbox = sender as WatermarkTextBox;
            if (textbox != null)
            {
                textbox.FocusHandlerRunning = true;
                textbox.Foreground = textbox.ForegroundBrush;
                if (!textbox.HasCustomText)
                {
                    textbox.Text = "";
                }
                textbox.FocusHandlerRunning = false;
            }
        }

        private static void OnLostFocusClassHandler(object sender, RoutedEventArgs e)
        {
            var textbox = sender as WatermarkTextBox;
            if (textbox != null && !textbox.HasCustomText)
            {
                textbox.FocusHandlerRunning = true;
                textbox.Foreground = textbox.WatermarkBrush;
                textbox.Text = textbox.WatermarkText;
                textbox.FocusHandlerRunning = false;
            }
        }

        private static void OnTextChangedClassHandler(object sender, TextChangedEventArgs e)
        {
            var textbox = sender as WatermarkTextBox;
            if (textbox != null && !textbox.FocusHandlerRunning)
            {
                textbox.HasCustomText = textbox.Text != "" && textbox.Text != null;
            }
            if (textbox.Text == null || textbox.Text.Length == 0 && !textbox.HasEffectiveKeyboardFocus)
            {
                textbox.Text = textbox.WatermarkText;
                textbox.Foreground = textbox.WatermarkBrush;
                textbox.HasCustomText = false;
            }
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            ForegroundBrush = Foreground;
            if (Text == null || Text.Length == 0)
            {
                Foreground = WatermarkBrush;
                Text = WatermarkText;
                HasCustomText = false;
            }
            else
            {
                HasCustomText = true;
            }
        }
    }
}
