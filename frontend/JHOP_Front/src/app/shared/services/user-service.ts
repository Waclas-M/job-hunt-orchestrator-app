import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { AuthService } from './auth-service';
import { environment } from '../../../environments/environment';

@Injectable({
  providedIn: 'root',
})
export class UserService {
  private http = inject(HttpClient);
  private authService = inject(AuthService);
  
  // Get Profile Data
  GetManyProfiles(){
    return this.http.get(environment.BaseUrl + "/GetUserProfiles")
  }

  GetProfileById(id : any){
    return this.http.get(environment.BaseUrl + `/GetUserProfile?id=${id}`)
  }
  
  GetProfilePhotos(){
    return this.http.get(environment.BaseUrl + "/GetUserProfilesPhotos")
  }


  // PersonalData

  PostPersonalData(formData: any){
    return this.http.post(environment.BaseUrl + "/AddProfilePersonalData", formData)
  }

  EditPersonalData(formData: any){
    return this.http.put(environment.BaseUrl + "/EditProfilePersonalData", formData)
  }

  EditPersonalDataLinks(formData: any){
    return this.http.put(environment.BaseUrl + "/EditProfileLinks", formData)
  }
  EditUserPersonalProfileData(formData: any){
    return this.http.put(environment.BaseUrl + "/EditProfileUserProfile", formData)
  }

  // Education

  PostEducation(formData: any){
    return this.http.post(environment.BaseUrl + "/AddProfileEducation", formData)
  }
  EditEducation(formData: any){
    return this.http.put(environment.BaseUrl + "/EditProfileEducation", formData)
  }
  

  DeleteEducation(id: any ){
    return this.http.delete(environment.BaseUrl + `/DeleteProfileEducation/${id}`)
  }
  // Experience

  PostExperience(formData: any){
    return this.http.post(environment.BaseUrl + "/AddProfileExperience", formData)
  }
  EditExperience(formData: any){
    return this.http.put(environment.BaseUrl + "/EditProfileExperience", formData)
  }


  DeleteExperience(id: any ){
    return this.http.delete(environment.BaseUrl + `/DeleteProfileExperience/${id}`)
  }
  // Language

  PostLanguage(formData:any){
    return this.http.post(environment.BaseUrl + "/AddProfileLanguage", formData)
  }
  EditLanguage(formData: any){
    return this.http.put(environment.BaseUrl + "/EditProfileLanguage", formData)
  }


  DeleteLanguage(id: any ){
    return this.http.delete(environment.BaseUrl + `/DeleteProfileLanguage/${id}`)
  }
  //Interest

  PostInterest(formData:any){
    return this.http.post(environment.BaseUrl + "/AddProfileInterest", formData)
  }
  EditInterest(formData: any){
    return this.http.put(environment.BaseUrl + "/EditProfileInterest", formData)
  }


  DeleteInterest(id: any ){
    return this.http.delete(environment.BaseUrl + `/DeleteProfileInterest/${id}`)
  }
  // Skill

  PostSkill(formData:any){
    return this.http.post(environment.BaseUrl + "/AddProfileSkill", formData)
  }
  EditSkill(formData: any){
    return this.http.put(environment.BaseUrl + "/EditProfileSkill", formData)
  }



  DeleteSkill(id: any ){
    return this.http.delete(environment.BaseUrl + `/DeleteProfileSkill/${id}`)
  }

  // Strenght
  PostStrenght(formData:any){
    return this.http.post(environment.BaseUrl + "/AddProfileStrenght", formData)
  }
  EditStrenght(formData: any){
    return this.http.put(environment.BaseUrl + "/EditProfileStrenght", formData)
  }



  DeleteStrenght(id: any ){
    return this.http.delete(environment.BaseUrl + `/DeleteProfileStrenght/${id}`)
  }

  // Profile Photo
  PostProfilePhoto(formData:any){
    return this.http.post(environment.BaseUrl + "/AddProfilePhoto", formData)
  }

  DeleteProfilePhoto(id: any){
    return this.http.delete(environment.BaseUrl + `/DeleteProfilePhoto/${id}`)
  }

  // Cv Offers

  GetCvOffers(){
    return this.http.get(environment.BaseUrl + "/CV/Offers")
  }
  
  GetCvFile(id: any){
    return this.http.get(environment.BaseUrl + `/CV/DownloadCV${id}`,{responseType: 'blob',})
  }

  DeleteCvFile(id: number ){
    return this.http.delete(environment.BaseUrl + `/CV/Offers/Delete/${id}`)
  }

  GetOfferStatus(){
    return this.http.get(environment.BaseUrl + `/CheckStatus`)
  }

  // Projects
  PostProject(formData:any){
    return this.http.post(environment.BaseUrl + "/AddProfileProject", formData)
  }

  EditProject(formData: any){
    return this.http.put(environment.BaseUrl + "/EditProfileProject", formData)
  }

  DeleteProject(id: any ){
    return this.http.delete(environment.BaseUrl + `/DeleteProfileProject/${id}`)
  }


}
