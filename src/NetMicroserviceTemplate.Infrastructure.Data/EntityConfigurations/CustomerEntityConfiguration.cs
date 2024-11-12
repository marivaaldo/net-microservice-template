namespace NetMicroserviceTemplate.Infrastructure.Data.EntityConfigurations;

internal sealed class CustomerEntityConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.ToTable("Customers");
        builder.HasKey(x=> x.Id);
        builder.HasIndex(x => x.Email).IsUnique();
        builder.Property(x => x.Email).IsRequired();
        builder.OwnsOne(x => x.Address);
    }
}
