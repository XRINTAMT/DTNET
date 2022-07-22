using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChanger : MonoBehaviour
{
    public List<Material> ListMaterial;
    public MeshRenderer GameObjectMesh;

    public void ChangeMareial(int index)
    {
        if (ListMaterial == null || ListMaterial.Count == 0)
        {
            return;
        }

        if (index < 0)
        {
            GameObjectMesh.material = ListMaterial[0];
            return;
        }

        if (index >= ListMaterial.Count) 
        {
            GameObjectMesh.material = ListMaterial[ListMaterial.Count - 1];
        }
        else
        {
            GameObjectMesh.material = ListMaterial[index];
        }
    }
}
