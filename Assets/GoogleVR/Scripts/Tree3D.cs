/*
 
  Anthony D'incau - 2013
  3D Fractal Tree
  
  Feel free to use this code for any commercial or non-commercial use.
  
  Simply attach this script to an empty gameOjbect in the Unity Game engine in order
  to draw a 3D fractal tree. Don't forget to uncomment the buildNewTree() call in the
  start function if you're not using the MainController class to control the scene.
  
*/

using UnityEngine;
using System.Collections;

public class Tree3D : MonoBehaviour {
	public float posX=50.0f;
	public float posY=0f;
	public float posZ=10f;
	public int depth = 9;
	public float angleSpread = 21.0f;
	public Material branchMat;
	public bool averaged = false;
	private float deg_to_rad = 3.14159265f / 180.0f;
	private float scale = 0.5f;
	private LineRenderer line;
	private GameObject fractalTree;
	private GameObject branch;
	public float randomness = 0.0f;
	public GameObject treeLeaf;
	private GameObject treeLeafClone;
	
	void Start () {
		//un-comment the line below to build the tree on start if not using MainController
		buildNewTree();
	}
	
	public void killOldTree(){
		Destroy(fractalTree);	
	}
	
	public void buildNewTree(){
		//Create a new empty gameObject to store our fractal tree
		fractalTree = new GameObject ("Fractal_Tree");
		fractalTree.transform.parent = gameObject.transform;
		if(averaged){angleSpread*=2;}
		drawTree(posX, posY, posZ, 0.0f, 90.0f, 90.0f, depth);	
	}
	
	//A recursive function used to draw the fractal tree
	void drawTree(float x1, float y1, float z1, float y3, float angle, float angle2, int depth){
		if (depth != 0){
			//Set the x2 point
			float x2 = x1 + (Mathf.Cos(angle * deg_to_rad) * depth * scale);

			//Set the z2 point
			float z2 = z1 + (Mathf.Cos(angle2 * deg_to_rad) * depth * scale);
			
			//Set the y2 point
			float y2 = y1 + (Mathf.Sin(angle * deg_to_rad) * depth * scale);
			
			//Set the y4 point
			float y4 = y3 + (Mathf.Sin(angle2 * deg_to_rad) * depth * scale);
			
			//Average the y values
			float n1 = (y3+y1)/2;
			float n2 = (y4+y2)/2;
			
			//Call the drawline function, provide the coordinate data
			drawLine(x1, n1, z1, x2, n2, z2, depth);
			
			//Iterate the function recursively, change the rotation of the branches
			if(!averaged){

				drawTree(x2, y2, z2, y4, angle - (angleSpread - Random.value * randomness), angle2 - (angleSpread - Random.value * randomness), depth - 1);
				drawTree(x2, y2, z2, y4, angle + (angleSpread - Random.value * randomness), angle2 + (angleSpread - Random.value * randomness), depth - 1);
				drawTree(x2, y2, z2, y4, angle + (angleSpread - Random.value * randomness), angle2 - (angleSpread - Random.value * randomness), depth - 1);
				drawTree(x2, y2, z2, y4, angle - (angleSpread - Random.value * randomness), angle2 + (angleSpread - Random.value * randomness), depth - 1);

			}
			
			//Iterate the function recursively, change the rotation of the branches (rounded version)
			if(averaged){
				drawTree(x2, y2, z2, y4, angle - (angleSpread - Random.value * randomness), angle2, depth - 1);
				drawTree(x2, y2, z2, y4, angle + (angleSpread - Random.value * randomness), angle2, depth - 1);
				drawTree(x2, y2, z2, y4, angle, angle2 - (angleSpread - Random.value * randomness), depth - 1);
				drawTree(x2, y2, z2, y4, angle, angle2 + (angleSpread - Random.value * randomness), depth - 1);
			}
		}
	}
	
	//Draws a single branch of the tree
	void drawLine(float x1, float y1, float z1, float x2, float y2, float z2, int color){
		//Create a gameObject for the branch
		branch = new GameObject ("branch");

		//Make the branch child of the main gameobject
		branch.transform.parent = fractalTree.transform;

		//Add a line renderer to the branch gameobject
		line = branch.AddComponent<LineRenderer>() as LineRenderer;
		
		//Change the material of the LineRenderer
		line.material = branchMat;
		
		//Thin the branches through each iteration
		line.SetWidth(color*0.06f, color*0.04f);
   
		//Draw the line.
		line.SetPosition(0, new Vector3(x1,y1,z1));
		line.SetPosition(1, new Vector3(x2,y2,z2));
	}
}