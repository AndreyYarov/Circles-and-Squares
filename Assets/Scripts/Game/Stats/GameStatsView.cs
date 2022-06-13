using System.Collections;
using UnityEngine;
using TMPro;

namespace Game.Stats
{
    public class GameStatsView : MonoBehaviour
    {
        [SerializeField] private TMP_Text m_ScoreText;
        [SerializeField] private TMP_Text m_PathText;

        private int _score = 0;
        private Coroutine scoreCoroutine;

        public void SetScore(int score, float delay)
        {
            if (scoreCoroutine != null)
                StopCoroutine(scoreCoroutine);

            if (delay <= 0f)
            {
                _score = score;
                m_ScoreText.text = _score.ToString();
            }
            else
                scoreCoroutine = StartCoroutine(AnimateScoreAsync(score, delay));
        }

        private IEnumerator AnimateScoreAsync(int end, float delay)
        {
            int start = _score;
            float t = 0f;
            do
            {
                yield return null;
                t += Time.deltaTime;
                _score = Mathf.RoundToInt(Mathf.Lerp(start, end, t / delay));
                m_ScoreText.text = _score.ToString();
            } while (t < delay);
        }

        public void SetPath(float path)
        {
            m_PathText.text = path.ToString("N0");
        }
    }
}
