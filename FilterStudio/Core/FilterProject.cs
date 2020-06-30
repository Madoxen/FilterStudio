using FilterStudio.Interfaces;
using FilterStudio.VM;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace FilterStudio.Core
{

    /// <summary>
    /// Model class
    /// </summary>
    public class FilterProject
    {
        /// <summary>
        /// List of filters used in this project
        /// </summary>
        public List<FilterVMData> filterData = new List<FilterVMData>();
        /// <summary>
        /// Bitmap used as reference in this project
        /// </summary>
        public string usedBitmapName = "default.png";


    }
}
