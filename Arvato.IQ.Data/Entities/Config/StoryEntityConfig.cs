using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arvato.IQ.Data.Entities.Config
{
    internal class StoryEntityConfig : EntityTypeConfiguration<Story>
    {
        public StoryEntityConfig()
        {
            this.HasKey(t => t.StoryId);
            this.Property(t => t.Title)
                .HasMaxLength(500)
                .IsRequired();
            
        }
    }
}
