namespace DesignPatterns;

public class Program
{
    public static void Main()
    {
        var shop = new PizzaStore(new ShitPizzaFactory(new MosPizzaIngredientFactory()));
        var smt = shop.OrderPizza("asd");

        var localShop = new MosPizzaStore(new MosPizzaIngredientFactory());
        var smt2 = localShop.OrderPizza("asd");
    }
}