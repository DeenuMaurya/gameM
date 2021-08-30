using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path
{
    private List<Vector3> waypoints = new List<Vector3>();
    private GameObject player;
    private Dictionary<int, int> slides = new Dictionary<int, int>();
    public int waypointIndex = 0;
    List<int> lastIndex = new List<int>();
    int[] last;

    public Path(){

    }
    public Path(GameObject newPlayer){
      player = newPlayer;
      GameObject ways = GameObject.Find("BoardWaypoints");
      for (int i = 0; i < 86; i++)
      {
          waypoints.Add(ways.transform.GetChild(i).transform.position);
      }
      player.GetComponent<Move>().setWaypoints(waypoints);
      player.GetComponent<Move>().setTarget(waypointIndex);
      initializeSlides();
    }

    private void initializeSlides()
    {
        slides.Add(4, 13);
        slides.Add(18, 7);
        slides.Add(25, 12);
        slides.Add(32, 52);
        slides.Add(41, 62);
        slides.Add(47, 28);
        slides.Add(60, 83);
        slides.Add(71, 49);


    }

    public void setPosition(int newPosition){
      waypointIndex = newPosition;
    }

    public int getPosition(){
      return waypointIndex;
    }

    public List<Vector3> getWaypoints(){
      return waypoints;
    }

    public void move(int move){
      int temp = waypointIndex + move;
      temp = checkWin(temp);
      int check;
      if(slides.TryGetValue(temp, out check)){
        player.GetComponent<Move>().setSlide(temp, check);
        waypointIndex = check;
      }else{
        player.GetComponent<Move>().setTarget(temp);
        waypointIndex = temp;
      }
    }

    private int checkWin(int temp){
      if(temp >= waypoints.Count){
        temp = waypointIndex;
      }
      return temp;
    }

    public bool isWinner(){
      if(waypointIndex == ((waypoints.Count)-1)){
        return true;
      }
      return false;
    }

    public void die(){
      waypointIndex = 0;
      player.GetComponent<Move>().goToStart();
    }
}
