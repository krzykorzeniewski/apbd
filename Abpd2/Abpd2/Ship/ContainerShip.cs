using Abpd2.Containers;
using Abpd2.Exceptions;

namespace Abpd2.Ship;

public class ContainerShip
{
    private List<Container> _containers;
    private double _maxSpeed;
    private int _maxAmountOfContainers;
    private double _maxCargoWeight;
    private double _currentWeight;

    public ContainerShip(double maxSpeed, int maxAmountOfContainers, double maxCargoWeight)
    {
        _maxSpeed = maxSpeed;
        _maxAmountOfContainers = maxAmountOfContainers;
        _maxCargoWeight = maxCargoWeight;
        _containers = new List<Container>();
        _currentWeight = CalculateCurrentWeight(_containers);
    }

    public void AddContainer(Container container)
    {
        List<Container> tempList = _containers;
        tempList.Add(container);
        
        CheckWeightAndContainerCount(tempList);
        
        _containers.Add(container);
        UpdateWeight();
        Console.WriteLine("Container added!");
    }

    public void AddContainers(List<Container> containers)
    {
        List<Container> tempList = _containers;
        tempList.AddRange(containers);
        
        CheckWeightAndContainerCount(tempList);

        _containers = tempList;
        UpdateWeight();
        Console.WriteLine("Containers added!");

    }

    public void RemoveContainer(Container container)
    {
        if (_containers.Remove(container))
        {
            Console.WriteLine("Container removed!");
            UpdateWeight();
        }
        else
        {
            Console.WriteLine("Container is not on this ship!");
        }
    }

    private void CheckWeightAndContainerCount(List<Container> containers)
    {
        if (CalculateCurrentWeight(containers)  > _maxCargoWeight)
        {
            throw new LoadLimitException("Maximum load reached! Unable to load new container");
        }  
        if (containers.Count > _maxAmountOfContainers)
        {
            throw new ContainerLimitException("Maximum container amount reached! Unable to add container!");
        }
    }
    private double CalculateCurrentWeight(List<Container> containers)
    {
        double tempWeight = 0.0;
        foreach (Container temp in containers) 
        {
            tempWeight += temp.Weight + temp.CargoWeight;
        }
        return tempWeight;
    }

    private void UpdateWeight()
    {
        _currentWeight = CalculateCurrentWeight(_containers);
    }
}