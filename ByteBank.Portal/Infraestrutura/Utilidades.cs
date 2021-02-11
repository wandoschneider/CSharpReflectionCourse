using System;
using System.Linq;

namespace ByteBank.Portal.Infraestrutura
{
    public static class Utilidades
    {
        public static string ConverterPathParaNomeAssembly(string path)
        {
            var prefixoAssembly = "ByteBank.Portal";
            var pathComPontos = path.Replace('/', '.');

            return $"{prefixoAssembly}{pathComPontos}";
        }

        public static string ObterTipoDeConteudo(string path)
        {
            if (path.EndsWith(".css"))
                return "text/css; charset=utf-8";
            
            if (path.EndsWith(".js"))
                return "application/js; charset=utf-8";
            
            if (path.EndsWith(".html"))
                return "text/html; charset=utf-8";

            throw new NotImplementedException("Tipo de Conteúdo Não Previsto!");
        }

        public static bool IsArquivo(string path)
        {
            var partesPath = path.Split(new char[] {'/'}, StringSplitOptions.RemoveEmptyEntries);
            var ultimaParte = partesPath.Last();

            return ultimaParte.Contains('.');
        }
    }
}