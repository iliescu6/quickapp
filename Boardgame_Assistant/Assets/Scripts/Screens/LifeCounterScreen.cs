using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeCounterScreen : MonoBehaviour, ScreenInterface
{
    [SerializeField] CounterLogic counterPrefab;

    [SerializeField] GameObject leftSideFour;
    [SerializeField] GameObject rightSideFour;
    [SerializeField] GameObject containerOne;//container used for 1/2/3 players
    List<CounterLogic> counters = new List<CounterLogic>();
    SerializablePreset currentPreset;

    private void Awake()
    {

    }

    public void Restart()
    {
        int index = 1;
        foreach (CounterLogic counter in counters)
        {
            counter.Initialize(currentPreset, index);
            index++;
        }
    }

    public void DeleteCurrentMatchSession()
    {
        for (int i = counters.Count - 1; i >= 0; i--)
        {
            Destroy(counters[i].gameObject);
        }
        counters = new List<CounterLogic>();
        gameObject.SetActive(false);
    }

    public void Initialize(SerializablePreset preset)
    {
        ScreensController.Instance.currentScreen = this;
        ScreensController.Instance.ShowOptionsButton();
        currentPreset = preset;
        switch (preset.lifeCounter.players)
        {
            case (0):
                CounterLogic counterOne = (CounterLogic)Instantiate(counterPrefab, containerOne.transform);
                counterOne.Initialize(preset, 1);
                counters.Add(counterOne);
                break;
            case (1):
                for (int i = 0; i < 2; i++)
                {
                    CounterLogic counter = (CounterLogic)Instantiate(counterPrefab, containerOne.transform);
                    if (preset.lifeCounter.matchType == 0)
                    {
                        counter.rectTransform.Rotate(new Vector3(0, 0, 180 * i));
                    }
                    counter.Initialize(preset, i + 1);
                    counters.Add(counter);
                }
                break;
            case (2):
                for (int i = 0; i < 3; i++)
                {
                    CounterLogic counter = (CounterLogic)Instantiate(counterPrefab, containerOne.transform);
                    counter.Initialize(preset, i + 1);
                    counters.Add(counter);
                }
                break;
            case (3):
                if (preset.lifeCounter.matchType == 0 || preset.lifeCounter.matchType == 2)//1-free for all, 3= 2 vs 2
                {
                    for (int i = 0; i < 4; i++)
                    {
                        CounterLogic counter = null;
                        if (i < 2)
                        {
                            counter = (CounterLogic)Instantiate(counterPrefab, leftSideFour.transform);
                        }
                        else
                        {
                            counter = (CounterLogic)Instantiate(counterPrefab, rightSideFour.transform);
                            counter.rectTransform.Rotate(new Vector3(0, 0, 180 * i));
                        }
                        counter.Initialize(preset, i + 1);
                        counters.Add(counter);
                    }
                }
                else
                {
                    for (int i = 0; i < 3; i++)
                    {
                        CounterLogic counter = (CounterLogic)Instantiate(counterPrefab, leftSideFour.transform);
                        counter.Initialize(preset, i + 1);
                        counters.Add(counter);
                    }
                    CounterLogic rightcounter = (CounterLogic)Instantiate(counterPrefab, rightSideFour.transform);
                    rightcounter.rectTransform.Rotate(new Vector3(0, 0, 180));
                    rightcounter.Initialize(preset, 4, 1);
                    counters.Add(rightcounter);
                }
                break;
        }
    }
}
