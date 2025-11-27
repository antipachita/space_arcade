using UnityEngine;

public class SelfDesctruct : MonoBehaviour
{
    [SerializeField] float timeTillDestroy = 3f;
    void Start()
    {
        Destroy(gameObject, timeTillDestroy);
    }


}
