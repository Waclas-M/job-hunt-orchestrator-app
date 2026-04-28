import { ChangeDetectorRef, Component, EventEmitter, inject, Input, Output } from '@angular/core';
import { UserService } from '../../../user-service';
import { FormBuilder, FormControl, ReactiveFormsModule, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { CvService } from '../../../cv-service';
import { CvProfileReadModel } from '../../../../../../../ReadModels/ProfilDataReadModel';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-cv-configuration-modal',
  standalone: true,
  imports: [ReactiveFormsModule, CommonModule],
  templateUrl: './cv-configuration-modal.html',
  styleUrl: './cv-configuration-modal.css',
})
export class CvConfigurationModal {

  @Output() form = new EventEmitter<any>();

  private formBuilder: FormBuilder = inject(FormBuilder);

  private toastr = inject(ToastrService)
  private cdr = inject(ChangeDetectorRef);
  private userService = inject(UserService);

  profiles: CvProfileReadModel[] = [];
  selectedProfile: CvProfileReadModel | null = null;
  UserPersonalData: any = {};  
  ListOfExperiences : any[] = [];
  ListOfEducations : any[] = []; 
  ListOfLanguages : any[] = [];
  ListOfStrengths : any[] = [];
  ListOfSkills : any[] = [];
  ListOfInterests : any[]= [];

 

  DeactivationOfControl(form: any,processAutoControlName: string, idsControlName: string){
  form.get(processAutoControlName)?.valueChanges.subscribe((isAuto: any) => {
    if (isAuto) {
      form.get(idsControlName)?.disable();
      form.get(idsControlName)?.setValue([]);
    } else {
      form.get(idsControlName)?.enable();
    }

  })}



  public cvFormGenerate = this.formBuilder.group({

    offerURL: ['',[Validators.required, Validators.pattern('https?://.+')]],
    cvForm: [0,[Validators.required]],


    userEducationsProcessAuto: [true],
    userExperiencesProcessAuto: [true],
    userStrengsProcessAuto: [true],
    userSkillsProcessAuto: [true],
    
    userEducationsIds: this.formBuilder.nonNullable.control<number[]>([]),
    userExperiencesIds: this.formBuilder.nonNullable.control<number[]>([]),
    userStrengsIds: this.formBuilder.nonNullable.control<number[]>([]),
    userSkillsIds: this.formBuilder.nonNullable.control<number[]>([]),
  });

  

  onCheckboxChange(controlName: string, id: number, event: Event) {
  const checkbox = event.target as HTMLInputElement;
  const control = this.cvFormGenerate.get(controlName) as FormControl<number[]>;

  const current = control.value ?? [];

  if (checkbox.checked) {
    // dodaj (bez duplikatów)
    if (!current.includes(id)) {
      control.setValue([...current, id]);
    }
  } else {
    // usuń
    control.setValue(current.filter(x => x !== id));
  }

  control.markAsDirty();
  control.markAsTouched();
  }

  isSelected(controlName: string, id: number): boolean {
  const control = this.cvFormGenerate.get(controlName) as FormControl<number[]>;
  return (control.value ?? []).includes(id);
  }

  isIdsDisabled(controlName: string): boolean {
  const c = this.cvFormGenerate.get(controlName);
  return !!c?.disabled;
  }



  ProfileIdApiHelper(form: any){
    form.addControl('profileId', this.formBuilder.control(this.selectedProfile?.id ?? null));
    return form;
  }

   GetUserProfilesData(){
    this.userService.GetManyProfiles().subscribe({
      next: (res:any) => {
   
        for (let profile of res) {
          
          this.profiles.push(profile);
        }
        this.selectedProfile = this.profiles[0] ?? null;
        this.UserPersonalData = this.selectedProfile?.personalData ;
        this.ListOfExperiences = this.selectedProfile?.jobExperiences ?? [];
        this.ListOfEducations = this.selectedProfile?.educations ?? [];
        this.ListOfInterests = this.selectedProfile?.interests ?? [];
        this.ListOfLanguages = this.selectedProfile?.languages ?? [];
        this.ListOfSkills = this.selectedProfile?.skills ?? [];
        this.ListOfStrengths = this.selectedProfile?.strengths ??[];
        this.cdr.detectChanges();
      },
      error: (err:any) => {console.log(err)}
    })
  }

    

  StartingConfiguration(){
    this.cvFormGenerate.get("userEducationsProcessAuto")?.setValue(true);
    this.cvFormGenerate.get("userExperiencesProcessAuto")?.setValue(true);
    this.cvFormGenerate.get("userStrengsProcessAuto")?.setValue(true);
    this.cvFormGenerate.get("userSkillsProcessAuto")?.setValue(true);
    
    this.cvFormGenerate.get("userEducationsIds")?.setValue([]);
    this.cvFormGenerate.get("userExperiencesIds")?.setValue([]);
    this.cvFormGenerate.get("userStrengsIds")?.setValue([]);
    this.cvFormGenerate.get("userSkillsIds")?.setValue([]);

    this.cvFormGenerate.get("userEducationsIds")?.disable();
    this.cvFormGenerate.get("userExperiencesIds")?.disable();
    this.cvFormGenerate.get("userStrengsIds")?.disable();
    this.cvFormGenerate.get("userSkillsIds")?.disable();

  }

  ngOnInit(){

    this.StartingConfiguration();
    this.GetUserProfilesData();

    this.DeactivationOfControl(this.cvFormGenerate, "userEducationsProcessAuto", "userEducationsIds");
    this.DeactivationOfControl(this.cvFormGenerate, "userExperiencesProcessAuto", "userExperiencesIds");
    this.DeactivationOfControl(this.cvFormGenerate, "userStrengsProcessAuto", "userStrengsIds");
    this.DeactivationOfControl(this.cvFormGenerate, "userSkillsProcessAuto", "userSkillsIds");
  }

  onSubmit(){
    var output  = this.ProfileIdApiHelper(this.cvFormGenerate);
    const payload ={
      ... output.value,
    }
    this.form.emit(payload);
  }

}
