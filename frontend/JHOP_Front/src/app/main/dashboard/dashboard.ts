import { Component, inject, OnInit, Signal, signal } from '@angular/core';
import { AuthService } from '../../shared/services/auth-service';
import { Router } from '@angular/router';
import { UserService } from '../../shared/services/user-service';
import { ToastrService } from 'ngx-toastr';
import { DatePipe } from '@angular/common';

@Component({
  selector: 'app-dashboard',
  imports: [DatePipe],
  templateUrl: './dashboard.html',
  styleUrl: './dashboard.css',
})
export class Dashboard implements OnInit {
  private authService = inject(AuthService);
  private toastr = inject(ToastrService);
  private router = inject(Router);
  private userService = inject(UserService);
  public name = signal<string>('');

  offers = signal<any[]>([]);

  loadOffers(){
    this.userService.GetCvOffers().subscribe({
    next: (res: any) => {
      this.offers.set([...res].reverse());
      console.log(this.offers());
    },
    error: (err: any) => {
      this.toastr.error("Wystąpił błąd przy pobieraniu.", "Error");
      console.log(err);
    }
  });
  }

  downoladCvByID(cvFileId: any, companyName?: string) {
    this.userService.GetCvFile(cvFileId).subscribe({
      next: (blob: Blob) => {
        const url = window.URL.createObjectURL(blob);
        const a = document.createElement('a');
        a.href = url;
        a.download = `${companyName ?? 'cv'}.pdf`;
        document.body.appendChild(a);
        a.click();
        document.body.removeChild(a);

        setTimeout(() => {
          window.URL.revokeObjectURL(url);
        }, 1000);
      },
      error: (err) => {
        console.error('Błąd pobierania pliku:', err);
      }
    });
  }

  deleteOfferRekord(offerId: number){
    console.log(offerId)
    this.userService.DeleteCvFile(offerId).subscribe({
      next : (res: any) => {
        this.toastr.success(res, "Success");
        this.loadOffers()

      },
      error : (err:any) => {
         this.toastr.error(err, "Error");
      }

    })
  }

  formatDate(dateStr: string): string {
  const date = new Date(dateStr);

  return date.toLocaleString('pl-PL', {
    day: '2-digit',
    month: '2-digit',
    year: 'numeric',
    hour: '2-digit',
    minute: '2-digit'
  });
}

  ngOnInit(): void {
    this.loadOffers()
  }
}