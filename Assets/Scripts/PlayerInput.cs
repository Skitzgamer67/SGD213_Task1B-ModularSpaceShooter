using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// PlayerInput handles all of the player specific input behaviour, and passes the input information
/// to the appropriate scripts
/// </summary>
public class PlayerInput : MonoBehaviour
{

    // local references
    private PlayerMovement playerMovement;

    private WeaponBase weapon;
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

    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        weapon = GetComponent<WeaponBase>();
    }

    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal"); // Reads the horizontal input axis

        if (horizontalInput != 0.0f) // If movement input is not zero
        {
            // Ensure playerMovement Script is populated to avoid errors
            if (playerMovement != null)
            {
                playerMovement.MovePlayer(horizontalInput * Vector2.right); // Pass movement input to PlayerMovement
            }
        }

        // Triggered by Clicking
        if (Input.GetButton("Fire1"))
        {
            // If the player has a current weapon, fire it
            if (weapon != null)
            {
                weapon.Shoot(); // Tell the current weapon to shoot
            }
        }
    }

    /// <summary>
    /// SwapWeapon handles creating a new WeaponBase component based on the given weaponType. This
    /// will popluate the newWeapon's controls and remove the existing weapon ready for usage.
    /// </summary>
    /// <param name="weaponType">The given weaponType to swap our current weapon to, this is an enum in WeaponBase.cs</param>
    public void SwapWeapon(WeaponType weaponType)
    {
        // make a new weapon dependent on the weaponType
        WeaponBase newWeapon = null;
        switch (weaponType)
        {
            case WeaponType.machineGun:
                newWeapon = gameObject.AddComponent<WeaponMachineGun>(); // Add the selected weapon component to the player
                break;
            case WeaponType.tripleShot:
                newWeapon = gameObject.AddComponent<WeaponTripleShot>(); // Add the selected weapon component to the player
                break;
        }

        // Copy bullet prefab and spawn point from the old weapon
        newWeapon.UpdateWeaponControls(weapon);
        // Remove the old weapon so only one weapon is active
        Destroy(weapon);
        // Set the current weapon to be the newWeapon
        weapon = newWeapon;
    }
}
