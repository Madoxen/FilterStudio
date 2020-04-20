﻿using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

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
        public RelayCommand LoadImageCommand { get; set; }
        public RelayCommand SaveImageCommand { get; set; }


        private readonly ObservableCollection<FilterVM> currentTree;
        
 

        public ExecutionEngineVM(ObservableCollection<FilterVM> currentTree)
        {
            this.currentTree = currentTree;


            ExecuteTreeCommand = new RelayCommand(ExecuteTree, CanExecuteTree);
            LoadImageCommand = new RelayCommand(LoadImage);
            SaveImageCommand = new RelayCommand(SaveImage, CanSaveImage);
        }



        public void ExecuteTree()
        {
            Bitmap currentOutput = currentlyLoadedBitmap;
            foreach (FilterVM filter in currentTree)
            {
                filter.LastInput = currentOutput;
                filter.Operate();
                currentOutput = filter.LastOutput;
            }
            LastOutputBitmap = currentTree.Last().LastOutput;
        }

    

        public bool CanExecuteTree(object _)
        {
            if (currentTree?.Count > 0 && CurrentlyLoadedBitmap != null)
                return true;
            return false;
        }

        public void LoadImage()
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.ShowDialog();
            string fileName = fileDialog.FileName;

            //TODO: show error to user?
            if (!File.Exists(fileName))
                return;


            CurrentlyLoadedBitmap = new Bitmap(fileName);
        }

        public void SaveImage()
        {
            SaveFileDialog fileDialog = new SaveFileDialog();
            fileDialog.ShowDialog();
            string fileName = fileDialog.FileName; 
            LastOutputBitmap.Save(fileName); 
        }


        public bool CanSaveImage(object _)
        {
            return LastOutputBitmap != null ? true : false;
        }


    }
}