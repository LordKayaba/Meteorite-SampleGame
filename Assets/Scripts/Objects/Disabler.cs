using System.Collections;
using UnityEngine;

public class Disabler : MonoBehaviour
{
    public void SetStartTime(float time) => StartCoroutine(DisableRoutine(time));

    IEnumerator DisableRoutine(float time)
    {
        yield return new WaitForSeconds(5);
        gameObject.SetActive(false);
    }
}