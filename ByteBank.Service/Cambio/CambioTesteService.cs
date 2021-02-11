using System;

namespace ByteBank.Service.Cambio
{
    public class CambioTesteService : ICambioService
    {
        private readonly Random _rdm = new Random();
        public decimal Calcular(string moedaOrigem, string MoedaDestino, decimal valor) =>
            valor * (decimal) _rdm.NextDouble();
    }
}