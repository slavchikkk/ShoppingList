using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingList.Services
{
    /// <summary>
    /// Управление сохранением и загрузкой данных.
    /// </summary>
    public interface IJsonDataService
    {
      /// <summary>
      /// Сохраняет данныу в JSON файл
      /// </summary>
      Task SaveAllData();
      /// <summary>
      /// Выгружает данные из JSON файла
      /// </summary>
      Task LoadAllData();
    }
}
