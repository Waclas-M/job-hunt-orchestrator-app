import { CommonModule } from '@angular/common';
import { ChangeDetectorRef, Component, inject, OnInit } from '@angular/core';
import { Form, FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { CvProfileReadModel } from '../../../../ReadModels/ProfilDataReadModel';
import { ProfileForms } from '../../shared/forms/ProfileForms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { UserService } from '../../shared/services/user-service';
import { LanguagesDictionary } from '../../shared/dictionary/languages-dictionary';

@Component({
  selector: 'app-profile',
  imports: [ReactiveFormsModule,CommonModule],
  templateUrl: './profile.html',
  styleUrl: './profile.css',
})
export class Profile implements OnInit {

  // Podstawowe zmmienne i serwisy
  public formBuilder = inject(FormBuilder);
  private router = inject(Router)
  private toastr = inject(ToastrService)
  private userService = inject(UserService);
  private cdr = inject(ChangeDetectorRef);
  public profileForms = inject(ProfileForms);
  

  // Listy danych użytkownika
  profiles: CvProfileReadModel[] = [];
  selectedProfile: CvProfileReadModel | null = null;
  UserPersonalData: any = {};  
  ListOfExperiences : any[] = [];
  ListOfEducations : any[] = []; 
  ListOfLanguages : any[] = [];
  ListOfStrengths : any[] = [];
  ListOfSkills : any[] = [];
  ListOfInterests : any[]= [];
  ListOfProjects : any[] = [];
  languages: { id: number, name: string }[] = [];
  PhotoToShow: any = '';
 

  ProfileIdApiHelper(form: any){
    form.addControl('profileId', this.formBuilder.control(this.selectedProfile?.id ?? null));
    return form;
  }

  // Lokalne zmienne i formularze 
    // Dane użytkownika

  UserDataform = this.profileForms.CreateUserDataForm()

    // Lista doświadczeń użytkownika

  
  Experienceform = this.profileForms.CreateExperienceform()

    // Dane wykształcenia użytkownika

  Educationform = this.profileForms.CreateEducationform()

  // Personal profile 
  personalProfileForm = this.profileForms.CreatePersonalProfileForm()

  // Dane kontaktowe użytkownika - linki do mediów społecznościowych i innych stron 
  ContactLinksForm = this.profileForms.CreateContactLinksForm()

  // Języki użytkownika

  languageForm = this.profileForms.CreateLanguageForm()
  // Umiejętności 

  skillForm = this.profileForms.CreateSkillForm()

  //  Atuty

  strengthForm = this.profileForms.CreateStrengthForm()

  // Zainteresowania

  interestForm = this.profileForms.CreateInterestForm()

  // Zdjęcie profilowe
  profilePhotoForm = this.profileForms.CreateProfilePhotoForm()

  // Projekty
  projectsForm = this.profileForms.CreateProjectForm()




  FormDictionary: { [key: string]: FormGroup } = {
  UserDataform: this.UserDataform,
  Experienceform: this.Experienceform,
  Educationform: this.Educationform,
  personalProfileForm: this.personalProfileForm,
  ContactLinksForm: this.ContactLinksForm,
  languageForm: this.languageForm,
  skillForm: this.skillForm,
  strengthForm: this.strengthForm,
  interestForm: this.interestForm,
  profilePhotoForm: this.profilePhotoForm,
  projectsForm: this.projectsForm
  };


  // Powtarzalne działania - Dezaktywacja pola endDate w przypadku zaznaczenia isCurrent

  DeactivationOfEndDateControl(form: any){
    form.get('isCurrent')?.valueChanges.subscribe((isCurrent: any) => {
      if (isCurrent) {
        form.get('endDate')?.disable();
        form.get('endDate')?.setValue('');
      } else {
       form.get('endDate')?.enable();
      }
      this.cdr.detectChanges();

  })}

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
        this.ListOfProjects = this.selectedProfile?.projects ?? [];

        this.ContactLinksForm.patchValue({
          linkedInURL: this.UserPersonalData.linkedInURL ?? '',
          gitHubURL: this.UserPersonalData.gitHubURL ?? ''})
        this.personalProfileForm.patchValue({
          personalProfile: this.UserPersonalData.personalProfile ?? ''
        }) 
        this.cdr.detectChanges();
      },
      error: (err:any) => {console.log(err)}
    })
  }

  GetProfileActualization(){
    this.userService.GetProfileById(this.selectedProfile?.id).subscribe({
      next: (res: any) => {
        this.profiles[0] = res[0]
        this.selectedProfile = this.profiles[0] ?? null;
        this.UserPersonalData = this.selectedProfile?.personalData ;
        this.ListOfExperiences = this.selectedProfile?.jobExperiences ?? [];
        this.ListOfEducations = this.selectedProfile?.educations ?? [];
        this.ListOfInterests = this.selectedProfile?.interests ?? [];
        this.ListOfLanguages = this.selectedProfile?.languages ?? [];
        this.ListOfSkills = this.selectedProfile?.skills ?? [];
        this.ListOfStrengths = this.selectedProfile?.strengths ??[];
        this.ListOfProjects = this.selectedProfile?.projects ?? [];
        this.ContactLinksForm.patchValue({
          linkedInURL: this.UserPersonalData.linkedInURL ?? '',
          gitHubURL: this.UserPersonalData.gitHubURL ?? ''})
        this.personalProfileForm.patchValue({
          personalProfile: this.UserPersonalData.persolalProfile ?? ''
        })
        this.cdr.detectChanges();
      },
      error: (err:any) => {console.log(err)}
      
    })
  }

  GetProfilePhotoActualization(){
    this.userService.GetProfilePhotos().subscribe({
      next: (res: any) => {
        this.selectedProfile = this.profiles[0];
        this.selectedProfile.profilePhoto = res[0];
        this.FormDictionary['profilePhotoForm'].patchValue({
          selector: this.selectedProfile?.profilePhoto?.id ?? 0
        })
        const photoData = this.selectedProfile?.profilePhoto?.data;
        this.PhotoToShow = photoData
          ? `data:image/jpeg;base64,${photoData}`
          : null;
        this.cdr.detectChanges();

        console.log("Selected Profile: ",this.selectedProfile);
        console.log("Photo Profile form: ",this.FormDictionary['profilePhotoForm'].value);
        }
      })
    }
      

   convertDictionaryToArray() {
    return Object.entries(LanguagesDictionary).map(([id, name]) => ({
      id: Number(id),
      name: name
    }));
  }

  MapLanguagesName(){
    return 
  }

  onAddTechnology(technology: string) {
    const value = technology.trim();

    if (!value) return;

    const technologies = this.projectsForm.get('technologies')?.value ?? [];

    if (technologies.includes(value)) return;

    const updated = [...technologies, value];

    this.projectsForm.get('technologies')?.setValue(updated);

    console.log('Dodawany tech:', value);
    console.log('Zaktualizowane technologie:', updated);
  }

  onDeleteProjectTechnology(technology: string) {
    const technologies = this.projectsForm.get('technologies')?.value ?? [];
    const updated = technologies.filter((t: string) => t !== technology);

    this.projectsForm.get('technologies')?.setValue(updated);

    console.log('Usuwany tech:', technology);
    console.log('Po usunięciu:', updated);
  }

  





  ngOnInit(): void {
    
    this.GetUserProfilesData();
    this.GetProfilePhotoActualization();

    this.Experienceform.get('selector')?.valueChanges.subscribe(() =>
       this.Experienceform = this.OnRecordChange(this.Experienceform,this.ListOfExperiences,'Experienceform'));

    this.Educationform.get('selector')?.valueChanges.subscribe(() =>
       this.Educationform = this.OnRecordChange(this.Educationform,this.ListOfEducations,'Educationform'));

    this.languageForm.get('selector')?.valueChanges.subscribe(() =>
       this.languageForm = this.OnRecordChange(this.languageForm,this.ListOfLanguages,'languageForm'));

    this.skillForm.get('selector')?.valueChanges.subscribe(() =>
       this.skillForm = this.OnRecordChange(this.skillForm,this.ListOfSkills,'skillForm'));

    this.strengthForm.get('selector')?.valueChanges.subscribe(() =>
       this.strengthForm = this.OnRecordChange(this.strengthForm,this.ListOfStrengths,'strengthForm'));

    this.interestForm.get('selector')?.valueChanges.subscribe(() =>
       this.interestForm = this.OnRecordChange(this.interestForm,this.ListOfInterests,'interestForm'));

    this.projectsForm.get('selector')?.valueChanges.subscribe(() =>
       this.projectsForm = this.OnRecordChange(this.projectsForm,this.ListOfProjects,'projectsForm'));

    this.DeactivationOfEndDateControl(this.Experienceform);
    this.DeactivationOfEndDateControl(this.Educationform);

    this.languages = this.convertDictionaryToArray()
    
  }

  // Funckje obsługujące formularze i interakcje z backendem

    // Funkcja obsługująca dodawanie lub aktualizację doświadczenia zawodowego
  
 
  closeModal(){
    this.UserDataform.patchValue({
      firstName: this.UserPersonalData.firstName,
      lastName: this.UserPersonalData.lastName,
      email: this.UserPersonalData.email,
      phoneNumber: this.UserPersonalData.phoneNumber
    })
  }

  UpdateEndDateState(form: FormGroup) {
  const isCurrent = form.get('isCurrent')?.value;

  if (isCurrent) {
      form.get('endDate')?.disable({ emitEvent: false });
      form.get('endDate')?.setValue('', { emitEvent: false });
    } else {
      form.get('endDate')?.enable({ emitEvent: false });
    }

    this.cdr.detectChanges();
  }


  OnRecordChange(subscribe_form: FormGroup,ListofChoice : any[],formType: string){
    const selectedId = subscribe_form.get('selector')?.value;
    if(selectedId != 0){
      const record = ListofChoice.find(e => e.id == selectedId)
      subscribe_form.patchValue(record)
    }else{
      subscribe_form.patchValue(this.profileForms.FormCreationDictionary[formType]().value,{ emitEvent: false })
    }

    if (subscribe_form.get('isCurrent')?.value != null){
      this.UpdateEndDateState(subscribe_form)
    }
    

    return subscribe_form; 
  }

  onFileSelected(event: Event): void {
  const input = event.target as HTMLInputElement;

  if (input.files && input.files.length > 0) {
    this.FormDictionary['profilePhotoForm'].patchValue({ selectedFile: input.files[0] });
    console.log(this.FormDictionary['profilePhotoForm'].get('selectedFile')?.value);

  }
}

  // Po Submit na dowolnej formie
  onSubmit(formType: string){
    let form = this.FormDictionary[formType];
    console.log(formType)
    console.log(form.value)
    
    if (form.valid){
      if (form.get('selector')?.value == 0){
        if (formType === 'profilePhotoForm') {
          form.removeControl('profilePhoto');
          let requestBody: FormData | any;

          requestBody = new FormData();
          requestBody.append('SelectedFile', form.get('selectedFile')?.value as File);
          requestBody.append('ProfileId',  this.formBuilder.control(this.selectedProfile?.id ?? null).value);
          let func = this.profileForms.FormPostApiDictionary[formType];
          this.PostNewRecord(requestBody, func, "Pomyślnie dodano!", "Zdjęcie dodane pomyślnie!",false);
        }else{
          let func = this.profileForms.FormPostApiDictionary[formType];
          this.PostNewRecord(form,func,"Pomyślnie dodano!")
        }
        
      }else{
        let func = this.profileForms.FormPutApiDictionary[formType];
        this.PutRecord(form,func,"Pomyślnie zaktualizowano")
      }
    }else{
      form.markAllAsTouched();
    }

  }

  onDelete(formType: string){
    let form = this.FormDictionary[formType];
    if (form.get('selector')?.value == 0){
        this.toastr.error("Wybierz rekord do usunięcia.",'error')
      }else{
        let func = this.profileForms.FormDeleteApiDictionary[formType];
        this.DeleteRecord(form.get('selector')?.value,func,"Pomyślnie Usunięto!")
        form.patchValue({'selector': [0]})
      }

  }


  PostNewRecord(form: any, apiFunction : any , message : string,  title: string = 'Succes', aditionalActions: boolean = true){
    let payload : any = null;
    if (aditionalActions){
    var api_request = this.ProfileIdApiHelper(form);
     const { selector , ...rest } = api_request.value;
     payload = rest;
    }
    else{    
      payload = form;
    }

    apiFunction(payload).subscribe({
      next: (res:any) => {
        this.toastr.success(message,title)
        this.GetProfileActualization();
        this.GetProfilePhotoActualization();
      },
      error: (err: any) =>{
        this.toastr.error("Wystąpił błąd przy zapisie.","Error")
        console.log(err)
      }
    })
  }

  PutRecord(form: any, apiFunction : any , message : string = 'Pomyślnie zaktualizowano !' ,title: string = 'Succes'){
    var api_request = this.ProfileIdApiHelper(form);
    const { selector , ...payload } = api_request.value;
    const payloadWithId = {
            id: selector,
            ...payload
          };
    apiFunction(payloadWithId).subscribe({
            next: (res:any) => { 
              this.toastr.success(message,title);
              this.GetProfileActualization();
          }, 
            error: (err:any) => {
              this.toastr.error("Błąd przy aktualizacji danych","Error")
              console.log(err) 
          }
        })
      }

  DeleteRecord(id: any, apiFunction : any , message : string = 'Pomyślnie usunięto !' ,title: string = 'Succes'){
    apiFunction(id).subscribe({
          next: (res:any) => { 
            this.toastr.success(message,title);
            
            this.GetProfileActualization();
            this.GetProfilePhotoActualization();
        }, 
          error: (err:any) => {
            this.toastr.error("Błąd przy aktualizacji danych","Error")
            console.log(err)
        }
      })
    }



}
