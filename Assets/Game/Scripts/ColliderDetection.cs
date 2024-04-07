using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Scenes.Scripts;
using UnityEngine;


public class ColliderDetection : MonoBehaviour {

    [SerializeField] 
    private List<ZoneConfig> _zoneConfigs;


    private void Awake() {
        // TODO: - 
        // var drunkSettings = Drunk.GetSettings();
        // drunkSettings.drunkenness = 0f;
    }

    private void OnTriggerEnter(Collider other) {
        Debug.Log($"XXX TRIGGER ENTER: {other.name} {other.tag}");

        var zoneConfig = _zoneConfigs.FirstOrDefault(zoneConfig => other.CompareTag(zoneConfig.Id));
        if (zoneConfig == null) {
            Debug.LogError($"ZoneConfig not found for {other.name}");
            return;
        }
        SetupZoneConfig(zoneConfig);

        if (zoneConfig.Id == "zone1") {
            // BootController.Instance.RestartGame();
        }
    }

    private void SetupZoneConfig(ZoneConfig zoneConfig) {
        Game.Instance.ZmenTlacitka = zoneConfig.ZmenTlacitka;
        Game.Instance.VariantZmenyTlacitok = zoneConfig.VariantZmenyTlacitok;
        Game.Instance.ZmenTlacitkaKedStojis = zoneConfig.ZmenTlacitkaKedStojis;
        Game.Instance.ZmenTlacitkaPoCase = zoneConfig.ZmenTlacitkaPoCase;
        
        Game.Instance.TocenieHlavy = zoneConfig.TocenieHlavy;
        Game.Instance.AkoMocTaToci = zoneConfig.AkoMocTaToci;
        Game.Instance.DlzkaJednohoTocenia = zoneConfig.DlzkaJednohoTocenia;
        
        Game.Instance.ZanasanieDoStrany = zoneConfig.ZanasanieDoStrany;
        Game.Instance.AkoMocTaZanasa = zoneConfig.AkoMocTaZanasa;
        Game.Instance.DlzkaJednehoZanosuDoStrany = zoneConfig.DlzkaJednehoZanosuDoStrany;

        // TODO : - 
        // var drunkSettings = Drunk.GetSettings();
        // drunkSettings.drunkenness = zoneConfig.Drunkenness;
        //
        // var blurSettings = Blurry.GetSettings();
        // blurSettings.strength = zoneConfig.Blurr;
    }
}
