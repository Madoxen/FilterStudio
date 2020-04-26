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
            private set { SetProperty(ref lastOutput, value); }
        }


        private bool canReorder;
        /// <summary>
        /// Indicates if given node can change order in project tree
        /// </summary>
        public bool CanReorder
        {
            get { return canReorder; }
            set { SetProperty(ref canReorder, value); }
        }


        private bool canDelete;
        /// <summary>
        /// Indicates if given node can be deleted
        /// </summary>
        public bool CanDelete
        {
            get { return canDelete; }
            set { SetProperty(ref canDelete, value); }
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
        public IFilter UnderlayingFilter
        {
            get { return underlayingFilter; }
        }

        public FilterVM(IFilter UnderlayingFilter)
        {
            underlayingFilter = UnderlayingFilter;
            LastInput = UnderlayingFilter.Input;
            LastOutput = UnderlayingFilter.Output;
        }

        /// <summary>
        /// Operates underlaying filter, and sets properties of this filter after the operation
        /// </summary>
        public Bitmap Operate(in Bitmap input)
        {
            underlayingFilter.Input = input;
            underlayingFilter.Operate();

            //Note that we do not update Properties, to avoid INCP update
            lastInput = underlayingFilter.Input;
            lastOutput = underlayingFilter.Output;

            return underlayingFilter.Output;
        }


        //Notifies UI about bitmap changes
        public void NotifyUI()
        {
            //Trigger INCP
            RaisePropertyChanged(nameof(LastInput));
            RaisePropertyChanged(nameof(LastOutput));
        }

    }
}
