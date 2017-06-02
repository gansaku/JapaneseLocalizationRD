using ColossalFramework.Math;
using System.Linq;

namespace JapaneseLocalizationRD.AI {

    /// <summary>
    /// 高架道路の名前生成
    /// </summary>
    public class RoadBridgeAIEx : RoadBridgeAI {

        public override string GenerateName( ushort segmentID, ref NetSegment data ) {

            if( (this.m_info.m_vehicleTypes & VehicleInfo.VehicleType.Car) != VehicleInfo.VehicleType.None
                && (!this.m_highwayRules || this.m_info.m_forwardVehicleLaneCount + this.m_info.m_backwardVehicleLaneCount >= 2) ) {

                JapaneseLocalizationRD RDinst = JapaneseLocalizationRD.Instance;
                Randomizer randomizer = new Randomizer( (int)data.m_nameSeed );
                int random = new Randomizer( (int)data.m_nameSeed ).Int32( (uint)RDinst.Texts.Count() );
                string suffix = null;

                if( this.m_highwayRules ) {
                    suffix = RDinst.GetRandomSuffix( random, RDinst.SUFFIX_HIGHWAY );
                } else {
                    if( this.m_info.m_forwardVehicleLaneCount + this.m_info.m_backwardVehicleLaneCount >= 4 ) {
                        suffix = RDinst.GetRandomSuffix( random, RDinst.SUFFIX_LARGE_BRIDGE );
                    } else {
                        suffix = RDinst.GetRandomSuffix( random, RDinst.SUFFIX_BRIDGE );
                    }
                }

                if( suffix == null ) {
                    return string.Empty;
                }

                return RDinst.StripTown( RDinst.Texts[random] ) + suffix;

            } else {
                return string.Empty;
            }
        }

    }
    /// <summary>
    /// トンネルの名前生成
    /// </summary>
    public class RoadTunnelAIEx : RoadTunnelAI {
        public override string GenerateName( ushort segmentID, ref NetSegment data ) {


            if( (this.m_info.m_vehicleTypes & VehicleInfo.VehicleType.Car) != VehicleInfo.VehicleType.None
                && (!this.m_highwayRules || this.m_info.m_forwardVehicleLaneCount + this.m_info.m_backwardVehicleLaneCount >= 2) ) {

                JapaneseLocalizationRD RDinst = JapaneseLocalizationRD.Instance;
                Randomizer randomizer = new Randomizer( (int)data.m_nameSeed );

                int random = randomizer.Int32( (uint)RDinst.Texts.Count() );

                return RDinst.StripTown( RDinst.Texts[random] ) + RDinst.GetRandomSuffix( random, RDinst.SUFFIX_TUNNEL );

            }
            return string.Empty;

        }
    }
    /// <summary>
    /// 道路の名前生成
    /// </summary>
    public class RoadAIEx : RoadAI {

        public override string GenerateName( ushort segmentID, ref NetSegment data ) {

            if( (this.m_info.m_vehicleTypes & VehicleInfo.VehicleType.Car) != VehicleInfo.VehicleType.None ) {

                JapaneseLocalizationRD RDinst = JapaneseLocalizationRD.Instance;

                int random = new Randomizer( (int)data.m_nameSeed ).Int32( (uint)RDinst.Texts.Count() );
                string suffix = null;

                if( this.m_enableZoning ) {
                    if( (this.m_info.m_setVehicleFlags & Vehicle.Flags.OnGravel) != (Vehicle.Flags)0 ) {
                        suffix = RDinst.GetRandomSuffix( random, RDinst.SUFFIX_STREET );
                    } else if( this.m_info.m_halfWidth >= 12f ) {
                        suffix = RDinst.GetRandomSuffix( random, RDinst.SUFFIX_WIDE_STREET );
                    } else {
                        suffix = RDinst.GetRandomSuffix( random, RDinst.SUFFIX_STREET );
                    }
                } else if( this.m_highwayRules ) {
                    if( this.m_info.m_hasForwardVehicleLanes && this.m_info.m_hasBackwardVehicleLanes ) {
                        suffix = RDinst.GetRandomSuffix( random, RDinst.SUFFIX_HIGHWAY );
                    } else if( this.m_info.m_forwardVehicleLaneCount >= 2 || this.m_info.m_backwardVehicleLaneCount >= 2 ) {
                        suffix = RDinst.GetRandomSuffix( random, RDinst.SUFFIX_HIGHWAY );
                    }
                }

                if( suffix == null ) {
                    return string.Empty;
                }

                return RDinst.StripTown( RDinst.Texts[random] ) + suffix;

            } else {
                return string.Empty;
            }
        }
    }



}
