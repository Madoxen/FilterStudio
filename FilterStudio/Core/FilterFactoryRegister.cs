using FilterStudio.Concrete.FilterFactories;
using FilterStudio.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace FilterStudio.Core
{
    /// <summary>
    /// Class that holds all filter factories with their respective name identifiers
    /// </summary>
    public static class FilterFactoryRegister
    {
        public readonly static Dictionary<string, IFilterFactory> Factories = new Dictionary<string, IFilterFactory>()
        {
            ["BasicFilter"] = new BasicFilterFactory(),
            ["GaussianFilter"] = new GaussianFilterFactory(),
        };
    }
}
