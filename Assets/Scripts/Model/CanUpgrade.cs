internal interface CanUpgrade
{
    bool IsUpgradeable(int numCoins);
    void Upgrade();
}