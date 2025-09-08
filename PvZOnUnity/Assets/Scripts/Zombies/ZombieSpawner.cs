using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class ZombieSpawner : MonoBehaviour
{
    public Transform[] spawnPoints;
    public GameObject[] zombies; // Индексы должны соответствовать zombieTypes
    public string waveConfig = "1 2 3; 1 2; 1 2; 2 5; 1 2 4; 1 2 2 4; 1 1 2 2 3; wave 1 1 1 1 1 1 1 2 2 2 3 3 5;";

    private List<int> zombieTypes;
    private Queue<Wave> waves = new Queue<Wave>();

    private void Start()
    {
        ParseWaveConfig(waveConfig);
        StartCoroutine(SpawnWaves());
    }

    private void ParseWaveConfig(string config)
    {
        var parts = config.Split(';').Select(p => p.Trim()).Where(p => !string.IsNullOrEmpty(p)).ToList();

        zombieTypes = parts[0].Split(' ').Select(int.Parse).ToList();

        for (int i = 1; i < parts.Count; i++)
        {
            string part = parts[i];
            bool isFinalWave = part.StartsWith("wave");

            if (isFinalWave)
            {
                Debug.Log("🔥 Большая волна!");
                part = part.Substring(4).Trim();
            }

            var tokens = part.Split(' ').Select(int.Parse).ToList();
            if (tokens.Count < 2) continue;

            int delay = tokens.Last();
            var zombieIds = tokens.Take(tokens.Count - 1).ToList();

            waves.Enqueue(new Wave { zombieIds = zombieIds, delay = delay });
        }
    }

    private IEnumerator<WaitForSeconds> SpawnWaves()
    {
        while (waves.Count > 0)
        {
            Wave wave = waves.Dequeue();
            yield return new WaitForSeconds(wave.delay);

            foreach (int id in wave.zombieIds)
            {
                if (id < 0 || id >= zombies.Length) continue;

                int spawnPoint = Random.Range(0, spawnPoints.Length);
                Instantiate(zombies[id], spawnPoints[spawnPoint].position, Quaternion.identity);
            }
        }
    }

    private class Wave
    {
        public List<int> zombieIds;
        public int delay;
    }
}