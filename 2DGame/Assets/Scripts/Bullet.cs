using UnityEngine;

public class Bullet : MonoBehaviour
{
    private void Start()
    {
        Fade();
    }

    void Fade()
    {
        Destroy(gameObject, 0.15f);
    }
}
