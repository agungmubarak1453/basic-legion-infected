using System;
using System.Linq;

using UnityEngine;

namespace BasicLegionInfected.Data
{
    public abstract class ACatalogue<T> : ScriptableObject
    {
        public CatalogueItem<T>[] Items;

        public T GetItem(string code)
        {
            var catalogueItem = Items.FirstOrDefault(catalogueItem => catalogueItem.Code.Equals(code));

            if (catalogueItem.Equals(default))
            {
                return default;
            }
            else
            {
                return catalogueItem.Item;
            }
        }
    }
}
