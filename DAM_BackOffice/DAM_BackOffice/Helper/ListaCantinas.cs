using DAM_BackOffice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAM_BackOffice.Helper
{
    public class ListaCantinas
    {
        public static List<Cantina> Cantinas { get; set; }

        public static void UpdateListaCantinas(Cantina c)
        {
            var cantina = Cantinas.FirstOrDefault(x => x.IdCantina == c.IdCantina);
            var index = Cantinas.IndexOf(cantina);

            if (index != -1)
            {
                Cantinas[index] = c;
            }
        }
    }
}
