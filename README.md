Black Hole Math

A Windows desktop app that calculates the basic physical properties of a black hole from its mass, and draws a rough visual of it in real time.

Enter a mass in kilograms or solar masses and the app returns the Schwarzschild radius, circumference, and surface area of the event horizon, classifies the black hole by mass, and resizes and recolors an animated black hole graphic to match.

Table of Contents:
Features
Tech Stack
Getting Started
Usage
The Math
Classification and Notes
The Visual
Project Structure
Known Limitations
Roadmap

Features
Two input units: kilograms or solar masses
Event horizon calculations: radius, circumference, and surface area, displayed in scientific notation
Mass classification: stellar-mass, intermediate, supermassive, or unknown
Contextual notes: color-coded messages about how large or dense the resulting black hole is
Live visual: the black hole and its accretion disk scale (logarithmically) with the computed radius, and the glow color shifts with size
Custom sci-fi UI: borderless window with a custom title bar, dark theme, the Orbitron font, and an animated intro
Input validation: non-numeric input shows an inline error instead of crashing
Tech Stack
	
Language:	C#
UI framework	WPF (Windows Presentation Foundation)
Target framework	net10.0-windows
Font	Orbitron (variable), bundled as a resource

WPF is Windows-only, so this app runs on Windows only.

Getting Started
Prerequisites
Windows 10 or later
.NET 10 SDK
Optional: a recent Visual Studio with the .NET desktop development workload (the solution uses the newer .slnx format, so use an up-to-date version)
Clone
bash
git clone https://github.com/<your-username>/BlackHoleMath-Application.git
cd BlackHoleMath-Application
Build and run from the command line
bash
dotnet build
dotnet run --project BlackHoleMath/BlackHoleMath.csproj
Build and run from Visual Studio
Open BlackHoleMath.slnx.
Set BlackHoleMath as the startup project (it is the only project).
Press F5 to run.
Publish a standalone build (optional)
bash
dotnet publish BlackHoleMath/BlackHoleMath.csproj -c Release -r win-x64 --self-contained false
Usage
Choose a Mass Unit (Kilograms or Solar Masses).
Type a value into Mass Value. Scientific notation works, e.g. 1.989e30.
Click Calculate.
Read the results panel and watch the visual update.

The window can be dragged by its title bar, minimized or closed with the title bar buttons, and resized with the grip in the bottom-right corner.

Example

Input: 1 Solar Masses

Output	Value (approx.)
Radius	2.954E+003 m
Circumference	1.856E+004 m
Surface Area	1.097E+008 m²
Classification	Stellar-Mass Black Hole

A black hole with the mass of the Sun would have an event horizon only about 3 km in radius.

The Math

The app computes the Schwarzschild radius, the radius of the event horizon of a non-rotating, uncharged black hole:

r = 2GM / c²

From the radius, it derives:

Circumference = 2πr
Surface Area  = 4πr²
Constants used
Constant	Symbol	Value
Gravitational constant	G	6.6742 × 10⁻¹¹ m³ kg⁻¹ s⁻²
Speed of light	c	2.99792458 × 10⁸ m/s
Solar mass	M☉	1.989 × 10³⁰ kg

When the unit is Solar Masses, the input is multiplied by M☉ to get kilograms before calculating. All outputs are in SI units (meters, square meters).

Classification and Notes

After each calculation, the app labels the black hole by mass (in kilograms) and shows a note based on the radius.

Mass classification
Mass (kg)	Classification
< 1 × 10³¹	Stellar-Mass Black Hole
1 × 10³¹ to 1 × 10³²	Intermediate Black Hole
> 1 × 10³² to 1 × 10³⁶	Supermassive Black Hole
> 1 × 10³⁶	Unknown Black Hole Type
Radius notes
Radius	Note	Color
< 0.0001 m	Extremely small radius, very dense black hole	Red
> 1,000,000 m	Quite large, supermassive black hole range	Green
> 1,000 m	Stellar-mass black hole range	Yellow
otherwise	No note	n/a
The Visual

The canvas on the right shows an event horizon (black disk with a glowing outline) in front of a tilted accretion disk.

Scaling: the radius is mapped on a log₁₀ scale from 10⁻¹⁰ m to 10¹⁰ m onto a disk diameter of 20 to 300 px, and values outside that range are clamped. This keeps tiny and enormous black holes both visible.
Accretion disk: sized proportionally to the event horizon (1.8× wide, 0.6× tall) and rotated -20°.
Glow and outline color by rendered size:
Rendered size	Color
> 200 px	Cream 
#fff4d6
> 100 px	Orange 
#ff8c1a
otherwise	Crimson 
#ec1857
Intro animation: on launch the black hole fades and scales in, followed by the accretion disk.

The visual shows relative scale only. It is not a physically accurate render, and it is not drawn to scale against real-world sizes.

Project Structure
BlackHoleMath-Application/
├── BlackHoleMath.slnx              # Solution file
├── .gitattributes
├── .gitignore
└── BlackHoleMath/
    ├── BlackHoleMath.csproj        # Project file (net10.0-windows, WPF)
    ├── App.xaml / App.xaml.cs      # Application entry point and theme resources
    ├── AssemblyInfo.cs             # WPF theme dictionary locations
    ├── MainWindow.xaml             # UI layout, custom title bar, intro animation
    ├── MainWindow.xaml.cs          # Calculation logic, classification, visual updates
    ├── black_hole.ico              # Window/app icon
    └── Fonts/
        └── Orbitron-Variable.ttf   # Bundled UI font

All calculation and UI logic currently lives in MainWindow.xaml.cs:

Method	Purpose
CalculateButton_Click	Parses input, runs the math, updates results, notes, and classification
UpdateBlackHoleVisual	Scales, centers, and recolors the black hole and accretion disk
Window_Loaded	Starts the intro animation
TitleBar_MouseLeftButtonDown, MinimizeButton_Click, CloseButton_Click	Custom window chrome behavior
Known Limitations
Non-rotating black holes only. Spin (Kerr) and charge are not modeled.
Mass validation is minimal. Only non-numeric input is rejected. Zero and negative masses are accepted and produce meaningless output.
Culture-dependent parsing. Numbers are parsed using the system locale, so a decimal comma vs. decimal point may matter.
Classification thresholds are simplified and do not match standard astronomical definitions (for example, real supermassive black holes such as Sagittarius A* exceed the 10³⁶ kg cutoff).
The visual is illustrative, not to scale and not a physical simulation.
Windows only, because of WPF.
Roadmap

Ideas for future work:

 Input validation for zero and negative mass
 Standard astronomical mass classes and more units (grams, Earth masses, kilometers, light-years)
 Extra properties: density, lifetime
 Spinning (Kerr) black holes
 Unit tests for the calculation logic, separated from the UI
 Refactor to MVVM
 Screenshots and a release build
Contributing

Acknowledgments
Orbitron font by Matt McInerney, licensed under the SIL Open Font License
Physics: Schwarzschild solution to Einstein's field equations
