using System.Text.Json;

namespace DesignPatterns;

public interface IWeatherData
{
    public double Temperature { get; }
}

public interface IDisplay
{
    public void DisplayData();
}

public class WeatherData : IWeatherData
{
    private double _temperature;
    private string _city;

    public WeatherData(double temperature, string city)
    {
        _temperature = temperature;
        _city = city;
    }

    public double Temperature => _temperature;
    public string City => _city;
}

public class WeatherAggregator : IObservable<WeatherData>
{
    private readonly List<IObserver<WeatherData>> _observers;
    private List<WeatherData> _weathers;

    public WeatherAggregator()
    {
        _observers = new List<IObserver<WeatherData>>();
        _weathers = new List<WeatherData>();
    }

    public IDisposable Subscribe(IObserver<WeatherData> observer)
    {
        if(!_observers.Contains(observer))
            _observers.Add(observer);

        foreach (var weather in _weathers)
        {
            observer.OnNext(weather);
        }

        return new Unsubscriber<WeatherData>(_observers, observer);
    }

    public void WeatherStatus(WeatherData weather)
    {
        if (!_weathers.Contains(weather))
        {
            _weathers.Add(weather);

            foreach (var observer in _observers)
            {
                observer.OnNext(weather);
            }
        }
    }
}

public class Unsubscriber<T> : IDisposable
{
    private List<IObserver<T>> _observers;
    private IObserver<T> _observer;

    public Unsubscriber(List<IObserver<T>> observers, IObserver<T> observer)
    {
        _observers = observers;
        _observer = observer;
    }

    public void Dispose()
    {
        if (_observers.Contains(_observer))
            _observers.Remove(_observer);
    }
}

public abstract class Display : IDisplay, IObserver<WeatherData>
{
    protected IDisposable _cancellation;
    protected List<string> _weatherInfos = new List<string>();
    public string Name { get; set; }

    public Display(string name)
    {
        Name = name;
    }

    public abstract void DisplayData();

    public void Subscribe(IObservable<WeatherData> weatherAggregator)
    {
        _cancellation = weatherAggregator.Subscribe(this);
    }

    public void Unsubscribe()
    {
        _cancellation.Dispose();
    }

    public void OnCompleted()
    {
        _weatherInfos.Clear();
    }

    public void OnError(Exception error)
    {
        // No implementation
    }

    public void OnNext(WeatherData value)
    {
        string weatherInfo = $"{value.City}: {value.Temperature}";
        
        if(!_weatherInfos.Contains(weatherInfo))
            _weatherInfos.Add(weatherInfo);
        
        _weatherInfos.Sort();
        
        DisplayData();
    }
}

public class ConcreteDisplay : Display
{
    public ConcreteDisplay(string name) : base(name)
    {
        
    }

    public override void DisplayData()
    {
        foreach (var info in _weatherInfos)
        {
            Console.WriteLine(info);
        }
    }
}