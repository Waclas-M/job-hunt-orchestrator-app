import { ChangeDetectorRef, Component, inject, signal,Input  } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { LaborMarketService } from '../../shared/services/labor-market-service';
import { RouterLink } from '@angular/router';
import { Form, FormBuilder, FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { CvProfileReadModel } from '../../../../ReadModels/ProfilDataReadModel';
import { CvService } from '../../shared/services/cv-service';
import { UserService } from '../../shared/services/user-service';
import { CvConfigurationModal } from '../../shared/services/components/shared-modal/cv-configuration-modal/cv-configuration-modal';
import { CvLoadingProcess } from '../../shared/functionality/CvLoadingProcess';

@Component({
  selector: 'app-job-offer-section',
  imports:  [ReactiveFormsModule, CommonModule,CvConfigurationModal],
  templateUrl: './job-offer-section.html',
  styleUrl: './job-offer-section.css',
})
export class JobOfferSection {

 

  private formBuilder: FormBuilder = inject(FormBuilder);
  private toastr = inject(ToastrService)
  private laborMarketService = inject(LaborMarketService);
  private cdr = inject(ChangeDetectorRef);
  private cvService = inject(CvService);
  public cvLoadingProcess = inject(CvLoadingProcess);
 


  offers = signal([] as any[]);
  categoryId: number = 1;
  chosenOffer: any = null;  



  
  GetNewOffers(categoryId: number) {
    this.laborMarketService.GetOffersByCategory(categoryId).subscribe({
      next: (response: any) => {
        this.offers.set(response);
        console.log('Offers fetched successfully:', response);
        this.cdr.detectChanges();
      },
      error: (error:any) => {
        this.toastr.error('Error fetching offers', 'Error');
        console.error('Error fetching offers:', error);
      },
    })

  }

  selectOffer(offer: any) {
  this.chosenOffer = offer;
  console.log('Wybrana oferta:', this.chosenOffer);
  }

  GenerateCV(formData: any) {
    console.log('Otrzymane dane z formularza:', formData);
    

    const payload = {
    ...formData,
    offerURL: this.chosenOffer?.url || ''
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


  
  ngOnInit(): void { 
    this.GetNewOffers(this.categoryId);
    this.cvLoadingProcess.StartingCheckOfferStatus();
  }

  

}

