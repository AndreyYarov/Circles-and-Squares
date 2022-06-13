using UnityEngine.Events;

namespace Game.Stats
{
    public class GameStats
    {
        private int _score;
        private float _path;
        private UnityEvent _onScoreChanged = new UnityEvent();
        private UnityEvent _onPathChanged = new UnityEvent();

        public UnityEvent OnScoreChanged => _onScoreChanged;
        public int Score
        {
            get => _score;
            set
            {
                if (_score != value)
                {
                    _score = value;
                    OnScoreChanged.Invoke();
                }
            }
        }

        public UnityEvent OnPathChanged => _onPathChanged;
        public float Path
        {
            get => _path;
            set
            {
                if (_path != value)
                {
                    _path = value;
                    OnPathChanged.Invoke();
                }
            }
        }

        public GameStats(int score, float path)
        {
            _score = score;
            _path = path;
        }
    }
}
