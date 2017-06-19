using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Abstract;
using System.Data.Entity.Design.PluralizationServices;
using System.Globalization;
using DAL.Entity;

namespace DAL.Concrete
{
    public class AdminPanelRepository : IAdminPanelRepository
    {
        private readonly DbContext _dbContext;
        private readonly List<string> _notNeededProperties;

        public AdminPanelRepository(DbContext context)
        {
            _dbContext = context;
            _notNeededProperties = new List<string>{ "Database", "ChangeTracker", "Configuration"};
        }

        public List<string> GetTableNamesList()
        {
            var r = _dbContext.GetType().GetProperties().Where(prop => !_notNeededProperties.Contains(prop.Name)).Select(t => t.Name).ToList();
            return r;
        }

        public DbSet GetDbSetByType<T>() where T : class
        {
            var dbSet = _dbContext.Set(typeof(T));
            dbSet.Load();
            return dbSet;
        }

        public void ContextSaveChanges()
        {
            _dbContext.SaveChanges();
        }
    }
}
