using Ecng.Common;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Diagnostics;
using System.Linq;

namespace fx.Configuration
{
    public static class ConfigManagerEx
    {
        private static readonly Dictionary<Type, ConfigurationSection> _sections = new Dictionary<Type, ConfigurationSection>();
        private static readonly Dictionary<Type, ConfigurationSectionGroup> _sectionGroups = new Dictionary<Type, ConfigurationSectionGroup>();
        private static readonly SyncObject _sync = new SyncObject();
        private static readonly Dictionary<Type, Dictionary<string, object>> _services = new Dictionary<Type, Dictionary<string, object>>();
        private static readonly Dictionary<Type, List<Action<object>>> _subscribers = new Dictionary<Type, List<Action<object>>>();

        static ConfigManagerEx()
        {
            try
            {
                InnerConfig = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            }
            catch (Exception ex)
            {
                Trace.WriteLine( ex );
                Console.WriteLine( ex );
            }
            if ( InnerConfig == null)
                return;
            Trace.WriteLine("ConfigManager FilePath=" + InnerConfig.FilePath);
            InitSections( InnerConfig.Sections);
            InitSectionGroups( InnerConfig.SectionGroups);

            static void InitSections(ConfigurationSectionCollection sections)
            {
                try
                {
                    foreach (ConfigurationSection section in (NameObjectCollectionBase)sections)
                    {
                        if (!_sections.ContainsKey(section.GetType()))
                            _sections.Add(section.GetType(), section);
                    }
                }
                catch (Exception ex)
                {
                    Trace.WriteLine( ex );
                }
            }

            static void InitSectionGroups(ConfigurationSectionGroupCollection groups)
            {
                foreach (ConfigurationSectionGroup group in (NameObjectCollectionBase)groups)
                {
                    if (!_sectionGroups.ContainsKey(group.GetType()))
                        _sectionGroups.Add(group.GetType(), group);
                    InitSections(group.Sections);
                    InitSectionGroups(group.SectionGroups);
                }
            }
        }

        public static System.Configuration.Configuration InnerConfig { get; }

        public static T GetSection<T>() where T : ConfigurationSection => (T)GetSection( typeof(T));

        public static ConfigurationSection GetSection(Type sectionType) => !_sections.ContainsKey(sectionType) ? null : _sections[sectionType];

        public static T GetSection<T>(string sectionName) where T : ConfigurationSection => (T)GetSection( sectionName);

        public static ConfigurationSection GetSection(string sectionName) => InnerConfig.GetSection(sectionName);

        public static T GetSectionByType<T>() where T : ConfigurationSection => (T)GetSectionByType( typeof(T));

        public static ConfigurationSection GetSectionByType(Type type) => GetSection( (type.GetAttribute<ConfigSectionAttribute>() ?? throw new ArgumentException("Type '{0}' isn't marked ConfigSectionAttribute.".Put( type ) )).SectionType);

        public static T GetGroup<T>() where T : ConfigurationSectionGroup => (T)GetGroup( typeof(T));

        public static ConfigurationSectionGroup GetGroup(Type sectionGroupType) => !_sectionGroups.ContainsKey(sectionGroupType) ? null : _sectionGroups[sectionGroupType];

        public static T GetGroup<T>(string sectionName) where T : ConfigurationSectionGroup => (T)GetGroup( sectionName);

        public static ConfigurationSectionGroup GetGroup(string sectionName) => InnerConfig.GetSectionGroup(sectionName);

        public static T TryGet<T>(string name, T defaultValue = default)
        {
            try
            {
                string str = AppSettings.Get(name);
                if (!str.IsEmpty())
                    return str.To<T>();
            }
            catch (Exception ex)
            {
                Trace.WriteLine( ex );
            }
            return defaultValue;
        }

        public static NameValueCollection AppSettings => ConfigurationManager.AppSettings;

        public static event Action<Type, object> ServiceRegistered;

        public static void SubscribeOnRegister<T>(Action<T> registered)
        {
            if (registered == null)
                throw new ArgumentNullException(nameof(registered));
            List<Action<object>> actionList;
            if (!_subscribers.TryGetValue(typeof(T), out actionList))
            {
                actionList = new List<Action<object>>();
                _subscribers.Add(typeof(T), actionList);
            }
            actionList.Add( svc => registered( ( T )svc ) );
        }

        private static void RaiseServiceRegistered(Type type, object service)
        {
            Action<Type, object> serviceRegistered = ServiceRegistered;
            if (serviceRegistered != null)
                serviceRegistered(type, service);
            List<Action<object>> actionList;
            if (!_subscribers.TryGetValue(type, out actionList))
                return;
            foreach (Action<object> action in actionList)
                action(service);
        }

        private static Dictionary<string, object> GetDict<T>() => GetDict( typeof(T));

        private static Dictionary<string, object> GetDict(Type type)
        {
            Dictionary<string, object> dictionary1;
            if ( _services.TryGetValue(type, out dictionary1))
                return dictionary1;
            Dictionary<string, object> dictionary2 = new Dictionary<string, object>( StringComparer.InvariantCultureIgnoreCase );
            _services.Add(type, dictionary2);
            return dictionary2;
        }

        public static void RegisterService<T>(T service) => RegisterService( typeof(T).AssemblyQualifiedName, service);

        public static void RegisterService<T>(string name, T service)
        {
            lock ( _sync )
                GetDict<T>()[name] = service;
            RaiseServiceRegistered( typeof(T), service );
        }

        public static bool IsServiceRegistered<T>() => IsServiceRegistered<T>( typeof(T).AssemblyQualifiedName);

        public static bool IsServiceRegistered<T>(string name)
        {
            lock ( _sync )
                return GetDict<T>().ContainsKey(name);
        }

        public static T TryGetService<T>() => !IsServiceRegistered<T>() ? default(T) : GetService<T>();

        public static void TryRegisterService<T>(T service)
        {
            if ( IsServiceRegistered<T>())
                return;
            RegisterService( service);
        }

        public static T GetService<T>() => GetService<T>( typeof(T).AssemblyQualifiedName);

        public static T GetService<T>(string name)
        {
            object obj = null;
            lock ( _sync )
            {
                Dictionary<string, object> dict = GetDict<T>();
                if (dict.TryGetValue(name, out obj))
                    return (T)obj;
                if (obj != null)
                {
                    if (!dict.ContainsKey(name))
                        dict.Add(name, obj);
                }
            }
            return (T)obj;
        }

        public static IEnumerable<T> GetServices<T>()
        {
            IEnumerable<T> array;
            lock ( _sync )
                array = GetDict<T>().Values.Cast<T>().ToArray();
            return array.Distinct();
        }
    }
}
