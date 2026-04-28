export interface CvProfileReadModel {
  id: number;
  userId: string;
  user: any | null;
  profileIndex: number;
  name: string;
  profilePhoto: ProfilePhotoReadModel ;

  personalData: UserPersonalDataReadModel[];
  educations: EducationReadModel[];
  jobExperiences: JobExperienceReadModel[];

  languages: UserLanguageReadModel[];
  interests: UserInterestReadModel[];
  skills: UserSkillReadModel[];
  strengths: UserStrengthReadModel[];
  projects: UserProjectReadModel[];
}

export interface JobExperienceReadModel {
  id: number;                 // = selectedExperienceId (albo id z bazy)
  companyName: string;
  jobTitle: string;
  jobDescription: string;
  startDate: string;          // najlepiej ISO "YYYY-MM-DD" (taki string ogarnie input[type=date])
  endDate: string;            // jw.
  isCurrent: boolean;
}

export interface EducationReadModel {
  id: number;                 // = selectedEducationId (albo id z bazy)
  schoolName: string;
  studyProfile: string;
  startDate: string;          // ISO string
  endDate: string;            // ISO string
  isCurrent: boolean;
}

export interface ContactLinksReadModel {
  linkedInURL: string;
  gitHubURL: string;
}

export interface PersonalProfileReadModel {
  personalProfile: string;
}

export interface UserStrengthReadModel {
  id: number;
  strength: string; // Strength
}

export interface UserSkillReadModel {
  id: number;
  skill: string; // Skill
}

export interface UserLanguageReadModel {
  id: number;
  language: string; // Language
  level: number;    // Level
}

export interface UserInterestReadModel {
  id: number;
  interest: string;      // Intrest (zostawiam tak jak w backendzie)
  description: string;  // Description
}

export interface UserPersonalDataReadModel {
  firstName: string;
  lastName: string;
  email: string;
  phoneNumber: string;
  personalProfile: string | null;
  gitHubURL: string | null;
  linkedInURL: string | null;
}

export interface ProfilePhotoReadModel {
  id : number;
  data: Blob;
  cratedAt: string; // ISO string

}

export interface UserProjectReadModel {
  id: number;
  projectName: string;
  projectDescription: string;
  projectURL: string;
  startDate: string;          // ISO string
  endDate: string; 
  technologies: string[]; // Technologies used in the project

}


export const emptyCvProfileReadModel: CvProfileReadModel = {
  id: 0,
  userId: '',
  user: null,
  profileIndex: 0,
  name: '',
  profilePhoto: {
    id: 0,
    data: new Blob(),
    cratedAt: ''
  },
  educations: [],
  jobExperiences: [],
  personalData: [],


  languages: [],
  interests: [],
  skills: [],
  strengths: [],
  projects: []
};