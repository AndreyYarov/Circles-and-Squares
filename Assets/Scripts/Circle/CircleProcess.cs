using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Circle
{
    public class CircleProcess : MonoBehaviour
    {
        [SerializeField] private CircleMovement m_Movement;
        [SerializeField] private Camera m_MainCamera;
        [SerializeField, Min(0.01f)] private float m_Radius;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Pickable"))
            {
                Destroy(collision.gameObject);
            }
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
            {
                Vector3 target = m_MainCamera.ScreenToWorldPoint(Input.mousePosition);
                target.z = 0f;
                if (Vector3.SqrMagnitude(transform.position - target) <= m_Radius * m_Radius)
                    m_Movement.Stop();
                else
                    m_Movement.SetPath(target);
            }
        }

        private void Reset()
        {
            m_Radius = 1f;
            m_MainCamera = Camera.main;
            m_Movement = GetComponent<CircleMovement>();
        }

        private void OnValidate()
        {
            transform.localScale = Vector3.one * m_Radius;
        }
    }
}
