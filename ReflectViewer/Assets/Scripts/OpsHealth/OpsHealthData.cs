using System;

namespace Unity.Reflect.Viewer.UI
{
    /// <summary>
    /// Practicing adding additional functionality to Viewer. 
    /// </summary>
    public struct OpsHealthData : IEquatable<OpsHealthData>
    {
        public bool showGameObjectsWithIotData;

        public bool Equals(OpsHealthData other)
        {
            return showGameObjectsWithIotData == other.showGameObjectsWithIotData;
        }

        public override bool Equals(object obj)
        {
            return obj is OpsHealthData other && Equals(other);
        }

        public override int GetHashCode()
        {
            return showGameObjectsWithIotData.GetHashCode();
        }

        public static bool operator ==(OpsHealthData a, OpsHealthData b)
        {
            return a.Equals(b);
        }

        public static bool operator !=(OpsHealthData a, OpsHealthData b)
        {
            return !(a == b);
        }
    }
}
