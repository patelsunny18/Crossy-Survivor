using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    public List<GameObject> enemyPrefabList = new List<GameObject>();

    public GameObject enemyPrefab;
    public GameObject bossPrefab;
    [SerializeField] private float spawnRate = 10f;
    private float spawnTimer; 

    // Update is called once per frame
    void Update()
    {
        //Only spawn normal enemies when in normal phase(not boss phase)
        if(!GameManager.getIsBossPhase()){
            SpawnEnemy();
        }
    }

    private void SpawnEnemy() {
        if (Time.time > spawnTimer) {
           
            Vector3 cameraPosition = GameManager.instance.camera.transform.position;
            int newX = 13;
            int leftOrRight = (Random.Range(0,2));
            int newZ = Random.Range(15,25);
            if(leftOrRight==1){
                newX = -13;
            }

            int randomEnemy = Random.Range(0, 3);
   
            Vector3 randomLocation = new Vector3(cameraPosition.x +newX,1,cameraPosition.z+newZ);
            Instantiate(enemyPrefabList[randomEnemy], randomLocation, transform.rotation);
            spawnTimer = Time.time + spawnRate;
        }
    }

    public void SpawnBoss(){
        Vector3 cameraPosition = GameManager.instance.camera.transform.position;
        Vector3 bossLocation = new Vector3(cameraPosition.x,1,cameraPosition.z+15);
        Instantiate(bossPrefab, bossLocation, transform.rotation);
    }
}