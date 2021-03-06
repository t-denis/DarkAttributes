﻿//------------------------------------------------------------------------------
// <copyright file="DarkAttributesPackage.cs" company="Company">
//     Copyright (c) Company.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System.ComponentModel.Composition;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using DarkAttributes.Pages;
using DarkAttributes.Services;
using Microsoft.VisualStudio.ComponentModelHost;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio.Text.Classification;

namespace DarkAttributes.Definitions
{
    /// <summary>
    /// This is the class that implements the package exposed by this assembly.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The minimum requirement for a class to be considered a valid package for Visual Studio
    /// is to implement the IVsPackage interface and register itself with the shell.
    /// This package uses the helper classes defined inside the Managed Package Framework (MPF)
    /// to do it: it derives from the Package class that provides the implementation of the
    /// IVsPackage interface and uses the registration attributes defined in the framework to
    /// register itself and its components with the shell. These attributes tell the pkgdef creation
    /// utility what data to put into .pkgdef file.
    /// </para>
    /// <para>
    /// To get loaded into VS, the package must be referred by &lt;Asset Type="Microsoft.VisualStudio.VsPackage" ...&gt; in .vsixmanifest file.
    /// </para>
    /// </remarks>
    [PackageRegistration(UseManagedResourcesOnly = true)]
    [InstalledProductRegistration("#110", "#112", "1.0", IconResourceID = 400)] // Info on this package for Help/About
    [Guid(DarkAttributesPackage.PackageGuidString)]
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "pkgdef, VS and vsixmanifest are valid VS terms")]
    [ProvideOptionPage(typeof(DarkAttributesOptionsPage), "DarkAttributes", "General", 0, 0, true)]
    [ProvideAutoLoad(UIContextGuids.NoSolution)]
    [ProvideAutoLoad(UIContextGuids.SolutionExists)]
    public sealed class DarkAttributesPackage : Package
    {
        /// <summary>
        /// DarkAttributesPackage GUID string.
        /// </summary>
        public const string PackageGuidString = "b4c92d6f-2e7f-4d8c-b21d-2badda8ebe6e";

        /// <summary>
        /// Initializes a new instance of the <see cref="DarkAttributesPackage"/> class.
        /// </summary>
        public DarkAttributesPackage()
        {
            // Inside this method you can place any initialization code that does not require
            // any Visual Studio service because at this point the package object is created but
            // not sited yet inside Visual Studio environment. The place to do all the other
            // initialization is the Initialize method.
        }

        #region Package Members

        /// <summary>
        /// Initialization of the package; this method is called right after the package is sited, so this is the place
        /// where you can put all the initialization code that rely on services provided by VisualStudio.
        /// </summary>
        protected override void Initialize()
        {
            base.Initialize();

            var componentModel = (IComponentModel)GetGlobalService(typeof(SComponentModel));
            var classificationFormatMapService = componentModel.GetService<IClassificationFormatMapService>();
            var classificationTypeRegistryService = componentModel.GetService<IClassificationTypeRegistryService>();
            var vsServiceProvider = componentModel.GetService<SVsServiceProvider>();

            StorageService.Instance = new StorageService(vsServiceProvider);
            TextPropertiesService.Instance = new TextPropertiesService(classificationTypeRegistryService, classificationFormatMapService);
            TextPropertiesService.Instance.UpdateTextPropertiesFromSettings();
        }

        #endregion
    }
}
