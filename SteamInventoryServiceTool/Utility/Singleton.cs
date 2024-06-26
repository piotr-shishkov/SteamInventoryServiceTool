using System;

namespace SteamInventoryServiceTool.Utility;

public abstract class Singleton<T> where T : class
{
    private static readonly Lazy<T> _lazyInstance = new Lazy<T>(CreateInstanceOfClass);

    public static T Instance => _lazyInstance.Value;
    
    private static T CreateInstanceOfClass()
    {
        return Activator.CreateInstance(typeof(T), true) as T ?? throw new InvalidOperationException();
    }
}