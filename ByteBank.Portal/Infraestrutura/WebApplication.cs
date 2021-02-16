using System;
using System.Net;
using System.Reflection;
using System.Text;
using ByteBank.Portal.Controller;
using ByteBank.Portal.Infraestrutura.IoC;
using ByteBank.Service;
using ByteBank.Service.Cambio;
using ByteBank.Service.Cartao;

namespace ByteBank.Portal.Infraestrutura
{
    public class WebApplication
    {
        private readonly string[] _prefixos;
        private readonly IContainer _container = new ContainerSimples();
        public WebApplication(string[] prefixos)
        {
            if (prefixos == null)
                throw new ArgumentNullException(nameof(prefixos));

            _prefixos = prefixos;
            Configurar();
        }
        private void Configurar()
        {
            _container.Registrar<ICambioService, CambioTesteService>();
            _container.Registrar<ICartaoService, CartaoServiceTeste>();
        }
        public void Iniciar()
        {
            while (true)
            {
                ManipularRequisicao();
            }

        }

        private void ManipularRequisicao()
        {
            var httpListener = new HttpListener();

            foreach (var prefixo in _prefixos)
            {
                httpListener.Prefixes.Add(prefixo);
            }

            httpListener.Start();

            var contexto = httpListener.GetContext();
            var request = contexto.Request;
            var response = contexto.Response;

            var path = request.Url.PathAndQuery;

            if (Utilidades.IsArquivo(path))
            {
                var manipulador = new ManipuladorRequisicaoArquivo();
                manipulador.Manipular(response, path);
            }
            else
            {
                var manipulador = new ManipuladorRequisicaoController(_container);
                manipulador.Manipular(response, path);
            }

            httpListener.Stop();
        }
    }
}