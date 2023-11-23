namespace API.Models
{
    public class HeroiSuperpoder
    {
        protected HeroiSuperpoder() { }

        public HeroiSuperpoder(int heroiId, int superpoderId)
        {
            HeroiId = heroiId;
            SuperpoderId = superpoderId;
        }

        public int HeroiId { get; private set; }

        public int SuperpoderId { get; private set; }
    }
}
