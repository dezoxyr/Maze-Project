using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class SC_BackgroundScaler : MonoBehaviour
{
    Image m_backgroundImage;
    RectTransform m_rt;
    float m_ratio;

    // Start is called before the first frame update
    void Start()
    {
        m_backgroundImage = GetComponent<Image>();
        m_rt = m_backgroundImage.rectTransform;
        m_ratio = m_backgroundImage.sprite.bounds.size.x / m_backgroundImage.sprite.bounds.size.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (!m_rt)
            return;

        //Scale image proportionally to fit the screen dimensions, while preserving aspect ratio
        if (Screen.height * m_ratio >= Screen.width)
        {
            m_rt.sizeDelta = new Vector2(Screen.height * m_ratio, Screen.height);
        }
        else
        {
            m_rt.sizeDelta = new Vector2(Screen.width, Screen.width / m_ratio);
        }
    }
}
