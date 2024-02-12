using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointsMoving : MonoBehaviour
{
    public bool isRelaxing;
    
    [SerializeField] private Transform[] points;
    [SerializeField] private Transform target;
    [SerializeField] private float speed;
    [SerializeField] private int relaxDuration;
    [SerializeField] private int relaxFrequency;

    private int index;
    private int relaxNum;
    private int pointsCount;

    

    private void Start()
    {
        Transform[] points = new Transform[pointsCount];

        isRelaxing = false;

        StartCoroutine(RunRelaxRoutine());
    }
    private void Update()
    {
        if (!isRelaxing) MoveToTarget();

        if (transform.position == target.position) ChangeTarget();
    }

    /// <summary>
    /// Движение к цели
    /// </summary>
    private void MoveToTarget()
    {
        transform.position = Vector3.MoveTowards(transform.position, target.position, Time.deltaTime) * speed;
    }

    /// <summary>
    /// Стоит на месте
    /// </summary>
    private void StandsStill()
    {
        transform.position = Vector3.MoveTowards(transform.position, transform.position, Time.deltaTime);
    }
    
    /// <summary>
    /// Найти индекс в массиве points
    /// </summary>
    private int FindIndex(Transform[] arr, Transform point)
    {
        for (int i = 0; i < arr.Length; i++)
        {
            if (arr[i] == point) return i;
        }
        return -1;
    }
    
    /// <summary>
    /// Смена цели
    /// </summary>
    private void ChangeTarget()
    {
        index = FindIndex(points, target);

        if (index <= 1) target = points[index + 1];
        else target = points[0];
    }

    /// <summary>
    /// Рандомно определяет отдых (частота задается в Инпекторе)
    /// </summary>
    private void IfRelax()
    {
        relaxNum = Random.Range(0, relaxFrequency + 1);

        if (relaxNum == 1) isRelaxing = true;
    }
    
    /// <summary>
    /// Отдых, стоя на месте. Продолжительность задается в Инспекторе.
    /// </summary>
    IEnumerator TimeToRelax()
    {
        IfRelax();

        StandsStill();
        
        yield return new WaitForSeconds(relaxDuration);
        isRelaxing = false;
    }

    /// <summary>
    /// Зацикливает корутину для ее выполнения каждую 1с
    /// </summary>
    IEnumerator RunRelaxRoutine()
    {
        while (true)
        {
            StartCoroutine(TimeToRelax());
            yield return new WaitForSeconds(1);
        }
    }
}


