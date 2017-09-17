using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Ninject;
using TypeRacerDomain.Abstract;
using TypeRacerDomain.Concrete;

namespace TypeRacerWeb.Infrastructure
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel kernel;

        public NinjectDependencyResolver(IKernel kernelParam)
        {
            kernel = kernelParam;
            AddBindings();
        }

        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }
        private void AddBindings()
        {
            kernel.Bind<IGlobalRacesRepository>().To<RacesRepository>();
            kernel.Bind<ITracksRepository>().To<TracksRepository>();
        }
    }
}