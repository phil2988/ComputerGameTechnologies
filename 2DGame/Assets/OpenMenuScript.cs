using UnityEngine;

public class OpenMenuScript : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] public GameObject borderPrefab;

    private MenuBoxTemplate menuBox;
    
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
           
            if (!menuBox)
            {
                menuBox = GetComponent<MenuBoxTemplate>();
                //menuBox = new MenuBoxTemplate();
                menuBox.SetSize(new Vector3(1, 1, 1));
            }
            
            
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            GameObject menu = menuBox.GetMenuBox();

            if (menu)
            {
                menuBox.SetSize(new Vector3(menu.transform.localScale.x + (float)0.1, menu.transform.localScale.y + (float)0.1, menu.transform.localScale.z + (float)0.1));
            }


        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (menuBox.GetMenuBox())
            {
                menuBox.Destroy();
            }
            
        }
    }
}
