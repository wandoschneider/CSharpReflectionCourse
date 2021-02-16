using System;
using System.Net;
using System.Text;
using ByteBank.Portal.Infraestrutura.Binding;
using ByteBank.Portal.Infraestrutura.Filtros;
using ByteBank.Portal.Infraestrutura.IoC;

namespace ByteBank.Portal.Infraestrutura
{
    public class ManipuladorRequisicaoController
    {
        private readonly ActionBinder _actionBinder = new ActionBinder();
        private readonly FilterResolver _filterResolver = new FilterResolver();
        private readonly ControllerResolver _controllerResolver;

        public ManipuladorRequisicaoController(IContainer container)
        {
            _controllerResolver = new ControllerResolver(container);
        }
        public void Manipular(HttpListenerResponse response, string path)
        {
            var partes = path.Split(new char[] { '/' }, System.StringSplitOptions.RemoveEmptyEntries);
            var controllerNome = partes[0];
            var actionNome = partes[1];

            var controllerNomeCompleto = $"ByteBank.Portal.Controller.{controllerNome}Controller";

            // var controllerWrapper = Activator.CreateInstance("ByteBank.Portal", controllerNomeCompleto, new object[0]);
            // var controller = controllerWrapper.Unwrap();

            var controller = _controllerResolver.ObterController(controllerNomeCompleto);

            var actionBindInfo = _actionBinder.ObterActionBindInfo(controller, path);

            var filterResult = _filterResolver.VerificarFiltros(actionBindInfo);

            if(filterResult.PodeContinuar)
            {
                var resultadoAction = (string)actionBindInfo.Invoke(controller);

                var buffer = Encoding.UTF8.GetBytes(resultadoAction);

                response.StatusCode = 200;
                response.ContentType = "text/html; charset=utf-8";
                response.ContentLength64 = buffer.Length;

                response.OutputStream.Write(buffer, 0, buffer.Length);
                response.OutputStream.Close();
            }
            else
            {
                response.StatusCode = 307;
                response.RedirectLocation = "/Erro/Inesperado";
                response.OutputStream.Close();
            }
        }
    }
}