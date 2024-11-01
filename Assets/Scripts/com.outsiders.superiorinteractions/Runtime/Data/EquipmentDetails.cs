using UnityEngine;


[CreateAssetMenu(fileName = "EquipmentDetails", menuName = "UOI/Equipment Data/Equipment Detail Container")]
public class EquipmentDetails : ScriptableObject
{
    public string UniqueID;
    public string Name;
    public string Specification;
    public string Model;
    public string Usage;
    public Vector3 Dimensions;
    public string Purpose;
    public string PopupData;
    public AudioClip VoiceOverClip;
}
