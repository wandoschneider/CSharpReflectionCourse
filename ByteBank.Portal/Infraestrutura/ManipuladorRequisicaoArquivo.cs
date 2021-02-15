using System.Net;
using System.Reflection;

namespace ByteBank.Portal.Infraestrutura
{
    public class ManipuladorRequisicaoArquivo
    {
        public void Manipular(HttpListenerResponse response, string path)
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
                using(resourceStream)
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
    }
}