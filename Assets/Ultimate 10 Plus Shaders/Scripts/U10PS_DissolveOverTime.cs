using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(MeshRenderer))]
public class U10PS_DissolveOverTime : MonoBehaviour
{
    private MeshRenderer meshRenderer;

    public float speed = .5f;
    private Material[] mats;
    public Color[] Colors;


    //private Sequence _cubeSeq;
    
    private void Awake(){
        meshRenderer = GetComponent<MeshRenderer>();
        mats = meshRenderer.materials;
    }

    private IEnumerator FetchTween()
    {
        var seq = DOTween.Sequence();
        seq.Append(transform.DOMoveX(Random.Range(-88, 87), 1f).SetEase(Ease.Linear))
            .Insert(0,transform.DOMoveY(Random.Range(-63, 63), 1f).SetEase(Ease.Linear))
            .Insert(0,transform.DOLocalRotate(RandomVec3(), seq.Duration()));

        while (!Input.GetButton("Jump"))
        {
            seq.Play();
        }
        
        yield return new WaitForSeconds(seq.Duration());
    }
    private static Vector3 RandomVec3()
    {
        return new Vector3(Random.Range(-180, 180), Random.Range(-180, 180), Random.Range(-180, 180));
    }
   

    private float t = 0.0f;
    private void Update(){

        //mats[0].SetColor("_EdgeColor", RandomColor());
        mats[0].SetFloat("_Cutoff", Mathf.Sin(t * speed));
        t += Time.deltaTime;
        
        // Unity does not allow meshRenderer.materials[0]...
        //meshRenderer.materials = mats;
    }

    private Color RandomColor()
    {
        return Colors[Random.Range(0, Colors.Length)];
    }
}
