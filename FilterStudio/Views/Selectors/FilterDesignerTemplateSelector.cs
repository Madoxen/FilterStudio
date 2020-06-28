using FilterStudio.Concrete;
using FilterStudio.VM;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace FilterStudio.Views.Selectors
{
    public class FilterDesignerTemplateSelector : DataTemplateSelector
    {
        private readonly static Dictionary<Type, string> DataProviderToViewTemplateDictionary = new Dictionary<Type, string>()
        {
            [typeof(BasicMatrixFilterDataProviderVM)] = "BasicMatrixFilterTemplate",
            [typeof(GaussianFilterDataProviderVM)] = "GaussianMatrixFilterTemplate",
        };

        private readonly static string FallbackTemplateID = "NullTemplate";


         /// <summary>
        /// Selects DataTemplate baseing on IFilter concrete type
        /// </summary>
        /// <param name="item"></param>
        /// <param name="container"></param>
        /// <returns></returns>
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            FrameworkElement elem = container as FrameworkElement;
            if (elem == null)
            {
                return null;
            }
            if (item == null || !(item is FilterDataProviderVM))
            {
                //throw new ApplicationException();
                return null;
            }
            else
            {
                string templateID = DataProviderToViewTemplateDictionary.GetValueOrDefault(item.GetType(), FallbackTemplateID);
                return elem.FindResource(templateID) as DataTemplate;
            }
            throw new ApplicationException();
        }
    }
}
