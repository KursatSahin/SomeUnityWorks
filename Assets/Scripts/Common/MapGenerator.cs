using System.Collections.Generic;
using UnityEngine;

namespace Common
{
    public class MapGenerator : MonoBehaviour
    {
        [Header("This script didnt use to be simplify the map.")]
        public int mapSize;
        // Start is called before the first frame update
        void Start()
        {
            GenereteMap();
        }

        [ContextMenu("GenereteMap")]
        public List<List<GameObject>> GenereteMap()
        {
            List<List<GameObject>> mapMatrix = new List<List<GameObject>>();

            var upperLeftCornerPos = new Vector3((-2*(mapSize/2))-3,0,(-2*(mapSize/2))-3);

            for (int i = 0; i < mapSize; i++)
            {
                var floorLine = new List<GameObject>();
                
                for (int j = 0; j < mapSize; j++)
                {
                    var floor = ObjectPooler.SharedInstance.GetPooledObject(PoolingObjectTags.FloorCubeTag);
                    floor.SetActive(true);
                    floor.transform.position = upperLeftCornerPos + new Vector3(i*2,0,j*2);
                    floorLine.Add(floor);
                }
                mapMatrix.Add(floorLine);
            }

            return mapMatrix;
        }
    }
}
