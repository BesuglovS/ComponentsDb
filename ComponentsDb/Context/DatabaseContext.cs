using ComponentsDb.DomainClasses;
using ComponentsDb.Exceptions;
using System.Data.Entity;
using System.Windows.Forms;

namespace ComponentsDb.Context
{
    class DatabaseContext: DbContext
    {
        private static DatabaseConnectionInfo _connectionInfo;

        public DatabaseContext(): base(GetConnectionString())
        {
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<DatabaseContext>());
        }
        
        private static string GetConnectionString()
        {
            try
            {
                _connectionInfo = GetConnectionInfo();
            }
            catch (FetchConnectionDataException exc)
            {
                MessageBox.Show(exc.Message, "Не удалось прочитать реквизиты для подключения к базе данных");
                return null;
            }

            var cs = "data source=tcp:" + _connectionInfo.ConnectionServer +
                   "; Database=" + _connectionInfo.ConnectionDatabaseName + 
                   "; User Id=" + _connectionInfo.ConnectionUsername + 
                   "; Password=" + _connectionInfo.ConnectionPassword + 
                   "; multipleactiveresultsets=True";

            return cs;
        }

        private static DatabaseConnectionInfo GetConnectionInfo()
        {
            var result = ConncetionInfoFetcher.GetConnectionInfoIni();
            if (result != null)
            {
                return result;
            }

            result = ConncetionInfoFetcher.GetConnectionInfoUdl();
            return result;
        }
        
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Component>()
                .HasMany(c => c.Parts)
                .WithRequired(c => c.ParentComponent)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Component>()
                .HasMany(c => c.IsPartOf)
                .WithRequired(c => c.ChildComponent)
                .WillCascadeOnDelete(false);
        }

        public DbSet<Component> Components { get; set; }
        public DbSet<ComponentLink> ComponentLinks { get; set; }
    }
}
