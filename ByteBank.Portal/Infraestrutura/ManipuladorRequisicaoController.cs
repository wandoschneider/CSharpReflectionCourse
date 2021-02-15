using System;
using System.Net;
using System.Text;
using ByteBank.Portal.Infraestrutura.Binding;

namespace ByteBank.Portal.Infraestrutura
{
    public class ManipuladorRequisicaoController
    {
        private readonly ActionBinder _actionBinder = new ActionBinder();
        public void Manipular(HttpListenerResponse response, string path)
        {
            var partes = path.Split(new char[] { '/' }, System.StringSplitOptions.RemoveEmptyEntries);
            var controllerNome = partes[0];
            var actionNome = partes[1];

            var controllerNomeCompleto = $"ByteBank.Portal.Controller.{controllerNome}Controller";

            var controllerWrapper = Activator.CreateInstance("ByteBank.Portal", controllerNomeCompleto, new object[0]);
            var controller = controllerWrapper.Unwrap();

            var methodInfo = _actionBinder.ObterActionBindInfo(controller, path);

            var resultadoAction = (string)methodInfo.Invoke(controller);

            var buffer = Encoding.UTF8.GetBytes(resultadoAction);
            
            response.StatusCode = 200;
            response.ContentType = "text/html; charset=utf-8";
            response.ContentLength64 = buffer.Length;

            response.OutputStream.Write(buffer, 0, buffer.Length);
            response.OutputStream.Close();
        }
    }
}