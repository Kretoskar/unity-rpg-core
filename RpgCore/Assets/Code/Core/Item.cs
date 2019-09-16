using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core {
    public abstract class Item : ScriptableObject {
        public abstract string ID();
        public abstract string Name();
        public abstract string Slug();
        public abstract string Description();
        public abstract int Value();
        public abstract int Rarity();
        public abstract bool Stackable();
    }
}
