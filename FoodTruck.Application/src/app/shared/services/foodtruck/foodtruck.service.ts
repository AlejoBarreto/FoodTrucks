import { Injectable } from '@angular/core';
import { FoodTruck } from '../../types/foodtruck.type';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class FoodTruckService {
  apiHost = 'https://localhost:7201'
  constructor(
    private httpClient: HttpClient
  ) { }

  getFoodTrucksInRadius(latitude: string, longitude: string, radius: number):Observable<FoodTruck[]>
  {
    return this.httpClient.get<FoodTruck[]>
      (`${this.apiHost}/foodTruck-api/FoodTruck/GetFoodTrucksAsync?Latitude=${latitude}&Longitude=${longitude}&Radius=${radius}`)
  }
}
