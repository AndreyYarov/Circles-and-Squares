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
        [SerializeField, Min(1)] private int m_PathPixelsDistance = 10;

        private bool dragFinger = false;
        private List<Vector3> screenPath = new List<Vector3>();

        private void Update()
        {
            if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
            {
                Vector3 target = m_MainCamera.ScreenToWorldPoint(Input.mousePosition);
                target.z = 0f;
                if (Vector3.SqrMagnitude(transform.position - target) <= m_Radius * m_Radius)
                    m_Movement.Stop();
                else
                {
                    dragFinger = true;
                    screenPath.Add(Input.mousePosition);
                }
            }
            else if (dragFinger)
            {
                if (Input.GetMouseButtonUp(0))
                {
                    if (screenPath[screenPath.Count - 1] != Input.mousePosition)
                        screenPath.Add(Input.mousePosition);

                    Vector3[] pathCorners = new Vector3[screenPath.Count];
                    for (int i = 0; i < screenPath.Count; i++)
                    {
                        Vector3 point = i == 0 || i == screenPath.Count - 1 ? screenPath[i] :                //interpolation beetween closest corners 
                            screenPath[i - 1] * 0.25f + screenPath[i] * 0.5f + screenPath[i + 1] * 0.25f;    //for smooth moving
                        pathCorners[i] = m_MainCamera.ScreenToWorldPoint(point);
                        pathCorners[i].z = 0f;
                    }

                    m_Movement.SetPath(pathCorners);

                    dragFinger = false;
                    screenPath.Clear();
                }
                else if (Vector3.Distance(screenPath[screenPath.Count - 1], Input.mousePosition) >= m_PathPixelsDistance)
                    screenPath.Add(Input.mousePosition);
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
