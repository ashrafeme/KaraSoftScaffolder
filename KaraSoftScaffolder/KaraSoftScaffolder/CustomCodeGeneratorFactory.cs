using Microsoft.AspNet.Scaffolding;
using System;
using System.ComponentModel.Composition;
using System.Drawing;
using System.Runtime.Versioning;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace KaraSoftScaffolder
{
    [Export(typeof(CodeGeneratorFactory))]
    public class CustomCodeGeneratorFactory : CodeGeneratorFactory
    {
        /// <summary>
        ///  Information about the code generator goes here. 
        /// </summary>
        private static CodeGeneratorInformation _info = new CodeGeneratorInformation(
            displayName: "Karasoft MVC 5 for C.R.U.D",
            description: "Generates ASP.NET MVC controller and views for C.R.U.D (inserting, editing, deleting, and listing) from an Entity Framework data context.",
            author: "Ashraf Ezzat",
            version: new Version(1, 0, 0, 0),
            id: typeof(CustomCodeGenerator).Name,
            icon: ToImageSource(Resources._TemplateIconSample),
            gestures: new[] { "Controller" },
            categories: new[] { Categories.Common, Categories.MvcController, Categories.Other });

        public CustomCodeGeneratorFactory()
            : base(_info)
        {
        }
        /// <summary>
        /// This method creates the code generator instance.
        /// </summary>
        /// <param name="context">The context has details on current active project, project item selected, Nuget packages that are applicable and service provider.</param>
        /// <returns>Instance of CodeGenerator.</returns>
        public override ICodeGenerator CreateInstance(CodeGenerationContext context)
        {
            return new CustomCodeGenerator(context, Information);
        }

        /// <summary>
        /// Provides a way to check if the custom scaffolder is valid under this context
        /// </summary>
        /// <param name="codeGenerationContext">The code generation context</param>
        /// <returns>True if valid, False otherwise</returns>
        public override bool IsSupported(CodeGenerationContext codeGenerationContext)
        {
            if (ProjectLanguage.CSharp.Equals(codeGenerationContext.ActiveProject.GetCodeLanguage()))
            {
                FrameworkName targetFramework = codeGenerationContext.ActiveProject.GetTargetFramework();
                return (targetFramework != null) &&
                        String.Equals(".NetFramework", targetFramework.Identifier, StringComparison.OrdinalIgnoreCase) &&
                        targetFramework.Version >= new Version(4, 5);
            }

            return false;
        }
        /// <summary>
        /// Helper method to convert Icon to Imagesource.
        /// </summary>
        /// <param name="icon">Icon</param>
        /// <returns>Imagesource</returns>
        public static ImageSource ToImageSource(Icon icon)
        {
            ImageSource imageSource = Imaging.CreateBitmapSourceFromHIcon(
                icon.Handle,
                Int32Rect.Empty,
                BitmapSizeOptions.FromEmptyOptions());

            return imageSource;
        }
    }
}
