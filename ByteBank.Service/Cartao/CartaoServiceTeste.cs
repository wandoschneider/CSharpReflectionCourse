namespace ByteBank.Service.Cartao
{
    public class CartaoServiceTeste : ICartaoService
    {
        public string ObterCartaoDeCreditoDeDestaque() => "ByteBank Gold Platinum";

        public string ObterCartaoDeDebitoDeDestaque() => "ByteBank Universitário";
    }
}