using System;
using UnityEngine;
using UnityEngine.UI;

public class BuildingListItem : MonoBehaviour
{	
    [SerializeField]
    private Text _label = default;

	private BuildingInfo _buildingInfo;
	private string _buildingKey;
	private string _buildingName = default;
	private string _buildingSurface = default;
	private string _buildingType = default;
	private string _buildingProfitXP = default;


	/// <summary>
	/// Set what this item should be showing.
	/// </summary>
	/// <param name="buildingKey"></param>
	/// <param name="buildingInfo"></param>
	public void SetData(string buildingKey, BuildingInfo buildingInfo)
	{
		_buildingKey = buildingKey;
		_buildingInfo = buildingInfo;
		_buildingName = buildingInfo.GetBuildingName();
		_buildingSurface = buildingInfo.GetBuildingSurface();
		_buildingType = buildingInfo.GetBuildingType();
		_buildingProfitXP = buildingInfo.GetBuildingProfitXP();

        _label.text = buildingKey;
	}

	/// <summary>
	/// Called from the UnityUI Button component.
	/// </summary>
	public void OnClicked()
	{
		_buildingInfo.ShowBuilding(_buildingKey, _buildingName, _buildingSurface, _buildingProfitXP);
	}
}
