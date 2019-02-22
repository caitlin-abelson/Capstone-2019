using System.Collections.Generic;
using DataObjects;

namespace DataAccessLayer
{
    public interface IItemAccessor
    {
        List<Item> SelectItemNamesAndIDs();
    }
}