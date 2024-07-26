using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore;

namespace ChecklistVeiculos.Services
{
    public class ServiceBase<T> where T : DbContext
    {
        private readonly T context;

        public ServiceBase(T context, ILogger logger)
        {
            this.context = context;
            Logger = logger;
        }

        protected ILogger Logger { get; }

        protected T Context { get { return this.context; }}
        
    }
}