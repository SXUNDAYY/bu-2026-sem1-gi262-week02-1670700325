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

        // 1. declare Players variable อยู่จุดเริ่มต้น

        // 7. declare Exit variable สร้างแถวตรงกลาง Wall สักครึ่งนึง


        public void Start()
        {
            // 1. random player at the position <0, 0> map

            // 2. create obstacles

            // 3. create floor (วิธีสร้างพื้น)
            for(int y = 0; y< rows; y++)
            {
                for (int x = 0; x < columns; x++)
                {
                    int r = UnityEngine.Random.Range(0,floorTiles.Length);
                    GameObject tile = Instantiate(floorTiles[0],new Vector2(x,y),Quaternion.identity);
                    tile.name = "Floor" + x +"_" + y;
                }

            }

            // 4. create walls (วิธีการสร้างขอบข้างนอก)
            for(int y = -1; y< rows+1; y++) //-1 จุดเริ่มต้น +1 ในจุดสุดท้าย การสร้างขอบนอก
            {
                for (int x = -1; x < columns+1; x++)
                {
                    if (x == -1 || x == columns || y == -1 || y == rows)//ถ้าไม่อยากได้ข้างในให้ใช้ if มาช่วย
                    {
                        int r = UnityEngine.Random.Range(0,floorTiles.Length);
                        GameObject tile = Instantiate(wallTiles[0],new Vector2(x,y),Quaternion.identity);
                        tile.name = "Wall" + x +"_" + y;
                    }
                }

            }

            // 5. random foods (กรณีที่สุ่ม)
            int numberOfFoods = UnityEngine.Random.Range(1, 5);//Random อาหารกี่ชิ้น อย่างตรงนี้จะเป็น 1-5 ชิ้น
            for (int i = 0; i < numberOfFoods; i++)
            {
                int x_Food = UnityEngine.Random.Range(0, columns);
                int y_Food = UnityEngine.Random.Range(0, rows);
                Instantiate(foodTiles[0], new Vector2(x_Food, y_Food), Quaternion.identity);
            }

            // 6. generate item along with the saveItemMap (กรณีที่เซ็ตไอเท็ม)
            for (int y = 0; y < saveItemMap.GetLength(0); y++)
            {
                for (int x = 0; x < saveItemMap.GetLength(1); x++)
                {
                    string item = saveItemMap[x,y];
                    if (!string.IsNullOrEmpty(item))//มีไว้เช็ดกันข้อมูลระเบิด
                    {
                        foreach (var foodTile in floorTiles)
                        {
                            if (foodTile.name == item)
                            {
                                Instantiate(foodTile, new Vector2(x, y), Quaternion.identity);
                                break;
                            }
                        }
                    }
                }
            }

            // 7. place exit อยู่ขวาบนสุด

        }
    }

}