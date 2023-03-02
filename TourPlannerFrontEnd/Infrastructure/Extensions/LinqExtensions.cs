using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace TourPlannerFrontEnd.Infrastructure.Extensions
{
    internal static class LinqExtensions
    {
        public static IEnumerable<T2> SelectViewModels<T, T2>(this IEnumerable<T> models)
            where T : class
            where T2 : ViewModel<T>
        {
            List<T2> viewModels = new List<T2>();
            foreach (T model in models)
            {
                T2 viewModel = (T2)Activator.CreateInstance(typeof(T2));
                viewModel.Model = model;
                viewModels.Add(viewModel);
            }

            return viewModels;
        }
    }
}
