using FilterStudio.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.IO;
using System.Linq;
using Microsoft.Win32;
using Newtonsoft.Json;
using FilterStudio.Concrete;
using FilterStudio.Core;
using System.Drawing;

namespace FilterStudio.VM
{
    public class MainVM : BaseVM
    {

        #region Properties
        private ObservableCollection<FilterVM> filters = new ObservableCollection<FilterVM>();
        /// <summary>
        /// List of filters that makeup whole project
        /// </summary>
        public ObservableCollection<FilterVM> Filters
        {
            get { return filters; }
            set { SetProperty(ref filters, value); }
        }

        private FilterVM selectedFilter;
        /// <summary>
        /// Currently selected filter
        /// </summary>
        public FilterVM SelectedFilter
        {
            get { return selectedFilter; }
            set { SetProperty(ref selectedFilter, value); }
        }
        #endregion


        #region Commands
        public RelayCommand SaveProjectCommand { get; set; }
        public RelayCommand LoadProjectCommand { get; set; }
        public RelayCommand CreateNewProjectCommand { get; set; }

        public RelayCommand<string> AddFilterCommand { get; set; }
        public RelayCommand RemoveFilterCommand { get; set; }
        #endregion

        #region Private Members
        private readonly ExecutionEngineVM executionEngineVM;
        public ExecutionEngineVM ExecutionEngineVM
        {
            get
            {
                return executionEngineVM;
            }
        }



        private static readonly JsonSerializerSettings jsonSerializerSettings = new JsonSerializerSettings()
        {
            NullValueHandling = NullValueHandling.Ignore,
            TypeNameHandling = TypeNameHandling.Auto
        };

        #endregion


        public MainVM()
        {
            executionEngineVM = new ExecutionEngineVM(Filters);
            SaveProjectCommand = new RelayCommand(SaveProject, CanSaveProject);
            LoadProjectCommand = new RelayCommand(LoadProject);
            CreateNewProjectCommand = new RelayCommand(CreateNewProject);
            AddFilterCommand = new RelayCommand<string>(AddFilter, CanAddFilter);
            RemoveFilterCommand = new RelayCommand(RemoveFilter, CanRemoveFilter);
        }


        #region Project Related Methods
        private void CreateNewProject()
        {
            Filters = new ObservableCollection<FilterVM>();
        }

        /// <summary>
        /// Opens file dialog and if everything's fine, saves project to a JSON file
        /// </summary>
        private void SaveProject()
        {
            SaveFileDialog fileDialog = new SaveFileDialog();
            fileDialog.ShowDialog();
            string fileName = fileDialog.FileName;

    
            //TODO: Data save
            FilterProject project = new FilterProject();
         //   project.usedBitmapPath = ExecutionEngineVM.CurrentlyLoadedBitmap.Save("")
            foreach (FilterVM vm in Filters)
            {
                project.filterData.Add(new FilterVMData(vm));
            }
            
            string jsonContents = JsonConvert.SerializeObject(project, jsonSerializerSettings);
            Debug.WriteLine(jsonContents);
            File.WriteAllText(fileName, jsonContents);
        }


        private bool CanSaveProject(object _)
        {
            if (filters != null)
                return true;
            return false;
        }


        /// <summary>
        /// Opens file dialog and if everything's fine, loads project from JSON file
        /// </summary>
        private void LoadProject()
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.ShowDialog();
            string fileName = fileDialog.FileName;

            //TODO: show error to user?
            if (!File.Exists(fileName))
                return;

            //TODO: Last used bitmap load
            FilterProject project = JsonConvert.DeserializeObject<FilterProject>(File.ReadAllText(fileName), jsonSerializerSettings);
            Filters = new ObservableCollection<FilterVM>();
            foreach (FilterVMData data in project.filterData)
            {
                Filters.Add(new FilterVM(data));
            }
        }


        #endregion

        #region Filter Related Methods

        /// <summary>
        /// Creates new filter VM with given filter
        /// </summary>
        //TODO: Add presets 
        private void AddFilter(string FilterFactoryID)
        {
            FilterVM vm = FilterFactoryRegister.Factories[FilterFactoryID].CreateNewFilter();
            Filters.Add(vm);
            SelectedFilter = vm;
        }


      
        private bool CanAddFilter(object _)
        {
            return Filters != null ? true : false;
        }


        private void RemoveFilter()
        {
            Filters.Remove(SelectedFilter);
            //Set selection to null to avoid little memory leak
            SelectedFilter = null;
        }

        private bool CanRemoveFilter(object _)
        {
            return (SelectedFilter != null && SelectedFilter.CanDelete) ? true : false;
        }

        #endregion



    }
}
