using System.Collections;
using System.Collections.Generic;
using Scenes.Scripts;
using UnityEngine;
using UnityEngine.Serialization;

public class Game : MonoBehaviour {
    [Header("TLACITKA")]
    [SerializeField]
    public bool ZmenTlacitka = false;
 
    [SerializeField]
    public int VariantZmenyTlacitok = 0;
    
    [SerializeField]
    public bool ZmenTlacitkaKedStojis = false;

    [SerializeField] 
    public float ZmenTlacitkaPoCase;
    

    [Space(10)]
    [Header("TOCENIE HLAVY")]
    
    [SerializeField] 
    public bool TocenieHlavy;

    [SerializeField]
    public float AkoMocTaToci = 0.01f;
    
    [SerializeField] 
    public float DlzkaJednohoTocenia = 1f;
       

    [Space(10)]
    [Header("ZANASANIE DO STRANY")]
    [SerializeField] 
    public bool ZanasanieDoStrany;

    [SerializeField]
    public float AkoMocTaZanasa = 0.5f;
    
    [SerializeField] 
    public float DlzkaJednehoZanosuDoStrany = 3f;
    
    
        private static Game _instance;
      
        private void Awake() {
            if (_instance == null) {
                _instance = this;
            }
            else {
                Destroy(gameObject);
            }
        }

        public static Game Instance => _instance;


        public void ApplyZoneConfig(ZoneConfig zoneConfig) {
            Debug.Log($"XXX Zone {zoneConfig.Id}");
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

