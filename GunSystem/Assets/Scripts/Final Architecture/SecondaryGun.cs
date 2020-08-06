using UnityEngine;

namespace GunSystem
{
    [CreateAssetMenu(fileName = "new SecondaryGun", menuName = "Guns/SecondaryGun", order = 2)]
    public class SecondaryGun : BaseGun
    {
        protected override void Fire()
        {
            throw new System.NotImplementedException();
        }
    }
}