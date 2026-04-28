import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { AuthService } from './auth-service';
import { environment } from '../../../environments/environment';

@Injectable({
  providedIn: 'root',
})
export class CvService {
  
  private http = inject(HttpClient);
  private authService = inject(AuthService);

  GenerateCV(formData: any){
    return this.http.post(environment.BaseUrl + "/GenerateCv", formData)
  }
  GetCvFile(){
    return this.http.get(environment.BaseUrl + "/GetCVFile")
  }


}
