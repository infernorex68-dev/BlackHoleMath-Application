using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Media.Animation;

namespace BlackHoleMath
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void TitleBar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void MinimizeButton_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Storyboard intro = (Storyboard)FindResource("IntroAnimation");
            intro.Begin();
        }
        private void CalculateButton_Click(object sender, RoutedEventArgs e)
        {
            ErrorText.Text = "";
            NoteText.Text = "";

            // Read mass input
            if (!double.TryParse(MassInputBox.Text, out double massInput))
            {
                ErrorText.Text = "Please enter a valid number.";
                return;
            }

            const double SolarMass = 1.989e30;
            double mass;

            bool isSolarMass = UnitComboBox.SelectedIndex == 1;
            mass = isSolarMass ? massInput * SolarMass : massInput;

            const double GravitationalConstant = 6.6742e-11;
            const double SpeedOfLight = 2.99792458e8;

            double radius = GravitationalConstant * 2 * mass / (SpeedOfLight * SpeedOfLight);
            double circumference = 2 * Math.PI * radius;
            double surfaceArea = 4 * Math.PI * radius * radius;

            // Update results text
            RadiusText.Text = $"Radius: {radius:E3} m";
            CircumferenceText.Text = $"Circumference: {circumference:E3} m";
            SurfaceAreaText.Text = $"Surface Area: {surfaceArea:E3} m²";

            // Notes based on radius
            if (radius < 0.0001)
            {
                NoteText.Foreground = System.Windows.Media.Brushes.Red;
                NoteText.Text = "Warning: extremely small radius — very dense black hole.";
            }
            else if (radius > 1000000)
            {
                NoteText.Foreground = System.Windows.Media.Brushes.Green;
                NoteText.Text = "Note: quite large — supermassive black hole range.";
            }
            else if (radius > 1000)
            {
                NoteText.Foreground = System.Windows.Media.Brushes.Yellow;
                NoteText.Text = "Note: stellar-mass black hole range.";
            }

            // Classification
            string classification;
            if (mass < 1e31) classification = "Stellar-Mass Black Hole";
            else if (mass > 1e36) classification = "Unknown Black Hole Type";
            else if (mass <= 1e32) classification = "Intermediate Black Hole";
            else classification = "Supermassive Black Hole";

            ClassificationText.Text = $"Classification: {classification}";

            // Update the visual (Will hook this up properly)
            UpdateBlackHoleVisual(radius);
        }

        private void UpdateBlackHoleVisual(double radius)
        {
            double logRadius = Math.Log10(Math.Max(radius, 1e-10));
            double minLog = -10, maxLog = 10;
            double minSize = 20, maxSize = 300;

            double t = (logRadius - minLog) / (maxLog - minLog);
            t = Math.Max(0, Math.Min(1, t));

            double size = minSize + t * (maxSize - minSize);

            // Resize + center the black hole
            BlackHoleEllipse.Width = size;
            BlackHoleEllipse.Height = size;
            Canvas.SetLeft(BlackHoleEllipse, (VisualCanvas.ActualWidth - size) / 2);
            Canvas.SetTop(BlackHoleEllipse, (VisualCanvas.ActualHeight - size) / 2);

            // Resize + center the accretion disk proportionally
            double diskWidth = size * 1.8;
            double diskHeight = size * 0.6;
            AccretionDisk.Width = diskWidth;
            AccretionDisk.Height = diskHeight;
            Canvas.SetLeft(AccretionDisk, (VisualCanvas.ActualWidth - diskWidth) / 2);
            Canvas.SetTop(AccretionDisk, (VisualCanvas.ActualHeight - diskHeight) / 2);

            // Color + glow sync
            System.Windows.Media.Color glowColor;
            if (size > 200)
            {
                glowColor = (Color)ColorConverter.ConvertFromString("#fff4d6");
                BlackHoleEllipse.Stroke = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#fff4d6"));
            }
            else if (size > 100)
            {
                glowColor = (Color)ColorConverter.ConvertFromString("#ff8c1a");
                BlackHoleEllipse.Stroke = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#ff8c1a"));
            }
            else
            {
                glowColor = (Color)ColorConverter.ConvertFromString("#ec1857");
                BlackHoleEllipse.Stroke = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#ec1857"));
            }
            GlowEffect.Color = glowColor;
        }
    }
}