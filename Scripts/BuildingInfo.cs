using UnityEngine;

public class BuildingInfo : MonoBehaviour
{
    [SerializeField]
    private BuildingSprite _buildingPrefab = default;
    [SerializeField]
    private Transform _worldRoot = default;

    private BuildingSprite _buildingInstance = null;

    private string _buildingName = default;
    private string _buildingSurface = default;
    private string _buildingType = default;
    private string _buildingProfitXP = default;
    

    /// <summary>
    /// Build a building.
    /// </summary>
    /// <param name="key">The key of the building to be built.</param>
    public void ShowBuilding(string key, string name, string surface, string profitXP)
    {
        if(_buildingInstance == null)
        {
            _buildingInstance = MonoBehaviour.Instantiate(_buildingPrefab);
            _buildingInstance.transform.SetParent(_worldRoot, false);
        }

        _buildingInstance.SetData(key, name, surface, profitXP);
    }

    // TODO : Create {get; set} for the values
    public string GetBuildingName() {
        return _buildingName;
	}

    public string GetBuildingSurface() {
        return _buildingSurface;
    }

    public string GetBuildingType() {
        return _buildingType;
	}

    public string GetBuildingProfitXP() {
        return _buildingProfitXP;
	}

	public void SetName(string buildingName) { _buildingName = buildingName; }
    public void SetType(string buildingType) { _buildingType = buildingType; }
    public void SetSurface(string buildingSurface) { _buildingSurface = buildingSurface; }
    public void SetProfitXP(string buildingProfitXP) { _buildingProfitXP = buildingProfitXP; }

}
