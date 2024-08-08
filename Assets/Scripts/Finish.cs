using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Finish : MonoBehaviour
{
    [SerializeField] Animator anim;
    [SerializeField] GameObject first_P;
    [SerializeField] GameObject second_P;
    [SerializeField] GameObject third_P;

    int first;
    int second;

    [SerializeField] ParticleSystem[] confettis;

    public void Win()
    {
        anim.enabled = true;
        foreach (ParticleSystem particle in confettis)
        {
            particle.Play();
        }
    }

    private void OnTriggerExit(Collider col)
    {
        if (col.gameObject.CompareTag("Player") )
        {
            Character c = col.gameObject.GetComponent<Character>();
            OnFinish(c);

            //Win UI
            StartCoroutine(OpenFinishUI(GameState.Victory));
        }

        if (col.gameObject.CompareTag("Bot"))
        {
            Character c = col.gameObject.GetComponent<Character>();
            OnFinish(c);

            //Lost UI
            StartCoroutine(OpenFinishUI(GameState.Fail));
        }

    }

    private IEnumerator OpenFinishUI(GameState state)
    {
        yield return new WaitForSeconds(2);
        GameManager.Ins.ChangeState(state);
    }

    private void OnFinish(Character c)
    {
        Win();
        Character winner = c;
        winner.transform.position = Vector3.MoveTowards(winner.transform.position, first_P.transform.position + Vector3.up * .5f, 5f);
        winner.Win();
        CameraFollow.Ins.SetTarget(winner.transform);

        first_P.GetComponent<MeshRenderer>().material.color = winner.colorSet.color;

        Character[] cs = FindObjectsOfType<Character>();

        foreach (Character _c in cs)
        {
            if (_c != winner)
            {
                _c.Lose();
            }
        }

        cs = cs.Where(val => val != winner).ToArray();
        int min = 0;

        for (int i = 0; i < cs.Length; i++)
        {
            if (cs[i].transform.position.z < cs[min].transform.position.z)
            {
                min = i;
            }
        }

        first = second = min;
        for (int i = 0; i < cs.Length; i++)
        {
            if (cs[i].transform.position.z > cs[first].transform.position.z)
            {
                second = first;
                first = i;
            }
            else if (cs[i].transform.position.z > cs[second].transform.position.z && i != first)
            {
                second = i;
            }

        }

        cs[first].transform.position = second_P.transform.position + Vector3.up * .5f;
        second_P.GetComponent<MeshRenderer>().material.color = cs[first].colorSet.color;

        cs[second].transform.position = third_P.transform.position + Vector3.up * .5f;
        third_P.GetComponent<MeshRenderer>().material.color = cs[second].colorSet.color;
    }
}
