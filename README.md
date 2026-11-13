# QuickAccessTrail — Revit API Add-in

A collection of productivity tools developed using **C#, .NET, WPF, and the Autodesk Revit API** to simplify repetitive tasks and improve workflow efficiency inside Autodesk Revit.

## Project Overview

QuickAccessTrail is a custom Revit add-in that brings multiple productivity commands together in one place.

The project focuses on automating common Revit operations, improving navigation, managing dimensions and sheets, and simplifying element-related workflows.

## Features

The following tools are represented in the project source code:

### 1. Text and Dimension Tools
- Convert text to uppercase.
- Convert text to lowercase.
- Dimension override functionality.
- Equal dimension text functionality.
- Equal dimension value functionality.

### 2. Sheet Management
- Navigate to the next sheet.
- Navigate to the previous sheet.
- Batch sheet creation.

### 3. Linked Model Tools
- Retrieve linked element IDs.
- Zoom to linked elements.
- Create a section box around linked elements.

### 4. Workset Management
- Workset management interface.
- Workset assignment functionality.

### 5. Floor Type Management
- Floor type selection and management interface.

## Technologies Used

| Technology | Purpose |
|---|---|
| C# | Application development |
| Autodesk Revit API | Revit automation |
| WPF | User interface development |
| XAML | UI design |
| .NET | Application framework |
| Visual Studio | Development and debugging |
| Git & GitHub | Version control |

## Project Architecture

The project follows a modular structure to separate commands, business logic, models, and user interfaces.

```text
QuickAccessTrail/
│
├── Command/
│   ├── BatchSheetCommand.cs
│   ├── DimensionOverrideCommand.cs
│   ├── EqTextCommand.cs
│   ├── EqValueCommand.cs
│   ├── NextSheet.cs
│   ├── PreviousSheet.cs
│   ├── SectionBoxLinkedCommand.cs
│   ├── WorksetManagementCommand.cs
│   └── ...
│
├── Filters/
│   ├── DimensionFilter.cs
│   ├── InputParser.cs
│   └── LinkElementFinder.cs
│
├── Helper/
│   ├── BoundingBoxHelper.cs
│   ├── UnitConversionSqM.cs
│   └── UnitConverter.cs
│
├── Model/
│   ├── EqTextModel.cs
│   ├── FloorTypeItem.cs
│   ├── OverrideModel.cs
│   └── SheetModel.cs
│
├── Request/
│   ├── SectionBox3DRequest.cs
│   ├── WorksetAssignmentRequest.cs
│   └── ZoomRequest.cs
│
├── Services/
│   ├── SectionBoxServices.cs
│   ├── SheetCreationService.cs
│   ├── WorksetAssignmentService.cs
│   └── ZoomLinkedElementService.cs
│
├── ViewModels/
│   └── FloorTypeViewModel.cs
│
├── WPF/
│   ├── BatchCreationWindow.xaml
│   ├── DimensionOverride.xaml
│   ├── EqTextWindow.xaml
│   ├── FloorLevelWindow.xaml
│   ├── SectionBoxLinkedElement.xaml
│   ├── WorksetManagementWindow.xaml
│   └── ZoomWindow.xaml
│
├── Resources/
│   └── PNG icons
│
├── Main.cs
├── QuickAccessTrail.addin
└── QuickAccessTrail.csproj
```

## Installation

1. Clone or download this repository.
2. Open `QuickAccessTrail.slnx` in a compatible version of Visual Studio.
3. Configure the project for the supported Revit API version.
4. Build the project.
5. Configure the `.addin` manifest with the correct path to the compiled DLL.
6. Place the manifest in the appropriate Revit Addins version folder.
7. Launch Autodesk Revit and verify that the add-in loads.

**Note:** The Revit version, .NET target framework, and build configuration should match the installed Revit environment.

## Development

This project is developed as a practical Revit API automation project, with an emphasis on:

- Reusable helper methods.
- Separation of commands and services.
- WPF-based user interfaces.
- Revit element selection and filtering.
- Linked model interaction.
- Unit conversion and dimension handling.

## Repository

GitHub: https://github.com/prasad-patgave/QuickAccessTrail

## Author

**Prasad Patgave**

C# Developer | Revit API Developer

---

*QuickAccessTrail — Making everyday Revit workflows faster and easier.*
