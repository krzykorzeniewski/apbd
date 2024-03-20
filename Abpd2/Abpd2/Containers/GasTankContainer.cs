using Abpd2.Interfaces;

namespace Abpd2.Containers;

public class GasTankContainer : Container, IHazardNotifier
{
    private double _internalPressure;

    public GasTankContainer(double weight, double cargoWeight, double height, double depth, double maximumLoad, char type) : base(weight, cargoWeight, height, depth, maximumLoad, type)
    {
    }

    public override void Unload()
    {
        throw new NotImplementedException();
    }

    public override void LoadCargo(double cargoWeight)
    {
        throw new NotImplementedException();
    }

    public string Notify()
    {
        throw new NotImplementedException();
    }
}