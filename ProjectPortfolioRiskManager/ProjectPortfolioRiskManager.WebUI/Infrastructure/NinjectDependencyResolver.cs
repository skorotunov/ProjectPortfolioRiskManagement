using Ninject;
using ProjectPortfolioRiskManager.Domain.Abstract;
using ProjectPortfolioRiskManager.Domain.Concrete;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace ProjectPortfolioRiskManager.WebUI.Infrastructure
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
            kernel.Bind<ITemplateRepository>().To<TemplateRepository>();
            kernel.Bind<ICompanySizeRepository>().To<CompanySizeRepository>();
            kernel.Bind<IPositionRepository>().To<PositionRepository>();
            kernel.Bind<ISectionRepository>().To<SectionRepository>();
            kernel.Bind<IQuestionRepository>().To<QuestionRepository>();
            kernel.Bind<ILikertItemRepository>().To<LikertItemRepository>();
            kernel.Bind<IQuestionnaireRepository>().To<QuestionnaireRepository>();
        }
    }
}