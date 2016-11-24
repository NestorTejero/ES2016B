using UnityEngine;
using System.Collections;

interface CanUpgrade
{
    bool IsUpgradeable(int numCoins);
    void Upgrade();
}