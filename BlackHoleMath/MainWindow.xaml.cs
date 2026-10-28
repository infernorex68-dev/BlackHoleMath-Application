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
        private void CenterVisualElements()
        {
            double holeSize = BlackHoleEllipse.Width; // keep the default 100 from XAML
            Canvas.SetLeft(BlackHoleEllipse, (VisualCanvas.ActualWidth - holeSize) / 2);
            Canvas.SetTop(BlackHoleEllipse, (VisualCanvas.ActualHeight - holeSize) / 2);

            double diskWidth = AccretionDisk.Width;   // keep the default 180
            double diskHeight = AccretionDisk.Height; // keep the default 60
            Canvas.SetLeft(AccretionDisk, (VisualCanvas.ActualWidth - diskWidth) / 2);
            Canvas.SetTop(AccretionDisk, (VisualCanvas.ActualHeight - diskHeight) / 2);
        }
        private void GenerateStarfield()
        {
            var rnd = new Random();
            int starCount = 120;

            for (int i = 0; i < starCount; i++)
            {
                double size = rnd.NextDouble() * 2 + 1;
                double x = rnd.NextDouble() * this.ActualWidth;
                double y = rnd.NextDouble() * this.ActualHeight;
                double baseOpacity = rnd.NextDouble() * 0.6 + 0.3;

                var star = new Ellipse
                {
                    Width = size,
                    Height = size,
                    Fill = System.Windows.Media.Brushes.White,
                    Opacity = baseOpacity
                };

                var drift = new TranslateTransform();
                star.RenderTransform = drift;

                Canvas.SetLeft(star, x);
                Canvas.SetTop(star, y);
                StarfieldCanvas.Children.Add(star);

                // Slow drift movement (every star gets this)
                double driftDistance = rnd.NextDouble() * 15 + 5; // 5–20 px range
                double driftDuration = rnd.NextDouble() * 10 + 8;  // 8–18 seconds

                var driftAnim = new DoubleAnimation
                {
                    From = 0,
                    To = driftDistance,
                    Duration = TimeSpan.FromSeconds(driftDuration),
                    AutoReverse = true,
                    RepeatBehavior = RepeatBehavior.Forever,
                    BeginTime = TimeSpan.FromSeconds(rnd.NextDouble() * 5)
                };
                drift.BeginAnimation(TranslateTransform.YProperty, driftAnim);

                // Twinkle (about a third of stars)
                if (rnd.NextDouble() < 0.3)
                {
                    var twinkle = new DoubleAnimation
                    {
                        From = baseOpacity,
                        To = baseOpacity * 0.2,
                        Duration = TimeSpan.FromSeconds(rnd.NextDouble() * 2 + 1.5),
                        AutoReverse = true,
                        RepeatBehavior = RepeatBehavior.Forever,
                        BeginTime = TimeSpan.FromSeconds(rnd.NextDouble() * 3)
                    };
                    star.BeginAnimation(UIElement.OpacityProperty, twinkle);
                }
            }
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
            GenerateStarfield();
            Storyboard intro = (Storyboard)FindResource("IntroAnimation");
            intro.Begin();
            Storyboard glowPulse = (Storyboard)FindResource("GlowPulse");
            glowPulse.Begin();
            CenterVisualElements();

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
           const double ReducedPlanckConstant = 1.0545718e-34; // in m² kg / s
            const double BoltzmannConstant = 1.380649e-23; // in J/K
            bool isSolarMass = UnitComboBox.SelectedIndex == 1;
            mass = isSolarMass ? massInput * SolarMass : massInput;

            const double GravitationalConstant = 6.6742e-11;
            const double SpeedOfLight = 2.99792458e8;
            const double SphereVolumeFactor = 4.0 / 3.0 * Math.PI;
            

            double radius = GravitationalConstant * 2 * mass / (SpeedOfLight * SpeedOfLight);
            double volume = SphereVolumeFactor * Math.Pow(radius, 3);
            double circumference = 2 * Math.PI * radius;
            double surfaceArea = 4 * Math.PI * radius * radius;
            double surfaceGravity = SpeedOfLight * SpeedOfLight * SpeedOfLight * SpeedOfLight / (4 * GravitationalConstant * mass);
            double photonSphereRadius = 1.5 * radius;
            double hawkingTemperature = (1.0545718e-34 * SpeedOfLight * SpeedOfLight * SpeedOfLight) / (8 * Math.PI * GravitationalConstant * mass * 1.380649e-23);
            double gravitationalTimeDilation = 1 / Math.Sqrt(1 - (2 * GravitationalConstant * mass) / (radius * SpeedOfLight * SpeedOfLight));
            double observationRadius = radius + 1000;
            double timeDilationAtBuffer = 1 / Math.Sqrt(1 - (2 * GravitationalConstant * mass) / (observationRadius * SpeedOfLight * SpeedOfLight));
            double entropy = (BoltzmannConstant * SpeedOfLight * SpeedOfLight * SpeedOfLight * surfaceArea) / (4 *GravitationalConstant * ReducedPlanckConstant);
            double averageDensity = mass / volume;

            // Update results text
            RadiusText.Text = $"Radius: {radius:E3} m";
            CircumferenceText.Text = $"Circumference: {circumference:E3} m";
            SurfaceAreaText.Text = $"Surface Area: {surfaceArea:E3} m²";
            SurfaceGravityText.Text = $"Surface Gravity: {surfaceGravity:E3} m/s²";
            PhotonSphereText.Text = $"Photon Sphere Radius: {photonSphereRadius:E3} m";
            HawkingRadiationText.Text = $"Hawking Radiation Temperature: {hawkingTemperature:E3} K";
            GravitationalTimeDilationText.Text = $"Gravitational Time Dilation Factor: {gravitationalTimeDilation:E3}";
            TimeDilationAt1kmAboveEventHorizonText.Text = $"Time Dilation at 1 km above Event Horizon: {timeDilationAtBuffer:E3}";
            EntropyText.Text = $"Entropy: {entropy:E3} J/K";
            AverageDensityText.Text = $"Average Density: {averageDensity:E3} kg/m³";

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
            // Keep the orbit pivot centered on the same point as the disk/black hole
           

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