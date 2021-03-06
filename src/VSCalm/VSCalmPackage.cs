﻿using System;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.InteropServices;
using System.ComponentModel.Design;
using Microsoft.Win32;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio.OLE.Interop;
using Microsoft.VisualStudio.Shell;
using EnvDTE80;
using Extensibility;
using EnvDTE;
using System.Windows.Threading;
using VSCalm.Utility;
using VSCalm.Modifiers;

namespace VSCalm
{
    /// <summary>
    /// This is the class that implements the package exposed by this assembly.
    ///
    /// The minimum requirement for a class to be considered a valid package for Visual Studio
    /// is to implement the IVsPackage interface and register itself with the shell.
    /// This package uses the helper classes defined inside the Managed Package Framework (MPF)
    /// to do it: it derives from the Package class that provides the implementation of the 
    /// IVsPackage interface and uses the registration attributes defined in the framework to 
    /// register itself and its components with the shell.
    /// </summary>
    // This attribute tells the PkgDef creation utility (CreatePkgDef.exe) that this class is
    // a package.
    [PackageRegistration(UseManagedResourcesOnly = true)]
    // This attribute is used to register the information needed to show this package
    // in the Help/About dialog of Visual Studio.
    [InstalledProductRegistration("#110", "#112", "1.0", IconResourceID = 400)]
    // This attribute is needed to let the shell know that this package exposes some menus.
    [ProvideMenuResource("Menus.ctmenu", 1)]
    [Guid(GuidList.guidVSCalmPkgString)]
	[ProvideAutoLoad(Microsoft.VisualStudio.Shell.Interop.UIContextGuids80.NoSolution)]
	[ProvideAutoLoad(Microsoft.VisualStudio.Shell.Interop.UIContextGuids80.SolutionExists)]
    public sealed class VSCalmPackage : Package
    {
        /// <summary>
        /// Default constructor of the package.
        /// Inside this method you can place any initialization code that does not require 
        /// any Visual Studio service because at this point the package object is created but 
        /// not sited yet inside Visual Studio environment. The place to do all the other 
        /// initialization is the Initialize method.
        /// </summary>
        public VSCalmPackage()
        {
            Trace.WriteLine(string.Format(CultureInfo.CurrentCulture, "Entering constructor for: {0}", this.ToString()));

			timer = new DispatcherTimer();
			timer.Interval = TimeSpan.FromMilliseconds(100);
			timer.Tick += timer_Tick;
			timer.Start();
        }

        #region Package Members

        /// <summary>
        /// Initialization of the package; this method is called right after the package is sited, so this is the place
        /// where you can put all the initialization code that rely on services provided by VisualStudio.
        /// </summary>
        protected override void Initialize()
        {
            Trace.WriteLine (string.Format(CultureInfo.CurrentCulture, "Entering Initialize() of: {0}", this.ToString()));
            base.Initialize();

			WatchWindowEvents();

			Calm calm = new Calm();
			calm.CalmDown();
        }

        #endregion

		#region Window event handling

		private void WatchWindowEvents()
		{
			var dte = GetService(typeof(DTE)) as DTE;
			if (dte == null) return;

			var events2 = dte.Events as Events2;
			if (events2 == null) return;

			var windowVisibilityEvents = events2.get_WindowVisibilityEvents();
			if (windowVisibilityEvents == null) return;

			windowVisibilityEvents.WindowShowing += windowEvents_WindowShowing;
			windowVisibilityEvents.WindowHiding += windowEvents_WindowHiding;
		}

		DispatcherTimer timer;
		void windowEvents_WindowHiding(EnvDTE.Window Window)
		{
			// HACK When this event is triggered, the closed window can't be found yet, so it's title can't be converted.
			// Set a short timer to do it when we can.
			timer.Start();
		}

		void timer_Tick(object sender, EventArgs e)
		{
			timer.Stop();

			Calm calm = new Calm();
			calm.CalmDown();
		}

		void windowEvents_WindowShowing(EnvDTE.Window Window)
		{
			Calm calm = new Calm();
			calm.CalmDown();
		}

		#endregion

    }

}
