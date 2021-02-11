using System;
using ByteBank.Portal.Infraestrutura;

namespace ByteBank.Portal
{
    class Program
    {
        static void Main(string[] args)
        {
            var prefixos = new string[] { "http://localhost:4444/" };
            var webApplication = new WebApplication(prefixos);
            webApplication.Iniciar();
        }
    }
}
