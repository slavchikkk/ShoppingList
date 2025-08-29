using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingList
{
    public class ShoppingList
    {
      public static int nextId = 1;

      public int Id { get; set; }
      public string ListName { get; set; }
      public List<ShoppingItem> Items { get; set; }

      public ShoppingList(string listName, List<ShoppingItem> items)
      {
         Id = nextId++;
         this.ListName = listName;
         this.Items = items;
      }


    }
}
