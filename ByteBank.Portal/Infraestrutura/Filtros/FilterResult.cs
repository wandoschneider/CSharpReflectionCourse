namespace ByteBank.Portal.Infraestrutura.Filtros
{
    public class FilterResult
    {
        public bool PodeContinuar { get; private set; }
        
        public FilterResult(bool podeContinuar)
        {
            PodeContinuar = podeContinuar;
        }
    }
}