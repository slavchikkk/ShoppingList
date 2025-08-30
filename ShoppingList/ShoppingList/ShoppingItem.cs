using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingList
{
  /// <summary>
  /// Предмет покупки
  /// </summary>
  public class ShoppingItem
  {
    /// <summary>
    /// Название
    /// </summary>
    public string Name {  get; set; }

    /// <summary>
    /// Количество
    /// </summary>
    public int Amount { get; set; }

    /// <summary>
    /// Единица измерения
    /// </summary>
    public string Unit { get; set; }

    /// <summary>
    /// Отметка о том куплен предмет или нет
    /// </summary>
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
