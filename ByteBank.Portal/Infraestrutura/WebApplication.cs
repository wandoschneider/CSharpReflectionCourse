using System;
using System.Net;
using System.Reflection;
using System.Text;
using ByteBank.Portal.Controller;

namespace ByteBank.Portal.Infraestrutura
{
    public class WebApplication
    {
        private readonly string[] _prefixos;
        public WebApplication(string[] prefixos)
        {
            if (prefixos == null)
                throw new ArgumentNullException(nameof(prefixos));

            _prefixos = prefixos;
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

            var path = request.Url.AbsolutePath;

            if (Utilidades.IsArquivo(path))
            {
                var assembly = Assembly.GetExecutingAssembly();
                var nomeResource = Utilidades.ConverterPathParaNomeAssembly(path);

                var resourceStream = assembly.GetManifestResourceStream(nomeResource);

                if (resourceStream is null)
                {
                    response.StatusCode = 404;
                    response.OutputStream.Close();
                }
                else
                {
                    var bytesResource = new byte[resourceStream.Length];

                    resourceStream.Read(bytesResource, 0, (int)resourceStream.Length);

                    response.ContentType = Utilidades.ObterTipoDeConteudo(path);
                    response.StatusCode = 200;
                    response.ContentLength64 = resourceStream.Length;

                    response.OutputStream.Write(bytesResource, 0, bytesResource.Length);

                    response.OutputStream.Close();
                }
            }
            else if (path == "/Cambio/MXN")
            {
                var controller = new CambioController();
                var paginaConteudo = controller.MXN();

                var bufferArquivo = Encoding.UTF8.GetBytes(paginaConteudo);
                
                response.StatusCode = 200;
                response.ContentType = "text/html; charset=utf-8";
                response.ContentLength64 = bufferArquivo.Length;
                
                response.OutputStream.Write(bufferArquivo, 0, bufferArquivo.Length);
                response.OutputStream.Close();
            }

            httpListener.Stop();
        }
    }
}