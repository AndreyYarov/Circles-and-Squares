using UniRx;

namespace Game.Stats
{
    public class GameStatsPresenter
    {
        private GameStats _model;
        private GameStatsView _view;

        public GameStatsPresenter(GameStats model, GameStatsView view)
        {
            _model = model;
            _model.OnScoreChanged.AddListener(OnScoreChanged);
            _model.OnPathChanged.AddListener(OnPathChanged);

            _view = view;
            _view.SetScore(_model.Score, 0f);
            _view.SetPath(_model.Path);

            MessageBroker.Default.Receive<Events.ValueChangedEvent>().Subscribe(ReceiveEvent);
        }

        private void OnScoreChanged()
        {
            _view.SetScore(_model.Score, 2f);
        }

        private void OnPathChanged()
        {
            _view.SetPath(_model.Path);
        }

        private void ReceiveEvent(Events.ValueChangedEvent e)
        {
            if (e is Events.ScoreEvent se)
                _model.Score += se.Delta;
            else if (e is Events.PathEvent pe)
                _model.Path += pe.Delta;
        }
    }
}
