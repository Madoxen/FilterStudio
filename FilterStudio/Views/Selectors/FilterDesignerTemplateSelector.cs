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
            if ((item as FilterDataProviderVM) is BasicMatrixFilterDataProviderVM) //So this might seem like it's breaking Liskov, buuut we really cant do it other way, without creating whole plugin system
                //which is not the premise of this project (deadlines boiz).
            {
                return elem.FindResource("BasicMatrixFilterTemplate") as DataTemplate;
            }
            if ((item as FilterDataProviderVM) is GaussianFilterDataProviderVM) //So this might seem like it's breaking Liskov, buuut we really cant do it other way, without creating whole plugin system
                                                                   //which is not the premise of this project (deadlines boiz).
            {
                return elem.FindResource("GaussianMatrixFilterTemplate") as DataTemplate;
            }
            throw new ApplicationException();
        }



    }
}
