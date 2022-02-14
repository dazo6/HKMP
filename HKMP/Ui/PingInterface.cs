using Hkmp.Game.Settings;
using Hkmp.Networking.Client;
using Hkmp.Ui.Component;
using Hkmp.Ui.Resources;
using Hkmp.Util;
using UnityEngine;

namespace Hkmp.Ui {
    public class PingInterface {
        /// <summary>
        /// The margin between the text and the borders of the screen.
        /// </summary>
        private const float IconScreenBorderMargin = 20f;
        
        /// <summary>
        /// The margin between the text and the border of the screen.
        /// </summary>
        private const float TextScreenBorderMargin = 15f;

        /// <summary>
        /// The margin between the icon and the text.
        /// </summary>
        private const float IconTextMargin = 25f;

        /// <summary>
        /// The maximum width of the text component.
        /// </summary>
        private const float TextWidth = 50f;

        /// <summary>
        /// The maximum height of the text component.
        /// </summary>
        private const float TextHeight = 25f;

        /// <summary>
        /// The size (width and height) of the icon displayed in front of the text.
        /// </summary>
        private const float IconSize = 20f;

        private readonly ComponentGroup _pingComponentGroup;
        private readonly ModSettings _modSettings;
        private readonly NetClient _netClient;

        public PingInterface(
            ComponentGroup pingComponentGroup,
            ModSettings modSettings,
            NetClient netClient
        ) {
            _pingComponentGroup = pingComponentGroup;
            _modSettings = modSettings;
            _netClient = netClient;

            // Since we are initially not connected, we disable the object by default
            pingComponentGroup.SetActive(false);

            new ImageComponent(
                pingComponentGroup,
                new Vector2(
                    IconScreenBorderMargin, 1080f - IconScreenBorderMargin),
                new Vector2(IconSize, IconSize),
                TextureManager.NetworkIcon
            );

            var pingTextComponent = new TextComponent(
                pingComponentGroup,
                new Vector2(
                    IconScreenBorderMargin + IconSize + IconTextMargin, 1080f - TextScreenBorderMargin),
                new Vector2(TextWidth, TextHeight),
                "",
                FontManager.UIFontRegular,
                15,
                alignment: TextAnchor.MiddleLeft
            );

            // Register on update so we can set the text to the latest average RTT
            MonoBehaviourUtil.Instance.OnUpdateEvent += () => {
                if (!netClient.IsConnected) {
                    return;
                }

                pingTextComponent.SetText(netClient.UpdateManager.AverageRtt.ToString());
            };
        }

        public void SetEnabled(bool enabled) {
            _pingComponentGroup.SetActive(enabled && _netClient.IsConnected && _modSettings.DisplayPing);
        }
    }
}