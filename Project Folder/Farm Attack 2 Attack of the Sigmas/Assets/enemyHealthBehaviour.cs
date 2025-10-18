using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyHealthBehaviour : MonoBehaviour
{
    public float myHealth;
    public GameObject explodeGO;

    public Color flashColor = Color.red;
    public float flashDuration = 0.15f;

    private List<Material> myMats = new List<Material>();
    private List<Color> origColors = new List<Color>();

    void Start()
    {
        // Get unique copies of this object's materials
        Renderer[] rends = GetComponentsInChildren<Renderer>();
        foreach (Renderer r in rends)
        {
            foreach (Material m in r.materials)
            {
                myMats.Add(m);
                origColors.Add(m.color);
            }
        }
    }

    void Update()
    {
        if (myHealth <= 0)
        {
            FindObjectOfType<PointsBehaviour>()?.CallPoints();
            GameObject fx = Instantiate(explodeGO, transform.position, transform.rotation);
            Destroy(fx, 3);
            Destroy(gameObject);
        }
    }

    public void TakeDamage(float damageAmount)
    {
        myHealth -= damageAmount;
        StartCoroutine(Flash());
    }

    IEnumerator Flash()
    {
        // turn red
        foreach (Material m in myMats) m.color = flashColor;
        yield return new WaitForSeconds(flashDuration);
        // return to original
        for (int i = 0; i < myMats.Count; i++) myMats[i].color = origColors[i];
    }
}