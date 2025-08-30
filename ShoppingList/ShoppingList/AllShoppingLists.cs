using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingList
{
  /// <summary>
  /// Все созданные списки покупок.
  /// </summary>
  internal class AllShoppingLists
  {
    /// <summary>
    /// Все созданные списки.
    /// </summary>
    public static List<ShoppingList> AllLists { get; } = new List<ShoppingList>();

    /// <summary>
    /// Добить новый список.
    /// </summary>
    /// <param name="shoppingList">Список для добавления</param>
    public static void AddNewList(ShoppingList shoppingList)
    {  
      AllLists.Add(shoppingList);
    }

    /// <summary>
    /// Удалить список.
    /// </summary>
    /// <param name="shoppingList">Список для удаления</param>
    public static void RemoveList(ShoppingList shoppingList)
    {
      AllLists.Remove(shoppingList);
    }
  }
}
