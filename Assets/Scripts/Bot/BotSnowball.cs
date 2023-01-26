using UnityEngine;

public class BotSnowball : MonoBehaviour
{
    private Vector3 _startPos;
    private Vector3 _startScale;

    private void Awake()
    {
        _startPos = transform.localPosition;
        _startScale = transform.localScale;
    }

    public void Rolling(float scale)
    {
        Vector3 pos = new Vector3(0, scale * 0.5f, scale * 0.5f);

        transform.localPosition += pos;
        transform.localScale += new Vector3(scale, scale, scale);
        
    }

    public void UnRolling(float scale)
    {
        Vector3 pos = new Vector3(0, scale * 0.5f, scale * 0.5f);
        transform.localPosition -= pos;
    }
    public void ResetSnowball()
    {
        transform.localPosition = _startPos;
        transform.localScale = _startScale;
        gameObject.SetActive(false);
    }
}
