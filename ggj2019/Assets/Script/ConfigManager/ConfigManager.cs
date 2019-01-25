using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DaemonTools
{
    public class ConfigManager : Singleton<ConfigManager>
    {
        //声明配置表名
        const string UIPanelConfigName = "uiPanel";
        const string WordsConfigName = "Words";
        const string EndConfigName = "End";


        //声明配置表存储
        public Dictionary<string, UIPanelConfig> UIPanelConfigData;
        public Dictionary<int, WordsConfig> WordsConfigData;
        public Dictionary<int, EndConfig> EndConfigData;

        //用于游戏开始前启动
        public void InitUIConfig()
        {
            UIPanelConfigData = ConfigFactory<UIPanelConfig>.InitConfigs(UIPanelConfigName);
        }

        //实例化配置表
        public void InitConfigManager()
        {
            WordsConfigData = ConfigFactory<WordsConfig>.InitConfigs(WordsConfigName);
            EndConfigData = ConfigFactory<EndConfig>.InitConfigs(EndConfigName);
        }
    }
}