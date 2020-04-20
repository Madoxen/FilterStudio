using FilterStudio.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace FilterStudio.Core
{
    /// <summary>
    /// Engine that manages execution of Operation Tree
    /// </summary>
    public class ExecutionEngine
    {
        List<IFilter> filters = new List<IFilter>();
        public List<IFilter> Filter
        {
            get { return filters; }
        }



        /// <summary>
        /// Default constructor, will create empty Filters list
        /// </summary>
        public ExecutionEngine()
        { 
        }


        /// <summary>
        /// Constructor that will accept and create NEW instance of provided filter list
        /// </summary>
        /// <param name="filters"></param>
        public ExecutionEngine(List<IFilter> filters)
        {
            filters = new List<IFilter>(filters);
        }


        /// <summary>
        /// Executes filters from top to bottom
        /// </summary>
        public void Execute()
        {
            foreach (IFilter filter in filters)
            {
                filter.Operate();
            }
        }
    }
}
