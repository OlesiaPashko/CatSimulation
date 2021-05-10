using UnityEngine;

public class AutoBehaviour : MonoBehaviour
{
    [SerializeField] private float radius = 10f;
    
    // Start is called before the first frame update
    void Start()
    {
        GetBestAction();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void GetBestAction()
    {
        var interactableObjects = Physics.OverlapSphere(transform.position, radius);
        foreach (var interactableObject in interactableObjects)
        {
            if (interactableObject.gameObject.GetComponent<Touchable>() != null)
            {
                Debug.Log(interactableObject.gameObject);
            }
        }
    }
}
