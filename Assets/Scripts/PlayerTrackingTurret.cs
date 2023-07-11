using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTrackingTurret : MonoBehaviour
{
    private Entity entity;
    [SerializeField]
    private Animator animator;

    [SerializeField]
    private int cardinalPoints;

    private Vector2 direction;

    // Start is called before the first frame update
    void Start()
    {
        entity = gameObject.GetComponent<Entity>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        direction = SnapToCardinalPoint(entity.player.GetComponent<KaijuController>().GetDirectionToFrom(transform.position));
        animator.SetFloat("Horizontal", direction.x);
        animator.SetFloat("Vertical", direction.y);
    }

    private Vector2 SnapToCardinalPoint(Vector2 vector)
    {
        Vector2 closest = Vector2.zero;
        for (int i = 0; i < cardinalPoints; i++)
        {
            float angle = 2 * Mathf.PI * i / cardinalPoints;
            Vector2 point = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
            if (Vector2.Distance(vector, point) < Vector2.Distance(closest, vector))
            {
                closest = point;
            }
        }
        return closest;
    }
}
