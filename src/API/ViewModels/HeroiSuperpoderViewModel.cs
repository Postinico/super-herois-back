using API.Models;

namespace API.ViewModels
{
    public class HeroiSuperpoderViewModel
    {
        public HeroiSuperpoderViewModel(Heroi heroi, List<SuperpoderViewModel> superpoderes)
        {
            Heroi = heroi;
            Superpoderes = new();
            Superpoderes.AddRange(superpoderes);
        }

        public Heroi Heroi { get; private set; }

        public List<SuperpoderViewModel> Superpoderes { get; private set; }
    }
}
