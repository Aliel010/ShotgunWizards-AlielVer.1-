using UnityEngine;
using System.Collections;

public class MultiplayerCamera : MonoBehaviour {

    //private 
    private GameObject[] playerList;

    private float minPlayerDist;
    private float maxPlayerDist;

    private float initPlayerDistance;

    private float prevPlayerDist;

    //public 
    public float maxYCameraPos;
    public float minYCameraPos;
    public float cameraDistanceCap;
    public float singlePlayerCameraSpeed;
    public bool useDefaultMinYCameraPos;

    // Use this for initialization
    void Start ()
    {
        FetchPlayers();
        initPlayerDistance = Vector3.Distance(playerList[0].transform.position, playerList[1].transform.position);
        Init();
    }

    //initializes the min and max distances and positions
    void Init()
    {
        //min
        minPlayerDist = initPlayerDistance;
        prevPlayerDist = initPlayerDistance;
        if (useDefaultMinYCameraPos)
        {
            minYCameraPos = transform.position.y; //Whatever the camera position is before playing
        }
    }

	// Update is called once per frame
	void Update ()
    {
        //singlePlayer camera
        if (playerList.Length == 1)
        {
            singlePlayerCamera();
        }
        else if (playerList.Length == 2)
        {
            // two players
            SecondMethodCalculateCameraPos(GetPlayerDistance());
        }
	}

    void FetchPlayers()
    {
       playerList = GameObject.FindGameObjectsWithTag("Player");
    }

    //works, this is being used only for two players at the moment
    void SecondMethodCalculateCameraPos(float playerDistance)
    {
        //call the camera component specifically instead
        //As the player distance increases so does the camera height
        Vector3 cameraPos = GetComponentInChildren<Camera>().transform.localPosition;
        //Cap 
        if (playerDistance > cameraDistanceCap)
        {

            Vector3 cameraYPos = Vector3.Lerp(new Vector3(cameraPos.x, minYCameraPos, cameraPos.z), new Vector3(cameraPos.x, playerDistance, cameraPos.z), .5f);
            GetComponentInChildren<Camera>().transform.localPosition = cameraYPos;
        }
    }

    //Works
    void singlePlayerCamera()
    {
        //the minYcamera needs to follow the player
        //needs to be smoother
        transform.position = Vector3.Lerp(transform.localPosition, new Vector3(playerList[0].transform.position.x, transform.localPosition.y, playerList[0].transform.position.z), singlePlayerCameraSpeed * (Time.deltaTime));
        
    }

    float GetPlayerDistance()
    {
        //testing 2 players
        float updatedPlayerDist = Vector3.Distance(playerList[0].transform.position, playerList[1].transform.position);

        //print("Distance: " + updatedPlayerDist);
        //print("Distance Tracker: " + DistanceTracker(updatedPlayerDist));
        return updatedPlayerDist;
    }

    //Other camera methods being unused
    /*
    //works but not what we want //Not being used
    void CalculateCameraPos(float playerDistance)
    {
        int cameraStates = DistanceTracker(playerDistance);

        //if playerDistance increases
        if (cameraStates == 0)
        {
            //move camera up
            transform.Translate(new Vector3(0, 1) * Time.deltaTime);
        }
        else if (cameraStates == 1)
        {
            transform.Translate(new Vector3(0, -1) * Time.deltaTime);
        }
        else
        {
            return;
        }

    }
    //Not being used
    int DistanceTracker(float playerDist)
    {
        int increaseHeight = 0;
        int decreaseHeight = 1;

        if (playerDist > prevPlayerDist) //means it is increasing
        {
            prevPlayerDist = playerDist;
            return increaseHeight;
        }
        else if (playerDist < prevPlayerDist) //means it is decreasing
        {
            prevPlayerDist = playerDist;
            return decreaseHeight;
        }
        else
        {
            return -1;
        }
    }*/
}
