using UnityEngine;
using UniRx;
using Game.Events;

namespace Circle
{
    public class CircleMovement : MonoBehaviour
    {
        [SerializeField, Min(0.01f)] private float m_Speed;
        [SerializeField, Min(0.01f)] private float m_Acceleration;

        private float _fullTime; 
        private float _currentTime;
        private float _accelerationDistance;
        private float _accelerationTime;
        private CirclePath _path;

        public void SetPath(Vector3[] corners)
        {
            _path = new CirclePath(transform.position, corners);
            float d2 = m_Speed * m_Speed / m_Acceleration; //sum of acceleration and stopping paths
            if (_path.Length < d2)
                d2 = _path.Length;

            _accelerationDistance = d2 * 0.5f;
            _accelerationTime = Mathf.Sqrt(d2 / m_Acceleration);
            _fullTime = _accelerationTime * 2f + (_path.Length - d2) / m_Speed;
            _currentTime = 0f;
        }

        public void Stop()
        {
            _path = null;
        }

        private void FixedUpdate()
        {
            if (_path != null && !_path.Complete)
            {
                _currentTime = Mathf.Min(_currentTime + Time.fixedDeltaTime, _fullTime);
                float p;
                if (_currentTime < _accelerationTime)
                    p = m_Acceleration * _currentTime * _currentTime * 0.5f;
                else if (_fullTime - _currentTime < _accelerationTime)
                {
                    float remainingTime = _fullTime - _currentTime;
                    p = _path.Length - m_Acceleration * remainingTime * remainingTime * 0.5f;
                }
                else
                    p = _accelerationDistance + (_currentTime - _accelerationTime) * m_Speed;

                float delta = p - _path.Position;
                transform.position = _path.GetPosition(delta);
                MessageBroker.Default.Publish<ValueChangedEvent>(new PathEvent(delta));
            }
        }

        private void Reset()
        {
            m_Speed = 1f;
            m_Acceleration = 1f;
        }
    }
}
