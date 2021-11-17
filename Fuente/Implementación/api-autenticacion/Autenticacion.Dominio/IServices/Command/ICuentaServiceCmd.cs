using Autenticacion.Dominio.Modelo;
using Autenticacion.Dominio.Modelo.Command;

namespace Autenticacion.Dominio.IServices.Command
{
    public interface ICuentaServiceCmd
    {
        CodigoCmd codigoRecuperarCuenta(string AbreviacionApp, string usuario, string host);

        ResultadoResponse PostRecuperar(RecuperarCuentaCmd request, string host);

        ResultadoResponse PutContrasena(CuentaCmd cuenta, string host);

    }
}
