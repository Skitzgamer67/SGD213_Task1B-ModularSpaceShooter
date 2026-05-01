using UnityEngine;

public class WeaponTripleShot : WeaponBase
{
    /// <summary>
    /// Shoots three bullets in a spread pattern, provided enough time has passed.
    /// This version works with BulletBehaviour because BulletBehaviour moves using transform.up.
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

        // Angles for left, centre, and right bullets
        float[] spreadAngles = { 25f, 0f, -25f };

        for (int i = 0; i < spreadAngles.Length; i++)
        {
            Quaternion bulletRotation = bulletSpawnPoint.rotation * Quaternion.Euler(0f, 0f, spreadAngles[i]);

            Instantiate(
                bullet,
                bulletSpawnPoint.position,
                bulletRotation
            );
        }

        lastFiredTime = currentTime;
    }
}