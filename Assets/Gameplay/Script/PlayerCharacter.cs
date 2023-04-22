using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCharacter : MonoBehaviour
{
    private Rigidbody rigid;
    public float speed = 6;
    public float turnSpeed = 120;
    public ParticleSystem explosionParticles;

    public Rigidbody shell;
    public Transform muzzle;

    public float launchForce = 10;
    public AudioSource shootAudioSource;
    public AudioSource movementAudioSource;
    private float movementAudioOriginalPitch;
    public float movementAudioPitchRange = 0.1f;

    public AudioClip engineIdlingClip;
    public AudioClip engineDrivingClip;


    public float health;
    float healthMax;
    bool isAlive;


    public Slider healthSlider;                             // The slider to represent how much health the tank currently has.
    public Image healthFillImage;                           // The image component of the slider.
    public Color healthColorFull = Color.green;
    public Color HealthColorNull = Color.red;

    void Start ()
    {
        rigid = GetComponent<Rigidbody>();
        movementAudioOriginalPitch = movementAudioSource.pitch;
        healthMax = health;
        isAlive = true;
        RefreshHealthHUD();
        explosionParticles.gameObject.SetActive(false);
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        RefreshHealthHUD();
        if (health <= 0f && isAlive)
        {
            Death();
        }
    }

    public void RefreshHealthHUD()
    {
        healthSlider.value = health;
        healthFillImage.color = Color.Lerp(HealthColorNull, healthColorFull, health / healthMax);
    }

    public void Death()
    {
        isAlive = false;
        explosionParticles.transform.parent = null;
        explosionParticles.gameObject.SetActive(true);
        ParticleSystem.MainModule mainModule = explosionParticles.main;
        Destroy(explosionParticles.gameObject, mainModule.duration);
        gameObject.SetActive(false);

    }

    public void Move(float verticalInputValue)
    {
        if (!isAlive) return;
        Vector3 movement = transform.forward * verticalInputValue * speed * Time.deltaTime;
        rigid.MovePosition(rigid.position + movement);
    }

    public void Turn(float horizontalInputValue)
    {
        if (!isAlive) return;
        float turn = horizontalInputValue * turnSpeed * Time.deltaTime;
        Quaternion rotation = Quaternion.Euler(0, turn, 0);
        rigid.MoveRotation(rigid.rotation * rotation);
    }

    public void Fire()
    {
        if (!isAlive) return;
        Rigidbody shellInstance = Instantiate(shell, muzzle.position, muzzle.rotation) as Rigidbody;
        shellInstance.velocity = launchForce * muzzle.forward;
        shootAudioSource.Play();
    }


    public void EngineAudio(float verticalInputValue, float horizontalInputValue)
    {
        if (Mathf.Abs(verticalInputValue) < 0.1f && Mathf.Abs(horizontalInputValue) < 0.1f)
        {
            if (movementAudioSource.clip == engineDrivingClip)
            {
                movementAudioSource.clip = engineIdlingClip;
                movementAudioSource.pitch = Random.Range(movementAudioOriginalPitch - movementAudioPitchRange, movementAudioOriginalPitch + movementAudioPitchRange);
                movementAudioSource.Play();
            }
        }
        else
        {
            if (movementAudioSource.clip == engineIdlingClip)
            {
                movementAudioSource.clip = engineDrivingClip;
                movementAudioSource.pitch = Random.Range(movementAudioOriginalPitch - movementAudioPitchRange, movementAudioOriginalPitch + movementAudioPitchRange);
                movementAudioSource.Play();
            }
        }
    }
}
