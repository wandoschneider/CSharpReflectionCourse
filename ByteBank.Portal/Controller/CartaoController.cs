using ByteBank.Portal.Infraestrutura;
using ByteBank.Service;
using ByteBank.Service.Cartao;

namespace ByteBank.Portal.Controller
{
    public class CartaoController : ControllerBase
    {
        private ICartaoService _cartaoService;

        public CartaoController()
        {
            _cartaoService = new CartaoServiceTeste();
        }

        public string Debito() =>
            View(new { CartaoNome = _cartaoService.ObterCartaoDeDebitoDeDestaque() });

        public string Credito() =>
            View(new { CartaoNome = _cartaoService.ObterCartaoDeCreditoDeDestaque() });        
    }
}