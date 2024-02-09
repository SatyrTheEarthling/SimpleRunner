using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using Zenject;

/*
 * This class provide functionality to adding, store and remove instances of class that implements IModificator.
 * You can add several instances of the same modificator to list.
 * 
 * RegisterModificators() called in contructor - it looking for every non-abstract class that implements IModificator and add it to register. 
 * So there is no need to add new modificator to this class, only declare it.
 * 
 * There is methods AddModificator, RemoveModificator and IsHasModificator to act with list of modificators. 
 * 
 * Method Get<T>() provide access to list of modificators for provided Type.
 * For example Get<ISpeedModificator>() will return list of all actual modificators that implementing ISpeedModificator.
 * 
 * Also, this manager implements Zenject.ITickable and call OnUpdate for every ITickableModificator.
*/
public class ModificatorsManager: ITickable
{
    [Inject] private DiContainer _diContainer;

    /// <summary>
    /// Dictionary of all IModificator implementations available in assembly. Key is taken from const KEY from implementation.
    /// </summary>
    private Dictionary<string, Type> _modsRegistry = new Dictionary<string, Type>();

    private List<object> _instances = new List<object>();
    /// <summary>
    /// Dictionary that store every added IModificator instance that implement some Type.
    /// </summary>
    private Dictionary<Type, List<IModificator>> _mods = new Dictionary<Type, List<IModificator>>();
    /// <summary>
    /// Dictionary that store IModificator Types implemented by instance;
    /// </summary>
    private Dictionary<object, List<Type>> _instanceMods = new Dictionary<object, List<Type>>();

    public ModificatorsManager()
    {
        RegisterModificators();
    }

    public void Reset()
    {
        while (_instances.Count > 0)
            RemoveInstance(_instances[0]);
    }

    public void AddModificator(string key)
    {
        AddModificatorInternal(key);
    }

    public void RemoveModificator(string key)
    {
        if (!_modsRegistry.ContainsKey(key))
        {
            Debug.LogError($"There no modificators for key \"{key}\" in registry. Can't remove it");
            return;
        }

        var type = _modsRegistry[key];

        object instance = null;

        foreach (var inst in _instances)
        {
            if (inst.GetType() != type)
                continue;

            instance = inst;
            break;
        }

        if (instance == null)
        {
            Debug.LogError($"There no modificators for key \"{key}\". Can't remove it");
            return;
        }

        RemoveInstance(instance);
    }

    public List<T> Get<T>() where T : IModificator
    {
        var type = typeof(T);
        var result = new List<T>();

        if (!_mods.ContainsKey(type))
            return result;

        for (int i = 0; i < _mods[type].Count; i++)
        {
            result.Add((T)_mods[type][i]);
        }

        return result;
    }

    public bool IsHasModificator(string key)
    {
        var type = _modsRegistry[key];

        for (int i = 0; i < _instances.Count; i++)
        {
            if (_instances[i].GetType() == type)
                return true;
        }

        return false;
    }

    public void Tick()
    {
        var ticables = Get<ITickableModificator>();
        foreach(var ticable in ticables)
        {
            ticable.OnUpdate();
        }
    }

    private void AddModificatorInternal(string key)
    {
        if (!_modsRegistry.ContainsKey(key))
        {
            Debug.LogError($"There no modificators for key \"{key}\" in registry. Can't add it");
            return;
        }

        var type = _modsRegistry[key];
        var instance = type.GetConstructors()[0].Invoke(null);
        _diContainer.Inject(instance);

        var mod = (IModificator)instance;
        mod.OnAdd();

        _instances.Add(instance);
        _instanceMods.Add(instance, new List<Type>());

        foreach (var interfaceType in type.GetInterfaces())
        {
            if (interfaceType == typeof(IModificator))
            {
                continue;
            }

            if (!_mods.ContainsKey(interfaceType))
                _mods.Add(interfaceType, new List<IModificator>());

            _mods[interfaceType].Add(mod);
            _instanceMods[instance].Add(interfaceType);
        }
    }

    private void RemoveInstance(object instance)
    {
        var mod = (IModificator)instance;
        _instances.Remove(instance);

        foreach (var modType in _instanceMods[instance])
        {
            _mods[modType].Remove(mod);
        }

        _instanceMods.Remove(instance);

        mod.OnRemove();
    }

    private void RegisterModificators()
    {
        var assembly = Assembly.GetExecutingAssembly();

        var modificatorTypes = assembly.GetTypes()
            .Where(t => t.GetInterfaces().Contains(typeof(IModificator)) && !t.IsAbstract)
            .ToList();

        foreach (var type in modificatorTypes)
        {
            // Attempt to find the KEY field which is expected to be a public const string
            var keyField = type.GetField("KEY", BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy);

            if (keyField == null || !keyField.IsLiteral || keyField.IsInitOnly)
            {
                Debug.LogError($"Error: No public const string KEY found for modificator {type.Name}. Every IModificator must have a unique KEY.");
                continue;
            }

            var key = keyField.GetValue(null)?.ToString(); // null because it's a static field

            if (string.IsNullOrEmpty(key))
            {
                Debug.LogError($"Error: The KEY for {type.Name} is null or empty.");
                continue;
            }

            // Check if key already exists to prevent duplicates
            if (_modsRegistry.ContainsKey(key))
            {
                Debug.LogError($"Error: Duplicate KEY '{key}' found in {type.Name}. KEY must be unique for each modificator.");
                continue;
            }

            _modsRegistry.Add(key, type);
            Debug.Log($"Modificator {type.Name} registered");
        }
    }
}
