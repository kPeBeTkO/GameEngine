using System;
using System.Collections.Generic;
using System.Text;

namespace GameEngine.Logic
{
    public class KeyInput
    {
        public readonly string Key;
        public readonly bool Release;
        public bool Press => !Release;

        public KeyInput(string key, bool release = false)
        {
            Key = key;
            Release = release;
        }

        public override bool Equals(object obj)
        {
            if (obj is KeyInput key)
                return key.Key.Equals(Key);
            return false;
        }

        public override int GetHashCode()
        {
            return Key.GetHashCode();
        }
    }
}
