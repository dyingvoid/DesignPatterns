namespace DesignPatterns;

public class Program
{
    public static void Main()
    {
        var factory = new SimplePizzaFactory();
        var factory1 = new SimplePizzaFactory();

        var mosPizza = new PizzaStore(factory);
        var chelPizza = new PizzaStore(factory1);
    }
}