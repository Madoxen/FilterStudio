using FilterStudio.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Controls;

namespace FilterStudio.VM
{
    /// <summary>
    /// View model for detailed filter view
    /// </summary>
    public class FilterVM : BaseVM
    {

        private Bitmap lastInput;
        public Bitmap LastInput
        {
            get { return lastInput; }
            set { SetProperty(ref lastInput, value); }
        }

        private Bitmap lastOutput;
        public Bitmap LastOutput
        {
            get { return lastOutput; }
            private set { SetProperty(ref lastInput, value); }
        }


        private string name;
        /// <summary>
        /// User editable name
        /// </summary>
        public string Name
        {
            get { return name; }
            set { SetProperty(ref name, value); }
        }

        /// <summary>
        /// Underlaying Core filter
        /// Can be only set by using constructor
        /// </summary>
        private readonly IFilter underlayingFilter;

        public FilterVM(IFilter UnderlayingFilter)
        {
            underlayingFilter = UnderlayingFilter;
            LastInput = UnderlayingFilter.Input;
            LastOutput = UnderlayingFilter.Output;
        }

        /// <summary>
        /// Operates underlaying filter, and sets properties of this filter after the operation
        /// </summary>
        void Operate()
        {
            underlayingFilter.Input = LastInput;
            underlayingFilter.Operate();
            LastOutput = underlayingFilter.Output;
        }

    }
}
