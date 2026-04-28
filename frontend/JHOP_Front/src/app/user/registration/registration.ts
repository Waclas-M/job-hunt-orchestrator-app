import { Component, inject , OnInit } from '@angular/core';
import { AbstractControl, FormBuilder, ReactiveFormsModule, ValidatorFn, Validators } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { AuthService } from '../../shared/services/auth-service';
import { ToastrService } from 'ngx-toastr';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-registration',
  imports: [RouterLink,CommonModule,ReactiveFormsModule],
  templateUrl: './registration.html',
  styleUrl: './registration.css',
})
export class Registration implements OnInit{

  public formBuilder : FormBuilder = inject(FormBuilder);
  private authService = inject(AuthService);
  private router = inject(Router)
  private toastr = inject(ToastrService)


   ngOnInit(): void {
    if(this.authService.isLoggedIn()){
      this.router.navigateByUrl("/dashboard")
    }
  }

   passwordMatchValidator: ValidatorFn = (control: AbstractControl): null => {
    const password = control.get('password');
    const confirmPassword = control.get('confirmPassword');
    if (password && confirmPassword && password.value !== confirmPassword.value) {
      control.get('confirmPassword')?.setErrors({ passwordMismatch: true });
    } else {
      control.get('confirmPassword')?.setErrors(null);
    }
    return null;
  }

  
  form = this.formBuilder.group({
    name : ['', [Validators.required]],
    surName: ['', [Validators.required]],
    email: ['', [Validators.required,Validators.email]],
    password: ['', [Validators.required,Validators.minLength(6),Validators.pattern(/(?=.*[^a-zA-Z0-9])/)]],
    confirmPassword: ['', Validators.required],

  },{validators: this.passwordMatchValidator});

  onSubit(){
    if (this.form.valid){
      this.authService.register(this.form.value).subscribe({
        next: (res:any ) =>{
          this.form.reset();
          this.toastr.success('Zarejstrowano !','Pomyślne Zarejstrowano');
          this.router.navigateByUrl('/logowanie');

        },
        error: (err:any) => {
          this.toastr.error("Błąd przy rejestracji,'Error")
          console.log(err)
        }
      })
    }



  }
}
