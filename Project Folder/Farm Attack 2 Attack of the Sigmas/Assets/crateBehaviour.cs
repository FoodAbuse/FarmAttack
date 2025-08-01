using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class crateBehaviour : MonoBehaviour
{
    public List<GameObject> objectsToActivate;

    [Header("Starting health")]
    public int health = 100;

    private bool hasActivated = false;

    void Update()
    {
        if (!hasActivated && health <= 0)
        {
            hasActivated = true;
            ActivatePhysics();
        }
    }

    private void ActivatePhysics()
    {
        foreach (GameObject go in objectsToActivate)
        {
            BoxCollider[] cols = go.GetComponentsInChildren<BoxCollider>(includeInactive: true);
            foreach (BoxCollider col in cols)
            {
                col.enabled = true;
                Destroy(col, 5f);
            }

            Rigidbody[] bodies = go.GetComponentsInChildren<Rigidbody>(includeInactive: true);
            foreach (Rigidbody rb in bodies)
            {
                rb.isKinematic = false;
                rb.detectCollisions = true;
                Destroy(rb, 5f);
            }
        }

         StartCoroutine(DestroyObjectsAfterDelay(5f));
    }

 
    private IEnumerator DestroyObjectsAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        foreach (GameObject go in objectsToActivate)
            Destroy(go);
    }

    public void TakeDamage()
    {
        health += -1;
    }
}
