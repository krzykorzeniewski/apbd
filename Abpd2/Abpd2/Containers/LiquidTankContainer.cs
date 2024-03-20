using Abpd2.Exceptions;
using Abpd2.Interfaces;

namespace Abpd2.Containers;

public class LiquidTankContainer : Container, IHazardNotifier
{

    private bool _containsHazardousCargo;
    
    public LiquidTankContainer(double weight, double cargoWeight, double height, double depth, double maximumLoad, char type,
        bool containsHazardousCargo)
        : base(weight, cargoWeight, height, depth, maximumLoad, type)
    {
        _containsHazardousCargo = containsHazardousCargo;
    }

    public string Notify()
    {
        return "Warning, danger! container no: " + SerialNumber;
    }


    public override void Unload()
    {
        _cargoWeight = 0;
        Console.WriteLine("Container unloaded!");
    }

    public override void LoadCargo(double cargoWeight)
    {
        if (_containsHazardousCargo)
        {
            if (_cargoWeight + cargoWeight > _maximumLoad / 2)
            {
                Notify();
                throw new LoadLimitException(Notify());
            }
            _cargoWeight += cargoWeight;
            Console.WriteLine("Added " + cargoWeight + " kg of cargo!");
        }
        else
        {
            if (_cargoWeight + cargoWeight > _maximumLoad * 0.9)
            {
                throw new LoadLimitException(Notify());
            }
            _cargoWeight += cargoWeight;
            Console.WriteLine("Added " + cargoWeight + " kg of cargo!");
        }
    }
}