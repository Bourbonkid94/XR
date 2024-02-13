using UnityEngine;
using UnityEngine.XR;

public class zoom : MonoBehaviour
{
    public Camera magnifyingLensCamera; // Déclarez magnifyingLensCamera comme un objet de type Camera
    public Transform vrHead; // Déclarez vrHead comme un objet de type Transform

    void Update()
    {
        // Assurez-vous que les références sont définies
        if (magnifyingLensCamera == null || vrHead == null)
        {
            Debug.LogError("Références manquantes. Assurez-vous de définir les références de la caméra de la loupe et du transform de la caméra VR.");
            return;
        }

        // Vérifiez si la fonctionnalité de rotation est disponible sur le dispositif
        if (InputDevices.GetDeviceAtXRNode(XRNode.CenterEye).TryGetFeatureValue(CommonUsages.deviceRotation, out Quaternion deviceRotation))
        {
            // Appliquez la rotation du dispositif à la caméra de la loupe
            magnifyingLensCamera.transform.rotation = deviceRotation;
        }
        else
        {
            Debug.LogWarning("La récupération de la rotation du dispositif a échoué.");
        }
    }
}