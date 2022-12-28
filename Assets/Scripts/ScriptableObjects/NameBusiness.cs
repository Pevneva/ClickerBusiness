using UnityEngine;

[CreateAssetMenu(menuName = "Create New Business Name",fileName = "Business Name")]
public class NameBusiness : ScriptableObject
{
    [SerializeField] private string _name;
    [SerializeField] private string _nameUpgrade_1;
    [SerializeField] private string _nameUpgrade_2;
    
    public string BusinessName => _name;
    public string Upgrade1Name => _nameUpgrade_1;
    public string Upgrade2Name => _nameUpgrade_2;

}
