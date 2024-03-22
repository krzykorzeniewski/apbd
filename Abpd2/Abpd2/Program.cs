using Abpd2.Containers;
using Abpd2.Ship;
using Microsoft.VisualBasic;

namespace Abpd2;

public class Program
{
    private static List<Container> _containers = new List<Container>();
    private static List<ContainerShip> _containerShips = new List<ContainerShip>();
    
    public static void Main(string[] args)
    {
        while (true)
        {
            if (_containerShips.Count == 0)
            {
                Console.WriteLine("Available container ships: \n" +
                                  "none");
                Console.WriteLine("Available options: \n" +
                                  "1. Create container ship");
                int temp = Convert.ToInt32(Console.ReadLine());
                switch (temp)
                {
                    case 1: 
                        CreateContainerShip();
                        break;
                    default:
                        Console.WriteLine("Try again!");
                        break;
                }
            }
            else
            {
                Console.WriteLine("Available container ships: \n" +
                                  String.Join(',', _containerShips) + "\n");
                Console.WriteLine("Available containers: \n" +
                                  String.Join(',', _containers) + "\n");
                Console.WriteLine("Available options: \n " +
                                  "1. Create container ship \n" +
                                  "2. Remove container ship \n" +
                                  "3. Create container");
                int temp = Convert.ToInt32(Console.ReadLine());
                switch (temp)
                {
                    case 1:
                        CreateContainerShip();
                        break;
                    case 2:
                        RemoveContainerShip();
                        break;
                    case 3:
                        CreateContainer();
                        break;
                    default:
                        Console.WriteLine("Try again!");
                        break;
                }
            }
        }
    }

    private static void RemoveContainerShip()
    {
        Console.WriteLine("Select which container ship should be deleted by entering it's index");
        Console.WriteLine(String.Join(',',_containerShips));
        int temp = Convert.ToInt32(Console.ReadLine());
        _containerShips.RemoveAt(temp);
        Console.WriteLine("Ship removed!");
    }

    private static void CreateContainerShip()
    {
        Console.WriteLine("Enter ship's parameters line after line: " +
                          "max speed, max amount of containers, max cargo weight");
        double maxSpeed,maxCargoWeight;
        int maxAmountOfCont;
        maxSpeed = Convert.ToDouble(Console.ReadLine());
        maxAmountOfCont = Convert.ToInt32(Console.ReadLine());
        maxCargoWeight = Convert.ToDouble(Console.ReadLine());
        ContainerShip containerShip = new ContainerShip(maxSpeed, maxAmountOfCont, maxCargoWeight);
        _containerShips.Add(containerShip);
        Console.WriteLine("ContainerShip created!");
    }

    public static void CreateContainer()
    {
        Console.WriteLine("Enter container type (L - luqid, G - gas, R - refrigerated)");
        char type = Convert.ToChar(Console.ReadLine() ?? string.Empty);
        switch (type)
        {
            case 'L':
                CreateLiquidContainer(type);
                break;
            case 'G':
                CreateGasTankContainer(type);
                break;
            case 'R':
                CreateRefrigeratedContainer(type);
                break;
            default:
                Console.WriteLine("Try again!");
                break;
        }
    }

    private static void CreateRefrigeratedContainer(char type)
    {
        Console.WriteLine("Enter parameters (line by line):  " +
                          "weight, height, depth, maximumLoad, internalTemp" +
                          " typeOfProduct");
        double weight, height, depth, maximumLoad, internalTemperature;
        string typeOfProduct;
        weight = Convert.ToDouble(Console.ReadLine());
        height = Convert.ToDouble(Console.ReadLine());
        depth = Convert.ToDouble(Console.ReadLine());
        maximumLoad = Convert.ToDouble(Console.ReadLine());
        internalTemperature = Convert.ToDouble(Console.ReadLine());
        typeOfProduct = Console.ReadLine();
        Container container = new RefrigeratedContainer(weight, height, depth, maximumLoad, type, 
        internalTemperature, typeOfProduct);
        _containers.Add(container);
    }

    private static void CreateGasTankContainer(char type)
    {
        Console.WriteLine("Enter parameters (line by line):  " +
                          "weight, height, depth, maximumLoad, internalPressure");
        double weight, height, depth, maximumLoad, internalPressure;
        weight = Convert.ToDouble(Console.ReadLine());
        height = Convert.ToDouble(Console.ReadLine());
        depth = Convert.ToDouble(Console.ReadLine());
        maximumLoad = Convert.ToDouble(Console.ReadLine());
        internalPressure = Convert.ToDouble(Console.ReadLine());
        Container container =
            new GasTankContainer(weight, height, depth, maximumLoad, type, internalPressure);
        _containers.Add(container);
    }

    private static void CreateLiquidContainer(char type)
    {
        Console.WriteLine("Enter parameters (line by line):  " +
                          "weight, height, depth, maximumLoad, hazardousCargo (True or False)");
        double weight, height, depth, maximumLoad, internalPressure;
        bool hazardousCargo;
        weight = Convert.ToDouble(Console.ReadLine());
        height = Convert.ToDouble(Console.ReadLine());
        depth = Convert.ToDouble(Console.ReadLine());
        maximumLoad = Convert.ToDouble(Console.ReadLine());
        hazardousCargo = Convert.ToBoolean(Console.ReadLine());
        Container container =
            new LiquidTankContainer(weight, height, depth, maximumLoad, type, hazardousCargo);
        _containers.Add(container);
    }
}