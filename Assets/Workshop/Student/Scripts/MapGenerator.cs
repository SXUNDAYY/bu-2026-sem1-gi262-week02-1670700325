using JetBrains.Annotations;
using System;
using UnityEngine;

namespace Workshop.Student
{
    public class MapGenerator : MonoBehaviour
    {
        public int columns = 10;
        public int rows = 10;

        public GameObject[] floorTiles;
        public GameObject[] wallTiles;
        public GameObject[] foodTiles;

        public string[,] saveItemMap = new string[3, 3] {
            { " ", "Soda", " "},
            { " ", " ", " "},
            { " ", " ", "Food"},
        };

        // 1. declare Players variable 
        public GameObject[] player;
        // 7. declare Exit variable 
        public GameObject exit;

        public void Start()
        {
            // 1. random player at the position <0, 0> map
            int p = UnityEngine.Random.Range(0, player.Length);
            GameObject playerSpawn = Instantiate(player[p], new Vector2(0,0), Quaternion.identity);

            // 2. create obstacles
            int obstacle = 5;
            for (int y = 0; y < obstacle; y++)
            {

                GameObject tile = Instantiate(wallTiles[0],new Vector2(5,y),Quaternion.identity);
            }

            // 3. create floor 
            for (int y = 0; y< rows; y++)
            {
                for (int x = 0; x < columns; x++)
                {
                    int r = UnityEngine.Random.Range(0, floorTiles.Length);
                    GameObject tile = Instantiate(floorTiles[r],new Vector2(x,y),Quaternion.identity);
                    tile.name = "Floor" + x +"_" + y;
                }

            }

            // 4. create walls 
            for(int y = -1; y< rows+1; y++) 
            {
                for (int x = -1; x < columns+1; x++)
                {
                    if (x == -1 || x == columns || y == -1 || y == rows)
                    {
                        int r = UnityEngine.Random.Range(0,wallTiles.Length);
                        GameObject tile = Instantiate(wallTiles[r],new Vector2(x,y),Quaternion.identity);
                        tile.name = "wall" + x +"_" + y;
                    }
                }

            }

            // 5. random foods 
            int numberOfFoods = UnityEngine.Random.Range(1,3);
            for (int i = 0; i < numberOfFoods; i++)
            {
                int x_Food = UnityEngine.Random.Range(0, columns);
                int y_Food = UnityEngine.Random.Range(0, rows);
                int r = UnityEngine.Random.Range(0, foodTiles.Length);
                Instantiate(foodTiles[0], new Vector2(x_Food, y_Food), Quaternion.identity);
            }

            // 6. generate item along with the saveItemMap 
           for (int y = 0; y < saveItemMap.GetLength(0); y++)
            {
                for (int x = 0; x < saveItemMap.GetLength(1); x++)
                {
                    string item = saveItemMap[x, y];
                    if (!string.IsNullOrEmpty(item))
                    {
                        foreach (var foodTile in foodTiles)
                        {
                            if (foodTile.name == item)
                            {
                                {
                                    Instantiate(foodTile, new Vector2(x, y), Quaternion.identity);
                                    break;
                                }
                            }
                        }
                    }
                }
            }

            // 7. place exit 

            GameObject exitSpawn = Instantiate(exit,new Vector2(columns - 1, rows - 1),Quaternion.identity
);

        }
    }

}