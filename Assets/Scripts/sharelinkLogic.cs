using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class sharelinkLogic : MonoBehaviour
{
	public Sprite QueenOFTenImage;
	public void ShareButtonClick()
    {
		MainMenuController.Instance.ButtonClickSound();
		StartCoroutine(TakeScreenshotAndShare());
    }
	private IEnumerator TakeScreenshotAndShare()
	{
		yield return new WaitForEndOfFrame();

		//Texture2D ss = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
		//Texture2D ss = QueenOFTenImage.texture;

		Texture2D ss = new Texture2D((int)QueenOFTenImage.rect.width, (int)QueenOFTenImage.rect.height);
		Color[] newColors = QueenOFTenImage.texture.GetPixels((int)QueenOFTenImage.textureRect.x,
													 (int)QueenOFTenImage.textureRect.y,
													 (int)QueenOFTenImage.textureRect.width,
													 (int)QueenOFTenImage.textureRect.height);
		ss.SetPixels(newColors);
		ss.Apply();
		//ss.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
		//ss.Apply();

		string filePath = Path.Combine(Application.temporaryCachePath, "shared img.png");
		File.WriteAllBytes(filePath, ss.EncodeToPNG());

		
		Destroy(ss);

		//new NativeShare().AddFile(filePath)
		//	.SetSubject("QueenOfTen").SetText("Hello world!").SetUrl(ApiCode.Instance.ShareLinkURL)
		//	.SetCallback((result, shareTarget) => Debug.Log("Share result: " + result + ", selected app: " + shareTarget))
		//	.Share();

		new NativeShare().AddFile(filePath)
			.SetSubject("QueenOfTen").SetText("QueenOfTen").SetUrl(ApiCode.Instance.ShareLinkURL)
			.SetCallback((result, shareTarget) => Debug.Log("Share result: " + result + ", selected app: " + shareTarget))
			.Share();

	}
}
