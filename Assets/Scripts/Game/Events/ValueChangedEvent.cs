namespace Game.Events
{
    public abstract class ValueChangedEvent { }

    public abstract class ValueChangedEvent<T> : ValueChangedEvent
    {
        public readonly T Delta;

        public ValueChangedEvent(T delta)
        {
            Delta = delta;
        }
    }
}
