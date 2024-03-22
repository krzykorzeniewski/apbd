using Abpd2.Exceptions;
using Abpd2.Products;

namespace Abpd2.Containers;

public class RefrigeratedContainer : Container
{

    private double _internalTemperature;
    private string _typeOfProduct;
    private List<Product> _products;

    public RefrigeratedContainer(double weight, double height, double depth, double maximumLoad,
        char type, double internalTemperature, string typeOfProduct) 
        : base(weight, height, depth, maximumLoad, type)
    {
        _internalTemperature = internalTemperature;
        _typeOfProduct = typeOfProduct;
        _products = new List<Product>();
    }

    public void AddProduct(Product product)
    {
        CheckType(product);
        CheckTemperature(product);
        LoadCargo(product.Weight);
        _products.Add(product);
    }

    public void CheckTemperature(Product product)
    {
        if (_internalTemperature < product.RequiredTemperature)
        {
            throw new TemperatureException("Internal temperature cannot be lower than optimal temperature!");
        }
    }

    public void CheckType(Product product)
    {
        if (product.Type != _typeOfProduct)
        {
            throw new TypeMismatchException("This container can carry only one type of product!");
        }
    }

    public override void Unload()
    {
        _cargoWeight = 0;
        _products.Clear();
        Console.WriteLine("Container unloaded!");
    }

    public override void LoadCargo(double cargoWeight)
    {
        if (_cargoWeight + cargoWeight > _maximumLoad)
        {
            throw new LoadLimitException("Too much load!");
        }
        _cargoWeight += cargoWeight;
        Console.WriteLine("Added " + cargoWeight + " kg of cargo!");
    }
}