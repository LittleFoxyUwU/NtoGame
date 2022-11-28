using System.Collections;
using System.Collections.Generic;
using GameScripts.Extensions;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    [SerializeField]
	private int mapSize;
	[SerializeField]
	private float tileStep=3.16f;
	[SerializeField]
	private List<Transform> tile;
	private float posX=0.0f;
	private float posZ=0.0f;
	private int tileCountWithZero;

	private Transform _landFolder;
	
	void Start()
	{
		_landFolder = Instantiate(new GameObject().With(x => x.name = "Land")).transform;
		generateMap();
	}
	
	public void generateMap() {
		tileCountWithZero=tile.Count-1;
		for (int w=0;w<mapSize;w++) {
			for (int h=0;h<mapSize;h++) {
				var obj = Instantiate(tile[Random.Range(0,tileCountWithZero)], new Vector3(posX, 0, posZ),
					Quaternion.Euler(-90,0,0));
				obj.parent = _landFolder;
				posX+=tileStep;
			}
			posZ+=tileStep;
			posX=0;
		}
	}
}