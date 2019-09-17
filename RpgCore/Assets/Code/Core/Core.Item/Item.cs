using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core.Item {
    public abstract class Item : ScriptableObject {
        public abstract string ID { get;}
        public abstract string Name { get; }
        public abstract string Slug { get; }
        public abstract string Description { get; }
        public abstract int Value { get; }
        public abstract int Rarity { get; }
        public abstract bool Stackable { get; }
        public abstract Sprite Icon { get; }
    }
}
