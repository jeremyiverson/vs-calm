using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using VSCalm.Utility;

namespace VSCalm
{
    public class ColorCorrector
    {
        string[] dullGray1Keys;
        Color dullGray1 = Color.FromRgb(208, 210, 211);
        Color dullGray2 = Color.FromRgb(230, 231, 232);

        Color goodToolbarBackground = Color.FromRgb(173, 185, 205);
        Color goodTitleBackground = Color.FromRgb(77, 96, 130);

        public ColorCorrector()
        {
            InitializeKeys();
        }

        /// <summary>
        /// Changes various color to be like those in Visual Studio 2010.
        /// </summary>
        /// <remarks>
        /// This doesn't seem like the cleanest way to do this. It would probably
        /// be better to assign some resources instead of hardcoding the values.
        /// That would allow for different colors for focused vs. unfocused.
        /// </remarks>
        public void FixColors()
        {
            var app = Application.Current;

            

            foreach (var control in UIHelper.FindVisualChildren<Border>(app.MainWindow))
            {
                if (control.GetType().Name.Contains("VsMenu"))
                {
                    control.Background = new SolidColorBrush(this.goodToolbarBackground);
                }
                else if (control.GetType().Name.Contains("VsToolBarTray"))
                {
                    control.Background = new SolidColorBrush(this.goodToolbarBackground);
                }
                else if (control.GetType().Name.Contains("DragUndockHeader"))
                {
                    control.Background = new SolidColorBrush(this.goodTitleBackground);
                }
            }

            foreach (var control in UIHelper.FindVisualChildren<Grid>(app.MainWindow))
            {
                SolidColorBrush brush = control.Background as SolidColorBrush;
                if (brush != null)
                {
                    if (brush.Color == this.dullGray1)
                    {
                        control.Background = new SolidColorBrush(Color.FromRgb(41, 57, 85));
                    }
                    else if (brush.Color == this.dullGray2)
                    {
                        control.Background = System.Windows.Media.Brushes.White;
                    }
                    
                }
            }

        }

        private void InitializeKeys()
        {
            this.dullGray1Keys = new string[]
                {
                    "VsColor.AccentBorder",
                    "VsColor.AutoHideResizeGrip",
                    "VsColor.AutoHideTabBackgroundBegin",
                    "VsColor.AutoHideTabBackgroundEnd",
                    "VsColor.AutoHideTabBorder",
                    "VsColor.ComboBoxDisabledBackground",
                    "VsColor.ComboBoxPopupBorder",
                    "VsColor.CommandBarGradientBegin",
                    "VsColor.CommandBarGradientEnd",
                    "VsColor.CommandBarGradientMiddle",
                    "VsColor.CommandBarMenuBorder",
                    "VsColor.CommandBarMenuSeparator",
                    "VsColor.CommandBarOptionsBackground",
                    "VsColor.CommandBarSelected",
                    "VsColor.CommandBarShadow",
                    "VsColor.CommandBarToolBarBorder",
                    "VsColor.CommandShelfBackgroundGradientBegin",
                    "VsColor.CommandShelfBackgroundGradientEnd",
                    "VsColor.CommandShelfBackgroundGradientMiddle",
                    "VsColor.CommandShelfHighlightGradientBegin",
                    "VsColor.CommandShelfHighlightGradientEnd",
                    "VsColor.CommandShelfHighlightGradientMiddle",
                    "VsColor.DropDownDisabledBackground",
                    "VsColor.EnvironmentBackground",
                    "VsColor.EnvironmentBackgroundGradientBegin",
                    "VsColor.EnvironmentBackgroundGradientEnd",
                    "VsColor.EnvironmentBackgroundTexture1",
                    "VsColor.EnvironmentBackgroundTexture2",
                    "VsColor.FileTabBorder",
                    "VsColor.FileTabChannelBackground",
                    "VsColor.FileTabDocumentBorderHighlight",
                    "VsColor.FileTabDocumentBorderShadow",
                    "VsColor.FileTabGradientDark",
                    "VsColor.FileTabGradientLight",
                    "VsColor.PanelBorder",
                    "VsColor.PanelGradientDark",
                    "VsColor.PanelGradientLight",
                    "VsColor.PanelTitleBar",
                    "VsColor.ProjectDesignerBackgroundGradientBegin",
                    "VsColor.ProjectDesignerBackgroundGradientEnd",
                    "VsColor.ProjectDesignerBorderInside",
                    "VsColor.ProjectDesignerBorderOutside",
                    "VsColor.ProjectDesignerContentsBackground",
                    "VsColor.ProjectDesignerTabBackgroundGradientBegin",
                    "VsColor.ProjectDesignerTabBackgroundGradientEnd",
                    "VsColor.ProjectDesignerTabSepBottomGradientBegin",
                    "VsColor.ProjectDesignerTabSepTopGradientEnd",
                    "VsColor.StartPageButtonUnpinned",
                    "VsColor.StartPageSeparator",
                    "VsColor.ThreeDLightShadow",
                    "VsColor.TitleBarActive",
                    "VsColor.TitleBarActiveGradientBegin",
                    "VsColor.TitleBarActiveGradientEnd",
                    "VsColor.TitleBarActiveGradientMiddle1",
                    "VsColor.TitleBarActiveGradientMiddle2",
                    "VsColor.TitleBarInactive",
                    "VsColor.TitleBarInactiveGradientBegin",
                    "VsColor.TitleBarInactiveGradientEnd",
                    "VsColor.ToolboxSelectedHeadingBegin",
                    "VsColor.ToolboxSelectedHeadingEnd",
                    "VsColor.ToolboxSelectedHeadingMiddle1",
                    "VsColor.ToolboxSelectedHeadingMiddle2",
                    "VsColor.ToolWindowContentTabGradientBegin",
                    "VsColor.ToolWindowContentTabGradientEnd",
                    "VsColor.ToolWindowTabBorder",
                    "VsColor.ToolWindowTabGradientBegin"
                };
        }

        void FailedApproach1()
        {
            //Application.ResourceAssembly = System.Reflection.Assembly.GetCallingAssembly();
            //ResourceDictionary blueDictionary = new ResourceDictionary();
            //blueDictionary.Source = new Uri(
            //    string.Format("pack://application:,,,/{0};component/Modifiers/Blue.xaml",
            //        System.Reflection.Assembly.GetCallingAssembly().Location), 
            //    UriKind.Absolute);
            //app.MainWindow.Resources.MergedDictionaries.Add(blueDictionary);

            // Make a few changes.
            //foreach (ResourceDictionary dict in resourceDictionaries)
            //{
            //    var keysInThisDict = this.dullGray1Keys.Intersect(dict.Keys.OfType<string>());
            //    foreach (string key in keysInThisDict)
            //    {
            //        dict[key] = "#FF0000";
            //    }
            //}

            // Unload and reload the ResourceDictionaries.
            
            //app.Resources.MergedDictionaries.Clear();
            //foreach (ResourceDictionary dict in resourceDictionaries)
            //{
            //    app.Resources.MergedDictionaries.Add(dict);
            //}
        }
    }
}
