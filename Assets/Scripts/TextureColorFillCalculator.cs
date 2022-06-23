using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor;
using UnityEditor.UIElements;
using TMPro;

public class TextureColorFillCalculator : MonoBehaviour
{
    [SerializeField] Texture2D _texture;
    private TextMeshProUGUI percentage;

    private void Awake()
    {
        percentage = GameObject.FindGameObjectWithTag("Percentage").GetComponent<TextMeshProUGUI>();
    }
    public void Update()
	{
		
		var PREVIEW = new Image();
		var TOLERANCE = new Slider();
		var FIELD = new ObjectField();
		var COLOR = new ColorField();
		

		// PREVIEW
		PREVIEW.image = _texture;
		PREVIEW.scaleMode = ScaleMode.ScaleAndCrop;
		PREVIEW.style.flexGrow = 1f;

		// COLOR
		COLOR.label = "Reference color:";
		COLOR.value = Color.red;
		COLOR.RegisterValueChangedCallback((e) => RefreshInfo());
		void RefreshInfo()
		{
			if (_texture != null)
			{
				var colors = _texture.GetPixels();
				float fill = CalculateFill(colors, COLOR.value, TOLERANCE.value);
				float similarity = CalculateSimilarity(colors, COLOR.value);
				COLOR.label = $"{(fill * 100f):0.00}% filled";
				percentage.text = $"{(fill * 100f):0.00}% filled";
			}
		}
		RefreshInfo();

		// TOLERANCE
		TOLERANCE.label = "Fill tolerance [%]:";
		TOLERANCE.value = 0.1f;
		TOLERANCE.lowValue = 0f;
		TOLERANCE.highValue = 1f;
		TOLERANCE.RegisterValueChangedCallback((e) => RefreshInfo());

		// FIELD
		FIELD.objectType = typeof(Texture2D);
		FIELD.value = _texture;
		FIELD.RegisterValueChangedCallback(
			(e) =>
			{
				var newTexture = e.newValue as Texture2D;
				if (newTexture != null)
				{
					if (newTexture.isReadable)
					{
						_texture = newTexture;
						PREVIEW.image = newTexture;
						RefreshInfo();
					}
					else
					{
						UnityEditor.EditorUtility.DisplayDialog(
							"Texture is not readable",
							$"Texture '{newTexture.name}' is not readable. Choose different texture or enable \"Read/Write Enabled\" for needs of this demonstration.",
							"OK"
						);
						_texture = null;
						PREVIEW.image = null;
						return;
					}
				}
				else
				{
					_texture = null;
					PREVIEW.image = null;
					COLOR.label = "no texture";
				}
			}
		);
	}

	static float CalculateSimilarity(Color[] colors, Color reference)
	{
		Vector3 target = new Vector3 { x = reference.r, y = reference.g, z = reference.b };
		float accu = 0;
		const float sqrt_3 = 1.73205080757f;
		for (int i = 0; i < colors.Length; i++)
		{
			Vector3 next = new Vector3 { x = colors[i].r, y = colors[i].g, z = colors[i].b };
			accu += Vector3.Magnitude(target - next) / sqrt_3;
		}
		return 1f - ((float)accu / (float)colors.Length);
	}
	static float CalculateFill(Color[] colors, Color reference, float tolerance)
	{
		Vector3 target = new Vector3 { x = reference.r, y = reference.g, z = reference.b };
		int numHits = 0;
		const float sqrt_3 = 1.73205080757f;
		for (int i = 0; i < colors.Length; i++)
		{
			Vector3 next = new Vector3 { x = colors[i].r, y = colors[i].g, z = colors[i].b };
			float mag = Vector3.Magnitude(target - next) / sqrt_3;
			numHits += mag <= tolerance ? 1 : 0;
		}
		return (float)numHits / (float)colors.Length; ;
	}
}