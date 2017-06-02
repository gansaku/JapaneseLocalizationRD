using ColossalFramework;
using ColossalFramework.Plugins;

namespace JapaneseLocalizationRD.Util {
    /// <summary>
    /// 
    /// </summary>
    internal class ModUtil {

        /// <summary>
        /// このMODの配置されるフォルダを取得します。
        /// </summary>
        /// <returns></returns>
        internal static string GetModPath() {
            PluginManager manager = Singleton<PluginManager>.instance;

            foreach( PluginManager.PluginInfo current in manager.GetPluginsInfo() ) {
                if( current.assembliesString?.StartsWith( "JapaneseLocalizationRD" ) ?? false ) {
                    return current.modPath;
                }
            }
            return null;
        }
    }
}
