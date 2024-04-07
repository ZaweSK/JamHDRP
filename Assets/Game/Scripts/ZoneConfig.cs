using System;
using System.Collections.Generic;
using UnityEngine;

namespace Scenes.Scripts {
 

        [CreateAssetMenu(menuName = "Stuff/new ZoneConfig")]
        public class ZoneConfig: ScriptableObject {

                public string Id;
                
                [Space(10)]
                [Header("TLACITKA")]
                public bool ZmenTlacitka = false;
                public int VariantZmenyTlacitok = 0;
                public bool ZmenTlacitkaKedStojis;
                public float ZmenTlacitkaPoCase;
    

                [Space(10)]
                [Header("TOCENIE HLAVY")]
                public bool TocenieHlavy;
                public float AkoMocTaToci;
                public float DlzkaJednohoTocenia;
       

                [Space(10)]
                [Header("ZANASANIE DO STRANY")]
                public bool ZanasanieDoStrany;
                public float AkoMocTaZanasa;
    
                public float DlzkaJednehoZanosuDoStrany;
                
                
                [Space(10)]
                [Header("FEELING")]
                public float Drunkenness;
                public float Blurr;
        }
        
}