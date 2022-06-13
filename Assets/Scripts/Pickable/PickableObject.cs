using System;
using UnityEngine;
using UniRx;
using Game.Events;

namespace Pickable
{
    public class PickableObject : MonoBehaviour
    {
        [SerializeField] private int m_Cost;
        private Action _onCollect;

        public void Init(Action onCollect)
        {
            _onCollect = onCollect;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                MessageBroker.Default.Publish<ValueChangedEvent>(new ScoreEvent(m_Cost));
                if (_onCollect != null)
                    _onCollect.Invoke();
                else
                    Destroy(gameObject);
            }
        }
    }
}
