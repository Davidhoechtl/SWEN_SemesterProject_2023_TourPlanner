using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace TourPlannerFrontEnd.Infrastructure.Helper
{
    public static class ShowAsyncOperation
    {
        // ToDo BusyIndicator starten/stoppen
        public static async Task<bool> Run(Action action, NLog.ILogger log)
        {
            bool successfull = false;
            await Task.Run(() =>
            {
                try
                {
                    action?.Invoke();
                    successfull = true;
                }
                catch (Exception ex)
                {
                    log.Error(ex, "Error with executing the provided action.");
                }
            });

            return successfull;
        }

        public static async Task RunAndShowMessage(Action action, string successMsg, string errorMsg, NLog.ILogger log)
        {
            bool successfull = false;
            await Task.Run(() =>
            {
                try
                {
                    action?.Invoke();
                    successfull = true;
                }
                catch (Exception ex)
                {
                    log.Error(ex, "Error with executing the provided action.");
                }
            });

            if(successfull)
            {
                MessageBox.Show(successMsg);
            }
            else
            {
                MessageBox.Show(errorMsg);
            }
        }
    }
}
