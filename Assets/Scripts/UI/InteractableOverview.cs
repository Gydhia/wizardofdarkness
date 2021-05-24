using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractableOverview : MonoBehaviour
{
    public InteractableDatas IDatas;

    public Image DataImage;
    public Text DataText;

    public void Start()
    {
        GameUIController.Instance.OnInteractOverviewChange += ShowOverview;
        GameUIController.Instance.OnInteractOverviewCancel += HideOverview;

        DataText.gameObject.SetActive(false);
        DataImage.gameObject.SetActive(false);
    }

    public void ShowOverview(InteractableDatas Datas)
    {
        DataText.gameObject.SetActive(Datas.ShowText);
        DataImage.gameObject.SetActive(Datas.ShowImage);

        if (Datas.ShowImage)
            DataImage.sprite = Datas.ImageToShow;

        if (Datas.ShowText)
            DataText.text = Datas.TextToShow;
    }
    public void HideOverview()
    {
        DataText.gameObject.SetActive(false);
        DataImage.gameObject.SetActive(false);
    }
}
