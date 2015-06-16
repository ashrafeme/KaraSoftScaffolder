using EnvDTE;
using KaraSoftScaffolder.UI;
using Microsoft.AspNet.Scaffolding;
using Microsoft.AspNet.Scaffolding.Core.Metadata;
using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.AspNet.Scaffolding.EntityFramework;

namespace KaraSoftScaffolder
{
    public class CustomCodeGenerator : CodeGenerator
    {
        CustomViewModel _viewModel;

        /// <summary>
        /// Constructor for the custom code generator
        /// </summary>
        /// <param name="context">Context of the current code generation operation based on how scaffolder was invoked(such as selected project/folder) </param>
        /// <param name="information">Code generation information that is defined in the factory class.</param>
        public CustomCodeGenerator(
            CodeGenerationContext context,
            CodeGeneratorInformation information)
            : base(context, information)
        {
            _viewModel = new CustomViewModel(Context);
        }


        /// <summary>
        /// Any UI to be displayed after the scaffolder has been selected from the Add Scaffold dialog.
        /// Any validation on the input for values in the UI should be completed before returning from this method.
        /// </summary>
        /// <returns></returns>
        public override bool ShowUIAndValidate()
        {
            // Bring up the selection dialog and allow user to select a model type
            SelectModelWindow window = new SelectModelWindow(_viewModel);
            bool? showDialog = window.ShowDialog();
            return showDialog ?? false;
        }

        /// <summary>
        /// This method is executed after the ShowUIAndValidate method, and this is where the actual code generation should occur.
        /// In this example, we are generating a new file from t4 template based on the ModelType selected in our UI.
        /// </summary>
        public override void GenerateCode()
        {
            var project = Context.ActiveProject;

            // Get the selected code type
            var codeType = _viewModel.SelectedModelType.CodeType;
            var selectionRelativePath = GetSelectionRelativePath();

            string dbContextTypeName = _viewModel.SelectedContextType.TypeName;
            ICodeTypeService codeTypeService = GetService<ICodeTypeService>();
            CodeType dbContext = codeTypeService.GetCodeType(project, dbContextTypeName);

            // Get the Entity Framework Meta Data
            IEntityFrameworkService efService = Context.ServiceProvider.GetService<IEntityFrameworkService>();
            ModelMetadata efMetadata = efService.AddRequiredEntity(Context, dbContextTypeName, codeType.FullName);

            string controllerName = _viewModel.ControllerName;
            string controllerRootName = controllerName.Replace("Controller", "");
            string outputFolderPath = Path.Combine(selectionRelativePath, controllerName);
            string viewPrefix = _viewModel.ViewPrefix;
            string programTitle = _viewModel.ProgramTitle;


            AddMvcController(project: project
               , controllerName: controllerName
               , controllerRootName: controllerRootName
               , outputPath: outputFolderPath
               , ContextTypeName: dbContext.Name
               , modelType: codeType
               , efMetadata: efMetadata
               , viewPrefix: viewPrefix
               , overwrite: _viewModel.OverwriteViews);

            if (!_viewModel.GenerateViews)
                return;
            // Views for  C.R.U.D 
            string viewRootPath = GetViewsFolderPath(selectionRelativePath);
            string viewFolderPath = Path.Combine(viewRootPath, controllerRootName);
            foreach (string viewName in new string[4] { "Index", "Create", "Edit", "EditForm" })
            {
                AddView(project
                   , viewFolderPath, viewPrefix, viewName, programTitle
                   , controllerRootName, codeType, efMetadata
                   , referenceScriptLibraries: _viewModel.ReferenceScriptLibraries
                   , isLayoutPageSelected: _viewModel.LayoutPageSelected
                   , layoutPageFile: _viewModel.LayoutPageFile
                   , overwrite: _viewModel.OverwriteViews
                   );
            }

        }

