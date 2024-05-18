using Unity.Netcode;
using UnityEngine;

namespace NetworkTest
{
    public class NetworkTestPlayer : NetworkBehaviour
    {
        public NetworkVariable<Vector3> NetworkPosition = new NetworkVariable<Vector3>();

        public override void OnNetworkSpawn()
        {
            if (IsOwner)
                ForceMove();
        }

        public void ForceMove()
        {
            SubmitPositionRequestServerRpc(GetRandomPositionOnPlane());
        }

        [Rpc(SendTo.Server)]
        public void SubmitPositionRequestServerRpc(Vector3 position, RpcParams rpcParams = default)
        {
            transform.position = NetworkPosition.Value = position;
        }

        static Vector3 GetRandomPositionOnPlane()
        {
            return new Vector3(Random.Range(-3f, 3f), 1f, Random.Range(-3f, 3f));
        }

        void Update()
        {
            transform.position = NetworkPosition.Value;
        }
    }
}