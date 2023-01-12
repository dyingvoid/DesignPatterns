namespace DesignPatterns;

public abstract class Pizza
{
    public abstract string Name { get; }

    public void Prepare()
    {
        
    }
}

public class ShitPizza : Pizza
{ 
    public override string Name => "Shit";
}

public class CheesePizza : Pizza
{
    public override string Name => "Cheese";
}

public class SimplePizzaFactory
{
    public Pizza CreatePizza(string type)
    {
        if (type == "Cheese")
            return new CheesePizza();
        
        return new ShitPizza();
    }
}

public class PizzaStore
{
    private SimplePizzaFactory _factory;

    public PizzaStore(SimplePizzaFactory factory)
    {
        _factory = factory;
    }

    public Pizza OrderPizza(string type)
    {
        var pizza = _factory.CreatePizza(type);
        pizza.Prepare();

        return pizza;
    }
}

public abstract class LocalizedPizzaStore
{
    public Pizza OrderPizza(string type)
    {
        Pizza pizza = CreatePizza(type);
        pizza.Prepare();

        return pizza;
    }

    protected abstract Pizza CreatePizza(string type);
}

public class ChelPizzaStore : LocalizedPizzaStore
{
    protected override Pizza CreatePizza(string type)
    {
        return new CheesePizza();
    }
}

public class MosPizzaStore : LocalizedPizzaStore
{
    protected override Pizza CreatePizza(string type)
    {
        return new ShitPizza();
    }
}
