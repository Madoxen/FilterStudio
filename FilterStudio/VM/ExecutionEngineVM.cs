using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using System.Windows.Threading;
using System.Diagnostics;

namespace FilterStudio.VM
{
    public class ExecutionEngineVM : BaseVM
    {

        private Bitmap currentlyLoadedBitmap;
        /// <summary>
        /// Originally loaded bitmap
        /// </summary>
        public Bitmap CurrentlyLoadedBitmap
        {
            get { return currentlyLoadedBitmap; }
            set { SetProperty(ref currentlyLoadedBitmap, value); }
        }


        private Bitmap lastOutputBitmap;
        /// <summary>
        /// Bitmap outputted by last operation
        /// </summary>
        public Bitmap LastOutputBitmap
        {
            get { return lastOutputBitmap; }
            set { SetProperty(ref lastOutputBitmap, value); }
        }

        public RelayCommand ExecuteTreeCommand { get; set; }
        public RelayCommand CancelExecuteTreeCommand { get; set; }

        public RelayCommand LoadImageCommand { get; set; }
        public RelayCommand SaveImageCommand { get; set; }



        private int treeExecutionProgressValue;
        public int TreeExecutionProgressValue
        {
            get { return treeExecutionProgressValue; }
            private set { SetProperty(ref treeExecutionProgressValue, value); }
        }


        private TaskStatus treeExecutionTaskStatus;
        public TaskStatus TreeExecutionTaskStatus
        {
            get { return treeExecutionTaskStatus; }
            private set { SetProperty(ref treeExecutionTaskStatus, value); }
        }

        private readonly ObservableCollection<FilterVM> currentTree;
        private Task executeTreeTask;
        private CancellationTokenSource executeTreeCancellationTokenSource;
        private Progress<int> treeExecutionProgress;

        public string currentlyLoadedBitmapPath { get; private set; }


        public ExecutionEngineVM(ObservableCollection<FilterVM> currentTree)
        {
            this.currentTree = currentTree;
            ExecuteTreeCommand = new RelayCommand(StartExecuteTree, CanExecuteTree);
            LoadImageCommand = new RelayCommand(LoadImage);
            SaveImageCommand = new RelayCommand(SaveImage, CanSaveImage);
            CancelExecuteTreeCommand = new RelayCommand(() => { executeTreeCancellationTokenSource.Cancel(); }, (obj) => { return executeTreeTask.Status == TaskStatus.Running; });
        }


        /// <summary>
        /// Starts or cancels execution task on another non-UI thread
        /// </summary>
        //TODO: Consider spliting this into two methods: start and cancel
        private void StartExecuteTree()
        {
            if (executeTreeTask?.Status != TaskStatus.Running && executeTreeTask?.Status != TaskStatus.WaitingForActivation && executeTreeTask?.Status != TaskStatus.WaitingForActivation)
            {
                executeTreeCancellationTokenSource = new CancellationTokenSource();
                TreeExecutionProgressValue = 1;
                treeExecutionProgress = new Progress<int>();
                treeExecutionProgress.ProgressChanged += (sender, e) => { TreeExecutionProgressValue = e; };
                executeTreeTask = new Task(() =>
                {
                    try
                    {
                        ExecuteTree(treeExecutionProgress, executeTreeCancellationTokenSource.Token);
                    }
                    catch (OperationCanceledException ce)
                    {
                        TreeExecutionProgressValue = 0;
                    }
                }, executeTreeCancellationTokenSource.Token);
                executeTreeTask.Start();
            }
            else
            {
                executeTreeCancellationTokenSource.Cancel();
            }
        }

        /// <summary>
        /// Action that is called after tree execution
        /// This runs on UI thread to only update UI
        /// </summary>
        private void AfterExecuteTree()
        {
            Dispatcher.CurrentDispatcher.Invoke(() =>
                {
                    foreach (FilterVM v in currentTree)
                    {
                        v.NotifyUI();
                    }
                });
        }

        private void ExecuteTree(IProgress<int> progress, CancellationToken ct)
        {
            object lObject = new object();
            lock (lObject) //lock 
            {
                Bitmap currentOutput = currentlyLoadedBitmap;
                int counter = 1;
                foreach (FilterVM filter in currentTree)
                {
                    ct.ThrowIfCancellationRequested();
                    filter.Operate(currentOutput);
                    currentOutput = filter.LastOutput;
                    progress.Report(counter++);
                }
                LastOutputBitmap = currentTree.Last().LastOutput;
            }
            AfterExecuteTree();
            progress.Report(0);
        }

        private bool CanExecuteTree(object _)
        {
            return (currentTree?.Count > 0 && CurrentlyLoadedBitmap != null);
        }

        private void LoadImage()
        {
            try
            {
                OpenFileDialog fileDialog = new OpenFileDialog();
                fileDialog.ShowDialog();
                string fileName = fileDialog.FileName;

                CurrentlyLoadedBitmap = new Bitmap(fileName);
                currentlyLoadedBitmapPath = fileName;
            }
            catch (IOException)
            {
                Debug.WriteLine("Could not load bitmap");
            }
        }

        private void SaveImage()
        {
            SaveFileDialog fileDialog = new SaveFileDialog();
            fileDialog.ShowDialog();
            string fileName = fileDialog.FileName;
            LastOutputBitmap.Save(fileName);
        }


        private bool CanSaveImage(object _)
        {
            return LastOutputBitmap != null;
        }
    }
}
