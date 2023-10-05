using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class BuildingList : MonoBehaviour
{
    [SerializeField]
    private BuildingListItem _itemPrefab = default;
    [SerializeField]
    private Transform _itemsParent = default;

    [SerializeField]
    private BuildingInfo _buildingInfo = default;
    private DirectoryInfo _propertyPath = new DirectoryInfo("Assets/Properties/cig5.objects.properties.txt");
    private DirectoryInfo _buildingPath = new DirectoryInfo("Assets/Sprites/Buildings");
    private List<BuildingListItem> _itemInstances = new List<BuildingListItem>();

    [SerializeField]
    private string[] _buildings = new string[0];
    private string[] _buildingTypeFilters = new string[0];

    enum BuildingFilters {
        TYPE,
        SURFACE,
        PROFITXP,
	}

    public void Start()
    {
        _buildingTypeFilters = new string[] { "community", "residential", "commercial", "decoration" };
		GetBuildings();
        SetData(_buildings);
    }


    private void GetBuildings() {
        // Check the assets/sprites/buildings folder for different types of buildings
        List<string> buildingTypes = new List<string>();
        FileInfo[] buildings = _buildingPath.GetFiles();
        // Check for building names ending on f001 that are not animations
        for(int i = 0, n = buildings.Length; i < n; i++) {
            string[] buildingFullName = buildings[i].Name.Split('.');
            string[] buildingName = buildingFullName[0].Split('_');
            for (int j = 0, m = buildingName.Length; j < m; j++) {
                if(buildingName[j] == "animation") {
                    break;
				}
                if(buildingName[j] == "f001" && !buildingTypes.Contains(buildingName[0])){
                    buildingTypes.Add(buildingName[0]);
                    
                    break;
				}
            }
		}
        _buildings = buildingTypes.ToArray();
	}

    private string GetBuildingProperties(string building, BuildingFilters filter) {
        string filterText = "";
        switch(filter) {
            case BuildingFilters.SURFACE:
                filterText = ".surface";
                break;
            case BuildingFilters.TYPE:
                filterText = ".type";
                break;
            case BuildingFilters.PROFITXP:
                filterText = ".profitXP";
                break;
        }
        //Read the properties file 
        string[] text = File.ReadAllLines(_propertyPath.ToString());
        for(int i = 0, n = _buildings.Length; i < n; i++) { // overbodig!!!!!
            //Check if the buildings are found in the properties file and have a value to the right filter
            for(int j = 0, o = text.Length; j < o; j++) {
                if(text[j].Contains(building + filterText)) {
                    //Split the value from its variable and remove all spaces
                    string filterInfo = text[j].Split('=')[1];
                    filterInfo = filterInfo.Trim(' ');
                    return filterInfo;

                }          
            }
        }
        return null;
	}

    /// <summary>
    /// Set what buildings this list should be showing.
    /// </summary>
    /// <param name="buildingDatas"></param>
    private void SetData(string[] buildings)
    {
        for(int i = 0, n = _itemInstances.Count; i < n; i++)
        {
            BuildingListItem itemInstance = _itemInstances[i];
            MonoBehaviour.Destroy(itemInstance.gameObject);
        }

        _itemInstances.Clear();

        //Look up all the necessary values for all the buildings
        // TODO : Create an array of strings that belong to that building instead of reading the whole file over and over again to look up 1 value.
        for(int i = 0, n = buildings.Length; i < n; i++)
        {
            //Read the Type value of the building
            _buildingInfo.SetType(GetBuildingProperties(buildings[i], BuildingFilters.TYPE));
            //Check if the type of the building is the same as the types we want to show.
            for(int j = 0, o = _buildingTypeFilters.Length; j < o; j++) {
                if(_buildingInfo.GetBuildingType() == _buildingTypeFilters[j]) {
                    //Look up all the other values
                    _buildingInfo.SetSurface(GetBuildingProperties(buildings[i], BuildingFilters.SURFACE));
                    _buildingInfo.SetName(buildings[i]);
                    if(_buildingInfo.GetBuildingType() == "commercial") {
                        _buildingInfo.SetProfitXP(GetBuildingProperties(buildings[i], BuildingFilters.PROFITXP));
                    } else {
                        _buildingInfo.SetProfitXP("");
                    }
                    BuildingListItem itemInstance = MonoBehaviour.Instantiate(_itemPrefab);
                    itemInstance.SetData(buildings[i], _buildingInfo);
                    itemInstance.gameObject.transform.SetParent(_itemsParent, false);

                    _itemInstances.Add(itemInstance);
                }
            }
        }
    }
}
