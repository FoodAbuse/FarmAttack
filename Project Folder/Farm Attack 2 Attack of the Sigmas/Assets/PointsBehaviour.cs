using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PointsBehaviour : MonoBehaviour
{
    public GameObject pointsGO;
    public Transform spawnPos;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void CallPoints()
    {
        GameObject newPoints = Instantiate(pointsGO, spawnPos.position, spawnPos.rotation);
        newPoints.GetComponent<TextMeshProUGUI>().text = "+50".ToString();
        newPoints.transform.parent = this.gameObject.transform;
        Destroy(newPoints, 1);

    }

}
