using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Merge : MonoBehaviour
{
    private int ID;
    public GameObject MergedObject; // Prefab mới sẽ được tạo ra khi hợp nhất
    public Transform Archer; // Đối tượng hiện tại
    public Transform OtherArcher; // Đối tượng va chạm
    public float Distance; // Khoảng cách để hợp nhất
    public float MergeSpeed; // Tốc độ hợp nhất
    private bool CanMerge; // Kiểm tra xem có thể hợp nhất hay không

    void Start()
    {
        ID = GetInstanceID(); // Lấy ID duy nhất cho đối tượng
    }

    void Update()
    {
        // Có thể thêm logic khác ở đây nếu cần
    }

    private void FixedUpdate()
    {
        MoveTowards();
    }

    public void MoveTowards()
    {
        if (CanMerge)
        {
            // Di chuyển Archer về phía OtherArcher
            transform.position = Vector2.MoveTowards(transform.position, OtherArcher.position, MergeSpeed * Time.fixedDeltaTime);

            // Kiểm tra khoảng cách giữa hai đối tượng
            if (Vector2.Distance(transform.position, OtherArcher.position) < Distance)
            {
                // Kiểm tra ID để đảm bảo chỉ một đối tượng hợp nhất
                if (ID < OtherArcher.gameObject.GetComponent<Merge>().ID) { return; }

                Debug.Log($"SENDING MESSAGE FROM {gameObject.name} With The ID Number of {ID}");

                // Tạo đối tượng mới
                GameObject newObject = Instantiate(MergedObject, transform.position, Quaternion.identity);

                // Hủy đối tượng cũ
                Destroy(OtherArcher.gameObject);
                Destroy(gameObject);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Kiểm tra va chạm với đối tượng có tag "Archer"
        if (collision.gameObject.CompareTag("Player"))
        {
            // Kiểm tra màu sắc của đối tượng
            if (collision.gameObject.GetComponent<SpriteRenderer>().color == GetComponent<SpriteRenderer>().color)
            {
                Archer = transform; // Gán Archer là đối tượng hiện tại
                OtherArcher = collision.transform; // Gán OtherArcher là đối tượng va chạm
                CanMerge = true; // Cho phép hợp nhất
                Destroy(collision.gameObject.GetComponent<Rigidbody2D>()); // Hủy Rigidbody của đối tượng va chạm
                Destroy(GetComponent<Rigidbody2D>()); // Hủy Rigidbody của đối tượng hiện tại
            }
        }
    }
}