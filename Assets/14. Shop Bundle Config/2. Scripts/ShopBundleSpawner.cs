using UnityEngine;

public class ShopBundleSpawner : MonoBehaviour
{
    [SerializeField] private ShopBundleData ShopBundleData;
    [SerializeField] private GameObject BundlesGroup;
    [SerializeField] private GameObject NoAdsGroup;
    [SerializeField] private GameObject CoinsGroup;
    [SerializeField] private ShopBundleConfig ItemBundlePrefab;
    [SerializeField] private ShopBundleConfig SingleItemPrefab;

    private void Start()
    {
        for (int i = 0; i < ShopBundleData.BundleList.Count; i++)
        {
            switch (ShopBundleData.BundleList[i].Group)
            {
                case BundleGroup.Bundles:
                    SpawnBundle(ShopBundleData.BundleList[i], BundlesGroup);
                    break;
                case BundleGroup.NoAds:
                    SpawnBundle(ShopBundleData.BundleList[i], NoAdsGroup);
                    break;
                case BundleGroup.Coins:
                    SpawnBundle(ShopBundleData.BundleList[i], CoinsGroup);
                    break;
            }
        }
    }

    private void SpawnBundle(Bundle bundle, GameObject bundleGroup)
    {
        if (bundle.ItemList.Count == 0) return;

        if (bundle.ItemList.Count == 1)
        {
            ShopBundleConfig newBundleConfig = PoolingSystem.Spawn<ShopBundleConfig>(
                SingleItemPrefab.gameObject,
                bundleGroup.transform,
                SingleItemPrefab.transform.localScale,
                Vector3.zero,
                Quaternion.identity);

            newBundleConfig.LoadData(bundle);
        }
        else
        {
            ShopBundleConfig newBundleConfig = PoolingSystem.Spawn<ShopBundleConfig>(
                ItemBundlePrefab.gameObject,
                bundleGroup.transform,
                ItemBundlePrefab.transform.localScale,
                Vector3.zero,
                Quaternion.identity);

            newBundleConfig.LoadData(bundle);
        }
    }
}
