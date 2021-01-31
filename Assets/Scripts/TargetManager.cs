using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetManager : MonoBehaviour
{
    public string NextLevel;

    public Renderer[] TargetGameObjects;

    public Color TargetColor;

    public float TargetColorTime = 2f;
    public float FadeTime = 2f;

    private bool AlreadyHit = false;

    public void TargetHit(GameObject player)
    {
        if (!AlreadyHit)
        {
            AlreadyHit = true;

            StartCoroutine(Transition(player));
        }
    }

    private IEnumerator Transition(GameObject player)
    {
        //Disable input
        PlayerCharacterController controller = player.GetComponent<PlayerCharacterController>();
        controller.enabled = false;

        float delaystart = Time.time;
        float delayprogress = 0f;

        Color[] startcolors = new Color[TargetGameObjects.Length];
        for (int i = 0; i < TargetGameObjects.Length; i++)
        {
            startcolors[i] = TargetGameObjects[i].material.color;
        }

        Quaternion playerstartrotation = player.transform.Find("Main Camera").rotation;
        Vector3 playerstartposition = player.transform.position;

        //Play sound

        do
        {
            delayprogress = (Time.time - delaystart) / TargetColorTime;
            //Change object color
            for (int i = 0; i < TargetGameObjects.Length; i++)
            {
                TargetGameObjects[i].material.color = Color.Lerp(startcolors[i], TargetColor, delayprogress);
            }
            //Debug.Log(delayprogress);

            player.transform.position = Vector3.Lerp(playerstartposition, this.transform.position, delayprogress);
            player.transform.Find("Main Camera").rotation = Quaternion.Lerp(playerstartrotation, this.transform.rotation, delayprogress);
            yield return null;
        } while (delayprogress < 1);

        //Fade to black
        ViewFade fade = player.GetComponent<ViewFade>();
        fade.FadeOut(NextLevel);
    }
}
