using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodDestroyer : MonoBehaviour
{
    // If food will spawn int he wrong place
    private void OnTriggerStay2D(Collider2D coll)
    {
        if (coll.tag == "NormalFood" || coll.tag == "SpecialFood" || coll.tag == "Border" || coll.tag == "Tail")
        {
            Destroy(gameObject);
        }
    }
}
