using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace MGChat.ECS
{
    public class Manager
    {
        public static Manager Instance { get; } = new Manager();

        private Random _random;
        private List<int> _entities;
        private Dictionary<Type, List<Component>> _components;

        public Dictionary<Type, List<Component>> Components => _components;

        private Manager()
        {
            _random = new Random();
            _entities = new List<int>();
            _components = new Dictionary<Type, List<Component>>();
        }

        public int CreateEntity()
        {
            int newNumber = _random.Next();
            if (_entities.Contains(newNumber))
            {
                return CreateEntity();
            }
            _entities.Add(newNumber);
            return newNumber;
        }

        public bool DestroyEntity(int entity)
        {
            for (int i = _components.Count - 1; i >= 0; i--)
            {
                KeyValuePair<Type, List<Component>> KeyValue = _components.ElementAt(i);
                for (int j = KeyValue.Value.Count - 1; j >= 0; j--)
                {
                    if (KeyValue.Value[j].Parent == entity)
                    {
                        DeregisterComponent(KeyValue.Value[j]);
                        if (j > 0)
                        {
                            KeyValue.Value.RemoveAt(j);
                        }
                        else
                        {
                            _components.Remove(KeyValue.Key);
                        }
                    }
                }
            }
            return true;
        }

        public bool RegisterComponent(Component component)
        {
            Debug.WriteLine($"Registering Component {component}");
            Type currType = component.GetType();

            if (component.Registered)
            {
                Debug.WriteLine($"{component} is already Registered.");
                return false;
            }
            
            // Check that Entity actually exists
            if (!_entities.Contains(component.Parent))
            {
                Debug.WriteLine($"Missing Entity {component.Parent}");
                return false;
            }
            
            // Check if Component entry in Dict
            if (!_components.ContainsKey(currType))
            {
                Debug.WriteLine($"Missing Component {currType} in Dictionary. Adding...");
                _components.Add(currType, new List<Component>());
                _components[currType].Add(component);
                if (!_components.ContainsKey(currType))
                {
                    Debug.WriteLine($"Unexpected Error. Unable to register Component {currType}");
                    return false;
                }
                return true;
            }

            return true;
        }

        public bool DeregisterComponent(Component component)
        {
            Debug.WriteLine($"Deregistering Component {component}");
            Type currType = component.GetType();

            // Prevent duplicate registrations (not of the same component type, only of the same exact component)
            if (!component.Registered)
            {
                Debug.WriteLine($"{component} is not Registered");
                return false;
            }
            
            // Check that Entity actually exists
            if (!_entities.Contains(component.Parent))
            {
                Debug.WriteLine($"Missing Entity {component.Parent}");
                return false;
            }

            // Check if component entry in dict
            if (!_components.ContainsKey(currType))
            {
                Debug.WriteLine($"{currType} doesn't seem to exist. Something's gone wrong");
                return false;
            }

            _components[currType].Remove(component);
            return true;
        }

        public List<Component> Fetch<T>()
        {
            var list = _components[typeof(T)];
            return list;
        } 
    }
}