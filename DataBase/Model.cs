using Microsoft.EntityFrameworkCore;

namespace DataBase;

public class ColosseumContext: DbContext
{
    public DbSet<ExperimentalCondition> experimental_conditions { get; set; }
    
    public ColosseumContext()
    {

    }

    // The following configures EF to create a Sqlite database file in the
    // special "local" folder for your platform.
    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite("Data Source=D:\\Studies\\C#\\CardGame\\colosseum.sqlite");
}

public class ExperimentalCondition
{
    public int id { get; set; }
    public string cards_order { get; set; }
}
