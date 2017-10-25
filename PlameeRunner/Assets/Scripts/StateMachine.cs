namespace StateStuff
{
#region Implementing universal State Machine
    public class StateMachine<T>
    {
        public IState<T> currentState { get; private set; }
        public T Owner;

        public StateMachine(T _o)
        {
            Owner = _o;
            currentState = null;
        }

        public void ChangeState(IState<T> _newstate)
        {
            if (currentState != null)
                currentState.ExitState(Owner);
            currentState = _newstate;
            currentState.EnterState(Owner);
        }

        public void Update()
        {
            if (currentState != null)
                currentState.UpdateState(Owner);
        }
    }
    #endregion

#region State interface
    public interface IState<T>
    {
        void EnterState(T _owner);
        void ExitState(T _owner);
        void UpdateState(T _owner);
    }
#endregion
}