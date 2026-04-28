
import {ChangeDetectorRef, inject, signal } from '@angular/core';
import { UserService } from '../../shared/services/user-service';
import { Injectable } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { interval } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class CvLoadingProcess {
    private toastr = inject(ToastrService)
   
    private userService = inject(UserService);
    isLoading = signal(false);

    
      LoadingProcess(){
          this.isLoading.set(true);
          this.toastr.info("Proces w trackie" , "Info")
          var subscription = interval(15000).subscribe(() => {
            this.userService.GetOfferStatus().subscribe({
              next : (res: any) => {
    
                if (res['status'] == 0){
                  
                }if (res['status'] == 1) {
                  this.toastr.success("Zakończono przetwarzenie.\nSprawdź nowy rekord w dashbord." , "Success")
                  this.isLoading.set(false);
                  subscription.unsubscribe();
                }if (res['status'] == 2){
                  this.toastr.error("Zakończono przetwarzenie.\nWystąpił błąd w trakcie procesu" , "Failed")
                  this.isLoading.set(false);
                  subscription.unsubscribe();
                }
    
              },
              error : (err : any) => {
                this.toastr.error("Bład przy pozyskaniu statusu." , "Error")
                console.log(err)
              }
          })
        })
      }

      StartingCheckOfferStatus(){
        this.userService.GetOfferStatus().subscribe({
        next : (res: any) => {
            console.log(res)
            if (res['status'] == 0){
            this.LoadingProcess()
            }else if ([1, 2].includes(res.status)) {
            } 
        },
        error : (err : any) => {
        }
        })
    }
}