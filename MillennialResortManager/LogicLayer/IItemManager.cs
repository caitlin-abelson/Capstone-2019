using System.Collections.Generic;
using DataObjects;

namespace LogicLayer
{
    public interface IItemManager
    {
        List<Item> RetrieveItemNamesAndIDs();
    }
}