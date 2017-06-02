using ColossalFramework.Math;
using System.Linq;

namespace JapaneseLocalizationRD.AI {

    public class DistrictManagerEx : DistrictManager {
        /// <summary>
        /// 地名の生成
        /// </summary>
        /// <param name="district"></param>
        /// <returns></returns>
        public string GenerateName( int district ) {

            Randomizer randomizer = new Randomizer( this.m_districts.m_buffer[district].m_randomSeed );
            string[] texts = JapaneseLocalizationRD.Instance.Texts;
            return texts[randomizer.Int32( (uint)texts.Count() )];
        }

    }
}
