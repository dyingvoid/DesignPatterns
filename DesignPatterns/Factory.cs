namespace DesignPatterns;

public abstract class Cheese
{
    public abstract string Name { get; }
}

public class GoodCheese : Cheese
{
    public override string Name => "Good";
}

public class BadCheese : Cheese
{
    public override string Name => "Bad";
}

public interface IPizzaIngredientFactory
{
    public Cheese CreateCheese();
}

public class ChelPizzaIngredientFactory : IPizzaIngredientFactory
{
    public Cheese CreateCheese()
    {
        return new GoodCheese();
    }
}

public class MosPizzaIngredientFactory : IPizzaIngredientFactory
{
    public Cheese CreateCheese()
    {
        return new BadCheese();
    }
}

public abstract class Pizza
{
    protected IPizzaIngredientFactory _factory;
    protected string _name;
    protected Cheese _cheese;
    protected bool _isOverbaked;

    public Pizza(IPizzaIngredientFactory factory)
    {
        _factory = factory;
    }

    public void Cut()
    {
        Console.WriteLine("Cutting.");
    }

    public abstract void Prepare();
}

public class ShitPizza : Pizza
{
    public ShitPizza(IPizzaIngredientFactory factory) : base(factory)
    {
        
    }

    public override void Prepare()
    {
        Console.WriteLine("Preparing shit pizza.");
        _cheese = _factory.CreateCheese();
        _isOverbaked = true;
    }
}

public class CheesePizza : Pizza
{
    public CheesePizza(IPizzaIngredientFactory factory) : base(factory)
    {
        
    }
    
    public override void Prepare()
    {
        Console.WriteLine("Preparing cheese pizza.");
        _cheese = _factory.CreateCheese();
        _isOverbaked = false;
    }
}

public abstract class SimplePizzaFactory
{
    protected IPizzaIngredientFactory _ingredientFactory;

    public SimplePizzaFactory(IPizzaIngredientFactory ingredientFactory)
    {
        _ingredientFactory = ingredientFactory;
    }

    public abstract Pizza CreatePizza(string type);
}

public class ShitPizzaFactory : SimplePizzaFactory
{
    public ShitPizzaFactory(IPizzaIngredientFactory ingredientFactory) : base(ingredientFactory)
    {
        
    }

    public override Pizza CreatePizza(string type)
    {
        if (type == "Cheese")
            return new CheesePizza(_ingredientFactory);

        return new ShitPizza(_ingredientFactory);
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
        pizza.Cut();

        return pizza;
    }
}

public abstract class LocalizedPizzaStore
{
    protected IPizzaIngredientFactory _ingredientFactory;

    public LocalizedPizzaStore(IPizzaIngredientFactory ingredientFactory)
    {
        _ingredientFactory = ingredientFactory;
    }
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
    public ChelPizzaStore(IPizzaIngredientFactory ingredientFactory) : base(ingredientFactory)
    {
        
    }
    
    protected override Pizza CreatePizza(string type)
    {
        return new CheesePizza(_ingredientFactory);
    }
}

public class MosPizzaStore : LocalizedPizzaStore
{
    public MosPizzaStore(IPizzaIngredientFactory ingredientFactory) : base(ingredientFactory)
    {
        
    }
    
    protected override Pizza CreatePizza(string type)
    {
        return new ShitPizza(_ingredientFactory);
    }
}
