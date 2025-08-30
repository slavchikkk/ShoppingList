using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingList
{
  /// <summary>
  /// Список покупок
  /// </summary>
  public class ShoppingList
  {
    public static int nextId = 1;
    
    /// <summary>
    /// Id списка
    /// </summary>
    public int Id { get; set; }
    
    /// <summary>
    /// Название списка
    /// </summary>
    public string ListName { get; set; }
    
    /// <summary>
    /// Предметы для покупки
    /// </summary>
    public List<ShoppingItem> Items { get; set; }

    public ShoppingList(string listName, List<ShoppingItem> items)
    {
      Id = nextId++;
      this.ListName = listName;
      this.Items = items;
    }
  }
}
