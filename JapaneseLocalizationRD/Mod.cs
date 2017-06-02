
using ICities;
using JapaneseLocalizationRD.Util;
using System.IO;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace JapaneseLocalizationRD {
    /// <summary>
    /// MOD情報
    /// </summary>
    public class JapaneseLocalizationRDMod : IUserMod {

        public string Name => "Japanese Localization RD";

        public string Description => "version." + Version;

        public readonly string Version = "1.0.0.1";
    }


    public class JapaneseLocalizationRDLoadingExtension : LoadingExtensionBase {

        private RedirectCallsState? oldAIState;
        private RedirectCallsState? oldBridgeAIState;
        private RedirectCallsState? oldTunnelAIState;
        private RedirectCallsState? oldDistrictManagerState;

        public override void OnLevelLoaded( LoadMode mode ) {
            base.OnLevelLoaded( mode );

            Debug.Log( "Detour..." );

            oldAIState = RedirectionHelper.RedirectCalls( 
                typeof( RoadAI ).GetMethod( "GenerateName", BindingFlags.Instance | BindingFlags.Public ), 
                typeof( AI.RoadAIEx ).GetMethod( "GenerateName", BindingFlags.Instance | BindingFlags.Public ) );
            oldBridgeAIState = RedirectionHelper.RedirectCalls( 
                typeof( RoadBridgeAI ).GetMethod( "GenerateName", BindingFlags.Instance | BindingFlags.Public ), 
                typeof( AI.RoadBridgeAIEx ).GetMethod( "GenerateName", BindingFlags.Instance | BindingFlags.Public ) );
            oldTunnelAIState = RedirectionHelper.RedirectCalls( 
                typeof( RoadTunnelAI ).GetMethod( "GenerateName", BindingFlags.Instance | BindingFlags.Public ),
                typeof( AI.RoadTunnelAIEx ).GetMethod( "GenerateName", BindingFlags.Instance | BindingFlags.Public ) );
            oldDistrictManagerState = RedirectionHelper.RedirectCalls( 
                typeof( DistrictManager ).GetMethod( "GenerateName", BindingFlags.Instance | BindingFlags.NonPublic ), 
                typeof( AI.DistrictManagerEx ).GetMethod( "GenerateName", BindingFlags.Instance | BindingFlags.Public ) );

            Debug.Log( "Detour success " );
            //地名ファイルの読み込み
            JapaneseLocalizationRD.Instance.Texts = File.ReadAllLines( Path.Combine( ModUtil.GetModPath(), "strings.txt" ) );

            Debug.Log( "Localization File Loaded.  " + JapaneseLocalizationRD.Instance.Texts.Count() );
        }

        public override void OnLevelUnloading() {
            Debug.Log( "OnLevelUnloading" );

            if( oldAIState != null ) {
                RedirectionHelper.RevertRedirect( typeof( RoadAI ).GetMethod( "GenerateName", BindingFlags.Instance | BindingFlags.Public ), oldAIState.Value );
            }
            if( oldBridgeAIState != null ) {
                RedirectionHelper.RevertRedirect( typeof( RoadBridgeAI ).GetMethod( "GenerateName", BindingFlags.Instance | BindingFlags.Public ), oldBridgeAIState.Value );
            }
            if( oldTunnelAIState != null ) {
                RedirectionHelper.RevertRedirect( typeof( RoadTunnelAI ).GetMethod( "GenerateName", BindingFlags.Instance | BindingFlags.Public ), oldTunnelAIState.Value );
            }
            if( oldDistrictManagerState != null ) {
                RedirectionHelper.RevertRedirect( typeof( DistrictManager ).GetMethod( "GenerateName", BindingFlags.Instance | BindingFlags.NonPublic ), oldDistrictManagerState.Value );
            }

            oldBridgeAIState = null;
            oldAIState = null;
            oldTunnelAIState = null;
            oldDistrictManagerState = null;

            JapaneseLocalizationRD.Instance.Texts = null;

            base.OnLevelUnloading();
        }

        public override void OnReleased() {

            Debug.Log( "Mod Released" );
            base.OnReleased();
        }
    }


}
