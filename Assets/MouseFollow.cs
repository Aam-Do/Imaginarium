using UnityEngine;

public class MouseFollow : MonoBehaviour {
	Vector3 mousePosition;
	// public float moveSpeed = 0.1f;
	public Rigidbody2D rb;
	Vector2 position = new Vector2(0f, 0f);
	
	private void Start()
	{
		rb = GetComponent<Rigidbody2D>();
	}
	
	private void Update()
	{
		mousePosition = Input.mousePosition;
		// mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
		// position = Vector2.Lerp(transform.position, mousePosition, moveSpeed);
		position = Camera.main.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, -Camera.main.transform.position.z));
	}
	
	private void FixedUpdate()
	{
		rb.MovePosition(position);
	}
}