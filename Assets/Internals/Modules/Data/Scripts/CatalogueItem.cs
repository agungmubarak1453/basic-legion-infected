using System;

namespace BasicLegionInfected.Data
{
    [Serializable]
    public struct CatalogueItem<T>
    {
        public string Code;
        public T Item;
    }
}
