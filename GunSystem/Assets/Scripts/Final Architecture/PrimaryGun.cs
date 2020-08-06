using UnityEngine;

namespace GunSystem
{
    [CreateAssetMenu(fileName = "new PrimaryGun", menuName = "Guns/PrimaryGun", order = 1)]
    public class PrimaryGun : BaseGun
    {
        protected override void Fire()
        {
            throw new System.NotImplementedException();
        }
    }
}