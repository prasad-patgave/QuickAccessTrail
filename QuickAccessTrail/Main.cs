using Autodesk.Revit.UI;
using Quick_Access.Command;
using QuickAccessTrail.Command;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;


namespace QuickAccessTrail
{
    public class Main : IExternalApplication
    {
        public Autodesk.Revit.UI.Result OnShutdown(UIControlledApplication application)
        {
            return Autodesk.Revit.UI.Result.Succeeded;
        }

        public Autodesk.Revit.UI.Result OnStartup(UIControlledApplication application)
        {
            var tabName = "QuickAccess";
            try
            {
                application.CreateRibbonTab(tabName);
            }
            catch { }

            var generalPanelName_1 = "Text Update";
            var generalPanelName_2 = "Sheet Navigation";
            var generalPanelName_3 = "Dimension Update";
            var generalPanelName_4 = "Worksets";
            var generalPanelName_5 = "Linked Elements";
            var generalPanelName_6 = "MVVM Principle";
            RibbonPanel generalPanel_1 = null;
            RibbonPanel generalPanel_2 = null;
            RibbonPanel generalPanel_3 = null;
            RibbonPanel generalPanel_4 = null;
            RibbonPanel generalPanel_5 = null;
            RibbonPanel generalPanel_6 = null;
            var allPanel = application.GetRibbonPanels(tabName);
            var check_1 = allPanel.Where(x => x.Name == generalPanelName_1).FirstOrDefault();
            var check_2 = allPanel.Where(x => x.Name == generalPanelName_2).FirstOrDefault();
            var check_3 = allPanel.Where(x => x.Name == generalPanelName_3).FirstOrDefault();
            var check_4 = allPanel.Where(x => x.Name == generalPanelName_4).FirstOrDefault();
            var check_5 = allPanel.Where(x => x.Name == generalPanelName_5).FirstOrDefault();
            var check_6 = allPanel.Where(x => x.Name == generalPanelName_5).FirstOrDefault();
            if (check_1 == null)
            {
                generalPanel_1 = application.CreateRibbonPanel(tabName, generalPanelName_1);

            }
            else
                generalPanel_1 = check_1;

            if (check_2 == null)
            {
                generalPanel_2 = application.CreateRibbonPanel(tabName, generalPanelName_2);
            }
            else
                generalPanel_2 = check_2;

            if (check_3 == null)
            {
                generalPanel_3 = application.CreateRibbonPanel(tabName, generalPanelName_3);
            }
            else
                generalPanel_3 = check_3;

            if (check_4 == null)
            {
                generalPanel_4 = application.CreateRibbonPanel(tabName, generalPanelName_4);
            }
            else
                generalPanel_4 = check_4;
            if (check_5 == null)
            {
                generalPanel_5 = application.CreateRibbonPanel(tabName, generalPanelName_5);
            }
            else
                generalPanel_5 = check_5;
            if (check_6 == null)
            {
                generalPanel_6 = application.CreateRibbonPanel(tabName, generalPanelName_6);
            }
            else
                generalPanel_6 = check_6;
            var path = Assembly.GetExecutingAssembly().Location;
            Uri ToUpperUri = new Uri(Path.Combine(Path.GetDirectoryName(path), "Resources", "02_ToUpper.png"));
            BitmapImage ToUpperBitmap = new BitmapImage(ToUpperUri);
            var upperButtonData = new PushButtonData("To set text to upper case", "To Upper", path, typeof(ToUpper).FullName);
            var upperButton = generalPanel_1.AddItem(upperButtonData) as PushButton;
            upperButton.ToolTip = "Select and change text to upper case";
            upperButton.LargeImage = ToUpperBitmap;

            Uri ToLowerUri = new Uri(Path.Combine(Path.GetDirectoryName(path), "Resources", "01_ToLower.png"));
            BitmapImage ToLowerBitmap = new BitmapImage(ToLowerUri);
            var lowerButtonData = new PushButtonData("To set text to lower case", "To Lower", path, typeof(ToLower).FullName);
            var lowerButton = generalPanel_1.AddItem(lowerButtonData) as PushButton;
            lowerButton.ToolTip = "Select and change text to lower case";
            lowerButton.LargeImage = ToLowerBitmap;

            Uri nextUri = new Uri(Path.Combine(Path.GetDirectoryName(path), "Resources", "03_NextSheet.png"));
            BitmapImage nextBitmap = new BitmapImage(nextUri);
            var nextSheetData = new PushButtonData("click to next sheet", "Next Sheet", path, typeof(NextSheet).FullName);
            var nextSheet = generalPanel_2.AddItem(nextSheetData) as PushButton;
            nextSheet.ToolTip = "click for next sheet";
            nextSheet.LargeImage = nextBitmap;

            Uri previousuri = new Uri(Path.Combine(Path.GetDirectoryName(path), "Resources", "04_PreviousSheet.png"));
            BitmapImage Previousbitmap = new BitmapImage(previousuri);
            var previousSheetData = new PushButtonData("click to previous sheet", "Previous Sheet", path, typeof(PreviousSheet).FullName);
            var previousSheet = generalPanel_2.AddItem(previousSheetData) as PushButton;
            previousSheet.ToolTip = "click for previos sheet";
            previousSheet.LargeImage = Previousbitmap;

            Uri BatchSheetUri = new Uri(Path.Combine(Path.GetDirectoryName(path), "Resources", "05_BatchSheet.png"));
            BitmapImage BatchSheetBitmap = new BitmapImage(BatchSheetUri);
            var BatchSheetCreationButtonData = new PushButtonData("Create a batch sheets ", "Batch Sheets", path, typeof(BatchSheetCommand).FullName);
            var BatchSheetCreationButton = generalPanel_2.AddItem(BatchSheetCreationButtonData) as PushButton;
            BatchSheetCreationButton.ToolTip = "Create a batch sheet by pasting sheet name and sheet number";
            BatchSheetCreationButton.LargeImage = BatchSheetBitmap;

            Uri DimensionUri = new Uri(Path.Combine(Path.GetDirectoryName(path), "Resources", "06_DimensionOverride.png"));
            BitmapImage DimensionBitmap = new BitmapImage(DimensionUri);
            var DimensionOverrideData = new PushButtonData("Replace end dimension with Text", "Dimension" + "\n" + "Override", path, typeof(EqTextCommand).FullName);
            var DimensionOverride = generalPanel_3.AddItem(DimensionOverrideData) as PushButton;
            DimensionOverride.ToolTip = "Replace end segments of dimension with text";
            DimensionOverride.LargeImage = DimensionBitmap;

            Uri DimensionValueUri = new Uri(Path.Combine(Path.GetDirectoryName(path), "Resources", "07_DimensionValue.png"));
            BitmapImage DimensionValueBitmap = new BitmapImage(DimensionValueUri);
            var DimensionValueData = new PushButtonData("Replace end dimension with its original value", "Dimension" + "\n" + "Value", path, typeof(EqValueCommand).FullName);
            var DimensionValue = generalPanel_3.AddItem(DimensionValueData) as PushButton;
            DimensionValue.ToolTip = "Original value of end segments";
            DimensionValue.LargeImage = DimensionValueBitmap;

            Uri AllDimensionValueUri = new Uri(Path.Combine(Path.GetDirectoryName(path), "Resources", "11_DimensionTextToggle.png"));
            BitmapImage AllDimensionValueBitmap = new BitmapImage(AllDimensionValueUri);
            var AllDimensionValueData = new PushButtonData("Replacement all Value with text", "Override" + "\n" + "Value", path, typeof(DimensionOverrideCommand).FullName);
            var AllDimensionValue = generalPanel_3.AddItem(AllDimensionValueData) as PushButton;
            AllDimensionValue.ToolTip = "Replace all value with Text";
            AllDimensionValue.LargeImage = AllDimensionValueBitmap;

            Uri IdZoomLinkedUri = new Uri(Path.Combine(Path.GetDirectoryName(path), "Resources", "08_LinkId.png"));
            BitmapImage IdZoomLinkedBitmap = new BitmapImage(IdZoomLinkedUri);
            var IdZoomLinkedButtonData = new PushButtonData("Element Id Of Linked Elementt", "ID", path, typeof(LinkedIdCommand).FullName);
            var IdZoomLinkedData = generalPanel_5.AddItem(IdZoomLinkedButtonData) as PushButton;
            IdZoomLinkedData.ToolTip = "Elememt Id Of Linked Element";
            IdZoomLinkedData.LargeImage = IdZoomLinkedBitmap;

            Uri ZoomLinkedUri = new Uri(Path.Combine(Path.GetDirectoryName(path), "Resources", "09_ClashPoint.png"));
            BitmapImage ZoomLinkedBitmap = new BitmapImage(ZoomLinkedUri);
            var ZoomLinkedButtonData = new PushButtonData("Zoom Linked Element", "Clash" , path, typeof(ZoomLinkedElementCommand).FullName);
            var ZoomLinkedData = generalPanel_5.AddItem(ZoomLinkedButtonData) as PushButton;
            ZoomLinkedData.ToolTip = "Zoom at Clash point";
            ZoomLinkedData.LargeImage = ZoomLinkedBitmap;

            Uri ZoomLinkedElementUri = new Uri(Path.Combine(Path.GetDirectoryName(path), "Resources", "13_OpenLinkedIn3D.png"));
            BitmapImage ZoomLinkedElementBitmap = new BitmapImage(ZoomLinkedElementUri);
            var ZoomLinkedElementButtonData = new PushButtonData("Zoom Linked Element selection", "Zoom" , path, typeof(SectionBoxLinkedCommand).FullName);
            var ZoomLinkedElementData = generalPanel_5.AddItem(ZoomLinkedElementButtonData) as PushButton;
            ZoomLinkedElementData.ToolTip = "Zoom linked Element";
            ZoomLinkedElementData.LargeImage = ZoomLinkedElementBitmap;

            Uri WorksetUri = new Uri(Path.Combine(Path.GetDirectoryName(path), "Resources", "10_AssignWorkset.png"));
            BitmapImage WorksetBitmap = new BitmapImage(WorksetUri);
            var WorksetButtonData = new PushButtonData("Set elements to their workset", "Assign" + "\n" + "Workset", path, typeof(WorksetManagementCommand).FullName);
            var WorksetData = generalPanel_4.AddItem(WorksetButtonData) as PushButton;
            WorksetData.ToolTip = "Assign worksets to elements";
            WorksetData.LargeImage = WorksetBitmap;

            Uri FloorUri = new Uri(Path.Combine(Path.GetDirectoryName(path), "Resources", "14_FloorExport.png"));
            BitmapImage FloorBitmap = new BitmapImage(FloorUri);
            var FloorExportButtonData = new PushButtonData("Export Floors ", "Floor" + "\n" + "Export", path, typeof(FloorTypesCommnd).FullName);
            var FloorExportData = generalPanel_6.AddItem(FloorExportButtonData) as PushButton;
            FloorExportData.ToolTip = "Export Floors as per level";
            FloorExportData.LargeImage = WorksetBitmap;

            return Autodesk.Revit.UI.Result.Succeeded;
        }

       
    }
}
