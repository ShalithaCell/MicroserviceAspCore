using Identity.Infrastructure.DAL;
using Identity.Infrastructure.IServices.Repository;
using Identity.Model.DataModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Identity.Core.Services.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationDbContext _dbContext;

        private BaseRepository<Tenant> _tenents;

        public UnitOfWork(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IRepository<Tenant> Tenants
        {
            get
            {
                return _tenents ??
                    (_tenents = new BaseRepository<Tenant>(_dbContext));
            }
        }

        /// <summary>
        /// Save data chnages
        /// </summary>
        public void Commit()
        {
            _dbContext.SaveChanges();
        }

    }
}
