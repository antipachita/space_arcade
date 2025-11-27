using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject deathVFX;
    [SerializeField] GameObject hitVFX;

    [SerializeField] int points = 15;

    [SerializeField] int hitPoints = 2;

    GameObject parentGameObject;
    ScoreBoard scoreBoard;
    void Start()
    {
        scoreBoard = FindAnyObjectByType<ScoreBoard>();
        parentGameObject = GameObject.FindWithTag("Spawn At Runtime");
        AddRB();

    }

    void AddRB()
    {
        Rigidbody rg = gameObject.AddComponent<Rigidbody>();
        rg.useGravity = false;
    }
    void OnParticleCollision(GameObject other)

    {
        ProcessHit();
        Debug.Log(hitPoints);
        if (hitPoints <= 0)
        {
            KillEnemy();

        }


    }
    void ProcessHit()
    {
        GameObject vfx = Instantiate(hitVFX, transform.position, Quaternion.identity);
        vfx.transform.parent = parentGameObject.transform;
        hitPoints -= 1;
        scoreBoard.IncreaseScore(points);

    }

    void KillEnemy()
    {
        GameObject vfx = Instantiate(deathVFX, transform.position, Quaternion.identity);
        vfx.transform.parent = parentGameObject.transform;
        Destroy(gameObject);
    }
}
