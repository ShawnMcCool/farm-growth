using System;
using UnityEngine;
using System.Collections;

public class Field : MonoBehaviour {

    public int BuildTime = 5;
    public int GrowTime = 10;
    
    private Transform myTransform;
    private MeshRenderer tileRenderer;
    private Animation animations;

	void Start () {
        // cache transform lookup
	    myTransform = transform;

        // reference components
	    animations = GetComponent<Animation>();
	    tileRenderer = myTransform.FindChild("Tile").GetComponent<MeshRenderer>();
        
        // Here, I'm using animation tooling as more than just 
        // animation. I'm not saying it's a good practice, but
        // it's good to know.
        DefineBuildingAnimation();
	    DefineGrowingAnimation();

	    StartBuilding();
	}

    private void StartBuilding() {
        Debug.Log("Field is being built.");
        animations.Play("Building");
    }

    private void DefineBuildingAnimation() {
        // Create an event at the end of the building animation.
        var fieldWasBuiltEvent = new AnimationEvent();
        fieldWasBuiltEvent.time = 1f;
        fieldWasBuiltEvent.functionName = "FieldWasBuilt";
        animations["Building"].clip.AddEvent(fieldWasBuiltEvent);

        // Animation is 1 second so, I can scale it according 
        // to a changeable build time variable
	    animations["Building"].speed = 1f / BuildTime;
    }

    public void FieldWasBuilt() {
        Debug.Log("Field was built.");
        StartGrowing();
    }

    private void StartGrowing() {
        Debug.Log("Field begins growth.");
        animations.Stop();
        animations.Play("Growing");
    }

    private void DefineGrowingAnimation() {
        var fieldWasBuiltEvent = new AnimationEvent();
        fieldWasBuiltEvent.time = 1f;
        fieldWasBuiltEvent.functionName = "FieldWasGrown";
        animations["Growing"].clip.AddEvent(fieldWasBuiltEvent);

        animations["Growing"].speed = 1f / GrowTime;
    }

    public void FieldWasGrown() {
        Debug.Log("Field grew in.");
        StartGrowing();
    }

	void Update () {
	
	}
}
