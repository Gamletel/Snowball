using UnityEngine;

public class Snowball : MonoBehaviour
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
        Vector3 pos = new Vector3(0, scale * 0.4f, scale * 0.4f);
        transform.localPosition += pos;
    }

    public void UnRolling(float scale)
    {
        Vector3 pos = new Vector3(0, scale * 0.4f, scale * 0.4f);
        transform.localPosition -= pos;
    }

    public void ResetSnowball()
    {
        transform.localPosition = _startPos;
        transform.localScale = _startScale;
    }
}
