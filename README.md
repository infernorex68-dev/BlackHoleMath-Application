<div align="center">

# Black Hole Math

**A Windows desktop app that calculates the physics of a black hole from its mass and renders it live.**

![Language](https://img.shields.io/badge/language-C%23-ec1857?style=flat-square&labelColor=1a0e12)
![Framework](https://img.shields.io/badge/framework-WPF-ec1857?style=flat-square&labelColor=1a0e12)
![.NET](https://img.shields.io/badge/.NET-10-ec1857?style=flat-square&labelColor=1a0e12)
![Platform](https://img.shields.io/badge/platform-Windows-ec1857?style=flat-square&labelColor=1a0e12)

[Features](#features) · [Getting Started](#getting-started) · [The Math](#the-math) · [Roadmap](#roadmap)

</div>

---

## Overview

Enter a mass in kilograms or solar masses and Black Hole Math returns the properties of the corresponding non-rotating black hole: the event horizon's radius, circumference and area, surface gravity, photon sphere, Hawking temperature, gravitational time dilation, and Bekenstein-Hawking entropy. It also classifies the black hole by mass and resizes and recolors an animated black hole graphic to match, all inside a custom sci-fi interface.

<!--
Add a screenshot here once you have one:
1. Save it as docs/screenshot.png
2. Replace this comment with:  ![Black Hole Math screenshot](docs/screenshot.png)
-->

## Table of Contents

- [Features](#features)
- [Tech Stack](#tech-stack)
- [Getting Started](#getting-started)
- [Usage](#usage)
- [Example Output](#example-output)
- [The Math](#the-math)
- [Classification and Notes](#classification-and-notes)
- [The Visual](#the-visual)
- [Project Structure](#project-structure)
- [Known Limitations](#known-limitations)
- [Roadmap](#roadmap)
- [Contributing](#contributing)
- [Acknowledgments](#acknowledgments)

## Features

**Calculations**

- Two input units: kilograms or solar masses
- Event horizon: radius, circumference and surface area
- Surface gravity at the horizon
- Photon sphere radius, where light can orbit the black hole
- Hawking radiation temperature
- Gravitational time dilation, at the horizon and 1 km above it
- Bekenstein-Hawking entropy
- All results displayed in scientific notation with SI units

**Classification and feedback**

- Mass classification: stellar-mass, intermediate, supermassive, or unknown
- Color-coded notes about how large or dense the resulting black hole is
- Input validation: non-numeric input shows an inline error instead of crashing

**Visual and interface**

- The black hole and its accretion disk scale logarithmically with the computed radius, and the glow color shifts with size
- Custom sci-fi UI: borderless window with a custom title bar, dark theme, themed dropdown, and the Orbitron font
- Animated intro, a softly pulsing glow, and a drifting, twinkling starfield behind the whole window

## Tech Stack

| Component | Choice |
| --- | --- |
| Language | C# |
| UI framework | WPF (Windows Presentation Foundation) |
| Target framework | `net10.0-windows` |
| Font | Orbitron (variable), bundled as a resource |

> [!NOTE]
> WPF is Windows-only, so this app runs on Windows only.

## Getting Started

### Prerequisites

- Windows 10 or later
- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- Optional: a recent Visual Studio with the **.NET desktop development** workload (the solution uses the newer `.slnx` format, so use an up-to-date version)

### Clone

```bash
git clone https://github.com/infernorex68-dev/BlackHoleMath-Application.git
cd BlackHoleMath-Application
```

### Build and run from the command line

```bash
dotnet build
dotnet run --project BlackHoleMath/BlackHoleMath.csproj
```

### Build and run from Visual Studio

1. Open `BlackHoleMath.slnx`.
2. Set **BlackHoleMath** as the startup project (it is the only project).
3. Press **F5** to run.

### Publish a standalone build (optional)

```bash
dotnet publish BlackHoleMath/BlackHoleMath.csproj -c Release -r win-x64 --self-contained false
```

## Usage

1. Choose a **Mass Unit** (Kilograms or Solar Masses).
2. Type a value into **Mass Value**. Scientific notation works, for example `1.989e30`.
3. Click **Calculate**.
4. Read the **Results** and **Advanced** panels and watch the visual update.

The window can be dragged by its title bar, minimized or closed with the title bar buttons, and resized with the grip in the bottom-right corner.

## Example Output

Input: `5.972e24` **Kilograms** (the mass of the Earth)

| Quantity | Value (approx.) |
| --- | --- |
| Radius | 8.870E-003 m |
| Circumference | 5.573E-002 m |
| Surface Area | 9.886E-004 m² |
| Surface Gravity | 5.066E+018 m/s² |
| Photon Sphere Radius | 1.330E-002 m |
| Hawking Radiation Temperature | 2.054E-002 K |
| Gravitational Time Dilation Factor (at the horizon) | ∞ |
| Time Dilation at 1 km above Event Horizon | 1.000E+000 |
| Entropy | 1.306E+043 J/K |
| Classification | Stellar-Mass Black Hole |

Compressing the Earth to a black hole would leave an event horizon under a centimeter in radius. The 1 km time dilation reads 1.000 because 1 km is enormous compared with a horizon this small.

<details>
<summary>More examples: one solar mass</summary>

<br>

Input: `1` **Solar Masses**

| Quantity | Value (approx.) |
| --- | --- |
| Radius | 2.954E+003 m |
| Circumference | 1.856E+004 m |
| Surface Area | 1.097E+008 m² |
| Surface Gravity | 1.521E+013 m/s² |
| Photon Sphere Radius | 4.431E+003 m |
| Hawking Radiation Temperature | 6.169E-008 K |
| Time Dilation at 1 km above Event Horizon | 1.988E+000 |
| Entropy | 1.449E+054 J/K |
| Classification | Stellar-Mass Black Hole |

A black hole with the mass of the Sun would have an event horizon only about 3 km in radius.

</details>

Values are shown with a decimal point here. The app uses your system locale, so you may see a decimal comma.

## The Math

All calculations assume a non-rotating, uncharged (Schwarzschild) black hole of mass $M$. When the unit is Solar Masses, the input is multiplied by $M_\odot$ to get kilograms first. All outputs are in SI units.

### Constants used

| Constant | Symbol | Value |
| --- | --- | --- |
| Gravitational constant | $G$ | 6.6742 × 10⁻¹¹ m³ kg⁻¹ s⁻² |
| Speed of light | $c$ | 2.99792458 × 10⁸ m/s |
| Solar mass | $M_\odot$ | 1.989 × 10³⁰ kg |
| Reduced Planck constant | $\hbar$ | 1.0545718 × 10⁻³⁴ J·s |
| Boltzmann constant | $k_B$ | 1.380649 × 10⁻²³ J/K |

### Event horizon

The Schwarzschild radius is the radius of the event horizon:

$$r_s = \frac{2GM}{c^2}$$

From it, the app derives the circumference and surface area:

$$C = 2\pi r_s \qquad A = 4\pi r_s^2$$

### Surface gravity

The gravitational acceleration at the horizon:

$$g = \frac{c^4}{4GM}$$

### Photon sphere

The radius at which light can travel in a circular orbit:

$$r_{ph} = \tfrac{3}{2}\, r_s$$

### Hawking temperature

Quantum effects near the horizon make a black hole radiate like a blackbody. Larger black holes are colder:

$$T_H = \frac{\hbar c^3}{8\pi G M k_B}$$

For stellar-mass black holes this is billions of times colder than the cosmic microwave background (about 2.7 K), so it is a theoretical value rather than something observable.

### Gravitational time dilation

The app reports the factor by which a clock held stationary at radius $r$ runs slow compared with one far away:

$$\gamma(r) = \frac{1}{\sqrt{1 - \dfrac{r_s}{r}}}$$

- At the horizon ($r = r_s$) the factor diverges, so the app shows **∞** for every mass.
- The second reading is taken at $r = r_s + 1\ \text{km}$. Using a fixed distance above the horizon matters: measuring at a fixed multiple of $r_s$ (such as $2r_s$) gives the same number for every mass, because $r_s$ scales with mass.

### Entropy

The Bekenstein-Hawking entropy is proportional to the horizon's surface area:

$$S = \frac{k_B\, c^3\, A}{4\,\hbar\, G}$$

Substituting $A = 4\pi r_s^2$ gives the closed form in terms of mass:

$$S = \frac{4\pi\, k_B\, G\, M^2}{\hbar\, c}$$

Because entropy scales with $M^2$, doubling the mass quadruples the entropy.

### Average Density

The average density of a black hole is its mass divided by the volume of a sphere with the Schwarzschild radius:

$$\rho_{avg} = \frac{M}{V}$$

The Schwarzschild radius is:

$$r_s = \frac{2GM}{c^2}$$

The volume enclosed by the event horizon is:

$$V = \frac{4}{3}\pi r_s^3$$

Substituting both into the density formula gives the closed form in terms of mass:

$$\rho_{avg} = \frac{M}{\frac{4}{3}\pi r_s^3} = \frac{3c^6}{32\pi G^3 M^2}$$

Where:

- $G$ is the gravitational constant ($6.674 \times 10^{-11}\ \text{m}^3\text{kg}^{-1}\text{s}^{-2}$)
- $M$ is the mass of the black hole (kg)
- $c$ is the speed of light ($3 \times 10^8\ \text{m/s}$)

Because density scales with $1/M^2$, doubling the mass cuts the average density to a quarter. This is why supermassive black holes can have an average density lower than water, while stellar-mass ones are extraordinarily dense.

## Classification and Notes

After each calculation, the app labels the black hole by mass (in kilograms) and shows a note based on the radius.

### Mass classification

| Mass (kg) | Classification |
| --- | --- |
| below 1 × 10³¹ | Stellar-Mass Black Hole |
| 1 × 10³¹ to 1 × 10³² | Intermediate Black Hole |
| above 1 × 10³² to 1 × 10³⁶ | Supermassive Black Hole |
| above 1 × 10³⁶ | Unknown Black Hole Type |

### Radius notes

| Radius | Note | Color |
| --- | --- | --- |
| below 0.0001 m | Extremely small radius, very dense black hole | Red |
| above 1,000 m up to 1,000,000 m | Stellar-mass black hole range | Yellow |
| above 1,000,000 m | Quite large, supermassive black hole range | Green |
| otherwise | No note | n/a |

## The Visual

The canvas on the right shows an event horizon (a black disk with a glowing outline) in front of a tilted accretion disk, over a starfield that spans the whole window.

- **Scaling:** the radius is mapped on a log₁₀ scale from 10⁻¹⁰ m to 10¹⁰ m onto a disk diameter of 20 to 300 px. Values outside that range are clamped, which keeps tiny and enormous black holes both visible.
- **Accretion disk:** sized proportionally to the event horizon (1.8× wide, 0.6× tall) and rotated -20°.
- **Glow and outline color by rendered size:**

  | Rendered size | Color |
  | --- | --- |
  | above 200 px | Cream `#fff4d6` |
  | above 100 px | Orange `#ff8c1a` |
  | otherwise | Crimson `#ec1857` |

- **Animation:** on launch the black hole fades and scales in, followed by the accretion disk. The glow then pulses continuously, and 120 stars drift and twinkle in the background.

> [!NOTE]
> The visual shows relative scale only. It is not a physically accurate render, and it is not drawn to scale against real-world sizes.

## Project Structure

```text
BlackHoleMath-Application/
├── BlackHoleMath.slnx            # Solution file
├── README.md
├── .gitattributes
├── .gitignore
└── BlackHoleMath/
    ├── BlackHoleMath.csproj      # Project file (net10.0-windows, WPF)
    ├── App.xaml / App.xaml.cs    # Application entry point and theme resources
    ├── AssemblyInfo.cs           # WPF theme dictionary locations
    ├── MainWindow.xaml           # UI layout, custom title bar, animations
    ├── MainWindow.xaml.cs        # Calculation logic, classification, visual updates
    ├── black_hole.ico            # Window/app icon
    └── Fonts/
        └── Orbitron-Variable.ttf # Bundled UI font
```

All calculation and UI logic currently lives in `MainWindow.xaml.cs`:

| Method | Purpose |
| --- | --- |
| `CalculateButton_Click` | Parses input, runs the math, updates results, notes, and classification |
| `UpdateBlackHoleVisual` | Scales, centers, and recolors the black hole and accretion disk |
| `CenterVisualElements` | Centers the visual at its default size on startup |
| `GenerateStarfield` | Creates the drifting, twinkling background stars |
| `Window_Loaded` | Starts the starfield, intro animation, and glow pulse |
| `TitleBar_MouseLeftButtonDown`, `MinimizeButton_Click`, `CloseButton_Click` | Custom window chrome behavior |

## Known Limitations

- **Non-rotating black holes only.** Spin (Kerr) and charge are not modeled.
- **Mass validation is minimal.** Only non-numeric input is rejected. Zero and negative masses are accepted and produce meaningless output.
- **Culture-dependent parsing.** Numbers are parsed using the system locale, so a decimal comma versus a decimal point may matter.
- **Classification thresholds are simplified** and do not match standard astronomical definitions. For example, real supermassive black holes such as Sagittarius A* exceed the 10³⁶ kg cutoff, and any mass below 10³¹ kg (including the Earth) is labeled stellar-mass.
- **Hawking temperature and entropy are theoretical.** They follow from the standard formulas for an isolated Schwarzschild black hole and are not directly observable for astrophysical black holes.
- **The visual is illustrative,** not to scale and not a physical simulation.
- **Windows only,** because of WPF.

## Roadmap

Ideas for future work:

- [ ] Input validation for zero and negative mass
- [ ] Standard astronomical mass classes and more units (grams, Earth masses, kilometers, light-years)
- [ ] More properties, such as density and evaporation time from Hawking radiation
- [ ] Spinning (Kerr) black holes
- [ ] Unit tests for the calculation logic, separated from the UI
- [ ] Refactor to MVVM
- [ ] Scrollable layout for the left column on small windows
- [ ] Screenshots and a release build

## Contributing

Suggestions and bug reports are welcome through [GitHub Issues](https://github.com/infernorex68-dev/BlackHoleMath-Application/issues).

## Acknowledgments

- [Orbitron](https://fonts.google.com/specimen/Orbitron) font by Matt McInerney, licensed under the SIL Open Font License
- Physics: the Schwarzschild solution to Einstein's field equations, Hawking radiation, and Bekenstein-Hawking black hole entropy
