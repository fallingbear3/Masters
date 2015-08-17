using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Assets.Scripts.utils
{
    internal class Utils
    {
        public static T loadResource<T>(string name) where T : Object
        {
            return (T) Object.Instantiate(Resources.Load<T>(name));
        }

        public static Dictionary<string, Sprite[]> dict = new Dictionary<string, Sprite[]>();

        public static Sprite loadSprite(string atlasName, string spriteName)
        {
            Sprite[] textures = Resources.LoadAll<Sprite>(atlasName);
            string[] names = new string[textures.Length];

            for (int ii = 0; ii < names.Length; ii++)
            {
                names[ii] = textures[ii].name;
            }

            return  textures[Array.IndexOf(names, spriteName)];
        }
    }
}