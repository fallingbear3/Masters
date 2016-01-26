using System.Linq;

namespace DefaultNamespace
{
    public class Fighter
    {
        public enum Type
        {
            Janacek,
            Wagner,
            Martinu,
            Strauss,
            Rocc
        }

        public static Type[] Players = {Type.Janacek, Type.Wagner, Type.Martinu, Type.Strauss};

        public static bool HasNext(Type type)
        {
            var result = Players.Select((type1, i) => new {type1, i}).FirstOrDefault(arg1 => arg1.type1 == type);
            return result.i + 1 < Players.Length;
        }

        public static Type Next(Type type)
        {
            var result = Players.Select((type1, i) => new {type1, i}).FirstOrDefault(arg1 => arg1.type1 == type);
            return Players[result.i + 1];
        }
    }
}