namespace _Main.Scripts.Interface
{
    public interface IDieable
    {
        public bool IsDead { get; set; }
        public void OnDead();
        public void OnRevive();
    }
}