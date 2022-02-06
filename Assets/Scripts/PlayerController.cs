using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    private Animator playerAnim;
    private AudioSource playerAudio;

    public ParticleSystem explosionParticle;
    public ParticleSystem dirtyParticle;
    public AudioClip jumpSound;
    public AudioClip crashSound;

    public float jumpForce;
    public float gravityModifier;
        
    public bool isOnGround = true;
    public bool gameOver = false;
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        playerAnim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
        Physics.gravity *= gravityModifier;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround && !gameOver)
		{
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isOnGround = false;
            playerAnim.SetTrigger("Jump_trig");
            dirtyParticle.Stop();
            playerAudio.PlayOneShot(jumpSound, 1.0f);
        }
    }

    private void OnCollisionEnter(Collision colision)
	{
		if (colision.gameObject.CompareTag("Ground"))
		{
            isOnGround = true;
            dirtyParticle.Play();
		} 
        else if (colision.gameObject.CompareTag("Obstacle"))
		{
            Debug.Log("Game Over!");
            gameOver = true;
            playerAnim.SetBool("Death_b", true);
            playerAnim.SetInteger("DeathType_int", 1);
            explosionParticle.Play();
            dirtyParticle.Stop();
            playerAudio.PlayOneShot(crashSound, 1.0f);
        }
        
	}
}
