import { CommonModule } from '@angular/common';
import { Component, inject,OnInit } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router, RouterLink } from "@angular/router";
import { AuthService } from '../../shared/services/auth-service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-login',
  imports: [RouterLink,CommonModule,ReactiveFormsModule],
  templateUrl: './login.html',
  styleUrl: './login.css',
})
export class Login implements OnInit{

  public formBuilder : FormBuilder = inject(FormBuilder);
  private authService = inject(AuthService);
  private router = inject(Router)
  private toastr = inject(ToastrService)

  ngOnInit(): void {
    if(this.authService.isLoggedIn()){
      this.router.navigateByUrl("/dashboard")
    }
  }
  

  form = this.formBuilder.group({
    email: ['',[Validators.required]],
    password:['',[Validators.required]]
  })

  onSubmit(){
    if (this.form.valid){
      this.authService.login(this.form.value).subscribe({
        next: (res:any) => {
          this.authService.saveToken(res.token);
          this.form.reset();
          this.toastr.success('Zalogowano !','Pomyślne Logowanie');
          this.router.navigateByUrl("/dashboard")
        },
        error: (err:any) => {
          this.toastr.error('Bład przy próbie logowania','Error')
          console.log(err)
        }

      })
    }
  }

}
