/************************************************************************************

Copyright   :   Copyright 2014 Oculus VR, LLC. All Rights reserved.

Licensed under the Oculus VR Rift SDK License Version 3.2 (the "License");
you may not use the Oculus VR Rift SDK except in compliance with the License,
which is provided at the time of installation or download, or which
otherwise accompanies this software in either electronic or hard copy form.

You may obtain a copy of the License at

http://www.oculusvr.com/licenses/LICENSE-3.2

Unless required by applicable law or agreed to in writing, the Oculus VR SDK
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.

************************************************************************************/

using UnityEngine;
using UnityEngine.UI;
using System.Collections; // required for Coroutines

/// <summary>
/// Fades the screen from black after a new scene is loaded. Fade can also be controlled mid-scene using SetUIFade and SetFadeLevel
/// This is an alternative to the OVRScreenFade included in the OVR package
/// </summary>
public class OVRScreenFade2 : MonoBehaviour
{
    [Tooltip("Fade duration")]
	public float fadeTime = 2.0f;

    [Tooltip("Screen color at maximum fade")]
	public Color fadeColor = new Color(0.01f, 0.01f, 0.01f, 1.0f);

    public Image fadeImage;
    public bool fadeOnStart = true;

    private float uiFadeAlpha = 0;

	private Material fadeMaterial = null;
    private bool isFading = false;

    public float currentAlpha { get; private set; }

	void Awake()
	{
		// create the fade material
		fadeMaterial =  new Material (fadeImage.material);
        fadeImage.material = fadeMaterial;
	}

    /// <summary>
    /// Start a fade out
    /// </summary>
    public void FadeOut()
    {
        StartCoroutine(Fade(0,1));
    }


	/// <summary>
	/// Starts a fade in when a new level is loaded
	/// </summary>
	void OnLevelWasLoaded(int level)
	{
		StartCoroutine(Fade(1,0));
	}

    /// <summary>
    /// Automatically starts a fade in
    /// </summary>
    void Start()
    {
        if (fadeOnStart)
        {
            StartCoroutine(Fade(1,0));
        }
    }


    /// <summary>
	/// Set the UI fade level - fade due to UI in foreground
	/// </summary>
    public void SetUIFade(float level)
    {
        uiFadeAlpha = Mathf.Clamp01(level);
        SetMaterialAlpha();
    }
    /// <summary>
    /// Override current fade level
    /// </summary>
    /// <param name="level"></param>
    public void SetFadeLevel(float level)
    {
        currentAlpha = level;
        SetMaterialAlpha();
    }

	/// <summary>
	/// Fades alpha from 1.0 to 0.0
	/// </summary>
	IEnumerator Fade(float startAlpha, float endAlpha)
	{
		float elapsedTime = 0.0f;
		while (elapsedTime < fadeTime)
		{
			elapsedTime += Time.deltaTime;
            currentAlpha = Mathf.Lerp(startAlpha, endAlpha, Mathf.Clamp01(elapsedTime / fadeTime));
            SetMaterialAlpha();
			yield return new WaitForEndOfFrame();
		}
	}

    /// <summary>
    /// Update material alpha. UI fade and the current fade due to fade in/out animations (or explicit control)
    /// both affect the fade. (The max is taken) 
    /// </summary>
    private void SetMaterialAlpha()
    {
		Color color = fadeColor;
        color.a = Mathf.Max(currentAlpha, uiFadeAlpha);
		isFading = color.a > 0;
        if (fadeMaterial != null)
        {
            fadeMaterial.color = color;
            fadeImage.material = fadeMaterial;
            fadeImage.color = color;
            fadeImage.enabled = isFading;
        }
    }

    public void PositionForCamera(OVRCameraRig cameraRig)
    {
        //Position at double near clip plane distance so that fade quad is clearly visible
        transform.localPosition = new Vector3(0, 0, cameraRig.leftEyeCamera.nearClipPlane * 2);
    }
    
}
