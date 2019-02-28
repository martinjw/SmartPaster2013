using System;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.InteropServices;
using System.ComponentModel.Design;
using Microsoft.VisualStudio.Shell;
using EnvDTE;
using EnvDTE80;

namespace SmartPaster
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
    [Guid(GuidList.guidSmartPasterPkgString)]
    public sealed class SmartPasterPackage : Package
    {
        /// <summary>
        /// Default constructor of the package.
        /// Inside this method you can place any initialization code that does not require 
        /// any Visual Studio service because at this point the package object is created but 
        /// not sited yet inside Visual Studio environment. The place to do all the other 
        /// initialization is the Initialize method.
        /// </summary>
        public SmartPasterPackage()
        {
            Debug.WriteLine(string.Format(CultureInfo.CurrentCulture, "Entering constructor for: {0}", this.ToString()));
            //            Debug.WriteLine(string.Format(CultureInfo.CurrentCulture, "Entering constructor for: {0}", this.ToString()));
        }



        /////////////////////////////////////////////////////////////////////////////
        // Overridden Package Implementation
        #region Package Members

        /// <summary>
        /// Initialization of the package; this method is called right after the package is sited, so this is the place
        /// where you can put all the initialization code that rely on services provided by VisualStudio.
        /// </summary>
        protected override void Initialize()
        {
            Debug.WriteLine(string.Format(CultureInfo.CurrentCulture, "Entering Initialize() of: {0}", this.ToString()));
            base.Initialize();


            // Add our command handlers for menu (commands must exist in the .vsct file)
            var mcs = GetService(typeof(IMenuCommandService)) as OleMenuCommandService;
            if (null != mcs)
            {
                // Create the command for the menu item.
                var menuCommandVerbatimID = new CommandID(GuidList.guidSmartPasterCmdSet, (int)PkgCmdIDList.cmdidPasteAsVerbatimString);
                var menuItemVerbatim = new MenuCommand(CallPasteAsVerbatimString, menuCommandVerbatimID);
                mcs.AddCommand(menuItemVerbatim);

                var menuCommandStringID = new CommandID(GuidList.guidSmartPasterCmdSet, (int)PkgCmdIDList.cmdidPasteAsString);
                var menuItemString = new MenuCommand(CallPasteAsString, menuCommandStringID);
                mcs.AddCommand(menuItemString);

                var menuCommandBytesID = new CommandID(GuidList.guidSmartPasterCmdSet, (int)PkgCmdIDList.cmdidPasteAsBytes);
                var menuItemBytes = new MenuCommand(CallPasteAsBytes, menuCommandBytesID);
                mcs.AddCommand(menuItemBytes);

                var menuCommandSbID = new CommandID(GuidList.guidSmartPasterCmdSet, (int)PkgCmdIDList.cmdidPasteAsStringBuilder);
                var menuItemSb = new MenuCommand(CallPasteAsStringBuilder, menuCommandSbID);
                mcs.AddCommand(menuItemSb);

                var menuCommandID = new CommandID(GuidList.guidSmartPasterCmdSet, (int)PkgCmdIDList.cmdidPasteAsComment);
                var menuItem = new MenuCommand(CallPasteAsComment, menuCommandID);
                mcs.AddCommand(menuItem);
                
                var menuCommandReplaceID = new CommandID(GuidList.guidSmartPasterCmdSet, (int)PkgCmdIDList.cmdidPasteWithReplace);
                var menuItemReplace = new MenuCommand(CallPasteWithReplace, menuCommandReplaceID);
                mcs.AddCommand(menuItemReplace);

            }
        }
        #endregion



        /// <summary>
        /// This function is the callback used to execute a command when the a menu item is clicked.
        /// See the Initialize method to see how the menu item is associated to this function using
        /// the OleMenuCommandService service and the MenuCommand class.
        /// </summary>
        private void CallPasteAsComment(object sender, EventArgs e)
        {
            var dte = (DTE2)GetService(typeof(DTE));
            var sp = new SmartPaster();
            sp.PasteAsComment(dte);
        }

        private void CallPasteAsStringBuilder(object sender, EventArgs e)
        {
            var dte = (DTE2)GetService(typeof(DTE));
            var sp = new SmartPaster();
            sp.PasteAsStringBuilder(dte);
        }

        private void CallPasteAsString(object sender, EventArgs e)
        {
            var dte = (DTE2)GetService(typeof(DTE));
            var sp = new SmartPaster();
            sp.PasteAsString(dte);
        }

        private void CallPasteAsBytes(object sender, EventArgs e)
        {
            var dte = (DTE2)GetService(typeof(DTE));
            var sp = new SmartPaster();
            sp.PasteAsBytes(dte);
        }

        private void CallPasteAsVerbatimString(object sender, EventArgs e)
        {
            var dte = (DTE2)GetService(typeof(DTE));
            var sp = new SmartPaster();
            sp.PasteAsVerbatimString(dte);
        }

        private void CallPasteWithReplace(object sender, EventArgs e)
        {
            var dte = (DTE2)GetService(typeof(DTE));
            var sp = new SmartPaster();
            sp.PasteWithReplace(dte);
        }

    }
}
