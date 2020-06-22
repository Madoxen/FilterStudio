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

        public RelayCommand AddFilterCommand { get; set; }
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
        #endregion


        public MainVM()
        {
            executionEngineVM = new ExecutionEngineVM(Filters);
            SaveProjectCommand = new RelayCommand(SaveProject, CanSaveProject);
            LoadProjectCommand = new RelayCommand(LoadProject);
            CreateNewProjectCommand = new RelayCommand(CreateNewProject);
            AddFilterCommand = new RelayCommand(AddFilter, CanAddFilter);
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

            //TODO: show error to user?
            if (!File.Exists(fileName))
                return;

            //TODO: Data save


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

            //TODO: Data load
        }


        #endregion

        #region Filter Related Methods

        /// <summary>
        /// Creates new filter VM with given filter
        /// </summary>
        //TODO: Add presets 
        private void AddFilter()
        {
            BasicMatrixFilter concreteFilter = new BasicMatrixFilter
            (new double[3, 3] {
                { -1.0, -1.0, -1.0 },
                { -1.0, 8.0, -1.0 },
                { -1.0, -1.0, -1.0 }});


            FilterVM vm = new FilterVM(concreteFilter)
            {
                Name = "Filter #" + Filters.Count
            };
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
