using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceDisplay : MonoBehaviour
{
    float displaySpeedX = 0.0002f;
    float displaySpeedY = -0.0002f;
   
    //parent size
    public float pWidth, pHeight

    //hold childerns size
    float width, height;
    public SpriteRenderer monitorSprite;

    

    // Start is called before the first frame update
    void Start()
    {     
        //have to use transform.parent instead of getcomponent in parent because it searches this object first for a transform
        // get width and hieght from local scale
        
        width = this.GetComponent<Transform>().lossyScale.x;
        height = this.GetComponent<Transform>().lossyScale.y;
        pWidth = transform.parent.lossyScale.x;
        pHeight = transform.parent.lossyScale.y;

        monitorSprite = transform.parent.GetComponent<SpriteRenderer>();

        RANDOMISEDIRECTION();

            
    }

    void RANDOMISEDIRECTION()
    {
        int randomX = Random.Range(0, 2);
        int randomY = Random.Range(0, 2);

        if (randomX == 1)
        {
            displaySpeedX *= -1;

        }
        if (randomY == 1)
        {
            displaySpeedY *= -1;
        }
    }

    
    // Update is called once per frame
    void Update()
    {
        //size = GetComponentInParent<MeshCollider>().bounds.size; this is the code for 3d object to find edges
        //get the sprite renderer size to base off

        //update position
        this.transform.localPosition = new Vector3(this.transform.localPosition.x + displaySpeedX, this.transform.localPosition.y + displaySpeedY, this.transform.localPosition.z);

        //get the parents sprite size / 2 if your past that position positively your to far right and take away half your width to the right
        //hit the right or bottom
        //does not work if scaled
        if (this.transform.localPosition.x >= monitorSprite.size.x / 2 - width / 2 || this.transform.localPosition.x <= -monitorSprite.size.x / 2 + width / 2)
        {
            displaySpeedX *= -1;
        }
    
        if (this.transform.localPosition.y >= monitorSprite.size.y / 2 - height / 2 || this.transform.localPosition.y <= -monitorSprite.size.y / 2 + height / 2)
        {
            displaySpeedY *= -1;
        }

        
    }
}
