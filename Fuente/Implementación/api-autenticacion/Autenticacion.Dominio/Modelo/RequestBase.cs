using System;

namespace Autenticacion.Dominio.Modelo
{
    public class RequestBase<T>
    {
        public DateTime fecha { set; get; }
        public string usuario { set; get; }
        public T data { set; get; }

    }
}
