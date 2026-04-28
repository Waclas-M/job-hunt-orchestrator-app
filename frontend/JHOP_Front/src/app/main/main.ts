import { Component, inject } from '@angular/core';
import { Router, RouterLink, RouterOutlet } from '@angular/router';
import { AuthService } from '../shared/services/auth-service';

@Component({
  selector: 'app-main',
  imports: [RouterOutlet,RouterLink],
  templateUrl: './main.html',
  styleUrl: './main.css',
})
export class Main {
  private authService = inject(AuthService);
  private router = inject(Router);
  
   onLogOut(){
    this.authService.deleteToken()
    this.router.navigateByUrl('/logowanie')
    
  }
}
