import { Component, Input } from '@angular/core';
import * as L from 'leaflet';
import { FoodTruck } from '../../types/foodtruck.type';
import { FoodTruckService } from '../../services/foodtruck/foodtruck.service';
import { Subscription } from 'rxjs';
import { icon, Marker } from 'leaflet';

const iconRetinaUrl = 'assets/marker-icon-2x.png';
const iconUrl = 'assets/marker-icon.png';
const shadowUrl = 'assets/marker-shadow.png';
const iconDefault = icon({
  iconRetinaUrl,
  iconUrl,
  shadowUrl,
  iconSize: [25, 41],
  iconAnchor: [12, 41],
  popupAnchor: [1, -34],
  tooltipAnchor: [16, -28],
  shadowSize: [41, 41]
});
Marker.prototype.options.icon = iconDefault;
@Component({
  selector: 'app-map',
  templateUrl: './map.component.html',
  styleUrls: ['./map.component.css']
})
export class MapComponent {
  public foodTrucksData: any;
  public showMap = false;
  public showSearchButton = false;
  public isLoading = false;
  private centerLatitude = 37.7749;
  private centerLongitude = -122.4194;
  private newCenterLatitude = 0;
  private newCenterLongitude = 0;
  private newRadius = 0;
  private markers: L.Marker[] = [];
  foodTruckSubscription: Subscription;

  constructor(
    private foodTruckService : FoodTruckService,
  ){
    this.foodTruckSubscription = this.foodTruckService.getFoodTrucksInRadius(this.centerLatitude.toString(), this.centerLongitude.toString(), 300).subscribe(data => {
      this.foodTrucksData = data;
      this.showMap = true;
      this.initMap();
      this.addMarkers();   
    });

  }
  private map!: L.Map;

  ngOnInit(): void {
    
  }

  private initMap(): void {
    this.map = L.map('map').setView([this.centerLatitude, this.centerLongitude], 16); // Coordenadas por defecto (San Francisco)

    L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
      attribution: '&copy; <a href="https://www.openstreetmap.org/copyright">OpenStreetMap</a> contributors'
    }).addTo(this.map);

    this.map.on('moveend zoomend', () => {      
      const center = this.map.getCenter();
      const radius = this.calculateRadius();

      this.newCenterLatitude = center.lat;
      this.newCenterLongitude = center.lng;
      this.newRadius = radius > 1500 ? 1500 : Math.floor(radius);

      this.showSearchButton = true;
    });
  }

  private addMarkers(): void {
    this.foodTrucksData.forEach((truck: FoodTruck) => {
      const { latitude, longitude } = truck.location_2;
      
      const marker = L.marker([Number(latitude), Number(longitude)]);
      
      if (latitude && longitude) {
        marker
          .addTo(this.map)
          .bindPopup(`${truck.optionalText}`);
        this.markers.push(marker);
      }
    });
  }

  handleGetFoodTrucksSuccess(data: any) {
    this.foodTrucksData = data;
  }

  handleGetFoodTrucksError(data: any) {
    console.log('error');
  }

  calculateRadius(): number {
    // Obtiene el tamaño del mapa en píxeles
    const mapSize = this.map.getSize();

    // Convierte la distancia en píxeles a una distancia real en metros
    const point1 = this.map.getCenter();
    const point2 = this.map.containerPointToLatLng([mapSize.x / 2, mapSize.y]);
    
    // Calcula la distancia entre el centro del mapa y el borde
    const distance = this.map.distance(point1, point2);

    return distance; // Devuelve la distancia en metros
  }

  onSearch():void{
    this.isLoading = true;
    this.foodTruckService.getFoodTrucksInRadius(this.newCenterLatitude.toString(), this.newCenterLongitude.toString(), this.newRadius).subscribe(data => {      
      this.foodTrucksData = data;
      this.clearMarkers();
      this.addMarkers();
      this.isLoading = false;        
    });        
    this.showSearchButton = false;
  }

  clearMarkers(): void {
    this.markers.forEach(marker => this.map.removeLayer(marker));
    this.markers = [];
  }

  ngOnDestroy():void{
    this.foodTruckSubscription.unsubscribe();
  }
}