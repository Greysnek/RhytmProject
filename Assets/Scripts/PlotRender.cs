using System.Collections.Generic;
using UnityEngine;

public class PlotRender : MonoBehaviour
{
    [SerializeField] private GameObject column;
    [SerializeField] private float defaultHeight = 20;

    private List<Transform> _columns = new List<Transform>();
    public float length;

    public void Create(float[] values)
    {
        var columnPosition = column.transform.position;
        var deltaX = column.transform.localScale.x;

        _columns?.Clear();
        _columns = new List<Transform>();
        
        for (var i = 0; i < values.Length; i++)
        {
            _columns.Add(Instantiate(column, transform).transform);
            _columns[i].position = columnPosition;
            
            SetHeight(_columns[i], values[i]);

            columnPosition.x += deltaX;
        }

        length = _columns[0].localScale.x * _columns.Count;
    }

    public void UpdateValues(float[] values)
    {
        if (values.Length == _columns.Count)
        {
            for (var i = 0; i < values.Length; i++)
            {
                SetHeight(_columns[i], values[i]);
            }
        }
        else if (values.Length > _columns.Count)
        {
            for (var i = 0; i < _columns.Count; i++)
            {
                SetHeight(_columns[i], values[i]);
            }

            var deltaX = column.transform.localScale.x;
            var columnPosition = _columns[_columns.Count - 1].position;

            for (var i = _columns.Count; i < values.Length; i++)
            {
                columnPosition.x += deltaX;
                
                _columns.Add(Instantiate(column, transform).transform);
                _columns[i].position = columnPosition;
                
                SetHeight(_columns[i], values[i]);
            }
        }
        else if (values.Length < _columns.Count)
        {
            for (var i = 0; i < values.Length; i++)
            {
                SetHeight(_columns[i], values[i]);
            }
            
            _columns.RemoveRange(values.Length, _columns.Count - 1);
        }
        
        length = _columns[_columns.Count - 1].position.x + _columns[0].localScale.x / 2;
    }

    private void SetHeight(Transform columnTransform, float height)
    {
        var columnScale = columnTransform.localScale;
        columnScale.y = height * defaultHeight + columnScale.x;
        columnTransform.localScale = columnScale;
    }
}
