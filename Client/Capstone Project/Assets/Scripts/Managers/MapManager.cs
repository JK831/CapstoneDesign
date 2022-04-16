using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapManager : I_CheckClear
{
    //List<string> Map = new List<string>();
    Dictionary<int, GameObject> Map = new Dictionary<int, GameObject>();

    public float _blockStartHeight = 0.1f;
    public float _cameraRotationX = 81f;

    public bool CheckCleared() // 현재 캐릭터의 위치가 EndBlock이라면 True 반환
    {
        GameObject block = null;
        if (Map.TryGetValue(Managers.TargetObject.GetTargetObject("Character").GetComponent<Character>().CurrentPositionInMap, out block))
        {
            return block.GetComponent<Block>().BlockType.Equals('E');
        }

        return false;
    }


    public bool GenerateMap() // 씬 이름과 동일한 텍스트 파일 불러와서 맵 생성
    {
        string sceneName =  SceneManager.GetActiveScene().name;

        GameObject go = Managers.Resource.Instantiate("Island");
        if (go == null)
        {
            Debug.Log("Generating tile failed");
            return false;
        }

        int rowCount = 0;
        int blockId = 0;
        foreach (string line in System.IO.File.ReadLines(Application.dataPath + $"/Resources/MapGeneratingFiles/{sceneName}.txt"))
        {
            if (line.Length > (int)Define.Map.MapWidth) // 가로로 놓을 수 있는 블록의 최대 개수 20
            {
                Debug.Log("Too many Blocks on a row");
                return false;
            }

            float currentBlockZStartPosition = -(float)Define.Setting.BlockStartPosition - (int)Define.Setting.BlockWidth * rowCount;

            for (int colCount = 0; colCount < line.Length; colCount++)
            {
                string name = null;
                GameObject block = null;
                

                switch (line[colCount])
                {
                    case 'S':
                        name = "StartBlock";
                        break;
                    case 'B':
                        name = "SeaBlock";
                        break;
                    case '0':
                        name = "Nothing";
                        break;
                    case '1':
                        name = "Block";
                        break;
                    case 'T':
                        name = "TreeBlock";
                        break;
                    case 'V':
                        name = "CavinBlock";
                        break;
                    case 'N':
                        name = "CanonBlock";
                        break;
                    case 'l':
                        name = "CastleBlock";
                        break;
                    case 'H':
                        name = "HouseBlock";
                        break;
                    case 'W':
                        name = "TowerBlock";
                        break;
                    case '2':
                        name = "TowerBlock2";
                        break;
                    case 'E':
                        name = "EndBlock";
                        break;
                    default:
                        Debug.Log("Wrong charcter.");
                        return false;
                }

                if (name.Equals("Nothing"))
                {
                    Map.Add(blockId++, null);
                    continue;
                }
                else
                {   //해당하는 블록을 로드하여 적절한 위치에 생성해준다.
                    block = Managers.Resource.Instantiate($"{name}", go.transform);
                    block.transform.localPosition = new Vector3((float)Define.Setting.BlockStartPosition + (int)Define.Setting.BlockWidth * colCount, _blockStartHeight, currentBlockZStartPosition);
                    block.AddComponent<Block>();
                    block.GetComponent<Block>().BlockId = blockId;
                    block.GetComponent<Block>().BlockType = line[colCount];
                    Map.Add(blockId, block);

                    if (name.Equals("StartBlock"))
                    {
                        GameObject character = Managers.TargetObject.GetTargetObject("Character");
                        character.transform.position = block.transform.position + new Vector3(0, 0.9f, 0);
                        character.GetComponent<Character>().CurrentPositionInMap = blockId;
                        character.GetComponent<Character>().CurrentBlock = block;
                    }
                    else if (name.Equals("SeaBlock"))
                    {
                        block.transform.localPosition += new Vector3(0, -1.0f, 0);
                    }
                    else if (!(name.Equals("Block") || name.Equals("EndBlock")))
                    {
                        block.transform.localPosition += new Vector3(0, 1.6f, 0);
                    }
                    blockId++;
                }
            }
            rowCount++;
        }

        return true;
    }

    public Dictionary<int, GameObject> GetMap()
    {
        return Map;
    }

    public int Move(int direction, int velocity)
    {
        BE2_TargetObject targetObject = Managers.TargetObject.GetTargetObjectComponent();
        int currentPositionInMap = targetObject.CurrentPositionInMap;
        switch(direction)
        {
            case (int)Define.Direction.up:
                direction = -(int)Define.Map.MapWidth;
                break;
            case (int)Define.Direction.right:
                direction = 1;
                break;
            case (int)Define.Direction.down:
                direction = (int)Define.Map.MapWidth;
                break;
            case (int)Define.Direction.left:
                direction = -1;
                break;
            default:
                break;

        }


        while (velocity > 0)
        {

            GameObject block = null;
            if (currentPositionInMap + direction >= 0 && currentPositionInMap + direction < Map.Count)
            {
                if (direction < (int)Define.Map.MapWidth)
                {
                    if ((currentPositionInMap % (int)Define.Map.MapWidth) + direction >= (int)Define.Map.MapWidth)
                        continue;

                }
                
                if (Map.TryGetValue(currentPositionInMap + direction, out block) && block != null)
                {
                    char blockType = block.GetComponent<Block>().BlockType;
                    if (blockType.Equals('S') || blockType.Equals('1') || blockType.Equals('E'))
                    {
                        currentPositionInMap += direction;
                        targetObject.CurrentPositionInMap = currentPositionInMap;
                        Managers.Coin.AcquireCoin(currentPositionInMap);
                    }

                }

            }

            velocity--;
        }

        return currentPositionInMap;
    }

    public void Clear()
    {
        Map.Clear();
    }
}
