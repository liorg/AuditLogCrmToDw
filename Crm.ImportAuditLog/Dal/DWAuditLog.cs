

namespace Crm.ImportAuditLog.Dal
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    using System.Data.SqlClient;
    using Crm.ImportAuditLog.DataModel;

    public class DWAuditLog : DbContext
    {
        public DWAuditLog()
            : base("name=DW_Entities")
        {
        }
        public virtual DbSet<AuditLogModel> Audits { get; set; }
        public virtual DbSet<Job> Jobs { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //throw new UnintentionalCodeFirstException();
        }
        public int RemoveDuplicate()
        {
            return this.Database.SqlQuery<int>("DW_DeleteDuplicate").FirstOrDefault();
        }
    }
}

