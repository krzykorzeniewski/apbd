using Abpd2.Containers;
using Abpd2.Ship;

namespace Abpd2;

public class Program
{
    private static List<Container> _containers = new List<Container>();
    private static List<ContainerShip> _containerShips = new List<ContainerShip>();
    
    public static void Main(string[] args)
    {
        bool stillWork = true;
        try // przepraszam - musialem
        {
            while (stillWork)
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
                else if (_containerShips.Count != 0 && _containers.Count == 0)
                {
                    Console.WriteLine("Available container ships: \n" +
                                      PrintList(_containerShips) + "\n");
                    Console.WriteLine("Available containers: \n" +
                                      PrintList(_containers) + "\n");
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
                else
                {
                    Console.WriteLine("Available container ships: \n" +
                                      PrintList(_containerShips) + "\n");
                    Console.WriteLine("Available containers: \n" +
                                      PrintList(_containers) + "\n");
                    Console.WriteLine("Available options: \n " +
                                      "1. Create container ship \n" +
                                      "2. Remove container ship \n" +
                                      "3. Create container \n" +
                                      "4. Load container \n" +
                                      "5. Unload container \n" +
                                      "6. Add container to a ship \n" +
                                      "7. Add list of containers to a ship \n" +
                                      "8. Swap containers on a ship \n" +
                                      "9. Remove container from a ship \n" +
                                      "10. Exit the program");
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
                        case 4:
                            LoadContainer();
                            break;
                        case 5:
                            UnloadContainer();
                            break;
                        case 6:
                            AddContainer();
                            break;
                        case 7:
                            AddContainers();
                            break;
                        case 8:
                            SwapContainers();
                            break;
                        case 9:
                            RemoveContainer();
                            break;
                        case 10:
                            stillWork = false;
                            break;
                        default:
                            Console.WriteLine("Try again!");
                            break;
                    }
                }
            }
        }
        catch (Exception e) 
        {
            Console.WriteLine(e.Message);
        }
    }
    
    private static void RemoveContainer()
    {
        Console.WriteLine("Selectt the container ship to remove a container from by entering its index:");
        Console.WriteLine(PrintList(_containerShips));
        int ship = Convert.ToInt32(Console.ReadLine());

        Console.WriteLine("Enter the serial number of the container to be removed:");
        string serialNumber = Console.ReadLine();

        var containerToRemove = _containerShips[ship].Containers.FirstOrDefault(c => c?.SerialNumber == serialNumber);
        if (containerToRemove != null)
        {
            _containerShips[ship].RemoveContainer(containerToRemove);
            Console.WriteLine("Container removed successfully.");
        }
        else
        {
            Console.WriteLine("Container not found on the selected ship.");
        }
    }
    
    private static void AddContainers()
    {
        Console.WriteLine("Select the container ship to add containers to by entering its index:");
        Console.WriteLine(PrintList(_containerShips));
        
        int ship = Convert.ToInt32(Console.ReadLine());
        Console.WriteLine("Enter the indices of the containers to add, separated by spaces:");
        
        var containerIndices = Console.ReadLine().Split(' ').Select(int.Parse);
        List<Container> containersToAdd = containerIndices.Select(index => _containers[index]).ToList();
        
        _containerShips[ship].AddContainers(containersToAdd);
        Console.WriteLine("Containers added to the ship.");
    }

    private static void SwapContainers()
    {
        Console.WriteLine("Select the container ship to swap containers on by entering its index:");
        Console.WriteLine(PrintList(_containerShips));
        int ship = Convert.ToInt32(Console.ReadLine());
        
        Console.WriteLine("Enter the serial number of the container to be swapped:");
        string serialNumber = Console.ReadLine();
        
        Console.WriteLine("Select the replacement container by entering its index:");
        Console.WriteLine(PrintList(_containers));
        
        int replace = Convert.ToInt32(Console.ReadLine());
        _containerShips[ship].SwapContainer(_containers[replace], serialNumber);
        Console.WriteLine("Container swapped successfully.");
    }


    private static void AddContainer()
    {
        Console.WriteLine("Select which container ship to load a container to by entering it's index");
        Console.WriteLine(PrintList(_containerShips));
        int ship = Convert.ToInt32(Console.ReadLine());
        Console.WriteLine("Select which container is to be added");
        Console.WriteLine(PrintList(_containers));
        int cont = Convert.ToInt32(Console.ReadLine());
        if (!_containerShips[ship].Containers.Contains(_containers[cont]))
        {
            _containerShips[ship].AddContainer(_containers[cont]);
        }
        else
        {
            Console.WriteLine("Container is already on the ship!");
        }
    }

    private static void UnloadContainer()
    {
        Console.WriteLine("Select which container to load by entering it's index");
        Console.WriteLine(PrintList(_containers));
        int cont = Convert.ToInt32(Console.ReadLine());
        _containers[cont].Unload();
        Console.WriteLine("Container unloaded!");
    }

    private static void LoadContainer()
    {
        Console.WriteLine("Select which container to load by entering it's index");
        Console.WriteLine(PrintList(_containers));
        int cont = Convert.ToInt32(Console.ReadLine());
        Console.WriteLine("Enter total weight of the cargo");
        double weight = Convert.ToDouble(Console.ReadLine());
        _containers[cont].LoadCargo(weight);
        Console.WriteLine("Container loaded! total cargo weight: " + _containers[cont].CargoWeight);
    }

    private static void RemoveContainerShip()
    {
        Console.WriteLine("Select which container ship should be deleted by entering it's index");
        Console.WriteLine(PrintList(_containerShips));
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

    private static void CreateContainer()
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
                          "weight, height, depth, maximumLoad, internalTemp, " +
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

    private static string  PrintList<T>(List<T> list)
    {
        return String.Join(',', list);
    }
}