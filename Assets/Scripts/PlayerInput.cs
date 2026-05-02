using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// PlayerInput handles all of the player specific input behaviour, and passes the input information
/// to the appropriate scripts.
/// </summary>
public class PlayerInput : MonoBehaviour
{
    [Header("Powerup Settings")]
    [SerializeField] private float tripleShotDuration = 10f;

    // Local references
    private PlayerMovement playerMovement;
    private WeaponBase weapon;

    private Coroutine tripleShotTimerCoroutine;

    public WeaponBase Weapon
    {
        get
        {
            return weapon;
        }

        set
        {
            weapon = value;
        }
    }

    private void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        weapon = GetComponent<WeaponBase>();
    }

    private void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal"); // Reads the horizontal input axis

        if (horizontalInput != 0.0f) // If movement input is not zero
        {
            if (playerMovement != null)
            {
                playerMovement.MovePlayer(horizontalInput * Vector2.right); // Pass movement input to PlayerMovement
            }
        }

        // Triggered by clicking / pressing Fire1
        if (Input.GetButton("Fire1"))
        {
            if (weapon != null)
            {
                weapon.Shoot(); // Tell the current weapon to shoot
            }
        }
    }

    /// <summary>
    /// Creates a new WeaponBase component based on the given weaponType.
    /// If tripleShot is selected, it starts a timer and then switches back to machineGun.
    /// </summary>
    public void SwapWeapon(WeaponType weaponType)
    {
        if (weapon == null)
        {
            Debug.LogError("Player does not currently have a weapon to copy settings from.");
            return;
        }

        WeaponBase newWeapon = null;

        switch (weaponType)
        {
            case WeaponType.machineGun:
                newWeapon = gameObject.AddComponent<WeaponMachineGun>();

                // Stop the triple shot timer if switching back to machine gun manually
                if (tripleShotTimerCoroutine != null)
                {
                    StopCoroutine(tripleShotTimerCoroutine);
                    tripleShotTimerCoroutine = null;
                }

                break;

            case WeaponType.tripleShot:
                newWeapon = gameObject.AddComponent<WeaponTripleShot>();
                break;
        }

        if (newWeapon == null)
        {
            Debug.LogError("Weapon swap failed.");
            return;
        }

        // Copy bullet prefab and spawn point from the old weapon
        newWeapon.UpdateWeaponControls(weapon);

        // Remove the old weapon so only one weapon is active
        Destroy(weapon);

        // Set the current weapon to be the new weapon
        weapon = newWeapon;

        // Start or reset the triple shot timer
        if (weaponType == WeaponType.tripleShot)
        {
            if (tripleShotTimerCoroutine != null)
            {
                StopCoroutine(tripleShotTimerCoroutine);
            }

            tripleShotTimerCoroutine = StartCoroutine(TripleShotTimer());
        }
    }

    /// <summary>
    /// Waits for the triple shot duration, then switches the player back to machine gun.
    /// </summary>
    private IEnumerator TripleShotTimer()
    {
        yield return new WaitForSeconds(tripleShotDuration);

        tripleShotTimerCoroutine = null;

        // Only switch back if the player is still using triple shot
        if (weapon is WeaponTripleShot)
        {
            SwapWeapon(WeaponType.machineGun);
        }
    }
}