namespace Abpd2.Products;

public class Product
{
    private string _name;
    private double _requiredTemperature;
    private string _type;
    private double _weight;

    public Product(string name, double requiredTemperature, string type, double weight)
    {
        _name = name;
        _requiredTemperature = requiredTemperature;
        _type = type;
        _weight = weight;
    }

    public string Name
    {
        get => _name;
        set => _name = value ?? throw new ArgumentNullException(nameof(value));
    }

    public double RequiredTemperature
    {
        get => _requiredTemperature;
        set => _requiredTemperature = value;
    }

    public string Type
    {
        get => _type;
        set => _type = value ?? throw new ArgumentNullException(nameof(value));
    }

    public double Weight
    {
        get => _weight;
        set => _weight = value;
    }
}