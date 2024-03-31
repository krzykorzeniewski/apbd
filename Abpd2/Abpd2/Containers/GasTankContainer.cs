using Abpd2.Exceptions;
using Abpd2.Interfaces;

namespace Abpd2.Containers;

public class GasTankContainer : Container, IHazardNotifier
{
    private double _internalPressure;

    public GasTankContainer(double weight, double height, double depth, double maximumLoad, 
        char type, double internalPressure) 
        : base(weight, height, depth, maximumLoad, type)
    {
        _internalPressure = internalPressure;
    }

    public override void Unload()
    {
        _cargoWeight *= 0.05;
    }

    public override void LoadCargo(double cargoWeight)
    {
        if (_cargoWeight + cargoWeight > _maximumLoad)
        {
            throw new LoadLimitException("Too much load!");
        }
        _cargoWeight += cargoWeight;
        Console.WriteLine("Cargo loaded!");
    }

    public string Notify()
    {
        return "Warning, danger! container no: " + SerialNumber;
    }
}