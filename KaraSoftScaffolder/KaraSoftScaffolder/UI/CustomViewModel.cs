using Microsoft.AspNet.Scaffolding;
using Microsoft.AspNet.Scaffolding.EntityFramework;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace KaraSoftScaffolder.UI
{
    /// <summary>
    /// View model for code types so that it can be displayed on the UI.
    /// </summary>
    public class CustomViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="context">The code generation context</param>
        public CustomViewModel(CodeGenerationContext context)
        {
            Context = context;
            GenerateViews = true;
            OverwriteViews = true;
            ReferenceScriptLibraries = true;
            LayoutPageSelected = true;
        }

        /// <summary>
        /// This gets all the Model types from the active project.
        /// </summary>
        public IEnumerable<ModelType> ModelTypes
        {
            get
            {

                ICodeTypeService codeTypeService = (ICodeTypeService)Context
                    .ServiceProvider.GetService(typeof(ICodeTypeService));

                return codeTypeService
                    .GetAllCodeTypes(Context.ActiveProject)
                    .Where(codeType => codeType.IsValidWebProjectEntityType())
                    .Select(codeType => new ModelType(codeType));
            }
        }

        public IEnumerable<ModelType> ContextTypes
        {
            get
            {
                ICodeTypeService codeTypeService = (ICodeTypeService)Context
                   .ServiceProvider.GetService(typeof(ICodeTypeService));
                return codeTypeService
                   .GetAllCodeTypes(Context.ActiveProject)
                   .Where(codeType => codeType.IsValidDbContextType())
                   .Select(codeType => new ModelType(codeType));

              
            }
        }
       
        public bool GenerateViews { get; set; }
        public bool OverwriteViews { get; set; }
        public bool ReferenceScriptLibraries { get; set; } 
        public bool LayoutPageSelected { get; set; }
        public string LayoutPageFile { get; set; }
        public string ControllerName { get; set; }
        public string ProgramTitle { get; set; }                  
        public string ViewPrefix  { get; set; }

        public ModelType SelectedContextType { get; set; }

        //public ModelType SelectedModelType { get; set; }
        private ModelType _modelType;

        public ModelType SelectedModelType
        {
            get { return _modelType; }
            set
            {
                _modelType = value;
                OnPropertyChanged("SelectedModelType");

                ControllerName = _modelType.ShortTypeName + "Controller";
                OnPropertyChanged("ControllerName");

                ProgramTitle = _modelType.ShortTypeName;
                OnPropertyChanged("ProgramTitle");

                ViewPrefix = _modelType.ShortTypeName;
                OnPropertyChanged("ViewPrefix");
            }
        }

        public CodeGenerationContext Context { get; private set; }

        public ModelType DbContextModelType { get; set; }

        public virtual void OnPropertyChanged(string propertyName)
        {
            var propertyChanged = PropertyChanged;
            if (propertyChanged != null)
            {
                propertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
