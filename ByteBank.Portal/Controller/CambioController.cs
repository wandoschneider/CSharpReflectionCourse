using System.IO;
using System.Reflection;
using ByteBank.Portal.Infraestrutura;
using ByteBank.Service;
using ByteBank.Service.Cambio;
using ByteBank.Service.Cartao;
using ByteBank.Portal.Filtros;

namespace ByteBank.Portal.Controller
{
    public class CambioController : ControllerBase
    {
        private ICambioService _cambioService;
        private ICartaoService _cartaoService;
        public CambioController()
        {
            _cambioService = new CambioTesteService();
            _cartaoService = new CartaoServiceTeste();
        }
        
        [ApenasHorarioComercialFiltro]
        public string MXN()
        {
            var valorFinal = _cambioService.Calcular("MXN", "BRL", 1);

            return View(new
            {
                Valor = valorFinal
            });
        }

        [ApenasHorarioComercialFiltro]
        public string USD()
        {
            var valorFinal = _cambioService.Calcular("USD", "BRL", 1);

            return View(new {
                Valor = valorFinal
            });
        }

        [ApenasHorarioComercialFiltro]
        public string Calculo(string moedaDestino) =>
            Calculo("BRL", moedaDestino, 1);

        [ApenasHorarioComercialFiltro]
        public string Calculo(string moedaDestino, decimal valor) =>
            Calculo("BRL", moedaDestino, valor);

        [ApenasHorarioComercialFiltro]
        public string Calculo(string moedaOrigem, string moedaDestino, decimal valor)
        {
            var valorFinal = _cambioService.Calcular(moedaOrigem, moedaDestino, valor);
            var cartaoPromocao = _cartaoService.ObterCartaoDeCreditoDeDestaque();

            var modelo = new {
                MoedaDestino = moedaDestino,
                ValorDestino = valorFinal,
                MoedaOrigem = moedaOrigem,
                ValorOrigem = valor,
                CartaoPromocao = cartaoPromocao 
            };
            
            return View(modelo);
        }
    }
}