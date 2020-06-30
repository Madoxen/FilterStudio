using FilterStudio.Core;
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
    [JsonObject(MemberSerialization.OptIn)]
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

        [JsonProperty]
        private bool canReorder;
        /// <summary>
        /// Indicates if given node can change order in project tree
        /// </summary>
        public bool CanReorder
        {
            get { return canReorder; }
            set { SetProperty(ref canReorder, value); }
        }

        [JsonProperty]
        private bool canDelete;
        /// <summary>
        /// Indicates if given node can be deleted
        /// </summary>
        public bool CanDelete
        {
            get { return canDelete; }
            set { SetProperty(ref canDelete, value); }
        }

        [JsonProperty]
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
        [JsonProperty]
        private readonly IFilter underlayingFilter;
        public IFilter UnderlayingFilter
        {
            get { return underlayingFilter; }
        }

        [JsonProperty]
        private FilterDataProviderVM dataVM;
        public FilterDataProviderVM DataVM
        {
            get { return dataVM; }
            set { SetProperty(ref dataVM, value); }
        }


        public FilterVM(IFilter UnderlayingFilter)
        {
            underlayingFilter = UnderlayingFilter; //this will be later chosen by presets
            LastInput = UnderlayingFilter.Input;
            LastOutput = UnderlayingFilter.Output;
        }

        /// <summary>
        /// Creates new filter VM using saved data object
        /// </summary>
        /// <param name="data"></param>
        public FilterVM(FilterVMData data)
        {
            underlayingFilter = data.UnderlayingFilter;
            CanReorder = data.CanReorder;
            CanDelete = data.CanDelete;
            Name = data.Name;

            if (data.FilterDataProvider != null)
            {
                DataVM = (FilterDataProviderVM)Activator.CreateInstance(data.FilterDataProvider.GetType(), this); //Create concrete type, but cast it to base type
                DataVM.CopySettings(data.FilterDataProvider);
            }
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
