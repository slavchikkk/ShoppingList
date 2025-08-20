using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingList
{
    class ShoppingItem
    {
      public string Name {  get; set; }
      public int Amount { get; set; }

      public bool IsBought  { get; set; }

      public ShoppingItem(string Name, int Amount) 
      {
        this.Name = Name;
        this.Amount = Amount;
        IsBought = false;
      }
    }
}
