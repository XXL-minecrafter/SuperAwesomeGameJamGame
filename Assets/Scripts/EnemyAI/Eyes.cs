using System.Collections.Generic;
using UnityEngine;

public class Eyes : MonoBehaviour
{
    [SerializeField] private int precision = 6;
    [SerializeField] private float viewDistance = 10f;
    [SerializeField] private float fov = 60f;
    [Space]
    [SerializeField] private LayerMask detectionLayer;
    [SerializeField] private Vector3 rotation = new(0, 90f, 90f);

    public bool TargetInSight { get; private set; }

    private List<Detector> detectors;

    private Brain brain;

    private void Awake()
    {
        brain = GetComponentInParent<Brain>();
    }

    private void Start()
    {
        detectors = InitDetector(transform, precision, viewDistance, fov);
    }

    private void Update()
    {
        if (!PlayerStats.Instance.IsChewing) return;
        TargetInSight = Detect(detectors, out var hit, OnDetect);
    }

    /// <summary>
    /// What should happen if something was detected
    /// </summary>
    public virtual void OnDetect(GameObject ctx)
    {
        brain.IsChasing = true;
    }

    /// <summary>
    /// Creates detector objects as children of the player
    /// </summary>
    /// <param name="anchor">The parent anchor object of the detectors</param>
    /// <param name="detectorCount">The half the amount of detectors</param>
    /// <param name="viewDistance">The distance of the rays</param>
    /// <param name="fov">The field of view of the player</param>
    /// <returns>List of all detectors created</returns>
    public List<Detector> InitDetector(Transform anchor, float detectorCount, float viewDistance, float fov)
    {
        var detectors = new List<Detector>();

        int count = Mathf.FloorToInt(detectorCount * .5f);

        var eyeSocket = new GameObject("Eye Socket").transform;
        eyeSocket.SetParent(anchor, false);
        eyeSocket.rotation = Quaternion.Euler(rotation);

        for (int i = -count; i <= count; i++)
        {
            var detectorObject = new GameObject($"Detector_{i + count}", typeof(Detector));
            detectorObject.transform.rotation = Quaternion.Euler(0, i * (fov / detectorCount), 0);
            detectorObject.transform.SetParent(eyeSocket, false);

            var detector = detectorObject.GetComponent<Detector>();
            detector.SetViewDistance(viewDistance);

            detectors.Add(detector);
        }

        return detectors;
    }

    /// <summary>
    /// Goes through a list of detectors and checks if they found something
    /// </summary>
    /// <param name="detectors">A list of detectors that should be checked</param>
    /// <param name="other">Information about the detected object</param>
    /// <param name="onDetectAction">The action that should be invoked if something is detected</param>
    /// <returns>True, if something was detected. Otherwise false</returns>
    public bool Detect(List<Detector> detectors, out RaycastHit2D? other, System.Action<GameObject> onDetectAction)
    {
        foreach (var detector in detectors)
        {
            bool targetInSight = detector.TryDetectLayer(detectionLayer, out var hit);

            if (!targetInSight) continue;

            onDetectAction?.Invoke(hit.transform.gameObject);

            other = hit;
            return targetInSight;
        }

        other = null;
        return false;
    }
}
