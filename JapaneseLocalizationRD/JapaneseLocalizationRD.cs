using System.Linq;
using System.Text.RegularExpressions;

namespace JapaneseLocalizationRD {
    internal class JapaneseLocalizationRD {

        private JapaneseLocalizationRD() { }

        private static JapaneseLocalizationRD _inst;

        internal readonly string[] SUFFIX_STREET = new[] { "通", "通", "通", "通", "筋" };
        internal readonly string[] SUFFIX_WIDE_STREET = new[] { "本通", "大通" };
        internal readonly string[] SUFFIX_TUNNEL = new[] { "トンネル" };
        internal readonly string[] SUFFIX_HIGHWAY = new[] { "道路" };
        internal readonly string[] SUFFIX_BRIDGE = new[] { "橋" };
        internal readonly string[] SUFFIX_LARGE_BRIDGE = new[] { "大橋" };

        internal static JapaneseLocalizationRD Instance {
            get {
                if( _inst == null ) {
                    _inst = new JapaneseLocalizationRD();
                }
                return _inst;
            }
        }
        internal string StripTown( string name ) {
            Regex regex = new Regex( @"^(.{2,})([東西南北新本元中])?町$" );
            if( regex.IsMatch( name ) ) {
                Match match = regex.Match( name );
                return match.Groups[1].Value;
            } else {
                return name;
            }
        }

        internal string GetRandomSuffix( int rand, string[] values ) {
            int count = values.Count();
            return values[rand % count];
        }

        internal string[] Texts { get; set; }
    }
}
