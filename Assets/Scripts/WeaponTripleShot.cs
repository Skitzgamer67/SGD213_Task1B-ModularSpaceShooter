using UnityEngine;

public class WeaponTripleShot : WeaponBase
{
    /// <summary>
    /// Shoots three bullets in a spread pattern.
    /// Works with MoveConstantly bullets and BulletBehaviour bullets.
    /// </summary>
    public override void Shoot()
    {
        float currentTime = Time.time;

        if (currentTime - lastFiredTime <= fireDelay)
        {
            return;
        }

        if (bullet == null)
        {
            Debug.LogError("WeaponTripleShot is missing a bullet prefab.");
            return;
        }

        if (bulletSpawnPoint == null)
        {
            Debug.LogError("WeaponTripleShot is missing a bullet spawn point.");
            return;
        }

        float[] spreadAngles = { -25f, 0f, 25f };

        for (int i = 0; i < spreadAngles.Length; i++)
        {
            GameObject newBullet = Instantiate(
                bullet,
                bulletSpawnPoint.position,
                bulletSpawnPoint.rotation
            );

            // MoveConstantly bullets do not use rotation for movement so their movement direction must be changed manually
            MoveConstantly moveConstantly = newBullet.GetComponent<MoveConstantly>();

            if (moveConstantly != null)
            {
                Vector2 baseDirection = moveConstantly.Direction;
                Vector2 spreadDirection = Quaternion.Euler(0f, 0f, spreadAngles[i]) * baseDirection;

                // Move in the spread direction
                moveConstantly.Direction = spreadDirection;

                // Rotate the sprite 180 degrees so it visually faces correctly
                newBullet.transform.up = -spreadDirection;
            }
            else
            {
                // For BulletBehaviour bullets, movement uses transform.up
                newBullet.transform.rotation =
                    bulletSpawnPoint.rotation * Quaternion.Euler(0f, 0f, spreadAngles[i]);
            }
        }

        lastFiredTime = currentTime;
    }
}