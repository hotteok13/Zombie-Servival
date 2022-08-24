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
            원의 방정식에서 임의의 x랑 z에 해당하는 점이 반지름에 r인 원 안에 존재하는 범위입니다.
            
            0.3*2+z*2=1
            z=루트1*2-0.3*2
            z=루트 1-0.09
            z= 루트 0.91
            z=0.95(근사값)

            반지름 1인 원의 값으로(0.3,0.95)
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
