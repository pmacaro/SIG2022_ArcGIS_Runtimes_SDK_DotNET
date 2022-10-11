using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using Esri.ArcGISRuntime.Data;
using Esri.ArcGISRuntime.Geometry;
using Esri.ArcGISRuntime.Location;
using Esri.ArcGISRuntime.Mapping;
using Esri.ArcGISRuntime.Security;
using Esri.ArcGISRuntime.Symbology;
using Esri.ArcGISRuntime.Tasks;
using Esri.ArcGISRuntime.UI;
using Esri.ArcGISRuntime.Portal;

namespace ArcGISAppDemo2
{
    /// <summary>
    /// Provides map data to an application
    /// </summary>
    public class MapViewModel : INotifyPropertyChanged
    {
        private Map _map;

        /// <summary>
        /// Gets or sets the map
        /// </summary>
        public Map Map
        {
            get { return _map; }
            set { _map = value; OnPropertyChanged(); }
        }

        public MapViewModel()
        {
            // Instanciation de l'objet Map, centré sur Paris
            _map = new Map(BasemapType.StreetsNightVector, 48.8534, 2.3488, 12);

            // Initialisation des couches de la carte
            Initialize();
        }


        private async void Initialize()
        {
            // Création de l'accréditation d'accès
            var cred = await AuthenticationManager.Current.GenerateCredentialAsync(
                                                new Uri("http://ORGANISATION/sharing/rest"),
                                                "IDENTIFIANT_COMPTE_DEVELOPPEUR",
                                                "MDP_COMPTE_DEVELOPPEUR") as ArcGISTokenCredential;
            // Connexion ArcGIS Online 
            ArcGISPortal agol = await ArcGISPortal.CreateAsync(
                                                            new Uri("http://ORGANISATION/sharing/rest"),
                                                            cred,
                                                            System.Threading.CancellationToken.None);


            //// ITEM
            //// Création de l'item 
            //PortalItem featureLayerItem = await PortalItem.CreateAsync(agol, "IDENTIFIANT_ITEM"); // Coller ici l'identifiant de l'item de la couche à afficher

            //// Création d'une nouvelle couche d'entités basée sur l'item
            //FeatureLayer layerHospitals = new FeatureLayer(featureLayerItem, 0);

            //// Ajout de la nouvelle couche d'entité à la carte
            //_map.OperationalLayers.Add(layerHospitals);



            //// SERVICE
            //// Création de l'URI pour initialiser la couche d'entités
            //Uri serviceUri = new Uri("URI_SERVICE/0"); // Coller ici l'URI du service de la couche à afficher, en précisant le numéro de la couche (à partir de 0)

            //// Création d'une nouvelle couche d'entités basée sur l'URI
            //FeatureLayer layerHospitals = new FeatureLayer(serviceUri, cred);

            ////// Filtre sur Paris
            ////layerHospitals.DefinitionExpression = "cp_ville LIKE '%PARIS%'";

            //// Ajout de la nouvelle couche d'entité à la carte
            //_map.OperationalLayers.Add(layerHospitals);
        }

        /// <summary>
        /// Raises the <see cref="MapViewModel.PropertyChanged" /> event
        /// </summary>
        /// <param name="propertyName">The name of the property that has changed</param>
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var propertyChangedHandler = PropertyChanged;
            if (propertyChangedHandler != null)
                propertyChangedHandler(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
