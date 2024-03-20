using Abpd2.Exceptions;

namespace Abpd2.Containers;

public abstract class Container
{
    private static int _counter;
    private double _weight;
    protected double _cargoWeight;
    private double _height;
    private double _depth;
    protected double _maximumLoad;
    private char _type;
    private string _serialNumber;

    protected Container(double weight, double cargoWeight, double height, double depth, double maximumLoad, 
        char type)
    {
        _weight = weight;
        _cargoWeight = cargoWeight;
        _height = height;
        _depth = depth;
        _maximumLoad = maximumLoad;
        _type = type;
        _serialNumber = "KON-" + _type + "-" + _counter++;
    }


    public double Weight
    {
        get => _weight;
        set => _weight = value;
    }

    public double CargoWeight
    {
        get => _cargoWeight;
        set => _cargoWeight = value;
    }

    public double Height
    {
        get => _height;
        set => _height = value;
    }

    public double Depth
    {
        get => _depth;
        set => _depth = value;
    }

    public string SerialNumber
    {
        get => _serialNumber;
        set => _serialNumber = value ?? throw new ArgumentNullException(nameof(value));
    }

    public abstract void Unload();

    public abstract void LoadCargo(double cargoWeight);


}