using Abpd2.Containers;

namespace Abpd2.Ship;

public class ContainerShip
{
    private List<Container> _containers;
    private double _maxSpeed;
    private double _maxAmountOfContainers;
    private double _maxCargoWeight;

    public ContainerShip(double maxSpeed, double maxAmountOfContainers, double maxCargoWeight)
    {
        _maxSpeed = maxSpeed;
        _maxAmountOfContainers = maxAmountOfContainers;
        _maxCargoWeight = maxCargoWeight;
        _containers = new List<Container>();
    }
    
}