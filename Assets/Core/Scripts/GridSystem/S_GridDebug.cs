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

    [Header("Gizmos display")]
    [SerializeField, InfoBox("Use 'WireFramePlane'")] private GameObject _wireframePlane;
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
        SpawnDebugTile();
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

                //Wireframe
                GameObject tmp = Instantiate(_wireframePlane, tmpVectorPos, Quaternion.identity);
                list.Add(tmp);
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

        else if (Grid.fogGridsUsageStatement[x][y])
        {
            _wireframeCubes[x][y].GetComponent<MeshRenderer>().material = _fogTileMat;
        }

        else if (!Grid.fogGridsUsageStatement[x][y])
        {
            _wireframeCubes[x][y].GetComponent<MeshRenderer>().material = _nonUsedTileMat;
        }
        if (_debugTile.GetValue() != 0)
            _wireframeCubes[x][y].SetActive(true);
    }

    void RefreshAllDebugTile()
    {
        if (_debugTile.GetValue() == 0)
        {
            for (int x = 0; x < _wireframeCubes.Count; x++)
                for (int y = 0; y < _wireframeCubes[x].Count; y++)
                    _wireframeCubes[x][y].SetActive(false);
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

    /*
    private void OnDrawGizmos()
    {
        if(_gridData == null  || _gridData.gridsUsageStatement == null)
            return;

        if(_debugTile == null)
            return;

        DrawGizmoDisk(_gridData.mapSphereArea);
        _debugTileInt = _debugTile.GetValue();
        _mapTileSize = _gridData.tileSize;
        if (_debugTileInt > 0)
        {
            for (int x = 0; x < _gridData.gridsUsageStatement.Count; x++)
            {
                for (int y = 0; y < _gridData.gridsUsageStatement[x].Count; y++)
                {
                    if (_gridData.gridsUsageStatement[x][y].statement && (_debugTileInt == 1 || _debugTileInt == 3))
                        Gizmos.color = Color.green;

                    else if (Grid.fogGridsUsageStatement[x][y] && (_debugTileInt == 2 || _debugTileInt == 3))
                        Gizmos.color = Color.blue;

                    else if (!Grid.fogGridsUsageStatement[x][y] && (_debugTileInt == 1 || _debugTileInt == 3))
                        Gizmos.color = Color.red;
                    else
                        Gizmos.color = Color.clear;

                    if (gridDebugHighlight.Count > x && gridDebugHighlight[x][y])
                        Gizmos.color = new Vector4(255, 255 / 198, 255 / 41, 1);

                    Gizmos.DrawWireCube(new Vector3(_gridData.gridsUsageStatement[x].Count / 2 * -_mapTileSize + x * _mapTileSize,
                        0,
                        _gridData.gridsUsageStatement[y].Count / 2 * -_mapTileSize + y * _mapTileSize), new Vector3(_mapTileSize - 0.6f, _mapTileSize - 0.6f, _mapTileSize - 0.6f));

                }
            }
        }
    }
    public static void DrawGizmoDisk(float radius)
    {
        float corners = 72;
        Vector3 origin = Vector3.zero;
        Vector3 startRotation = Vector3.right * radius;
        Vector3 lastPosition = origin + startRotation;
        float angle = 0;
        while (angle <= 360)
        {
            angle += 360 / corners;
            Vector3 nextPosition = origin + (Quaternion.Euler(90, 0, angle) * startRotation);
            Gizmos.DrawLine(lastPosition, nextPosition);
            lastPosition = nextPosition;
        }
    }*/
}


