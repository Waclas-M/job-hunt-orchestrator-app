import { Form, FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import {inject } from '@angular/core';
import { UserService } from '../../shared/services/user-service';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class ProfileForms {

    private formBuilder : FormBuilder = inject(FormBuilder);
    private userService = inject(UserService);


    CreateUserDataForm(){
        return this.formBuilder.group({
        firstName: ["", [Validators.required]],
        lastName: ["", [Validators.required]],
        email: ["", [Validators.required, Validators.email]],
        phoneNumber: ["", [ Validators.pattern("^[0-9]+$")]],
        })
    }

    CreateExperienceform(){
        return this.formBuilder.nonNullable.group({
        companyName: ["",[Validators.required]],
        jobDescription: ["",[Validators.required]],
        startDate: ["",[Validators.required]],
        endDate: ["",[Validators.required]],
        jobTitle: ["",[Validators.required]],
        isCurrent: [false],
        selector: [0]
        })
    }

    CreateEducationform(){
        return this.formBuilder.nonNullable.group({
        schoolName: ["",[Validators.required]],
        studyProfile: ["",[Validators.required]],
        startDate: ["",[Validators.required]],
        endDate: ["",[Validators.required]],
        isCurrent: [false],
        selector: [0]
        })
    }

    CreatePersonalProfileForm(){
        return this.formBuilder.group({
            personalProfile: ["", [Validators.required]]
        })
    }

    CreateContactLinksForm(){
        return this.formBuilder.group({
        linkedInURL: ["", []],
        gitHubURL: ["", []]
        })
    }

    CreateLanguageForm(){
        return this.formBuilder.group({
        language: [4, [Validators.required]],
        level : [0,[Validators.required]],
        selector: [0]
        })
    }

    CreateSkillForm(){
        return this.formBuilder.group({
        skill : ["",[Validators.required]],
        selector: [0]
        })
    }

    CreateStrengthForm(){
        return this.formBuilder.group({
        strength : ["",[Validators.required]],
        selector: [0]
         })
    }

    CreateInterestForm(){
        return this.formBuilder.group({
        interest  : ["",[Validators.required]],
        description : ["",[Validators.required]],
        selector: [0]
        })
    }

    CreateProfilePhotoForm(){
        return this.formBuilder.group({
            profilePhoto: [null, [Validators.required]],
            selectedFile: [null],
            selector: [0]
        })
    }

    CreateProjectForm(){
        return this.formBuilder.group({
            projectName: ["", [Validators.required]],
            description: ["", [Validators.required]],
            projectURL: [""],
            startDate: ["", [Validators.required]],
            endDate: ["", [Validators.required]],
            technologies: [[] as string[], [Validators.required]],
            selector: [0]
        })
    }

    public FormPostApiDictionary: { [key: string]: Function } = {
        Experienceform: this.userService.PostExperience.bind(this.userService),
        Educationform: this.userService.PostEducation.bind(this.userService),
        languageForm: this.userService.PostLanguage.bind(this.userService),
        skillForm: this.userService.PostSkill.bind(this.userService),
        strengthForm: this.userService.PostStrenght.bind(this.userService),
        interestForm: this.userService.PostInterest.bind(this.userService),
        profilePhotoForm: this.userService.PostProfilePhoto.bind(this.userService),
        projectsForm: this.userService.PostProject.bind(this.userService)
    };

    public FormDeleteApiDictionary: { [key: string]: Function } = {
        Experienceform: this.userService.DeleteExperience.bind(this.userService),
        Educationform: this.userService.DeleteEducation.bind(this.userService),
        languageForm: this.userService.DeleteLanguage.bind(this.userService),
        skillForm: this.userService.DeleteSkill.bind(this.userService),
        strengthForm: this.userService.DeleteStrenght.bind(this.userService),
        interestForm: this.userService.DeleteInterest.bind(this.userService),
        profilePhotoForm: this.userService.DeleteProfilePhoto.bind(this.userService),
        projectsForm: this.userService.DeleteProject.bind(this.userService)
    };

    public FormPutApiDictionary: { [key: string]: Function } = {
        UserDataform: this.userService.EditPersonalData.bind(this.userService),
        Experienceform: this.userService.EditExperience.bind(this.userService),
        Educationform: this.userService.EditEducation.bind(this.userService),
        personalProfileForm: this.userService.EditUserPersonalProfileData.bind(this.userService),
        ContactLinksForm: this.userService.EditPersonalDataLinks.bind(this.userService),
        languageForm: this.userService.EditLanguage.bind(this.userService),
        skillForm: this.userService.EditSkill.bind(this.userService),
        strengthForm: this.userService.EditStrenght.bind(this.userService),
        interestForm: this.userService.EditInterest.bind(this.userService),
        projectsForm: this.userService.EditProject.bind(this.userService)
    };

    public FormCreationDictionary: { [key: string]: Function } = {
        UserDataform: this.CreateUserDataForm.bind(this),
        Experienceform: this.CreateExperienceform.bind(this),
        Educationform: this.CreateEducationform.bind(this),
        personalProfileForm: this.CreatePersonalProfileForm.bind(this),
        ContactLinksForm: this.CreateContactLinksForm.bind(this),
        languageForm: this.CreateLanguageForm.bind(this),
        skillForm: this.CreateSkillForm.bind(this),
        strengthForm: this.CreateStrengthForm.bind(this),
        interestForm: this.CreateInterestForm.bind(this),
        projectsForm: this.CreateProjectForm.bind(this),
    };








}