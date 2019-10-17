using System;
using System.Collections.Generic;
using System.Text;

namespace VideoClub
{
    interface IConsultasYModificaciones
    {
        void ModificarBase(string query);
        bool ConsultarBase(string query);
    }
}
