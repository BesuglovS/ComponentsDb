using ComponentsDb.DomainClasses;
using ComponentsDb.Repositories;
using System.Collections.Generic;

namespace ComponentsDb.Context
{
    public static class DatabaseSeeder
    {
        public static void SeedData1()
        {
            var repo = new ComponentsRepo();

            repo.ComponentLinks.SlowTruncateTable();
            repo.Components.SlowTruncateTable();

            var c1 = new Component { Name = "Горячие клавиши", IsTopLevel = true };
            var c2 = new Component { Name = "Загрузить тестовые наборы данных" };
            var c3 = new Component { Name = "Набор 1 (Ctrl + 1)" };
            var c4 = new Component { Name = "Набор 2 (Ctrl + 2)" };
            var c5 = new Component { Name = "Набор 3 (Ctrl + 3)" };
            var c6 = new Component { Name = "Редактирование графа компонентов" };
            var c7 = new Component { Name = "Новый компонент верхнего уровня (Ctrl + Shift + N)" };
            var c8 = new Component { Name = "Новый вложенный компонент (Ctrl + N)" };
            var c9 = new Component { Name = "Переименовать (F2)" };
            var c10 = new Component { Name = "Удалить (Del)" };
            var c11 = new Component { Name = "Отчёты" };
            var c12 = new Component { Name = "Отчёт о сводном составе (Ctrl + R)" };
            
            var l1 = new ComponentLink { ParentComponent = c1, ChildComponent = c2, Quantity = 1 };
            var l2 = new ComponentLink { ParentComponent = c2, ChildComponent = c3, Quantity = 1 };
            var l3 = new ComponentLink { ParentComponent = c2, ChildComponent = c4, Quantity = 1 };
            var l4 = new ComponentLink { ParentComponent = c2, ChildComponent = c5, Quantity = 1 };
            var l5 = new ComponentLink { ParentComponent = c1, ChildComponent = c6, Quantity = 1 };
            var l6 = new ComponentLink { ParentComponent = c6, ChildComponent = c7, Quantity = 1 };
            var l7 = new ComponentLink { ParentComponent = c6, ChildComponent = c8, Quantity = 1 };
            var l8 = new ComponentLink { ParentComponent = c6, ChildComponent = c9, Quantity = 1 };
            var l9 = new ComponentLink { ParentComponent = c6, ChildComponent = c10, Quantity = 1 };
            var l10 = new ComponentLink { ParentComponent = c1, ChildComponent = c11, Quantity = 1 };
            var l11 = new ComponentLink { ParentComponent = c11, ChildComponent = c12, Quantity = 1 };

            repo.ComponentLinks.AddRange(
                new List<ComponentLink> {
                        l1, l2, l3, l4, l5, l6, l7, l8, l9, l10, l11
                });
        }

