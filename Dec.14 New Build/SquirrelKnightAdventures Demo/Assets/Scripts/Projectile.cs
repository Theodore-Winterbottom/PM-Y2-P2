using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Projectile : MonoBehaviour
{
    //bullet

    public GameObject bullet;

    //bullet force

    public float shootForce, upwardForce;

    //weapon stats

    public float timeBetweenShooting, spread, reloadTime, timeBetweenShots;
    public int magazineSize, bulletsPerTap;
    public bool allowButtonHold;

    int bulletsLeft, bulletsShot;

    //bools

    bool shooting, readyToShoot, reloading;

    //Reference
    public Camera playerCam;
    public Transform attackPoint;
}
