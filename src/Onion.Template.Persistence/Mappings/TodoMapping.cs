using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Onion.Template.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Onion.Template.Persistence.Mappings;

public class TodoMapping : IEntityTypeConfiguration<Todo>
{
	public void Configure(EntityTypeBuilder<Todo> builder)
	{
		builder.ToTable("TB_TODO");
		builder.HasKey(p => p.Id);
		builder
			.Property(p => p.Id)
			.HasColumnName("ID_TODO");
		builder
			.Property(p => p.UserId)
			.HasColumnName("ID_USER");
		builder
			.Property(p => p.Title)
			.HasColumnName("DS_TITLE");
		builder
			.Property(p => p.Completed)
			.HasColumnName("FL_COMPLETED");
		builder
			.Property(p => p.DtIncluded)
			.HasColumnName("DT_INCLUDED")
			.HasDefaultValueSql("getdate()");
		builder
			.Property(p => p.DtLastModified)
			.HasColumnName("DT_LAST_MODIFIED")
			.HasDefaultValueSql("getdate()");
		builder
			.Property(p => p.DtExcluded)
			.HasColumnName("DT_EXCLUDED");
		builder.HasOne(t => t.User)
			.WithMany(u => u.TodoList)
			.HasForeignKey(t => t.UserId)
			.OnDelete(DeleteBehavior.Restrict);

		builder.HasQueryFilter(p => p.DtExcluded == null);	
	}
}