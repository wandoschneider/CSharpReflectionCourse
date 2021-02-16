using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace ByteBank.Portal.Infraestrutura
{
    public abstract class ControllerBase
    {
        protected string View([CallerMemberName]string nomeArquivo = null)
        {
            var type = GetType();
            var diretorioNome = type.Name.Replace("Controller", "");
            var nomeCompletoResource = $"ByteBank.Portal.View.{diretorioNome}.{nomeArquivo}.html";
            var assembly = Assembly.GetExecutingAssembly();
            var streamRecurso = assembly.GetManifestResourceStream(nomeCompletoResource);

            var streamLeitura = new StreamReader(streamRecurso);
            var textoPagina = streamLeitura.ReadToEnd();

            return textoPagina;
        }

        protected string View(object modelo, [CallerMemberName] string nomeArquivo = null)
        {
            var viewBase = View(nomeArquivo);
            var todasPropriedadesModelo = modelo.GetType().GetProperties();

            var regex = new Regex("\\{{(.*?)\\}}");
            var viewProcessada = regex.Replace(viewBase, (match) => {
                var nomePropriedade = match.Groups[1].Value;
                var propriedade = todasPropriedadesModelo.Single(prop => prop.Name == nomePropriedade);

                var valorBruto = propriedade.GetValue(modelo);
                return valorBruto?.ToString();
            });

            return viewProcessada;
        }
    }
}