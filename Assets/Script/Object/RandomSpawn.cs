using UnityEngine;
using System.Collections;

public class RandomSpawn : MonoBehaviour
{
    [SerializeField] GameObject zombie;

    void Start()
    {

        StartCoroutine(nameof(CreateZombie));
        
    }

    public IEnumerator CreateZombie()
    {
        while (true)
        {
            ObjectPool.instance.GetQueue();
            yield return new WaitForSeconds(2f);
        }
    }

    public Vector3 RandomPosition()
    {
        /*
            x*2 + z*2 <=r*2
            ���� �����Ŀ��� ������ x�� z�� �ش��ϴ� ���� �������� r�� �� �ȿ� �����ϴ� �����Դϴ�.
            
            0.3*2+z*2=1
            z=��Ʈ1*2-0.3*2
            z=��Ʈ 1-0.09
            z= ��Ʈ 0.91
            z=0.95(�ٻ簪)

            ������ 1�� ���� ������(0.3,0.95)
        */
        float radius = 50f;

        float x = Random.Range(-radius, radius);

        float z = Mathf.Sqrt(Mathf.Pow(radius,2)-Mathf.Pow(x,2));

        if (Random.Range(0, 2) == 0)
        {
            z = -z;
        }
        return new Vector3(x, 0, z);
    }
}
