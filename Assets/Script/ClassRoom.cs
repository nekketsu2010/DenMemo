using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClassRoom
{
    private string roomGoukan;
    private string roomFloor;
    private string roomNumber;
    private string roomName;

    public ClassRoom(string roomGoukan, string roomFloor, string roomNumber, string roomName)
    {
        this.roomGoukan = roomGoukan;
        this.roomFloor = roomFloor;
        this.roomNumber = roomNumber;
        this.roomName = roomName;
    }

    public string getRoomGoukan() { return roomGoukan; }
    public string getRoomFloor() { return roomFloor; }
    public string getRoomNumber() { return roomNumber; }
    public string getRoomName() { return roomName; }
}
