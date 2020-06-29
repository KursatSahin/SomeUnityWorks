using System.Collections.Generic;
using UnityEngine;

namespace Common
{
    public class MapGenerator : MonoBehaviour
    {

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

            var upperLeftCornerPos = new Vector3((-2*(mapSize/2))-1,(-2*(mapSize/2))-1,0);
            // (Random.Range(0f, 10f)>2f)?0:1);
        
            for (int i = 0; i < mapSize; i++)
            {
                var floorLine = new List<GameObject>();
                for (int j = 0; j < mapSize; j++)
                {
                    var floor = ObjectPooler.SharedInstance.GetPooledObject(ObjectPooler.PoolingObjectTags.FloorCubeTag);
                    floor.SetActive(true);
                    floor.transform.position = upperLeftCornerPos + new Vector3(i*2,j*2,0);
                    floorLine.Add(floor);
                }
                mapMatrix.Add(floorLine);
            }

            //return map;
        }
    }
}
