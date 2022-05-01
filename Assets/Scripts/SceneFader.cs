using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

namespace TJ
{
public class SceneFader : MonoBehaviour {

	public Image img;
	public AnimationCurve curve;

	private void Awake()
	{
		img.enabled=true;
	}
	void Start ()
	{
		StartCoroutine(FadeIn());
	}

	public void FadeTo (string scene)
	{
		StartCoroutine(FadeOut(scene));
	}


	IEnumerator FadeIn ()
	{
		float t = 1f;

		while (t > 0f)
		{
			t -= Time.deltaTime;
			float a = curve.Evaluate(t);
			img.color = new Color (0f, 0f, 0f, a);
			yield return 0;
		}
	}

	public IEnumerator FadeOut(string scene)
	{
		float t = 0f;

		while (t < 1f)
		{
			t += Time.deltaTime;
			float a = curve.Evaluate(t);
			img.color = new Color(0f, 0f, 0f, a);
			yield return 0;
		}

		//SceneManager.LoadScene(scene);
	}
	public IEnumerator UI_Fade()
	{
		float t2 = 0f;
		while (t2 < 1f)
		{
			t2 += Time.deltaTime;
			float a = curve.Evaluate(t2);
			img.color = new Color(0f, 0f, 0f, a);
			yield return 0;
		}

		float t = 1f;
		while (t > 0f)
		{
			t -= Time.deltaTime;
			float a = curve.Evaluate(t);
			img.color = new Color (0f, 0f, 0f, a);
			yield return 0;
		}
	}
}	
}