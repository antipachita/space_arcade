using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float loadDelay = 1f;
    [SerializeField] ParticleSystem crashVFX;

    [SerializeField] GameObject children;

    void Start()
    {

    }


    void OnTriggerEnter(Collider other)
    {
        StartCrachSequence();
    }
    void StartCrachSequence()
    {
        crashVFX.Play();
        GetComponent<PlayerControls>().enabled = false;
        GetComponent<BoxCollider>().enabled = false;
        HideMeshInChildren();
        Invoke("ReloadLevel", loadDelay);
    }

    void HideMeshInChildren()
    {

        children.SetActive(false);

    }
    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

}
