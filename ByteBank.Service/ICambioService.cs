namespace ByteBank.Service
{
    public interface ICambioService
    {
        decimal Calcular(string moedaOrigem, string MoedaDestino, decimal valor);
    }
}