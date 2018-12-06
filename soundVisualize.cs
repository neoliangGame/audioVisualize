using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soundVisualize : MonoBehaviour {

    public GameObject cube = null;
    GameObject[] cubes = null;
    public float range = 5000f;

    public int number = 64;

	void Start () {
        CreateVisualCube(number, -60,60);
	}

    void CreateVisualCube(int number, int left, int right)
    {
        float stepX = (right - left) / (number - 1.0f);
        cubes = new GameObject[number];
        cubes[0] = cube;
        cubes[0].transform.position = new Vector3(left, 0f,0f);
        for(int i = 1;i < number; i++)
        {
            cubes[i] = GameObject.Instantiate(cube);
            cubes[i].transform.parent = cube.transform.parent;
            cubes[i].transform.position = new Vector3(left + stepX * i, 0f, 0f);
        }
    }
	
	void Update () {
        float[] spectrum = new float[number];
        GetComponent<AudioSource>().GetSpectrumData(spectrum, 0, FFTWindow.Hamming);

        for(int i = 0;i < number; i++)
        {
            cubes[i].transform.localScale = new Vector3(1f,range * spectrum[i], 1f);
        }
    }

    private void OnDestroy()
    {
        for(int i = 1;i < cubes.Length; i++)
        {
            GameObject.Destroy(cubes[i]);
        }
    }
}
