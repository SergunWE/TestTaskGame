using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class FaceTracking: MonoBehaviour
{
    [SerializeField] private ARFaceManager _arFaceManager;
    public float HeadRotation { get; private set; }

    void Start()
    {
        _arFaceManager.facesChanged += ArFaceManagerOnFaceChanged;
    }

    private void ArFaceManagerOnFaceChanged(ARFacesChangedEventArgs obj)
    {
        foreach (ARFace face in obj.updated)
        {
            var localHeadRotation = face.transform.localEulerAngles.y;

            // Приводим угол в диапазон [-180, 180] для удобства
            if(localHeadRotation > 180)
            {
                localHeadRotation -= 360;
            }
            HeadRotation = localHeadRotation;
        }
    }
}