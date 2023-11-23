namespace API.ViewModels
{
    public class HeroiViewModel
    {
        public HeroiViewModel
        (
            int id,
            string nomeHeroi
        )
        {
            Id = id;
            NomeHeroi = nomeHeroi;
        }

        public int Id { get; private set; }

        public string NomeHeroi { get; private set; }
    }
}
