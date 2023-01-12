namespace DesignPatterns;

public interface IPizza
{
    public void Prepare();
}

public class CheesePizza : IPizza
{
    public void Prepare()
    {
        
    }
}

public class MeatPizza : IPizza
{
    public void Prepare()
    {
        
    }
}

public class ShitPizza : IPizza
{
    public void Prepare()
    {
        
    }
}

public interface ISimplePizzaFactory
{
    public abstract IPizza CreatePizza(string type);
}

public class CoolPizzaFactory : ISimplePizzaFactory
{
    public  IPizza CreatePizza(string type)
    {
        IPizza pizza;

        if (type == "Cheese")
        {
            pizza = new CheesePizza();
        }
        else
        {
            pizza = new MeatPizza();
        }

        return pizza;
    }
}

public class ShitPizzaFactory : ISimplePizzaFactory
{
    public IPizza CreatePizza(string type)
    {
        IPizza pizza = new ShitPizza();

        return pizza;
    }
}

public abstract class PizzaStore
{
    private ISimplePizzaFactory _factory;

    public PizzaStore(ISimplePizzaFactory factory)
    {
        _factory = factory;
    }

    public IPizza OrderPizza(string type)
    {

        IPizza pizza = _factory.CreatePizza(type);
        pizza.Prepare();

        return pizza;
    }
}

public class ChelPizza : PizzaStore
{
    public ChelPizza(ISimplePizzaFactory factory) : base(factory)
    {
        
    }

    public IPizza OrderPizza(string type)
    {
        return new ShitPizza();
    }
}

