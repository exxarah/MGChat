using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using Newtonsoft.Json;

namespace MGChat.ECS
{
    public class Manager
    {
        public static Manager Instance { get; } = new Manager();

        private Random _random;
        private List<int> _entities;
        private Dictionary<Type, LinkedList<Component>> _components;

        public Dictionary<Type, LinkedList<Component>> Components => _components;
        public int EntitiesCount => _entities.Count;

        private Manager()
        {
            _random = new Random();
            _entities = new List<int>();
            _components = new Dictionary<Type, LinkedList<Component>>();
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
            var components = Fetch(entity);

            foreach (var component in components)
            {
                DeregisterComponent(component);
            }

            _entities.Remove(entity);
            return true;
        }

        public bool RegisterComponent(Component component)
        {
            if (component.Registered)
            {
                return false;
            }
            
            // Check that Entity actually exists
            if (!_entities.Contains(component.Parent))
            {
                return false;
            }
            
            Type currType = component.GetType();

            // Check if Component entry in Dict
            if (!_components.ContainsKey(currType))
            {
                _components.Add(currType, new LinkedList<Component>());
            }

            var node = _components[currType].First;
            while (node != null)
            {
                if (node.Value.Parent > component.Parent)
                {
                    _components[currType].AddBefore(node, component);
                    break;
                }

                node = node.Next;
            }
            _components[currType].AddLast(component);
            return true;
        }

        public bool DeregisterComponent(Component component)
        {
            Type currType = component.GetType();

            // Prevent duplicate registrations (not of the same component type, only of the same exact component)
            if (!component.Registered)
            {
                return false;
            }
            
            // Check that Entity actually exists
            if (!_entities.Contains(component.Parent))
            {
                return false;
            }

            // Check if component entry in dict
            if (!_components.ContainsKey(currType))
            {
                return false;
            }

            _components[currType].Remove(component);
            return true;
        }

        public void Clear()
        {
            _components = new Dictionary<Type, LinkedList<Component>>();
            _entities = new List<int>();
        }

        public List<Component> Fetch(int entity)
        {
            List<Component> list = new List<Component>();

            foreach (var kvp in _components)
            {
                foreach (var component in kvp.Value)
                {
                    if (component.Parent == entity)
                    {
                        list.Add(component);
                        break;
                    }
                }
            }
            return list;
        }

        public List<Component> Fetch<T>(int entity)
        {
            List<Component> list = Query<T>();
            var listFinal = list.Where(comp => comp.Parent == entity).ToList();
            return listFinal;
        }
        
        public List<Component> FetchAny<T>(int entity)
        {
            List<Component> list = Fetch(entity);
            var listFinal = list.Where(comp => comp.GetType() == typeof(T) || comp.GetType().IsSubclassOf(typeof(T))).ToList();
            return listFinal;
        }

        public List<Component> Query<T>()
        {
            if (!_components.ContainsKey(typeof(T))) { return null; }
            
            var list = _components[typeof(T)].ToList();
            return list;
        }

        public List<Component> QueryAny<T>()
        {
            List<Component> list = new List<Component>();
            foreach (var kvp in _components)
            {
                if (kvp.Key == typeof(T) || kvp.Key.IsSubclassOf(typeof(T)))
                {
                    list.AddRange(kvp.Value);
                }
            }

            return list;
        }

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

            //var listCombined = list1.Concat(list2).ToList();
            //var finalList = listCombined.GroupBy(component => component.Parent).Where(group => group.Count() >= 2).Select(group => group.ToList()).ToList();

            /*
            List<List<Component>> finalList = new List<List<Component>>();
            foreach (var c1 in list1)
            {
                foreach (var c2 in list2)
                {
                    if (c1.Parent == c2.Parent)
                    {
                        // Check for Duplicates
                        bool exists = false;
                        foreach (var list in finalList)
                        {
                            if (list[0].Parent == c1.Parent)
                            {
                                exists = true;
                                break;
                            }
                        }

                        if (!exists)
                        {
                            finalList.Add(new List<Component>(){c1, c2});
                        }
                    }
                }
            }
            */

            List<List<Component>> finalList = new List<List<Component>>();
            var node1 = list1.First;
            var node2 = list2.First;
            while (node1 != null && node2 != null)
            {
                if (node1.Value.Parent == node2.Value.Parent)
                {
                    finalList.Add(new List<Component>(){node1.Value, node2.Value});
                    node1 = node1.Next;
                    node2 = node2.Next;
                } else if (node1.Value.Parent > node2.Value.Parent)
                {
                    node2 = node2.Next;
                }
                else
                {
                    node1 = node1.Next;
                }
            }
            
            return finalList;
        }

        public List<Component> Fetch<T1, T2>(int entity)
        {
            var list = Query<T1, T2>();
            var listFinal = list.Where(comp => comp[0].Parent == entity).ToList();
            return listFinal[0];
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
             
            // var listCombined = list1.Concat(list2).Concat(list3).ToList();
            // var finalList = listCombined.GroupBy(component => component.Parent).Where(group => group.Count() == 3).Select(group => group.ToList()).ToList();
            
            List<List<Component>> finalList = new List<List<Component>>();
            var node1 = list1.First;
            var node2 = list2.First;
            var node3 = list3.First;
            while (node1 != null && node2 != null && node3 != null)
            {
                if (node1.Value.Parent == node2.Value.Parent  && node2.Value.Parent == node3.Value.Parent)
                {
                    finalList.Add(new List<Component>(){node1.Value, node2.Value, node3.Value});
                    node1 = node1.Next;
                    node2 = node2.Next;
                    node3 = node3.Next;
                    continue;
                }

                int smallestParent = Math.Min(Math.Min(node1.Value.Parent, node2.Value.Parent), node3.Value.Parent);
                if (node1.Value.Parent == smallestParent)
                {
                    node1 = node1.Next;
                }

                if (node2.Value.Parent == smallestParent)
                {
                    node2 = node2.Next;
                }

                if (node3.Value.Parent == smallestParent)
                {
                    node3 = node3.Next;
                }
            }
            
            return finalList;
        }
        
        public List<Component> Fetch<T1, T2, T3>(int entity)
        {
            var list = Query<T1, T2, T3>();
            var listFinal = list.Where(comp => comp[0].Parent == entity).ToList();
            return listFinal[0];
        }
    }
}