using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.Rendering;
using System.Security.Principal;

public class S_GridDebug : MonoBehaviour
{
    [Header("Data")]
    [SerializeField] private S_GridDebugTileInt _debugTile;
    [SerializeField] private S_GridData _gridData;
    [SerializeField] private S_FogData _fogData;

    [Header("Gizmos display")]
    [SerializeField, InfoBox("Use 'WireFramePlane'")] private GameObject _wireframePlane, _fogPlane;
    [SerializeField] private GameObject _planeFeedbackBoostBuilding;

    [SerializeField, BoxGroup] private Material _nonUsedTileMat, _usedTileMat, _fogTileMat;


    List<List<bool>> gridDebugHighlight = new List<List<bool>>();
    List<List<GameObject>> _wireframeCubes = new List<List<GameObject>>();
    

    private int _debugTileInt, _mapTileSize;

    private void Awake()
    {
        _gridData.Init();
        gridDebugHighlight = S_StaticFunc.Create2DimensionalList(_gridData.tileAmount, () => false);
        //_debugTile.SetValue(0);
        //SpawnDebugTile();
        StartCoroutine(LateSpawnDebug());
        _gridData.RefreshDebugTile += RefreshDebugTile;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            if(_debugTile == null || _gridData == null)
            {
                throw new NotImplementedException($"Missing reference in S_GridDebug : DebugTileInt = {_debugTileInt} | GridData = {((_gridData == null) ? "NULL" : _gridData)}");
                //return;
            }
            _debugTile.IncrementValue();
            RefreshAllDebugTile();
        }
    }

    public void DebugHighLightTile(Vector2Int coordinate)
    {
        gridDebugHighlight[coordinate.x][coordinate.y] = true;
    }

    IEnumerator LateSpawnDebug()
    {
        yield return new WaitForSeconds(0.05f);
        SpawnDebugTile();
    }
    //Do not rename it (Usage of Invoke at awake)
    void SpawnDebugTile()
    {
        _debugTileInt = _debugTile.GetValue();
        _mapTileSize = _gridData.tileSize;


        for (int x = 0; x < _gridData.gridsUsageStatement.Count; x++)
        {
            List<GameObject> list = new List<GameObject>();
            List<GameObject> listPlane = new List<GameObject>();

            for (int y = 0; y < _gridData.gridsUsageStatement[x].Count; y++)
            {
                Vector3 tmpVectorPos = new Vector3(_gridData.gridsUsageStatement[x].Count / 2 * -_mapTileSize + x * _mapTileSize,
                    0.05f,
                    _gridData.gridsUsageStatement[y].Count / 2 * -_mapTileSize + y * _mapTileSize);

                GameObject tmp;
                //Wireframe

                if (_fogData.fogGridsUsageStatement[x][y])
                {
                    tmp = Instantiate(_fogPlane, tmpVectorPos, Quaternion.identity);
                }
                    
                else
                    tmp = Instantiate(_wireframePlane, tmpVectorPos, Quaternion.identity);

                list.Add(tmp);
                //Always show fog
                if (!_fogData.fogGridsUsageStatement[x][y])
                    tmp.SetActive(false);

                //Plane feedback
                GameObject tmpPlane = Instantiate(_planeFeedbackBoostBuilding, tmpVectorPos, Quaternion.identity);
                tmpPlane.transform.localScale = new Vector3(4, 4, 4);
                listPlane.Add(tmpPlane);
                tmpPlane.SetActive(false);
            }
            _wireframeCubes.Add(list);
            _gridData.__planeFeedbackBoostBuildingList.Add(listPlane);
        }
    }

    public void RefreshDebugTile(int x, int y)
    {
        MeshRenderer _meshRenderer = _wireframeCubes[x][y].GetComponent<MeshRenderer>();
        if (_gridData.gridsUsageStatement[x][y].statement)
        {
            _wireframeCubes[x][y].GetComponent<MeshRenderer>().material = _usedTileMat;
        }

        else if (!_fogData.fogGridsUsageStatement[x][y])
        {
            _wireframeCubes[x][y].GetComponent<MeshRenderer>().material = _nonUsedTileMat;
        }
        if (_debugTile.GetValue() != 0)
            _wireframeCubes[x][y].SetActive(true);
    }

    //Use it when you want to refresh tiles
    void RefreshAllDebugTile()
    {
        if (_debugTile.GetValue() == 0)
        {
            for (int x = 0; x < _wireframeCubes.Count; x++)
                for (int y = 0; y < _wireframeCubes[x].Count; y++)
                {
                    //Always show fog
                    if (!_fogData.fogGridsUsageStatement[x][y])
                        _wireframeCubes[x][y].SetActive(false);
                    else
                        _wireframeCubes[x][y].SetActive(true);
                }

            return;
        }
        
        for (int x = 0; x < _wireframeCubes.Count; x++)
        {
            for (int y = 0; y < _wireframeCubes[x].Count; y++)
            {
                RefreshDebugTile(x, y);
            }
        }
    }
}


