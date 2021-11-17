using System;
using System.Collections.Generic;
using System.Text;

namespace Ordenes.Dominio.Modelo
{
    public class RequestBase<T>
    {
        public string usuario { set; get; }
        public T data { set; get; }

    }
}
