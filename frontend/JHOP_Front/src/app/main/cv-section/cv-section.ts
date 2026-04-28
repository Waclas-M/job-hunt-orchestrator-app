import { CommonModule } from '@angular/common';
import { Component, inject, OnInit, signal } from '@angular/core';
import { Form, FormBuilder, FormControl, ReactiveFormsModule, Validators } from '@angular/forms';
import { RouterLink } from "@angular/router";
import { ToastrService } from 'ngx-toastr';
import { ChangeDetectorRef } from '@angular/core';
import { CvService } from '../../shared/services/cv-service';
import { UserService } from '../../shared/services/user-service';
import { CvProfileReadModel } from '../../../../ReadModels/ProfilDataReadModel';
import { interval } from 'rxjs';
import { CvLoadingProcess } from '../../shared/functionality/CvLoadingProcess';
import { CvConfigurationModal } from "../../shared/services/components/shared-modal/cv-configuration-modal/cv-configuration-modal";

@Component({
  selector: 'app-cv-section',
  imports: [RouterLink, ReactiveFormsModule, CommonModule, CvConfigurationModal],
  templateUrl: './cv-section.html',
  styleUrl: './cv-section.css',
})
export class CvSection implements OnInit{

private formBuilder: FormBuilder = inject(FormBuilder);

  private toastr = inject(ToastrService)
  private cdr = inject(ChangeDetectorRef);
  private cvService = inject(CvService);
  private userService = inject(UserService);
  public cvLoadingProcess = inject(CvLoadingProcess);

  url = '';
  profiles: CvProfileReadModel[] = [];
  selectedProfile: CvProfileReadModel | null = null;
  UserPersonalData: any = {};  

  onInputChange(event: Event) {
    this.url = (event.target as HTMLInputElement).value;
    console.log('Aktualny URL:', this.url);
  }

   GetUserProfilesData(){
    this.userService.GetManyProfiles().subscribe({
      next: (res:any) => {
   
        for (let profile of res) {
          
          this.profiles.push(profile);
        }
        this.selectedProfile = this.profiles[0] ?? null;
        this.UserPersonalData = this.selectedProfile?.personalData ;
        this.cdr.detectChanges();
      },
      error: (err:any) => {console.log(err)}
    })
  }

  

  ngOnInit(){
    this.GetUserProfilesData();
    this.cvLoadingProcess.StartingCheckOfferStatus();
  }

    GenerateCV(formData: any) {
    console.log('Otrzymane dane z formularza:', formData);
    

    const payload = {
    ...formData,
    offerURL: this?.url || ''
    };

    console.log('Wysłane dane:', payload);

    this.cvService.GenerateCV(payload).subscribe({
      next: (response: any) => {
        this.toastr.success('CV generated successfully', 'Success');
        this.cvLoadingProcess.LoadingProcess();
        console.log('CV generated successfully:', response);
      },
      error: (error:any) => {
        this.toastr.error('Error generating CV', 'Error');
        console.error('Error generating CV:', error);
      },
    });

  }





}
