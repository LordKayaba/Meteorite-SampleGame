using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour, IAction
{
    public GameObject FX;

    public void Explode()
    {
        //AudioManager.Audio.FX(AudioManager.Audio.data.FXs.TNT);
        GameObject fx = Instantiate(FX);
        fx.transform.position = transform.position;
        Destroy(gameObject);
    }
}
