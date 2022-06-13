namespace Game.Events
{
    public class ScoreEvent : ValueChangedEvent<int>
    {
        public ScoreEvent(int delta) : base(delta) { }
    }
}
