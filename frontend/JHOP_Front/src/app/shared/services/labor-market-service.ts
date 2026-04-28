import { inject, Injectable } from '@angular/core';
import { AuthService } from './auth-service';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../environments/environment.development';

@Injectable({
  providedIn: 'root',
})
export class LaborMarketService {
  private http = inject(HttpClient);
  private authService = inject(AuthService);

  // Get Offers by Category
  GetOffersByCategory(categoryId: number) {
    return this.http.get(environment.BaseUrl + `/GetLaborMarketOffersByCategory/${categoryId}`)
  }

}
