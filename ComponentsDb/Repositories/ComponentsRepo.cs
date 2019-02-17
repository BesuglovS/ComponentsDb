using System;
using System.Windows.Forms;

namespace ComponentsDb.Repositories
{
    public class ComponentsRepo : IDisposable
    {
        public ComponentsRepository Components;
        public ComponentLinksRepository ComponentLinks;

        public ComponentsRepo()
        {
            try
            {
                Components = new ComponentsRepository();
                ComponentLinks = new ComponentLinksRepository();
            }
            catch (Exception)
            {
                MessageBox.Show("Ошибка соединения с базой данных", "Ошибка");
            }
            
        }


        public void Dispose()
        {
        }
    }
}
