using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DataBase;

public class ColosseumContext : DbContext
{
    public DbSet<ExperimentalCondition> ExperimentalConditions { get; set; }

    public ColosseumContext(DbContextOptions<ColosseumContext> options) : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
        // => options.UseSqlite("Data Source=D:\\Studies\\C#\\CardGame\\colosseum.sqlite");
        => options.UseSqlite("Data Source=colosseum.db");
}

[Table("experimental_conditions")]
[PrimaryKey("Id")]
public class ExperimentalCondition
{
    [Column("id")] public int Id { get; set; }

    [Column("cards_order")] public string CardsOrder { get; set; }
}