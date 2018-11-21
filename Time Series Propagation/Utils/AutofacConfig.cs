using Autofac;
using DataLayer.Interaction;
using DataLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Time_Series_Propagation.Interfaces;
using Time_Series_Propagation.Utils.Abstract;

namespace Time_Series_Propagation.Utils
{
    public class AutofacConfig
    {
        public static IContainer ConfigureContainer()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<MyApplication>().As<IApplication>();
            builder.RegisterType<NNDataInteraction>().As<INNDataInteraction>();
            builder.RegisterType<TimeSeriesPropagation>().As<ITimeSeriesPropagation>();

            return builder.Build();
        }
    }
}
