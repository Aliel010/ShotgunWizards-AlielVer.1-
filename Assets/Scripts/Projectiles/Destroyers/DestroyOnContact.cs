using UnityEngine;

public class DestroyOnContact : MonoBehaviour, Triggers {

	public void OnTriggerEnter(Collider other) {

        if (other.tag != "Projectile") {//if not a projectile

            Destroy(gameObject);//self-destruct
        }
    }

    public void OnColliderEnter(Collider other) {

        if (other.tag != "Projectile") {//if not a projectile

            Destroy(gameObject);//self-destruct
        }
    }
}
