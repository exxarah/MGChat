using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Newtonsoft.Json;

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

        public int CreateEntity(string json)
        {
            int newEntity = CreateEntity();
            
            var components = JsonConvert.DeserializeObject<List<Component>>(json, new JsonSerializerSettings()
            {
                ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor,
                TypeNameHandling = TypeNameHandling.Auto
            });

            if (components != null)
                foreach (var component in components)
                {
                    Debug.WriteLine("Initializing Entity");
                    component.Init(newEntity);
                }

            return newEntity;
        }

        public string ExportEntity(int entity)
        {
            var components = Fetch(entity);
            string json = JsonConvert.SerializeObject(components, Formatting.Indented, new JsonSerializerSettings()
            {
                TypeNameHandling = TypeNameHandling.Auto
            });
            Debug.WriteLine(json);
            return json;
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

        public List<Component> Fetch(int entity)
        {
            List<Component> list = new List<Component>();
            
            for (int i = _components.Count - 1; i >= 0; i--)
            {
                KeyValuePair<Type, List<Component>> KeyValue = _components.ElementAt(i);
                for (int j = KeyValue.Value.Count - 1; j >= 0; j--)
                {
                    if (KeyValue.Value[j].Parent == entity)
                    {
                        list.Add(KeyValue.Value.ElementAt(j));
                        break;
                    }
                }
            }
            return list;
        }

        public List<Component> Query<T>()
        {
            if (!_components.ContainsKey(typeof(T))) { return null; }
            
            var list = _components[typeof(T)];
            return list;
        }

        // TODO: Unclear if it works on more than 2 (in a different function obviously. Needs testing
        // https://stackoverflow.com/questions/4488054/merge-two-or-more-lists-into-one-in-c-sharp-net
        // https://stackoverflow.com/a/2697280
        public List<List<Component>> Query<T1, T2>()
        {
            if (!_components.ContainsKey(typeof(T1)) || !_components.ContainsKey(typeof(T2)))
            {
                return null;
            }
            var list1 = _components[typeof(T1)];
            var list2 = _components[typeof(T2)];

            var listCombined = list1.Concat(list2).ToList();

            // var finalList = list1.Intersect(list2, new ComponentEqualityComparer()).ToList();

            var finalList = listCombined.GroupBy(component => component.Parent).Select(group => group.ToList()).ToList();

            return finalList;
        }
        
        public List<List<Component>> Query<T1, T2, T3>()
        {
            if (!_components.ContainsKey(typeof(T1)) || !_components.ContainsKey(typeof(T2)) || !_components.ContainsKey(typeof(T3)))
            {
                return null;
            }
            var list1 = _components[typeof(T1)];
            var list2 = _components[typeof(T2)];
            var list3 = _components[typeof(T3)];
             
            var listCombined = list1.Concat(list2).Concat(list3).ToList();

            // var finalList = list1.Intersect(list2, new ComponentEqualityComparer()).ToList();

            var finalList = listCombined.GroupBy(component => component.Parent).Select(group => group.ToList()).ToList();

            return finalList;
        }
    }
}