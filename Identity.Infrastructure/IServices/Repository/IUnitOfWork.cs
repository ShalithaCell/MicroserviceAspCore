using Identity.Model.DataModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Identity.Infrastructure.IServices.Repository
{
    public interface IUnitOfWork
    {
        IRepository<Tenant> Tenants { get; }

        void Commit();
    }
}
