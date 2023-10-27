using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

[TestFixture]
public class BreakablePlatformTests
{
    Scene _scene;
    GameObject[] _rootObjects;

    [SetUp]
    public void SetUp()
    {
        _scene = SceneManager.LoadScene("BreakablePlatformTest", new LoadSceneParameters());
        _rootObjects = _scene.GetRootGameObjects();
    }

    [UnityTest]
    public IEnumerator RespawnColliderShouldDisableOnBreak()
    {
        // Find the game object by class type
        BreakablePlatform platform = GameObject.FindFirstObjectByType<BreakablePlatform>();

        // Test if the object is active
        Assert.IsNotNull(platform);
        Assert.IsTrue(platform.isActiveAndEnabled);

        // Perform coroutine
        yield return platform.TriggerBreak();

        // Get children from the object by class type
        Collider[] colliders = platform.GetComponentsInChildren<Collider>();

        // Get specific object by name
        Collider respawnCollider = colliders.First(x => x.name.Contains("BreakablePlatform"));
        Collider trigger = colliders.First(x => x.name.Contains("TriggerBox"));

        // Test case
        Assert.IsFalse(respawnCollider.enabled);
    }
}
