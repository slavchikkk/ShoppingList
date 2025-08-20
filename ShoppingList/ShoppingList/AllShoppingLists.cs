using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingList
{
  internal class AllShoppingLists
  {
    public static List<ShoppingList> AllLists { get; } = new List<ShoppingList>();

    public static void AddNewList(ShoppingList shoppingList)
    {  
      AllLists.Add(shoppingList);
    }

    public static void RemoveList(ShoppingList shoppingList)
    {
      AllLists.Remove(shoppingList);
    }

    public static ShoppingList GetListById(int id)
    {
      return AllLists.FirstOrDefault(list => list.Id == id);
    }
  }
}
