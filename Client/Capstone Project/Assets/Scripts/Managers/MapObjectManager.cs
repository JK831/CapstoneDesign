using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapObjectManager
{
    Dictionary<int, GameObject> objectMap = new Dictionary<int, GameObject>();

    public float _objectStartHeight = 1.7f;

    public bool GenerateObject()
    {
        string sceneName = SceneManager.GetActiveScene().name;

        GameObject go = GameObject.Find("Island");
        if (go == null)
        {
            Debug.Log("Generating object failed");
            return false;
        }

        int rowCount = 0;
        int blockId = 0;

        TextAsset asset = Resources.Load<TextAsset>($"MapGeneratingFiles/{sceneName}Object");
        string str = asset.text;
        string[] splitLines = str.Split('\n');
        int lines = splitLines.Length;

        for (int i = 0; i < lines; i++)
            splitLines[i] = splitLines[i].Trim('\r');

        //StringReader stringReader = new StringReader(asset.text);

        for (int i = 0; i < lines; i++ /*File.ReadLines(Application.dataPath + $"/Resources/MapGeneratingFiles/{sceneName}.txt")*/)
        {

            if (splitLines[i].Length > (int)Define.Map.MapWidth) // 가로로 놓을 수 있는 블록의 최대 개수 20
            {
                Debug.Log("Line length exeed map width");
                return false;
            }

            float currentObjectStartPosition = -(float)Define.Setting.BlockStartPosition - (int)Define.Setting.BlockWidth * rowCount;

            for (int colCount = 0; colCount < splitLines[i].Length; colCount++)
            {
                //Debug.Log($"splitLines[{i}][{colCount}] =  {splitLines[i][colCount]}");
                string name = null;
                GameObject currentObject = null;

                GameObject currentBlock; // 오브젝트가 배치된 블록의 타입을 변경하여 캐릭터가 이동할 수 없도록 함
                Managers.Map.GetMap().TryGetValue(blockId, out currentBlock);
                if (currentBlock == null) // 현재 위치에 블록이 없다면 오브젝트 생성할 필요 없음
                {
                    objectMap.Add(blockId++, null);
                    continue;
                }


                switch (splitLines[i][colCount])
                {
                    case '0':
                        name = "Nothing";
                        break;
                    case 'U':
                        name = "Knight(Up)";
                        break;
                    case 'D':
                        name = "Knight(Down)";
                        break;
                    case 'L':
                        name = "Knight(Left)";
                        break;
                    case 'R':
                        name = "Knight(Right)";
                        break;
                    //case 'T':
                    //    name = "Tiger(Up)";
                    //    break;
                    //case 'Q':
                    //    name = "Tiger(Left)";
                    //    break;
                    //case 'E':
                    //    name = "Tiger(Right)";
                    //    break;
                    //case 'Y':
                    //    name = "Tiger(Down)";
                    //    break;
                    //case 'B':
                    //    name = "BlackBull(Up)";
                    //    break;
                    //case 'Z':
                    //    name = "BlackBull(Left)";
                    //    break;
                    //case 'X':
                    //    name = "BlackBull(Right)";
                    //    break;
                    //case 'V':
                    //    name = "BlackBull(Down)";
                    //    break;
                    case '1':
                        name = "Tree1";
                        break;
                    case '2':
                        name = "Tree2";
                        break;
                    case '3':
                        name = "Tree3";
                        break;
                    case '4':
                        name = "Tree4";
                        break;
                    case '5':
                        name = "Tree5";
                        break;
                    case '6':
                        name = "Tree6";
                        break;
                    case '7':
                        name = "Tree7";
                        break;
                    case '8':
                        name = "Tree8";
                        break;
                    case 'C':
                        name = "Coin";
                        break;
                    case 'F':
                        name = "ForestCastle_Red";
                        break;
                    case 'T':
                        name = "ForestTower_Red";
                        break;
                    case 'E':
                        name = "ForestBrazzierRed (2)";
                        break;
                    default:
                        Debug.Log("Wrong charcter.");
                        return false;
                }


                if (name.Equals("Nothing"))
                {
                    objectMap.Add(blockId++, null);
                    continue;
                }

                else if (name.Equals("Coin"))
                {
                    currentObject = Managers.Resource.Instantiate($"MapObject/Coin", go.transform);
                    GameObject parentBlock = null;

                    if (Managers.Map.GetMap().TryGetValue(blockId, out parentBlock))
                    {
                        if (parentBlock == null)
                        {
                            Debug.Log($"There is no block under the coin{blockId}");
                            return false;
                        }
                        currentObject.transform.position += parentBlock.transform.position + new Vector3(0, Managers.Coin.coinHeight, 0);

                        Managers.Coin.CoinMap.Add(blockId, currentObject);
                    }

                    currentObject.AddComponent<Coin>().CoinId = blockId;
                    objectMap.Add(blockId++, currentObject);

                    continue;
                }   

                else if (name.Contains("Tree") || name.Contains("Forest"))
                {
                    currentObject = Managers.Resource.Instantiate($"MapObject/{name}", go.transform);

                    if (currentObject == null)
                    {
                        Debug.Log("Tree or Forest 오브젝트 생성 실패");
                        return false;
                    }

                    if (name.Equals("ForestCastle_Red"))
                        currentObject.transform.localScale = new Vector3(0.85f, 0.85f, 0.85f);
                }

                else
                {
                    int direction; // 적이 바라보는 방향
                    String[] objectName = name.Split('('); // 괄호를 기준으로 이름 나눈다

                    if (objectName[0].Contains("Knight"))
                    {
                        string knightPrefab = "Knight";
                        Debug.Log($"MapObject/{knightPrefab}{(int)Enum.Parse(typeof(Define.KnightTypeByStage), sceneName)}");
                        currentObject = Managers.Resource.Instantiate($"MapObject/{knightPrefab}{(int)Enum.Parse(typeof(Define.KnightTypeByStage), sceneName)}", go.transform);
                        
                    }
                    else
                    {
                        currentObject = Managers.Resource.Instantiate($"MapObject/{objectName}");
                    }

                    if (currentObject == null)
                    {
                        Debug.Log("Wrong Type");
                        return false;
                    }

                    if (objectName[1].Contains("Right"))
                    {
                        currentObject.transform.forward = currentObject.transform.right;
                        direction = 1;
                    }
                    else if (objectName[1].Contains("Down"))
                    {
                        currentObject.transform.forward = -currentObject.transform.forward;
                        direction = (int)Define.Map.MapWidth;
                    }
                    else if (objectName[1].Contains("Left"))
                    {
                        currentObject.transform.forward = -currentObject.transform.right;
                        direction = -1;
                    }
                    else
                    {
                        direction = -(int)Define.Map.MapWidth;
                    }

                    int deadBlockPosition = blockId + direction; //적이 바라보는 방향에 따라 지나가면 안 되는 블록을 설정한다.
                    if (deadBlockPosition > 0 && deadBlockPosition < Managers.Map.GetMap().Count)
                    {
                        GameObject originalBlock;
                        Managers.Map.GetMap().TryGetValue(deadBlockPosition, out originalBlock);
                        if (originalBlock == null)
                            break;
                        else if (originalBlock.GetComponent<Block>().BlockType == 'E')
                        {
                            Debug.Log("Wrong setting on knight position");
                            return false;
                        }

                        Managers.Map.GetMap().Remove(deadBlockPosition); // 기존 블록 삭제
                        GameObject deadBlock = Managers.Resource.Instantiate("DeadBlock", go.transform);
                        deadBlock.transform.position = originalBlock.transform.position;
                        Block deadBlockScript = deadBlock.AddComponent<Block>();
                        deadBlockScript.BlockId = deadBlockPosition;
                        deadBlockScript.BlockType = 'D';
                        GameObject.DestroyImmediate(originalBlock);
                        Managers.Map.GetMap().Add(deadBlockPosition, deadBlock);
                    }
                }

                currentObject.transform.localPosition += new Vector3((float)Define.Setting.BlockStartPosition + (int)Define.Setting.BlockWidth * colCount, _objectStartHeight, currentObjectStartPosition);

                if (name.Contains("Brazzier"))
                    currentObject.transform.position += new Vector3(0, 0.9f, 0);

                currentBlock.GetComponent<Block>().BlockType = 'O';
                 
                objectMap.Add(blockId, currentObject);
                blockId++;
            }
            rowCount++;
        }
        return true;
    }

    public void Clear()
    {
        objectMap.Clear();
    }
}