        private void AddView(Project project
           , string viewsFolderPath
           , string viewPrefix
           , string viewName
           , string programTitle
           , string controllerRootName
           , CodeType modelType
           , ModelMetadata efMetadata
           , bool referenceScriptLibraries = true
           , bool isLayoutPageSelected = true
           , string layoutPageFile = null
           , bool isBundleConfigPresent = true
           , bool overwrite = false)
        {
            string outputPath = Path.Combine(viewsFolderPath, viewPrefix + viewName);
            string templatePath = Path.Combine("MvcView", viewName);
            string viewDataTypeName = modelType.Namespace.FullName + "." + modelType.Name;

            if (layoutPageFile == null)
                layoutPageFile = string.Empty;
            Dictionary<string, object> templateParams = new Dictionary<string, object>(){
                {"ControllerRootName" , controllerRootName}
                , {"ModelMetadata", efMetadata}
                , {"ViewPrefix", viewPrefix}
                , {"ViewName", viewName}
                , {"ProgramTitle", programTitle}
                , {"ViewDataTypeName", viewDataTypeName}
                , {"IsPartialView" , false}
                , {"LayoutPageFile", layoutPageFile}
                , {"IsLayoutPageSelected", isLayoutPageSelected}
                , {"ReferenceScriptLibraries", referenceScriptLibraries}
                , {"IsBundleConfigPresent", isBundleConfigPresent}
                //, {"ViewDataTypeShortName", modelType.Name} // 可刪除
                //, {"MetaTable", _ModelMetadataVM.DataModel}
                , {"JQueryVersion","2.1.0"} // 如何讀取專案的 jQuery 版本
                , {"MvcVersion", new Version("5.1.2.0")}
            };
        }
        //add MVC Controller
        private void AddMvcController(EnvDTE.Project project
            , string controllerName
            , string controllerRootName
            , string outputPath
            , string ContextTypeName /*"Entities"*/
            , CodeType modelType
            , ModelMetadata efMetadata
            , string viewPrefix
            , bool overwrite = false
            )
        {
            if (modelType == null)
            {
                throw new ArgumentNullException("modelType");
            }
            if (String.IsNullOrEmpty(controllerName))
            {
                //TODO
                throw new ArgumentException("Action name cannot be null or empty.", "webFormsName");
            }

            PropertyMetadata primaryKey = efMetadata.PrimaryKeys.FirstOrDefault();
            string pluralizedName = efMetadata.EntitySetName;
            string modelNameSpace = modelType.Namespace != null ? modelType.Namespace.FullName : String.Empty;
            string relativePath = outputPath.Replace(@"\", @"/");


            //Project project = Context.ActiveProject;
            var templatePath = Path.Combine("MvcControllerWithContext", "Controller");
            var defaultNamespace = GetDefaultNamespace();
            string modelTypeVariable = GetTypeVariable(modelType.Name);
            string bindAttributeIncludeText = GetBindAttributeIncludeText(efMetadata);

            Dictionary<string, object> templateParams = new Dictionary<string, object>(){
                {"ControllerName", controllerName}
                , {"ControllerRootName" , controllerRootName}
                , {"Namespace", defaultNamespace}
                , {"AreaName", string.Empty}
                , {"ContextTypeName", ContextTypeName}
                , {"ModelTypeName", modelType.Name}
                , {"ModelVariable", modelTypeVariable}
                , {"ModelMetadata", efMetadata}
                , {"EntitySetVariable", modelTypeVariable}
                , {"UseAsync", true}
                , {"IsOverpostingProtectionRequired", true}
                , {"BindAttributeIncludeText", bindAttributeIncludeText}
                , {"OverpostingWarningMessage", "To protect from over posting attacks, please enable the specific properties you want to bind to, for more details see http://go.microsoft.com/fwlink/?LinkId=317598."}
                , {"RequiredNamespaces", new HashSet<string>(){modelType.Namespace.FullName}}
                , {"ViewPrefix", viewPrefix}
            };

            AddFileFromTemplate(project: project
                , outputPath: outputPath
                , templateName: templatePath
                , templateParameters: templateParams
                , skipIfExists: !overwrite);
        }

        protected string GetDefaultNamespace()
        {
            return Context.ActiveProjectItem == null
                ? Context.ActiveProject.GetDefaultNamespace()
                : Context.ActiveProjectItem.GetDefaultNamespace();
        }

        private string GetBindAttributeIncludeText(ModelMetadata efMetadata)
        {
            string result = "";
            foreach (PropertyMetadata m in efMetadata.Properties)
                result += "," + m.PropertyName;
            return result.Substring(1);
        }

        private string GetTypeVariable(string typeName)
        {
            return typeName.Substring(0, 1).ToLower() + typeName.Substring(1, typeName.Length - 1);
        }

        private TService GetService<TService>() where TService : class
        {
            return (TService)ServiceProvider.GetService(typeof(TService));
        }

        protected string GetSelectionRelativePath()
        {
            return Context.ActiveProjectItem == null ? String.Empty : GetProjectRelativePath(Context.ActiveProjectItem);
        }

        private string GetAreaName(string selectionRelativePath)
        {
            string[] dirs = selectionRelativePath.Split(new char[1] { '\\' });

            if (dirs[0].Equals("Areas"))
                return dirs[1];
            else
                return string.Empty;

        }
        private string GetViewsFolderPath(string selectionRelativePath)
        {
            return GetRelativeFolderPath(selectionRelativePath, "Views");
        }
        private string GetRelativeFolderPath(string selectionRelativePath, string folderName)
        {
            string keyControllers = "Controllers";
            string keyViews = folderName;

            return (
                (
                selectionRelativePath.IndexOf(keyControllers) >= 0)
                ? selectionRelativePath.Replace(keyControllers, keyViews)
                : Path.Combine(selectionRelativePath, keyViews)
                );
        }

        public string GetProjectRelativePath(ProjectItem projectItem)
        {
            Project project = projectItem.ContainingProject;
            string projRelativePath = null;

            string rootProjectDir = project.GetFullPath();
            rootProjectDir = EnsureTrailingBackSlash(rootProjectDir);
            string fullPath = projectItem.GetFullPath();

            if (!String.IsNullOrEmpty(rootProjectDir) && !String.IsNullOrEmpty(fullPath))
            {
                projRelativePath = MakeRelativePath(fullPath, rootProjectDir);
            }

            return projRelativePath;
        }


        public readonly string PathSeparator = Path.DirectorySeparatorChar.ToString();

        public string EnsureTrailingBackSlash(string str)
        {
            if (str != null && !str.EndsWith(PathSeparator, StringComparison.Ordinal))
            {
                str += PathSeparator;
            }
            return str;
        }


        public string MakeRelativePath(string fullPath, string basePath)
        {
            string tempBasePath = basePath;
            string tempFullPath = fullPath;
            StringBuilder relativePath = new StringBuilder();

            if (!tempBasePath.EndsWith(PathSeparator, StringComparison.OrdinalIgnoreCase))
            {
                tempBasePath += PathSeparator;
            }

            while (!String.IsNullOrEmpty(tempBasePath))
            {
                if (tempFullPath.StartsWith(tempBasePath, StringComparison.OrdinalIgnoreCase))
                {
                    relativePath.Append(fullPath.Remove(0, tempBasePath.Length));
                    if (String.Equals(relativePath.ToString(), PathSeparator, StringComparison.OrdinalIgnoreCase))
                    {
                        relativePath.Clear();
                    }
                    return relativePath.ToString();
                }
                else
                {
                    tempBasePath = tempBasePath.Remove(tempBasePath.Length - 1);
                    int lastIndex = tempBasePath.LastIndexOf(PathSeparator, StringComparison.OrdinalIgnoreCase);
                    if (-1 != lastIndex)
                    {
                        tempBasePath = tempBasePath.Remove(lastIndex + 1);
                        relativePath.Append("..");
                        relativePath.Append(PathSeparator);
                    }
                    else
                    {
                        return fullPath;
                    }
                }
            }

            return fullPath;
        }



    }
}
