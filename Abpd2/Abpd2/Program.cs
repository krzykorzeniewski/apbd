using Abpd2.Containers;

namespace Abpd2;

public class Program
{
    public static void Main(string[] args)
    {
        CreateContainer();
    }

    public static Container CreateContainer()
    {
        Console.WriteLine("Enter container type (L - luqid, G - gas, R - refrigerated)");
        char type = Convert.ToChar(Console.ReadLine() ?? string.Empty);
        switch (type)
        {
            case 'L':
                return CreateLiquidContainer();
                break;
            case 'G':
                return CreateGasTankContainer();
                break;
            case 'R':
                return CreateRefrigeratedContainer();
                break;
            default:
                Console.WriteLine("Try again!");
                return null;
                break;
        }
    }

    private static Container CreateRefrigeratedContainer()
    {
        Console.WriteLine("Enter parameters (line by line):  " +
                          "weight, cargoWeight, height, depth, maximumLoad, type, internalTemp" +
                          "typeOfProduct");
        double weight, cargoWeight, height, depth, maximumLoad, internalTemperature;
        char type;
        string typeOfProduct;
        weight = Convert.ToDouble(Console.ReadLine());
        cargoWeight = Convert.ToDouble(Console.ReadLine());
        height = Convert.ToDouble(Console.ReadLine());
        depth = Convert.ToDouble(Console.ReadLine());
        maximumLoad = Convert.ToDouble(Console.ReadLine());
        internalTemperature = Convert.ToDouble(Console.ReadLine());
        type = Convert.ToChar(Console.ReadLine());
        typeOfProduct = Console.ReadLine();
        Container container = new RefrigeratedContainer(weight, cargoWeight, height, depth, maximumLoad, type, 
        internalTemperature, typeOfProduct);
        return container;
    }

    private static Container CreateGasTankContainer()
    {
        Console.WriteLine("Enter parameters (line by line):  " +
                          "weight, cargoWeight, height, depth, maximumLoad, type, internalPressure");
        double weight, cargoWeight, height, depth, maximumLoad, internalPressure;
        char type;
        weight = Convert.ToDouble(Console.ReadLine());
        cargoWeight = Convert.ToDouble(Console.ReadLine());
        height = Convert.ToDouble(Console.ReadLine());
        depth = Convert.ToDouble(Console.ReadLine());
        maximumLoad = Convert.ToDouble(Console.ReadLine());
        type = Convert.ToChar(Console.ReadLine());
        internalPressure = Convert.ToDouble(Console.ReadLine());
        Container container =
            new GasTankContainer(weight, cargoWeight, height, depth, maximumLoad, type, internalPressure);
        return container;
    }

    private static Container CreateLiquidContainer()
    {
        Console.WriteLine("Enter parameters (line by line):  " +
                          "weight, cargoWeight, height, depth, maximumLoad, type, hazardousCargo (True or False)");
        double weight, cargoWeight, height, depth, maximumLoad, internalPressure;
        char type;
        bool hazardousCargo;
        weight = Convert.ToDouble(Console.ReadLine());
        cargoWeight = Convert.ToDouble(Console.ReadLine());
        height = Convert.ToDouble(Console.ReadLine());
        depth = Convert.ToDouble(Console.ReadLine());
        maximumLoad = Convert.ToDouble(Console.ReadLine());
        type = Convert.ToChar(Console.ReadLine());
        hazardousCargo = Convert.ToBoolean(Console.ReadLine());
        Container container =
            new LiquidTankContainer(weight, cargoWeight, height, depth, maximumLoad, type, hazardousCargo);
        return container;
    }
}