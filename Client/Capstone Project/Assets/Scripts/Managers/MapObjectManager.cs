using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapObjectManager
{
    Dictionary<int, GameObject> objectMap = new Dictionary<int, GameObject>();

    public float _objectStartHeight = 1.5f;

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

        Debug.Log(str);
        for (int i = 0; i < lines; i++)
            splitLines[i] = splitLines[i].Trim('\r');

        //StringReader stringReader = new StringReader(asset.text);

        for (int i = 0; i < lines; i++ /*File.ReadLines(Application.dataPath + $"/Resources/MapGeneratingFiles/{sceneName}.txt")*/)
        {
            Debug.Log($"i = {i}");
            Debug.Log($"splitLines{i}.Length = {splitLines[i].Length}");
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
                Debug.Log(splitLines[i][colCount]);

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
                    case 'T':
                        name = "Tiger";
                        break;
                    case 'B':
                        name = "BlackBull";
                        break;
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
                        currentObject.transform.position = parentBlock.transform.position + new Vector3(0, Managers.Coin.coinHeight, 0);
                        Debug.Log($"child = {currentObject.transform.position}");
                        Debug.Log($"parent = {parentBlock.transform.position}");
                        Managers.Coin.CoinMap.Add(blockId, currentObject);
                    }

                    currentObject.AddComponent<Coin>().CoinId = blockId;
                    objectMap.Add(blockId++, currentObject);

                    continue;
                }

                
                else if (name.Contains("Knight"))
                {
                    string knightPrefab = "Knight";
                    Debug.Log($"MapObject/{knightPrefab}{(int)Enum.Parse(typeof(Define.KnightTypeByStage), sceneName)}");
                    currentObject = Managers.Resource.Instantiate($"MapObject/{knightPrefab}{(int)Enum.Parse(typeof(Define.KnightTypeByStage), sceneName)}", go.transform);
                    int direction; // 기사가 바라보는 방향

                    if (currentObject == null)
                    {
                        Debug.Log("Wrong Knight Type");
                        return false;
                    }

                    if (name.Contains("Right"))
                    {
                        currentObject.transform.forward = currentObject.transform.right;
                        direction = 1;
                    }
                    else if (name.Contains("Down"))
                    {
                        currentObject.transform.forward = -currentObject.transform.forward;
                        direction = (int)Define.Map.MapWidth;
                    }
                    else if (name.Contains("Left"))
                    {
                        currentObject.transform.forward = -currentObject.transform.right;
                        direction = -1;
                    }
                    else
                    {
                        direction = -(int)Define.Map.MapWidth;
                    }

                    int deadBlockPosition = blockId + direction; //기사가 바라보는 방향에 따라 지나가면 안 되는 블록을 설정한다.
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

                else if (name.Contains("Tree"))
                {
                    currentObject = Managers.Resource.Instantiate($"MapObject/{name}", go.transform);

                    if (currentObject == null)
                    {
                        Debug.Log("Tree 생성 실패");
                        return false;
                    }
                }

                currentObject.transform.localPosition = new Vector3((float)Define.Setting.BlockStartPosition + (int)Define.Setting.BlockWidth * colCount, _objectStartHeight, currentObjectStartPosition);
                currentObject.transform.localPosition += new Vector3(0, -0.6f, 0);


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
