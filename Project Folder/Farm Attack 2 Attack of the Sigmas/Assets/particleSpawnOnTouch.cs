using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class particleSpawnOnTouch : MonoBehaviour
{
    public ObjectPool pool;

    private ParticleSystem ps;
    private ParticleCollisionEvent[] collisionEvents;

    void Start()
    {
        ps = GetComponent<ParticleSystem>();
        collisionEvents = new ParticleCollisionEvent[16];
    }

    void OnParticleCollision(GameObject other)
    {
        if (other.CompareTag("Floor"))
        {
            int eventCount = ps.GetCollisionEvents(other, collisionEvents);

            for (int i = 0; i < eventCount; i++)
            {
                Vector3 hitPos = collisionEvents[i].intersection;
                pool.GetObject(hitPos, Quaternion.identity); // use from pool
            }
        }
    }
}
