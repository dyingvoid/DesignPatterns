namespace DesignPatterns;

public class Singleton
{
    private static Singleton _instance;

    private Singleton()
    {
        
    }

    public static Singleton getInstance()
    {
        if (_instance == null)
        {
            _instance = new Singleton();
        }

        return _instance;
    }
}

public class PC
{
    public OS? OS { get; set; }

    public void Launch(string osName)
    {
        OS = OS.GetInstance(osName);
        Console.WriteLine(OS.Name);
    }
}

public class OS
{
    private static OS? _instance;
    
    public string? Name { get; set; }

    private OS(string name)
    {
        Name = name;
    }

    public static OS GetInstance(string name)
    {
        if (_instance == null)
            _instance = new OS(name);

        return _instance;
    }
}