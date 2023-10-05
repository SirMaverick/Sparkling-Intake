using UnityEngine;
using UnityEngine.UI;

public class BuildingSprite : MonoBehaviour
{
	[SerializeField]
	private SpriteRenderer _renderer = default;
	[SerializeField]
	private Sprite[] _sprites = default;
	[SerializeField]
	private Text _name = default;
	[SerializeField]
	private Text _surface = default;
	[SerializeField]
	private Text _profitXP = default;

	/// <summary>
	/// Set the data for this building.
	/// </summary>
	/// <param name="key"></param>
	public void SetData(string key, string name, string surface, string profitXP)
	{
		for(int i = 0, n = _sprites.Length; i < n; i++)
		{
			Sprite sprite = _sprites[i];
			
			if(sprite.name == string.Concat(key, "_f001"))
			{
				_renderer.sprite = sprite;
				_name.text = name;
				_surface.text = surface;
				_profitXP.text = profitXP;
				break;
			}
		}




	}
}