        public static void SeedData2()
        {
            var repo = new ComponentsRepo();

            repo.ComponentLinks.SlowTruncateTable();
            repo.Components.SlowTruncateTable();

            var c1 = new Component { Name = "Двигатель 2106", IsTopLevel = true };
            var c2 = new Component { Name = "Блок цилиндров" };
            var c3 = new Component { Name = "Коленвал" };
            var c4 = new Component { Name = "Распредвал" };
            var c5 = new Component { Name = "Поршень в сборе" };
            var c6 = new Component { Name = "Поршень" };
            var c7 = new Component { Name = "Компрессионное кольцо" };
            var c8 = new Component { Name = "Маслосъёмное кольцо" };
            var c9 = new Component { Name = "Шатун" };
            var c11 = new Component { Name = "Двигатель 2103", IsTopLevel = true };
            
            var l1 = new ComponentLink { ParentComponent = c1, ChildComponent = c2, Quantity = 1 };
            var l2 = new ComponentLink { ParentComponent = c1, ChildComponent = c3, Quantity = 1 };
            var l3 = new ComponentLink { ParentComponent = c1, ChildComponent = c4, Quantity = 2 };
            var l4 = new ComponentLink { ParentComponent = c1, ChildComponent = c5, Quantity = 4 };
            var l5 = new ComponentLink { ParentComponent = c5, ChildComponent = c6, Quantity = 1 };
            var l6 = new ComponentLink { ParentComponent = c5, ChildComponent = c7, Quantity = 2 };
            var l7 = new ComponentLink { ParentComponent = c5, ChildComponent = c8, Quantity = 1 };
            var l8 = new ComponentLink { ParentComponent = c1, ChildComponent = c9, Quantity = 4 };
            var l11 = new ComponentLink { ParentComponent = c11, ChildComponent = c2, Quantity = 1 };
            var l12 = new ComponentLink { ParentComponent = c11, ChildComponent = c3, Quantity = 1 };
            var l13 = new ComponentLink { ParentComponent = c11, ChildComponent = c4, Quantity = 2 };
            var l14 = new ComponentLink { ParentComponent = c11, ChildComponent = c5, Quantity = 4 };
            var l18 = new ComponentLink { ParentComponent = c11, ChildComponent = c9, Quantity = 4 };

            repo.ComponentLinks.AddRange(
                new List<ComponentLink> {
                        l1, l2, l3, l4, l5, l6, l7, l8,
                        l11, l12, l13, l14, l18,
                });
        }

        public static void SeedData3()
        {
            var repo = new ComponentsRepo();

            repo.ComponentLinks.SlowTruncateTable();
            repo.Components.SlowTruncateTable();

            var c1 = new Component { Name = "Блок 1", IsTopLevel = true };
            var c2 = new Component { Name = "Блок 2", IsTopLevel = true };
            //var c3 = new Component { Name = "Блок 3", IsTopLevel = true };                
            var c4 = new Component { Name = "Блок 4" };
            var c5 = new Component { Name = "Блок 5" };
            var c6 = new Component { Name = "Блок 6" };
            var c7 = new Component { Name = "Блок 7" };
            var c8 = new Component { Name = "Блок 8" };
            var c9 = new Component { Name = "Блок 9" };

            var l1 = new ComponentLink { ParentComponent = c1, ChildComponent = c4, Quantity = 1 };
            var l2 = new ComponentLink { ParentComponent = c1, ChildComponent = c5, Quantity = 1 };
            var l3 = new ComponentLink { ParentComponent = c1, ChildComponent = c6, Quantity = 2 };
            var l4 = new ComponentLink { ParentComponent = c2, ChildComponent = c5, Quantity = 4 };
            var l5 = new ComponentLink { ParentComponent = c2, ChildComponent = c6, Quantity = 1 };
            var l6 = new ComponentLink { ParentComponent = c2, ChildComponent = c7, Quantity = 2 };
            //var l7 = new ComponentLink { ParentComponent = c3, ChildComponent = c4, Quantity = 1 };
            //var l8 = new ComponentLink { ParentComponent = c3, ChildComponent = c7, Quantity = 4 };
            var l11 = new ComponentLink { ParentComponent = c4, ChildComponent = c8, Quantity = 1 };
            var l12 = new ComponentLink { ParentComponent = c4, ChildComponent = c9, Quantity = 1 };
            var l13 = new ComponentLink { ParentComponent = c8, ChildComponent = c9, Quantity = 2 };
            var l14 = new ComponentLink { ParentComponent = c1, ChildComponent = c8, Quantity = 2 };
            var l15 = new ComponentLink { ParentComponent = c9, ChildComponent = c6, Quantity = 2 };
            var l16 = new ComponentLink { ParentComponent = c5, ChildComponent = c9, Quantity = 2 };
            
            repo.ComponentLinks.AddRange(
                new List<ComponentLink> {
                        l1, l2, l3, /*l4, l5, l6, l7, l8,*/
                        l11, /*l12,*/ l13, l14, l15, l16
                });
        }
    }
}
