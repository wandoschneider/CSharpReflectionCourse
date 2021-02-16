using System;

namespace ByteBank.Portal.Filtros
{
    public abstract class FiltroAttribute : Attribute
    {
        public abstract bool PodeContinuar();
    }
}