import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { TOKEN_KEY } from './constants';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  
  private http = inject(HttpClient);
  
  register (formData : any){
    return this.http.post(environment.BaseUrl + '/signup',formData);
  }
  login (formData : any){
    return this.http.post(environment.BaseUrl + '/signin',formData);
  }

  isLoggedIn(){
    return this.getToken() != null? true:false;
  }

  saveToken(token:string){
    localStorage.setItem(TOKEN_KEY,token);  
  }

  getToken(){
    return localStorage.getItem(TOKEN_KEY);
  }

  deleteToken(){
    localStorage.removeItem(TOKEN_KEY);
  }
  getClaims(){
    return JSON.parse(window.atob(this.getToken()!.split('.')[1]))
  }


}
