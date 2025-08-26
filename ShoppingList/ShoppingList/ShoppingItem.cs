using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingList
{
    public class ShoppingItem
    {
      public string Name {  get; set; }
      public int Amount { get; set; }
      public string Unit { get; set; }
      public bool IsBought  { get; set; }

      public ShoppingItem(string Name, int Amount, string Unit) 
      {
        this.Name = Name;
        this.Amount = Amount;
        this.Unit = Unit;
        IsBought = false;
      }
    }
}
