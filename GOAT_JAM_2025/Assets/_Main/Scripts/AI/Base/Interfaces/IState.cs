
namespace Scripts.AI.Base.Interfaces
{
    public interface IState
    {
        void OnEnter();
        void Tick();
        void OnExit();
    }
}