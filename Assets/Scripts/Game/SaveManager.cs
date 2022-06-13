using System.Collections;
using UnityEngine;

namespace Game
{
    public class SaveManager : MonoBehaviour
    {
        [SerializeField] private Stats.GameStatsView m_GameStatsView;
        [SerializeField] private bool m_AutoSave = true;
        [SerializeField, Min(1f)] private float m_SaveInterval = 300f;

        private Stats.GameStats _stats;

        private void Awake()
        {
            int score = PlayerPrefs.HasKey("Score") ? PlayerPrefs.GetInt("Score") : 0;
            float path = PlayerPrefs.HasKey("Path") ? PlayerPrefs.GetFloat("Path") : 0f;
            _stats = new Stats.GameStats(score, path);
            new Stats.GameStatsPresenter(_stats, m_GameStatsView);
        }

        private void Start()
        {
            StartCoroutine(AutoSave());
        }

        private void OnDestroy()
        {
            Save();
        }

        private IEnumerator AutoSave()
        {
            float lastSaveTime = Time.time;
            while (true)
            {
                yield return null;
                if (m_AutoSave && Time.time - lastSaveTime >= m_SaveInterval)
                {
                    Save();
                    lastSaveTime = Time.time;
                }
            }
        }

        private void Save()
        {
            if (_stats != null)
            {
                PlayerPrefs.SetInt("Score", _stats.Score);
                PlayerPrefs.SetFloat("Path", _stats.Path);
                Debug.Log("Save");
            }
        }
    }
}
