using DAM_BackOffice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAM_BackOffice.Helper
{
    public static class ListaPratos
    {
        public static List<Prato> Pratos { get; set; }

        public static void UpdateListaPratos(Prato p)
        {
            var prato = Pratos.FirstOrDefault(x => x.IdPrato == p.IdPrato);
            var index = Pratos.IndexOf(prato);

            if(index != -1)
            {
                Pratos[index] = p;
            }
        }
    }
}
