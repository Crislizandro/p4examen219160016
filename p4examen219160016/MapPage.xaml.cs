using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plugin.Geolocator;

using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace p4examen219160016
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MapPage : ContentPage
	{
		public MapPage ()
		{
			InitializeComponent ();
		}
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            var localizacion = CrossGeolocator.Current;
            if (localizacion != null)
            {
                if (!localizacion.IsListening)
                {
                    await localizacion.StartListeningAsync(TimeSpan.FromSeconds(10), 100);
                }

                var posicion = await localizacion.GetPositionAsync();
                var centromap = new Xamarin.Forms.Maps.Position(posicion.Latitude, posicion.Longitude);
                mapa.MoveToRegion(new MapSpan(centromap, 1, 1));

                Pin ubicacion = new Pin();
                ubicacion.Label = "EEUU";
                ubicacion.Address = "ESTOY EN ESTADOS UNIDOS";
                ubicacion.Position = new Xamarin.Forms.Maps.Position( posicion.Latitude , posicion.Longitude);
                mapa.Pins.Add(ubicacion);
            }
            else {
                var posicion = await localizacion.GetLastKnownLocationAsync();
                var centromap = new Xamarin.Forms.Maps.Position(posicion.Latitude, posicion.Longitude);
                mapa.MoveToRegion(new MapSpan(centromap, 1, 1));
            }
        }
    }

}