using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder;

public class LocalTimeRecorder : MonoBehaviour
{
    public class ObjectState
    {
        public Vector3 position;
        public Quaternion rotation;
        public Vector3 velocity;

        public ObjectState (Vector3 _position, Quaternion _rotation, Vector3 _velocity)
        {
            position = _position;
            rotation = _rotation;
            velocity = _velocity;
        }
    }

    public float recordTime = 5f;
    [HideInInspector]
    public float rewindSpeed;
    bool isRewinding = false;

    private GameObject clone;

    List<ObjectState> objectStates;
    Rigidbody rb;

    void Start()
    {
        objectStates = new List<ObjectState>();
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (isRewinding)
        {
            Rewind();
        } else
        {
            Record();
        }
    }

    void Rewind()
    {
        int rewindCount = Mathf.RoundToInt(rewindSpeed * Time.deltaTime * 50);
        for (int i = 0; i < rewindCount; i++)
        {
            if (objectStates.Count > 0)
            {
                ObjectState pointInTime = objectStates[0];
                transform.position = pointInTime.position;
                transform.rotation = pointInTime.rotation;
                objectStates.RemoveAt(0);
            }
            else
            {
                StopRewind();
                break;
            }
        }
    }

    void Record()
    {
        if (objectStates.Count > Mathf.Round(recordTime / Time.fixedDeltaTime))
        {
            objectStates.RemoveAt(objectStates.Count - 1);
        }

        objectStates.Insert(0, new ObjectState(transform.position, transform.rotation, rb.velocity));
    }

    public void StartRewind(Material rewindMaterial)
    {
        isRewinding = true;
        rb.isKinematic = true;

        Destroy(clone);

        clone = Instantiate(gameObject);
        clone.transform.SetParent(gameObject.transform);
        clone.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
        clone.transform.localScale = new Vector3(transform.localScale.x + 0.001f, transform.localScale.y + 0.001f, transform.localScale.z + 0.001f);

        Component[] components = clone.GetComponents<Component>();
        foreach (Component component in components)
        {
            if (!(component is Transform) && !(component is MeshRenderer) && !(component is MeshFilter) && !(component is ProBuilderMesh))
            {
                Destroy(component);
            }
        }

        clone.GetComponent<Renderer>().material = rewindMaterial;
    }

    public void StopRewind()
    {
        isRewinding = false;
        rb.isKinematic = false;

        rb.velocity = objectStates[0].velocity;

        Destroy(clone);
    }
}