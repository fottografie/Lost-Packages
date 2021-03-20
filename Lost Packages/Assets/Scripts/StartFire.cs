using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartFire : MonoBehaviour
{
    public GameObject fireObject;
    public GameObject fire;

    private void OnMouseDown()
    {
        if (fire == null)
        {
            fire = Instantiate(fireObject, transform.position, Quaternion.Euler(-90, 0, 0));
            FindObjectOfType<AudioManager>().Play("Feuer");
            StartCoroutine(Delay());
        }
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(10f);

        Destroy(fire);
    }
}
