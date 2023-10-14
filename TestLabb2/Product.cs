namespace TestLabb2;

public record Product
{
    public string Id { get; set; }
    public string Name { get; set; }
    public double Price { get; set; }

    public override string ToString()
    {
        return Id + Name + Price;
    }
}