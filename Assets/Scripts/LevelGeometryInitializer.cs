using UnityEngine;

public class LevelGeometryInitializer : MonoBehaviour
{
    [SerializeField] private GameObject levelGeometry;

    private void Awake()
    {
        Instantiate(levelGeometry, transform);
    }
}