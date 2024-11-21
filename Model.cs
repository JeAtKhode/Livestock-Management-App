namespace Farm_App;

public class Animal
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set;}
    public double Cost{ get; set;}
    public  double Weight{get; set;}
    public string? Colour{ get; set;}
    
    public override string ToString()
    {
        return $"{GetType().Name,-15} {Id,-5}{Colour,-20}{Cost,-5} {Weight,-5}";

    }
}


[Table("Cow")]
public class Cow : Animal
{
    public double Milk{ get; set;}
    public override string ToString()
    {
        return base.ToString() + $"{Milk}";
    }

}

[Table("Sheep")]
public class Sheep : Animal
{
    public double Wool{ get; set;}
     public override string ToString()
    {
        return base.ToString() + $"{Wool}";
    }

}
