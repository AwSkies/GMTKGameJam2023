using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeOnDestruction : MonoBehaviour
{
    [SerializeField]
    private GameObject explosion;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnDestroy()
    {
        Instantiate(explosion, transform.position, Quaternion.identity);
    }
}
