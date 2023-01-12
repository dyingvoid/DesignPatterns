using System.Xml.Serialization;

namespace DesignPatterns;

public class Drink
{
    public double Cost { get; set; }
    public string Name { get; set; }
}

public abstract class Beverage
{
    protected string _description;

    public string GetDescription()
    {
        return _description;
    }
    
    public abstract double Cost();
}

public abstract class CondimentDecorator : Beverage
{
    public Beverage Beverage { get; set; }
    public CondimentDecorator(Beverage beverage)
    {
        Beverage = beverage;
    }
    public new abstract string GetDescription();
}

public class Espresso : Beverage
{
    public Espresso()
    {
        _description = "Espresso";
    }

    public override double Cost()
    {
        return 1.99;
    }
}

public class HouseBlend : Beverage
{
    public HouseBlend()
    {
        _description = "HouseBlend";
    }

    public override double Cost()
    {
        return 2.99;
    }
}

public class Mocha : CondimentDecorator
{
    public Mocha(Beverage beverage) : base(beverage)
    {
        
    }

    public override string GetDescription()
    {
        return Beverage.GetDescription() + ", Mocha";
    }

    public override double Cost()
    {
        return 0.59 + Beverage.Cost();
    }
}