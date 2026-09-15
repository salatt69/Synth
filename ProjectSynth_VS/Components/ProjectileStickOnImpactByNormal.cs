using RoR2;
using RoR2.Projectile;
using UnityEngine;
using UnityEngine.Events;

namespace ProjectSynth.Components
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(ProjectileController))]
    public class ProjectileStickOnImpactByNormal : MonoBehaviour, IProjectileImpactBehavior
    {
        public float minGroundNormalY = 0.65f;

        public bool ignoreCharacters = true;
        public bool ignoreWorld = false;

        public bool alignNormals = true;
        public bool invertNormal = false;

        public string stickSoundString = "";
        public ParticleSystem[] stickParticleSystem;
        public UnityEvent stickEvent;

        public Transform StuckTransform { get; private set; }
        public CharacterBody StuckBody { get; private set; }
        public bool Stuck { get; private set; }

        private Rigidbody rb;
        private ProjectileController pc;

        private Vector3 stuckLocalPos;
        private Quaternion stuckLocalRot;

        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
            pc = GetComponent<ProjectileController>();
        }

        private void OnDisable()
        {
            Stuck = false;
            StuckTransform = null;
            StuckBody = null;
        }

        public void OnProjectileImpact(ProjectileImpactInfo impactInfo)
        {
            if (!enabled || Stuck) return;

            var col = impactInfo.collider;
            if (!col) return;

            var n = impactInfo.estimatedImpactNormal;
            if (!PassesNormalGate(n)) return;

            var hb = col.GetComponent<HurtBox>();
            if (hb)
            {
                if (ignoreCharacters) return;

                var hc = hb.healthComponent;
                if (!hc) return;

                if (pc && pc.owner && hc.gameObject == pc.owner) return;

                StickToTransform(hb.transform, n, hc.body);
                return;
            }

            if (ignoreWorld) return;

            StickToTransform(col.transform, n, null);
        }

        private bool PassesNormalGate(Vector3 n)
        {
            if (n == Vector3.zero) return false;
            return n.normalized.y >= minGroundNormalY;
        }

        private void StickToTransform(Transform target, Vector3 impactNormal, CharacterBody body)
        {
            if (!target) return;

            if (alignNormals && impactNormal != Vector3.zero)
            {
                Vector3 up = invertNormal ? -impactNormal : impactNormal;
                up.Normalize();

                Vector3 forward = Vector3.ProjectOnPlane(transform.forward, up);
                if (forward.sqrMagnitude < 0.0001f)
                    forward = Vector3.ProjectOnPlane(Vector3.forward, up);
                if (forward.sqrMagnitude < 0.0001f)
                    forward = Vector3.ProjectOnPlane(Vector3.right, up);

                forward.Normalize();
                transform.rotation = Quaternion.LookRotation(forward, up);
            }

            Stuck = true;
            StuckTransform = target;
            StuckBody = body;

            stuckLocalPos = target.InverseTransformPoint(transform.position);
            stuckLocalRot = Quaternion.Inverse(target.rotation) * transform.rotation;

            if (rb)
            {
                rb.velocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
                rb.detectCollisions = false;
                rb.collisionDetectionMode = CollisionDetectionMode.Discrete;
                rb.isKinematic = true;
            }

            FireStickFx();
        }

        private void FixedUpdate()
        {
            if (!Stuck || !StuckTransform) return;

            transform.SetPositionAndRotation(
                StuckTransform.TransformPoint(stuckLocalPos),
                alignNormals ? (StuckTransform.rotation * stuckLocalRot) : transform.rotation
            );
        }

        private void FireStickFx()
        {
            if (stickParticleSystem != null)
            {
                for (int i = 0; i < stickParticleSystem.Length; i++)
                    if (stickParticleSystem[i]) stickParticleSystem[i].Play();
            }

            if (!string.IsNullOrEmpty(stickSoundString))
                Util.PlaySound(stickSoundString, gameObject);

            stickEvent?.Invoke();
        }
    }
}
