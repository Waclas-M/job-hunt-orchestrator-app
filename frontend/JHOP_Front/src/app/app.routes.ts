import { Routes } from '@angular/router';
import { Registration } from './user/registration/registration';
import { Login } from './user/login/login';
import { User } from './user/user';
import { Dashboard } from './main/dashboard/dashboard';
import { authGuard } from './shared/auth-guard';
import { Main } from './main/main';
import { Component } from '@angular/core';
import { Home } from './main/home/home';
import { CvSection } from './main/cv-section/cv-section';
import { LetterSection } from './main/letter-section/letter-section';
import { JobOfferSection } from './main/job-offer-section/job-offer-section';
import { Settings } from './main/settings/settings';
import { Profile } from './main/profile/profile';
import { claimReq } from './utils/claimReq-utils';
import { Forbidden } from './main/forbidden/forbidden';

export const routes: Routes = [
    {path:'',redirectTo:'/logowanie',pathMatch: 'full'},
    {path:'',component: User,children:[
        {path:"rejestracja",component:Registration},
        {path:"logowanie",component:Login}
    
    ]},
    {path:'',component: Main,canActivate: [authGuard],canActivateChild: [authGuard] ,children:[
        {path:"dashboard" ,component:Dashboard,data:{claimReq: claimReq.adminOruser  }},
        {path:"home",component: Home},
        {path:"Stwórz_CV",component: CvSection,data:{claimReq: claimReq.adminOruser  }},
        {path:"Stwórz_List_Motywacyjny",component: LetterSection,data:{claimReq: claimReq.adminOrLetterOperator  }},
        {path:"Oferty",component: JobOfferSection},
        {path:"Ustawienia",component: Settings},
        {path:"Profil",component: Profile},
        {path:"forbidden",component: Forbidden},

    ]}
];
