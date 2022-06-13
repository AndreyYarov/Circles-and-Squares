namespace Game.Events
{
    public class PathEvent : ValueChangedEvent<float>
    {
        public PathEvent(float delta) : base(delta) { }
    }
}
