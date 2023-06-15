using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

public class Checker : MonoBehaviour
{
    private Button button;

    public Checker[] neighborCheckers;

    public Image image;
    public Sprite rightImage;

    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(MoveImage);
    }

    public bool CheckNull => image.sprite == null;

    private void MoveImage()
    {
        for (int i = 0; i < neighborCheckers.Length; i++)
        {
            if (neighborCheckers[i].CheckNull)
            {
                neighborCheckers[i].SetImage(image.sprite);
                Hide();
                GameUI.Instance.Check();
                return;
            }
        }
    }

    public void MoveImageRand()
    {
        for (int i = 0; i < neighborCheckers.Length; i++)
        {
            if (neighborCheckers[i].CheckNull)
            {
                neighborCheckers[i].SetImage(image.sprite);
                Hide();
                return;
            }
        }
    }
    
    public bool CanMove()
    {
        for (int i = 0; i < neighborCheckers.Length; i++)
        {
            if (neighborCheckers[i].CheckNull)
            {
                return true;
            }
        }

        return false;
    }

    public bool CheckTrue()
    {
        return image.sprite == rightImage;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void Rand()
    {
    }

    public void SetImage(Sprite sprite)
    {
        var imageColor = image.color;
        imageColor.a = 1;
        image.color = imageColor;
        image.sprite = sprite;
    }

    public void SetRightImage(Sprite sprite)
    {
        rightImage = sprite;
    }
    
    public void Hide()
    {
        image.sprite = null;
        var imageColor = image.color;
        imageColor.a = 0;
        image.color = imageColor;
    }
}