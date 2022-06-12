using System.ComponentModel.DataAnnotations.Schema;

[Table("users")]
class User
{
    public long Id { get; set; }
    public string Name { get; set; } = string.Empty;
}

[Table("deals")]
class Deal
{
    public long Id { get; set; }
    public long UserId { get; set; }
    public virtual User? User { get; set; }
    public decimal Profit { get; set; }
}