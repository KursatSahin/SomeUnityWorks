using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

namespace Common
{
    public class MapGenerator : MonoBehaviour
    {
        [Header("This script didnt use to be simplify the map.")]
        [SerializeField] private GameObject floorPrefab;

        public int mapSize;
        // Start is called before the first frame update
        void Start()
        {
            GenereteMap();
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        [ContextMenu("GenereteMap")]
        public void GenereteMap()
        {
            GameObject map;

            List<List<GameObject>> mapMatrix = new List<List<GameObject>>();

            var upperLeftCornerPos = new Vector3((-2*(mapSize/2))-3,0,(-2*(mapSize/2))-3);

            for (int i = 0; i < mapSize; i++)
            {
                var floorLine = new List<GameObject>();
                for (int j = 0; j < mapSize; j++)
                {
                    var floor = ObjectPooler.SharedInstance.GetPooledObject(ObjectPooler.PoolingObjectTags.FloorCubeTag);
                    floor.SetActive(true);
                    floor.transform.position = upperLeftCornerPos + new Vector3(i*2,0,j*2);
                    floorLine.Add(floor);
                }
                mapMatrix.Add(floorLine);
            }

            //return map;
        }
    }
}
