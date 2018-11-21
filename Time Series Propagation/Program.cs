using Autofac;
using System;
using Time_Series_Propagation.Utils;
using Time_Series_Propagation.Utils.Abstract;

namespace Time_Series_Propagation
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            var container = AutofacConfig.ConfigureContainer();
            using (var scope = container.BeginLifetimeScope())
            {
                var app = scope.Resolve<IApplication>();
                app.Run();
            }
        }
    }
}
